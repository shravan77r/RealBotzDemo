using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealBotzDemo.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select DOB.")]
        public DateTime? DateOfBirth { get; set; }
        public string DOB { get; set; }

        [Required(ErrorMessage = "Please Enter Address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Email.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Select Country.")]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public List<Country> Countries { get; set; }

        public string Hobby { get; set; }
        public string HobbyNames { get; set; }
        public List<Hobby> Hobbies { get; set; }

        [Required(ErrorMessage = "Please Select Gender.")]
        public int Gender { get; set; }
        public List<SelectListItem> GenderList { get; set; }

        public string OperationType { get; set; }
        public List<UserList> userLists { get; set; }
    }
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class HobbyModel
    {
        public int Id { get; set; }
        public int HobbyId { get; set; }
        public int UserId { get; set; }
        public string HobbyName { get; set; }
        public string OperationType { get; set; }
    }
    public class UserList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CountryName { get; set; }
        public string Hobbies { get; set; }
        public string Gender { get; set; }
    }

}
