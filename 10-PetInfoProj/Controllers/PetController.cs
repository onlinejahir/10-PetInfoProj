using _10_PetInfoProj.ProjectModels;
using _10_PetInfoProj.ViewModels.PetVM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetInfo.Models.EntityModels;
using PetInfo.Services.Contracts.AllContracts;

namespace _10_PetInfoProj.Controllers
{
    public class PetController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PetController(IUnitService unitService, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            this._unitService = unitService;
            this._mapper = mapper;
            this._hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index(string text, string searchString, string sortOrder, int pageNumber)
        {
            ViewBag.Message = text;
            IQueryable<Pet> pets;
            if (!string.IsNullOrEmpty(searchString))
            {
                pets = _unitService.PetService.SearchPetResult(searchString);
            }
            else
            {
                pets = _unitService.PetService.GetAllPets();
            }
            ViewBag.PetNameSort = string.IsNullOrEmpty(sortOrder) ? "petNameDesc" : "";
            ViewBag.OwnerNameSort = sortOrder == "ownerNameAsc" ? "ownerNameDesc" : "ownerNameAsc";
            switch (sortOrder)
            {
                case "petNameDesc":
                    pets = pets.OrderByDescending(p => p.PetName);
                    break;
                case "ownerNameAsc":
                    pets = pets.OrderBy(p => p.OwnerName);
                    break;
                case "ownerNameDesc":
                    pets = pets.OrderByDescending(p => p.OwnerName);
                    break;
                default:
                    pets = pets.OrderBy(p => p.PetName);
                    break;
            }

            //Ensure page number is at least 1
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 4;

            IQueryable<PetViewModel> petsVM = pets.Select(p => new PetViewModel()
            {
                PetId = p.PetId,
                PetName = p.PetName,
                Age = p.Age,
                Color = p.Color,
                Gender = p.Gender,
                OwnerName = p.OwnerName,
                AnimalType = p.AnimalType,
                Address = p.Address,
                ImageName = p.ImageName
            });
            return View(await PaginatedList<PetViewModel>.CreateAsync(petsVM, pageNumber, pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<AnimalType> animalTypes = (await _unitService.AnimalTypeService.GetAllAnimalTypeAsync()).ToList();
            ViewBag.AnimalTypes = new SelectList(animalTypes, "AnimalTypeId", "AnimalTypeName");
            return View(new PetViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(PetViewModel petVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Sorry! invalid information, please enter valid information";
                List<AnimalType> animalTypes = (await _unitService.AnimalTypeService.GetAllAnimalTypeAsync()).ToList();
                ViewBag.AnimalTypes = new SelectList(animalTypes, "AnimalTypeId", "AnimalTypeName");
                return View(petVM);
            }
            else
            {
                // Save image to wwwroot/images
                string wwwRootPath = _hostEnvironment.WebRootPath;

                // Ensure ImageFile is not null (validation)
                if (petVM.ImageFile != null)
                {
                    // Generate a unique file name
                    string fileName = Path.GetFileNameWithoutExtension(petVM.ImageFile.FileName);
                    string extension = Path.GetExtension(petVM.ImageFile.FileName);

                    // Validate the file extension (optional, but recommended)
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    if (!allowedExtensions.Contains(extension.ToLower()))
                    {
                        ViewBag.Message = "Invalid file type. Only JPG, JPEG, and PNG are allowed.";
                        var animalTypes = (await _unitService.AnimalTypeService.GetAllAnimalTypeAsync());
                        ViewBag.AnimalTypes = new SelectList(animalTypes, "AnimalTypeId", "AnimalTypeName");
                        return View(petVM); // Return view with the same model for error correction
                    }

                    // Append timestamp to ensure unique file name
                    petVM.ImageName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    // Combine path
                    string path = Path.Combine(wwwRootPath, "images", petVM.ImageName);

                    // Save the file in wwwroot/images folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await petVM.ImageFile.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    ViewBag.Message = "Please upload an image.";
                    return View(petVM);
                }

                // Map the view model to the domain model
                Pet pet = _mapper.Map<Pet>(petVM);

                // Save to the repository
                await _unitService.PetService.AddPetAsync(pet);
                bool isSaved = await _unitService.SaveChangesAsync();

                // Provide feedback
                //ViewBag.Message = isSaved ? "Information has been saved successfully, thanks" : "Sorry! Information hasn't been saved";

                // Clear ModelState to reset the form
                //ModelState.Clear();
                if (isSaved)
                {
                    return RedirectToAction("Index", "Pet", new { text = "Information has been saved successfully, Thanks" });
                }
                ViewBag.Message = "Sorry! Information hasn't been saved";
                return View(petVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("Pet ID is not valid");
            }

            IEnumerable<AnimalType> animalTypes = (await _unitService.AnimalTypeService.GetAllAnimalTypeAsync());
            ViewBag.AnimalTypes = new SelectList(animalTypes, "AnimalTypeId", "AnimalTypeName");

            Pet? pet = await _unitService.PetService.GetPetByIdAsync(id.Value);
            if (pet == null) return NotFound($"No pet found with ID {id}");
            PetEditViewModel petEditVM = _mapper.Map<PetEditViewModel>(pet);
            return View(petEditVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PetEditViewModel petEditVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Sorry! Invalid information, please enter valid information.";
                return View(petEditVM);
            }

            // Retrieve the existing pet entity from the database
            Pet? existingPet = await _unitService.PetService.GetPetByIdAsync(petEditVM.PetId);
            if (existingPet == null)
            {
                ViewBag.Message = "Pet not found.";
                return View(petEditVM);
            }

            string wwwRootPath = _hostEnvironment.WebRootPath;

            // Handle image upload
            if (petEditVM.ImageFile != null)
            {
                // Generate a unique file name
                string fileName = Path.GetFileNameWithoutExtension(petEditVM.ImageFile.FileName);
                string extension = Path.GetExtension(petEditVM.ImageFile.FileName);

                // Validate file extension
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                if (!allowedExtensions.Contains(extension.ToLower()))
                {
                    ViewBag.Message = "Invalid file type. Only JPG, JPEG, and PNG are allowed.";
                    var animalTypes = (await _unitService.AnimalTypeService.GetAllAnimalTypeAsync());
                    ViewBag.AnimalTypes = new SelectList(animalTypes, "AnimalTypeId", "AnimalTypeName");
                    petEditVM.ImageName = existingPet.ImageName;
                    return View(petEditVM);
                }

                // Append timestamp to ensure unique file name
                string newImageName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string newImagePath = Path.Combine(wwwRootPath, "images", newImageName);

                // Save new image to the wwwroot/images folder
                using (var fileStream = new FileStream(newImagePath, FileMode.Create))
                {
                    await petEditVM.ImageFile.CopyToAsync(fileStream);
                }

                // Remove the old image from wwwroot/images
                if (!string.IsNullOrEmpty(existingPet.ImageName))
                {
                    string oldImagePath = Path.Combine(wwwRootPath, "images", existingPet.ImageName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Update the ImageName in the database
                existingPet.ImageName = newImageName;
            }

            //Update other fields
            existingPet.PetName = petEditVM.PetName;
            existingPet.Age = petEditVM.Age;
            existingPet.Color = petEditVM.Color;
            existingPet.Gender = petEditVM.Gender;
            existingPet.OwnerName = petEditVM.OwnerName;
            existingPet.AnimalTypeId = petEditVM.AnimalTypeId;
            existingPet.Address = petEditVM.Address;
            //_mapper.Map(petEditVM, existingPet); //automapper cannot be used here

            // Save to the database
            _unitService.PetService.UpdatePet(existingPet);
            bool isUpdated = await _unitService.SaveChangesAsync();

            if (isUpdated)
            {
                return RedirectToAction("Index", "Pet", new { text = "Information has been updated successfully." }); // Redirect to the list page or another page as needed 
                //ViewBag.Message = "Information has been updated successfully.";
                //PetViewModel updateExistingPetVM = _mapper.Map<PetViewModel>(existingPet);
                //return View(updateExistingPetVM);
            }
            else
            {
                ViewBag.Message = "Sorry! Information hasn't been updated.";
            }

            return View(petEditVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Pet? existingPet = await _unitService.PetService.GetPetByIdAsync(id);
            if (existingPet == null)
            {
                return RedirectToAction("Index", "Pet", new { text = "Sorry! there is no pet" });
            }
            //Optionally: Delete the image file from the server if there's an image associated
            if (!string.IsNullOrEmpty(existingPet.ImageName))
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "images", existingPet.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            await _unitService.PetService.RemovePet(existingPet);
            bool isRemoved = await _unitService.SaveChangesAsync();
            if (isRemoved)
            {
                return RedirectToAction("Index", "Pet");
            }
            return RedirectToAction("Index", "Pet", new { text = "Sorry! Pet hasn't been deleted" });
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Pet? existingPet = await _unitService.PetService.GetPetByIdAsync(id);
            if (existingPet == null)
            {
                return RedirectToAction("Index", "Pet", new { text = "Sorry! there is no pet" });
            }
            PetViewModel petVM = _mapper.Map<PetViewModel>(existingPet);
            return View(petVM);
        }
    }
}
