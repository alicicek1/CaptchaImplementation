using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Captcha.Models
{
    public class LoginVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Password must have at least 2 character.")]
        public string Password { get; set; }
        public bool CaptchaCheck { get; set; }
    }
}
