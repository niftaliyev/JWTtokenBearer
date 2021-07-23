using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTest.Models.DTO
{
    public class AccountCredentialsDTO
    {
        [Required(ErrorMessage = "tetete")]
        public string Email { get; set; }
        [Required(ErrorMessage = "fasgag")]
        public string Password { get; set; }
    }
}
