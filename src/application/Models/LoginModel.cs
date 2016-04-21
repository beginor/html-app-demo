using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Beginor.Owin.Application.Models {

    public class LoginModel {

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "记住密码")]
        public bool RememberMe { get; set; }

    }

}