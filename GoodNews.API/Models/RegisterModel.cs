using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.API.Models
{
    public class RegisterModel
    {
        //[Required]
        //public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        //[Required]
        //public DateTime Birthdate { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
