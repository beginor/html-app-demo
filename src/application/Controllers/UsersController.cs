using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Beginor.Owin.Application.Data;
using Beginor.Owin.Application.Models;

namespace Beginor.Owin.Application.Controllers {

    [RoutePrefix("api/users")]
    public class UsersController : ApiController {

        private UserManager<ApplicationUser> userMgr;

        public Castle.Core.Logging.ILogger Logger { get; set; } = Castle.Core.Logging.NullLogger.Instance;

        public UsersController(UserManager<ApplicationUser> userManager) {
            this.userMgr = userManager;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                userMgr.Dispose();
                userMgr = null;
            }
            base.Dispose(disposing);
        }

        [HttpGet, Route("~/api/admin/users"), Authorize(Roles = "ViewAllUsers")]
        public IHttpActionResult GetAllForAdmin([FromUri]string keyword = "") {
            try {
                var data = QueryUsers(keyword, true);
                return Ok(data);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route(""), Authorize(Roles = "ViewUsers")]
        public IHttpActionResult GetAll([FromUri]string keyword = "") {
            try {
                var data = QueryUsers(keyword, false);
                return Ok(data);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        private IList<ApplicationUserModel> QueryUsers(string keyword, bool isAdmin) {
            IList<ApplicationUserModel> users;
            var query = userMgr.Users.ProjectTo<ApplicationUserModel>();
            if (!string.IsNullOrEmpty(keyword)) {
                query = query.Where(u => u.UserName.Contains(keyword));
            }
            if (!isAdmin) {
                query = query.Where(u => u.LockoutEnabled == false);
            }
            users = query.ToList();
            return users;
        }

        [HttpGet, Route("{id:guid}"), Authorize(Roles = "ViewAllUsers")]
        public async Task<IHttpActionResult> GetById(string id) {
            try {
                var user = await userMgr.FindByIdAsync(id);
                if (user == null) {
                    return NotFound();
                }
                var model = Mapper.Map<ApplicationUserModel>(user);
                return Ok(model);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }


        [HttpPost, Route(""), Authorize(Roles = "CreateUser")]
        public async Task<IHttpActionResult> CreateUser(ApplicationUserModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try {
                var user = Mapper.Map<ApplicationUser>(model);
                IdentityResult result;
                if (string.IsNullOrEmpty(model.Password)) {
                    result = await userMgr.CreateAsync(user);
                }
                else {
                    result = await userMgr.CreateAsync(user, model.Password);
                }
                if (result == null || !result.Succeeded) {
                    return GetErrorResult(result);
                }
                Mapper.Map(user, model);
                return Ok(model);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        [HttpPut, Route("{id:guid}"), Authorize(Roles = "UpdateUser")]
        public async Task<IHttpActionResult> UpdateUser([FromUri]string id, [FromBody]ApplicationUserModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty(id)) {
                return BadRequest("UserId can not be empty.");
            }

            try {
                var user = await userMgr.FindByIdAsync(id);
                if (user == null) {
                    return NotFound();
                }
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.LockoutEnabled = model.LockoutEnabled;
                if (!user.LockoutEnabled) {
                    user.LockoutEndDateUtc = null;
                    user.AccessFailedCount = 0;
                }

                var result = await userMgr.UpdateAsync(user);
                if (result == null || !result.Succeeded) {
                    return GetErrorResult(result);
                }
                if (result.Succeeded && !string.IsNullOrEmpty(model.Password)) {
                    if (userMgr.HasPassword(user.Id)) {
                        var token = await userMgr.GeneratePasswordResetTokenAsync(user.Id);
                        result = await userMgr.ResetPasswordAsync(user.Id, token, model.Password);
                        if (result == null || !result.Succeeded) {
                            return GetErrorResult(result);
                        }
                        return Ok();
                    }
                    result = await userMgr.AddPasswordAsync(user.Id, model.Password);
                    if (result == null || !result.Succeeded) {
                        return GetErrorResult(result);
                    }
                    return Ok();
                }
                return Ok();
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        [HttpDelete, Route("{id:guid}"), Authorize(Roles = "DeleteUser")]
        public async Task<IHttpActionResult> DeleteUser(string id) {
            if (string.IsNullOrEmpty(id)) {
                return BadRequest("UserId can not be empty.");
            }
            try {
                var user = await userMgr.FindByIdAsync(id);
                if (user != null) {
                    var result = await userMgr.DeleteAsync(user);
                    if (result == null || !result.Succeeded) {
                        return GetErrorResult(result);
                    }
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result) {
            if (result == null) {
                return InternalServerError();
            }
            if (!result.Succeeded) {
                if (result.Errors != null) {
                    foreach (string error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }
                if (ModelState.IsValid) {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }
            return null;
        }
    }

}