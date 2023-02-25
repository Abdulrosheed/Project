using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Dto;
using Project.Model;
using Project.Repository.Interface;
using Project.Service.Interface;

namespace Project.Service.Implementation
{
    public class CarService : ICarService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICarRepository _carRepository;

        public CarService(IWebHostEnvironment webHostEnvironment, ICarRepository carRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _carRepository = carRepository;
        }

        public CarDto AddCar(CreateCarRequestModel model)
        {
            
            string path= Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            Directory.CreateDirectory(path);
            string contentType1= model.Image1.ContentType.Split('/')[1];
            string contentType2= model.Image2.ContentType.Split('/')[1];
            string imageName1 = $"{Guid.NewGuid().ToString().Substring(1, 7)}.{contentType1}";
            string imageName2 = $"{Guid.NewGuid().ToString().Substring(1, 7)}.{contentType2}";
            string fullpath1= Path.Combine(path,imageName1);
            string fullpath2= Path.Combine(path,imageName2);
            using (var filestream= new FileStream(fullpath1, FileMode.Create))
            {
                model.Image1.CopyTo(filestream);
            }
             using (var filestream= new FileStream(fullpath2, FileMode.Create))
            {
                model.Image2.CopyTo(filestream);
            }
            var car = new Car
            {
                Model =model.Model,
                ImagePath1 = imageName1,
                ImagePath2 = imageName2,
                Name = model.Name,
                IsAvailable = true,

            };
           var res =  _carRepository.AddCar(car);
           return new CarDto(res.Id , res.Name,res.Model,res.ImagePath1,res.ImagePath2,res.IsAvailable,res.BorrowedDate,res.DateToBeReturned,res.BorrowerEmail);
        

        }

        public IList<CarDto> GetBorrowedCarsByEmail(string email)
        {
            var cars = _carRepository.GetBorrowedCarByEmail(email);
            return cars.Select(a => new CarDto(a.Id,a.Name,a.Model,a.ImagePath1,a.ImagePath2,a.IsAvailable,a.BorrowedDate,a.DateToBeReturned,a.BorrowerEmail)).ToList();
        }

        public CarDto GetCar(int id)
        {
            var res =  _carRepository.GetCar(id);
           return new CarDto(res.Id , res.Name,res.Model,res.ImagePath1,res.ImagePath2,res.IsAvailable,res.BorrowedDate,res.DateToBeReturned,res.BorrowerEmail);
        }

        public IList<CarDto> ListAllAvailableCars()
        {
            return _carRepository.ListAllAvailableCars().Select(a => new CarDto(a.Id,a.Name,a.Model,a.ImagePath1,a.ImagePath2,a.IsAvailable,a.BorrowedDate,a.DateToBeReturned,a.BorrowerEmail)).ToList();

        }

        public IList<CarDto> ListAllNonAvailableCars()
        {
            return _carRepository.ListAllNonAvailableCars().Select(a => new CarDto(a.Id,a.Name,a.Model,a.ImagePath1,a.ImagePath2,a.IsAvailable,a.BorrowedDate,a.DateToBeReturned,a.BorrowerEmail)).ToList();

        }

        public CarDto UpdateCarStatus(int id , BookCarRequestModel model)
        {
            var car = _carRepository.UpdateCarStatus(id , model);
            if(car is null)return null;
            return new CarDto(car.Id , car.Name,car.Model,car.ImagePath1,car.ImagePath2,car.IsAvailable , car.BorrowedDate,car.DateToBeReturned,car.BorrowerEmail);

        }

       
    }
}