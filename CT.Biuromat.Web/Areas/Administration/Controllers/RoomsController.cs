using CT.Biuromat.Web.Areas.Administration.Services;
using CT.Biuromat.Web.Areas.Administration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CT.Biuromat.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class RoomsController : Controller
    {
        private readonly IRoomsService _roomsService;
        
        public RoomsController(IRoomsService roomsService)
        {
            _roomsService = roomsService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var model = _roomsService.PrepareVmForIndex();
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var model = _roomsService.PrepareVmForCreate();
            return View(model);
        }
        
        [HttpPost] 
        public IActionResult Create(RoomsCreateVm model)
        {
            _roomsService.AddToDatabase(model);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _roomsService.PrepareVmForDetails(id);
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _roomsService.PrepareVmForEdit(id);
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(RoomsEditVm model)
        {
            _roomsService.UpdateRoom(model);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult Remove(int id)
        {
            _roomsService.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }
    }
}