using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Dto;

namespace Project.Service.Interface
{
    public interface ICarService
    {
        CarDto AddCar(CreateCarRequestModel model);
        CarDto GetCar(int id);
        IList<CarDto> ListAllAvailableCars();
        IList<CarDto> GetBorrowedCarsByEmail(string email);

        IList<CarDto> ListAllNonAvailableCars();
        CarDto UpdateCarStatus(int id,  BookCarRequestModel model);
    }
}