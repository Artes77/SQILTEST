using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models;
using WebApplication5.Data.Interface;

namespace WebApplication5.Controllers
{
    [Route("api/permissions")]
    public class PermissionController : Controller
    {
        IPermission permission;
        IMapper mapper;

        public PermissionController(IPermission p, IMapper map)
        {
            permission = p;
            mapper = map;
        }

        [HttpGet]
        public PermissionView[] GetAllPermission()
        {
            var All = permission.GetPermissions().ToArray();
            PermissionView[] Perm = mapper.Map<Permission[], PermissionView[]>(All);
            return Perm;
        }


        [HttpGet("{id}")]
        public PermissionView GetPermission(int id)
        {
            var abc = permission.GetPermissionById(id);
            PermissionView Perm = mapper.Map<Permission, PermissionView>(abc);
            return Perm;

        }


        [HttpPut]
        public void EditPermission([FromBody]PermissionView model)
        {
            Permission perm = mapper.Map<PermissionView, Permission>(model);
            permission.UpdatePermission(perm);
        }

        [HttpPost]
        public PermissionView CreatePermission([FromBody]PermissionView model)
        {
            Permission perm = mapper.Map<PermissionView, Permission>(model);
            perm = permission.CreatePermission(perm);
            PermissionView a = mapper.Map<Permission, PermissionView>(perm);
            return a;
        }


        [HttpDelete("{id}")]
        public void DeletePermission(int id)
        {
            permission.DeletePermission(new Permission { Id = id });
        }
    }
}