using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Car
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Model {get;set;}
        public string? BorrowerEmail {get;set;} 
        public bool IsAvailable {get;set;}
        public DateTime BorrowedDate {get;set;}
        public DateTime DateToBeReturned {get;set;}
        public string ImagePath1 {get;set;}
        public string ImagePath2 {get;set;}

    }
}