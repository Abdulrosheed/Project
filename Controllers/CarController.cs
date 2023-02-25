using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Dto;
using Project.Service.Interface;

namespace Project.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService  _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        public IActionResult ListAllAvailableCars()
        {
            return View(_carService.ListAllAvailableCars());
        }
         public IActionResult ShowCars()
        {
            return View(_carService.ListAllAvailableCars());
        }
        public IActionResult ListAllNonAvailableCars ()
        {
          
            return View(_carService.ListAllNonAvailableCars());
        }
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create (CreateCarRequestModel requestModel)
        {
            
            _carService.AddCar(requestModel);
            return RedirectToAction ("ListAllAvailableCars");
        }
        public IActionResult ListAllRentedCarsByAUser()
        {
            return View(_carService.GetBorrowedCarsByEmail(User.FindFirst(ClaimTypes.Email).Value));
        }
        public IActionResult BookCar(int id)
        {
           var car =  _carService.GetCar(id);
           BookCarRequestModel res = new BookCarRequestModel{Id = car.Id , Name = car.Name , MDel = car.Model};

            return View(res);
        }
        [HttpPost]
        public IActionResult BookCar(BookCarRequestModel obj)
        {
            obj.BorrowerEmail = User.FindFirst(ClaimTypes.Email).Value;
            _carService.UpdateCarStatus(obj.Id , obj);
            return RedirectToAction("ListAllRentedCarsByAUser");
        }
    }
}