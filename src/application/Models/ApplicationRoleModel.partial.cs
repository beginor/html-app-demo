using System;
using System.ComponentModel.DataAnnotations;

namespace Beginor.Owin.Application.Models {

    partial class ApplicationRoleModel {

        public string Description { get; set; }

        /// <summary>
        /// a role is essential means it is required by controller's authorize attribute 
        /// </summary>
        /// <value>The essential.</value>
        public bool Essential { get; set; }

        public string Module { get; set; }

    }
}

