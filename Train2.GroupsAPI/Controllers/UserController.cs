using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Train2.Base;
using Train2.Business;

namespace Train2.GroupsAPI.Controllers
{
    [EnableCors(origins: "http://localhost:52525", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        UserService userService = new UserService();

        // get a user by id
        public HttpResponseMessage GetUser(int id)
        {
            UserResult userResult = userService.Get(id);
            HttpResponseMessage response;
            if (userResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, userResult.bUsers.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, userResult.ErrorMessages);

            return response;
        }

        // get all users
        public HttpResponseMessage GetAllUsers()
        {
            UserResult userResult = userService.GetAll();

            HttpResponseMessage response;

            if (userResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, userResult.bUsers);
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, userResult.ErrorMessages);
            return response;
        }

        // add a new user
        public HttpResponseMessage PostNewUser(BUser bUser)
        {
            UserResult userResult = userService.Add(bUser);
            HttpResponseMessage response;

            if (userResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, userResult.bUsers.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, userResult.ErrorMessages);
            return response;
        }

        // delete a user
        public HttpResponseMessage DeleteUser(int id)
        {
            UserResult userResult = userService.Delete(id);
            HttpResponseMessage response;

            if (userResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, userResult.bUsers.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, userResult.ErrorMessages);
            return response;
        }

        // update a user
        [HttpPut]
        public HttpResponseMessage UpdateUser(int id, BUser bUser)
        {
            UserResult userResult = userService.Edit(id, bUser);
            HttpResponseMessage response;

            if (userResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, userResult.bUsers.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, userResult.ErrorMessages);
            return response;
        }

    }
}
