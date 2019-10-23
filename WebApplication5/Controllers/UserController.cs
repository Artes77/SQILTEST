using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Data.Interface;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        IUser userRep;
        IMapper mapper;

        public UserController(IUser u, IMapper map)
        {
            userRep = u;
            mapper = map;
        }

        [HttpGet]
        public UserView[] GetAllUser()
        {
            UserView[] users = mapper.Map<User[], UserView[]>(userRep.GetUsers().ToArray());
            return users;
        }


        [HttpGet("{id}")]
        public UserView GetUser(int id)
        {
            UserView user = mapper.Map<User, UserView>(userRep.GetUserById(id));
            return user;
        }


        [HttpPut]
        public void EditUser([FromBody]UserView model)
        {
            User user = mapper.Map<UserView, User>(model);
            userRep.UpdateUser(user);
        }

        [HttpPost]
        public UserView CreateUser([FromBody]UserView model)
        {
            User user = mapper.Map<UserView, User>(model);
            user = userRep.CreateUser(user);
            UserView a = mapper.Map<User, UserView>(userRep.GetUserById(user.Id));
            return a;
        }


        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            userRep.DeleteUser(new User { Id = id });
        }
    }
}