using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using NHibernate.AspNet.Identity;
using NHibernate.Linq;
using Beginor.Owin.Application.Data;
using Beginor.Owin.Application.Models;
using Beginor.Owin.Application.Security;
using System.Net;

namespace Beginor.Owin.Application.Controllers {

    [RoutePrefix("api/roles")]
    public class RolesController : ApiController {

        private RoleStore<ApplicationRole> roleStore;
        private ISystemRolesFinder sysRolesFinder;

        public Castle.Core.Logging.ILogger Logger { get; set; } = Castle.Core.Logging.NullLogger.Instance;
        
        public RolesController(RoleStore<ApplicationRole> roleStore, ISystemRolesFinder sysRolesFinder) {
            this.roleStore = roleStore;
            this.sysRolesFinder = sysRolesFinder;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                this.roleStore = null;
            }
            base.Dispose(disposing);
        }

        [HttpGet, Route("refresh"), Authorize(Roles = "ViewAllRoles")]
        public IHttpActionResult Refresh() {
            try {
                var sysRolesDict = sysRolesFinder.FindSystemRoles();
                foreach (var sysRolesPair in sysRolesDict) {
                    var sysRoleNames = sysRolesPair.Value;
                    foreach (var sysRoleName in sysRoleNames) {
                        var role = roleStore.Context.Query<ApplicationRole>().FirstOrDefault(r => r.Name == sysRoleName);
                        if (role == null) {
                            role = new ApplicationRole {
                                Name = sysRoleName,
                                Essential = true,
                                Module = sysRolesPair.Key
                            };
                            roleStore.Context.Save(role);
                        }
                        else {
                            if (!role.Essential) {
                                role.Essential = true;
                                role.Module = sysRolesPair.Key;
                                roleStore.Context.Update(role);
                            }
                        }
                    }
                    roleStore.Context.Flush();
                    roleStore.Context.Clear();

                    var noneSysRoles = roleStore.Roles.Where(r => !sysRoleNames.Contains(r.Name) && r.Module == sysRolesPair.Key).ToList();
                    if (noneSysRoles.Any()) {
                        foreach (var noneSysRole in noneSysRoles) {
                            if (noneSysRole.Essential) {
                                noneSysRole.Essential = false;
                                roleStore.Context.Update(noneSysRole);
                            }
                        }
                    }
                    roleStore.Context.Flush();
                }

                return Ok();
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route(""), Authorize(Roles = "ViewAllRoles")]
        public IHttpActionResult GetAllRoles([FromUri]string keyword = "") {
            try {
                var query = roleStore.Roles;
                if (!string.IsNullOrEmpty(keyword)) {
                    query = query.Where(role => role.Name.Contains(keyword));
                }
                var roles = query.ProjectTo<ApplicationRoleModel>()
                                 //.GroupBy(m => m.Module)
                                 .ToList()
                                 .GroupBy(m => m.Module)
                                 .Select(g => new { module = g.Key, roles = g.ToList() })
                                 .ToList();
                return Ok(roles);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("{id:length(32)}"), Authorize(Roles = "ViewAllRoles")]
        public async Task<IHttpActionResult> GetById(string id) {
            try {
                var role = await roleStore.FindByIdAsync(id);
                if (role == null) {
                    return NotFound();
                }
                return Ok(Mapper.Map<ApplicationRoleModel>(role));
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        [HttpPut, Route("{id:length(32)}"), Authorize(Roles = "UpdateRole")]
        public async Task<IHttpActionResult> Update(string id, ApplicationRoleModel model) {
            try {
                var role = await roleStore.FindByIdAsync(id);
                if (role == null) {
                    return NotFound();
                }
                Mapper.Map(model, role);
                await roleStore.UpdateAsync(role);
                return Ok(Mapper.Map(role, model));
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        [HttpDelete, Route("{id:length(32)}"), Authorize(Roles = "DeleteRole")]
        public async Task<IHttpActionResult> Delete(string id) {
            try {
                var role = await roleStore.FindByIdAsync(id);
                if (role == null) {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                await roleStore.DeleteAsync(role);
                return Ok(Mapper.Map<ApplicationRoleModel>(role));
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }
    }
}

