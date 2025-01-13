using _10_PetInfoProj.ViewModels.AnimalTypeVM;
using _10_PetInfoProj.ViewModels.PetVM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetInfo.Models.EntityModels;
using PetInfo.Services.Contracts.AllContracts;

namespace _10_PetInfoProj.Controllers
{
    public class AnimalTypeController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;
        public AnimalTypeController(IUnitService unitService, IMapper mapper)
        {
            this._unitService = unitService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(string text)
        {
            ViewBag.Message = text;
            IEnumerable<AnimalType> animalTypes = await _unitService.AnimalTypeService.GetAllAnimalTypeAsync();
            IEnumerable<AnimalTypeViewModel> animalTypesVM = _mapper.Map<IEnumerable<AnimalTypeViewModel>>(animalTypes);
            return View(animalTypesVM);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AnimalTypeViewModel animalTypeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Sorry! invalid information, please enter valid information";
                return View(animalTypeVM);
            }
            //Map viewmodel to domain model
            AnimalType animalType = _mapper.Map<AnimalType>(animalTypeVM);

            //Save to the repository
            await _unitService.AnimalTypeService.AddAnimalTypeAsync(animalType);
            bool isSaved = await _unitService.SaveChangesAsync();

            if (isSaved)
            {
                return RedirectToAction("Index", "AnimalType", new { text = "Information has been saved successfully, Thanks" });
            }
            ViewBag.Message = "Sorry! information hasn't been saved";
            return View(animalTypeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AnimalType? animalType = await _unitService.AnimalTypeService.GetAnimalTypeByIdAsync(id);
            if (animalType == null)
            {
                return RedirectToAction("Index", "AnimalType", new { text = "Sorry! no Animal Type found." });
            }
            AnimalTypeViewModel animalTypeVM = _mapper.Map<AnimalTypeViewModel>(animalType);
            return View(animalTypeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AnimalTypeViewModel animalTypeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Invalid information, please enter valid information.";
                return View(animalTypeVM);
            }
            AnimalType? existingAnimalType = await _unitService.AnimalTypeService.GetAnimalTypeByIdAsync(animalTypeVM.AnimalTypeId);
            if (existingAnimalType == null)
            {
                ViewBag.Message = "Sorry! no animal type found";
                return View(animalTypeVM);
            }
            _mapper.Map(animalTypeVM, existingAnimalType);
            _unitService.AnimalTypeService.UpdateAnimalType(existingAnimalType);
            bool isUpdated = await _unitService.SaveChangesAsync();
            if (isUpdated)
            {
                return RedirectToAction("Index", "AnimalType", new { text = "Information has been updated successfully." });
            }
            ViewBag.Message = "Sorry! information hasn't been updated";
            return View(animalTypeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            AnimalType? existingAnimalType = await _unitService.AnimalTypeService.GetAnimalTypeByIdAsync(id);
            if (existingAnimalType == null)
            {
                return RedirectToAction("Index", "AnimalType", new { text = "Sorry! no Animal Type found" });
            }
            _unitService.AnimalTypeService.RemoveAnimalType(existingAnimalType);
            bool isRemoved = await _unitService.SaveChangesAsync();
            if (isRemoved)
            {
                return RedirectToAction("Index", "AnimalType", new { text = "Animal Type has been deleted." });
            }
            string text = "Sorry! Animal Type hasn't been deleted";
            return RedirectToAction("Index", "AnimalType", text);
        }

        [HttpGet("/AnimalType/ViewAnimalType/{id}")]
        public async Task<IActionResult> ViewAnimalType(int id)
        {
            List<Pet> pets = _unitService.PetService.GetAllPets()
                .Where(p => p.AnimalTypeId == id).ToList();
            if (pets == null || !pets.Any())
            {
                return RedirectToAction("Index", "AnimalType", new { text = "Sorry! no pet found for this type" });
            }
            List<PetViewModel> petsVM = _mapper.Map<List<PetViewModel>>(pets);
            return View("~/Views/Pet/ViewAllAnimalType.cshtml", petsVM);
        }
    }
}
