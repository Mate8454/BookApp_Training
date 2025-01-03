﻿using BookApp.Entities;
using BookApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookApp.Controllers
{
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        private UserRepository repository;

        public UserController()
        {
            repository = new UserRepository();
        }

        [Route("AddUser")]
        public IHttpActionResult AddUser([FromBody]User user)
        {
            repository.AddUser(user);
            return Ok(user);
        }

        [Route("AuthenticateUser/{email}/{password}")]
        public IHttpActionResult AuthenticateUser(string email, string password)
        {
            var user = repository.AuthenticateUser(email, password);

            if (user == null)
            {
               
                return Content(HttpStatusCode.Unauthorized, "Invalid credentials.");
            }

            
            return Ok(user );
        }


        [Route("GetUserDetails/{userid}")]
        public IHttpActionResult GetUserDetails(int userid)
        {
            var res = repository.GetUserDetail(userid);
            return Ok(res);
        }

        [HttpPut ,Route("UpdateUser")]
        public IHttpActionResult UpdateUser(User user) 
        {
            repository.UpdateUser(user);
             return Ok("User Updated");


        }
        
    }
}
