using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Beginor.Owin.Application.Security {

    public class ReflectionSystemRolesFinder : ISystemRolesFinder {

        public Castle.Core.Logging.ILogger Logger { get; set; } = Castle.Core.Logging.NullLogger.Instance;

        public IDictionary<string, IReadOnlyCollection<string>> FindSystemRoles() {
            var rolesDict = new Dictionary<string, IReadOnlyCollection<string>>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in assemblies) {
                FindRolesInAssembly(asm, rolesDict);
            }
            return rolesDict;
        }

        private void FindRolesInAssembly(Assembly asm, IDictionary<string, IReadOnlyCollection<string>> roles) {
            try {
                var baseType = typeof(System.Web.Http.Controllers.IHttpController);
                var controllers = asm.ExportedTypes
                                     .Where(t => baseType.IsAssignableFrom(t));
                foreach (var controller in controllers) {
                    var rolesInType = FindRolesInType(controller);
                    roles.Add(controller.FullName, rolesInType);
                }
            }
            catch (Exception ex) {
                Logger.Error($"Can not find roles in assembly {asm}", ex);
            }
        }

        private IReadOnlyCollection<string> FindRolesInType(Type type) {
            var list = new List<string>();
            try {
                var roles = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                .Where(m => m.GetCustomAttributes<System.Web.Http.AuthorizeAttribute>().Any())
                                .SelectMany(m => m.GetCustomAttributes<System.Web.Http.AuthorizeAttribute>())
                                .Union(type.GetCustomAttributes<System.Web.Http.AuthorizeAttribute>())
                                .Select(attr => attr.Roles)
                                .Distinct()
                                .Where(role => !string.IsNullOrEmpty(role));
                list.AddRange(roles);
            }
            catch (Exception ex) {
                Logger.Error($"Can not find roles from type {type}", ex);
            }
            return list.AsReadOnly();
        }
    }

}
