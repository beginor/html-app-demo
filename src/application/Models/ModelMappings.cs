using System;
using Castle.Core;
using AutoMapper;
using Beginor.Owin.Application.Data;

namespace Beginor.Owin.Application.Models {
    
    public class ModelMappings : IStartable {

        public void Start() {
            Mapper.CreateMap<ApplicationUser, ApplicationUserModel>();
            Mapper.CreateMap<ApplicationUserModel, ApplicationUser>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleModel>();
            Mapper.CreateMap<ApplicationRoleModel, ApplicationRole>();
        }

        public void Stop() {
            Mapper.Reset();
        }
    }
}

