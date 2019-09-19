﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Train2.GroupsMVC.Controllers
{
    public class StaticContentController : Controller
    {
        public ActionResult PageNotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("NotFound404");
        }

    }
}