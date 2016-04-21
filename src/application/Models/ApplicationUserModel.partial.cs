using System;
using System.ComponentModel.DataAnnotations;

namespace Beginor.Owin.Application.Models {
    
    partial class ApplicationUserModel {

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}

