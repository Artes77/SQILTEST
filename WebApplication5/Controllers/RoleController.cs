using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Data.Interface;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/roles")]
    public class RoleController : Controller
    {
        IRole roleRep;
        IMapper mapper;

        public RoleController(IRole r, IMapper map)
        {
            roleRep = r;
            mapper = map;
        }

        [HttpGet]
        public RoleView[] GetAllRole()
        {
            var abc = roleRep.GetRoles().ToArray();
            RoleView[] role = mapper.Map<Role[], RoleView[]>(abc);
            return role;
        }


        [HttpGet("{id}")]
        public RoleView GetRole(int id)
        {
            RoleView role = mapper.Map<Role, RoleView>(roleRep.GetRoleById(id));
            return role;
        }


        [HttpPut]
        public void EditRole([FromBody]RoleView model)
        {
            Role role = mapper.Map<RoleView, Role>(model);
            roleRep.UpdateRole(role);
        }

        [HttpPost]
        public RoleView CreateRole([FromBody]RoleView model)
        {
            Role role = mapper.Map<RoleView, Role>(model);
            role = roleRep.CreateRole(role);
            RoleView a = mapper.Map<Role, RoleView>(role);
            return a;
        }


        [HttpDelete("{id}")]
        public bool DeleteRole(int id)
        {
            return roleRep.DeleteRole(new Role { Id = id });
        }
    }
}