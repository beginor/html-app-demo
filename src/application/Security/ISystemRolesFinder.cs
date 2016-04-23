using System;
using System.Collections.Generic;

namespace Beginor.Owin.Application.Security {
    
    public interface ISystemRolesFinder {

        IDictionary<string, IReadOnlyCollection<string>> FindSystemRoles();

    }

}

