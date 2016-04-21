using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Beginor.Owin.Application.Models {

    public partial class UserInfoModel {

        public string Name { get; set; }

        public bool Authorized { get; set; }

        public Dictionary<string, bool> Roles { get; set; }

        public string LoginProvider { get; set; }

    }

}