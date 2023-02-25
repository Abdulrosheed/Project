using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.ApplicationContext;
using Project.Dto;
using Project.Model;
using Project.Repository.Interface;

namespace Project.Repository.Implementation
{
    public class CarRepository : ICarRepository
    {
        private readonly ContextClass _context;

        public CarRepository(ContextClass context)
        {
            _context = context;
        }

        public Car AddCar(Car model)
        {
            
            _context.Cars.Add(model);
            _context.SaveChanges();
            return model;
        }

        public IList<Car> GetBorrowedCarByEmail(string email)
        {
            return _context.Cars.Where(a => a.BorrowerEmail == email).ToList();
        }

        public Car GetCar(int id)
        {
            return _context.Cars.SingleOrDefault(a => a.Id == id);
        }

        public IList<Car> ListAllAvailableCars()
        {
            return _context.Cars.Where(a => a.IsAvailable == true).ToList();
        }

        

        public IList<Car> ListAllNonAvailableCars()
        {
            return _context.Cars.Where(a => a.IsAvailable == false).ToList();
        }

        public Car UpdateCar(Car model)
        {
            _context.Cars.Update(model);
            _context.SaveChanges();
            return model;
        }

        public Car UpdateCarStatus(int id , BookCarRequestModel model)
        {
            var car = _context.Cars.SingleOrDefault(a => a.Id == id);
            if(car == null)return null;
            if(model.BorrowedDate.Day >= DateTime.Now.Day && model.BorrowedDate.Day <= model.ReturnedDate.Day)
            {
                car.IsAvailable = false;
                car.BorrowerEmail = model.BorrowerEmail;
            }
            
            _context.Cars.Update(car);
            _context.SaveChanges();
            return car;
        }
    }
}