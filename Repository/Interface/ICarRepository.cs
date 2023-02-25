using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Dto;
using Project.Model;

namespace Project.Repository.Interface
{
    public interface ICarRepository
    {
        Car AddCar(Car model);
        Car GetCar(int id);
        IList<Car> ListAllAvailableCars();
        IList<Car> GetBorrowedCarByEmail(string email);
        IList<Car> ListAllNonAvailableCars();
        Car UpdateCar(Car model);
        Car UpdateCarStatus(int id , BookCarRequestModel model);
        
    }
}