using System;

namespace Beginor.Owin.Application.Data {

    public partial class ApplicationRole {

        public virtual string Description { get; set; }

        /// <summary>
        /// a role is essential means it is required by controller's authorize attribute 
        /// </summary>
        /// <value>The essential.</value>
        public virtual bool Essential { get; set; }

        public virtual string Module { get; set; }
    }
}

