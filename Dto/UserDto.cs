using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Dto
{
    public record UserDto(int Id , string FirstName , string LastName , string PhoneNumber , string Role , string Email);
    public record CreateUserRequestModel(string FirstName , string LastName , string PhoneNumber , string Email , string PassWord );
     public record LoginUserRequestModel(string Email , string PassWord );
    
}