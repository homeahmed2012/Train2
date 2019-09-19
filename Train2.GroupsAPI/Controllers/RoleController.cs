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
    public class RoleController : ApiController
    {
        private RoleService roleService = new RoleService();

        public HttpResponseMessage GetAllRoles()
        {
            RoleResult roleResult = roleService.GetAll();

            HttpResponseMessage response;

            if (roleResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, roleResult.bRoles);
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, roleResult.ErrorMessages);
            return response;
        }

        public HttpResponseMessage GetRole(int id)
        {
            RoleResult roleResult = roleService.Get(id);
            HttpResponseMessage response;

            if (roleResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, roleResult.bRoles.Single());
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, roleResult.ErrorMessages);
            }

            return response;
        }

        public HttpResponseMessage PostNewRole(BRole bRole)
        {
            RoleResult roleResult = roleService.Add(bRole);
            HttpResponseMessage response;

            if (roleResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, roleResult.bRoles.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, roleResult.ErrorMessages);
            return response;
        }

        public HttpResponseMessage DeleteRole(int id)
        {
            RoleResult roleResult = roleService.Delete(id);
            HttpResponseMessage response;

            if (roleResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, roleResult.bRoles.Single());
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, roleResult.ErrorMessages);
            }

            return response;
        }

        [HttpPut]
        public HttpResponseMessage UpdateRole(int id, BRole bRole)
        {
            RoleResult roleResult = roleService.Edit(id, bRole);
            HttpResponseMessage response;

            if (roleResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, roleResult.bRoles.Single());
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, roleResult.ErrorMessages);
            }

            return response;
        }
    }
}
