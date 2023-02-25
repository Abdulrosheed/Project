using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Project.Dto
{
    public record CarDto(int Id , string Name , string Model , string ImagePath1 ,  string ImagePath2 , bool IsAvailable , DateTime BorrowedDate , DateTime ReturnedDate,  string BorrowerEmail);
    
    public class CreateCarRequestModel
    {
        public string Name {get;set;}
        [BindProperty]
        public string Model {get;set;}
        public IFormFile Image1 {get;set;}
         public IFormFile Image2 {get;set;}


        //(string Name , string Model , IFormFile Image1 ,  IFormFile Image2  )
    }
    public class BookCarRequestModel
    {
        public string Name {get;set;}
         public int Id {get;set;}
        public string MDel {get;set;}
        
        public DateTime BorrowedDate {get;set;}
        
        public DateTime ReturnedDate {get;set;}
        public string BorrowerEmail {get;set;}
        
    }
}