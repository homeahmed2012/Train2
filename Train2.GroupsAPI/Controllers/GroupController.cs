using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using Train2.Base;
using Train2.Business;
using System.Web.Http.Cors;

namespace Train2.GroupsAPI.Controllers
{
    [EnableCors(origins: "http://localhost:52525", headers: "*", methods: "*")]
    public class GroupController : ApiController
    {
        private GroupService groupService = new GroupService();
        
        // get a group by id
        public HttpResponseMessage GetGroup(int id)
        {
            GroupResult groupResult = groupService.Get(id);
            HttpResponseMessage response;
            if (groupResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, groupResult.bGroups.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, groupResult.ErrorMessages);

            return response;
        }
        
        // get all groups
        public HttpResponseMessage GetAllGroups()
        {
            GroupResult groupResult = groupService.GetAll();

            HttpResponseMessage response;

            if (groupResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, groupResult.bGroups);
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, groupResult.ErrorMessages);
            return response;
        }
        
        // add a new group
        public HttpResponseMessage PostNewGroup(BGroup bGroup)
        {
            GroupResult groupResult = groupService.Add(bGroup);
            HttpResponseMessage response;

            if (groupResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, groupResult.bGroups.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, groupResult.ErrorMessages);
            return response;
        }
        
        // delete a group
        public HttpResponseMessage DeleteGroup(int id)
        {
            GroupResult groupResult = groupService.Delete(id);
            HttpResponseMessage response;

            if (groupResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, groupResult.bGroups.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, groupResult.ErrorMessages);
            return response;
        }
        
        // update a group
        [HttpPut]
        public HttpResponseMessage UpdateGroup(int id, BGroup bGroup)
        {
            GroupResult groupResult = groupService.Edit(id, bGroup);
            HttpResponseMessage response;

            if (groupResult.IsValid)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, groupResult.bGroups.Single());
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.BadRequest, groupResult.ErrorMessages);
            return response;
        }
    }
}
