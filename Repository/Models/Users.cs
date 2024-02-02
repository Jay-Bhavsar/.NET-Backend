using ENTITYAPP.DTO;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ENTITYAPP.Repository.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string email { get; set; }

        public string password { get; set; }

    }

  
}
