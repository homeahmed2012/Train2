using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Train2.Base;
using System.Text;
using Train2.Business;
using Train2.GroupsMVC.Filters;

namespace Train2.GroupsMVC.Controllers
{


    public class UserController : Controller
    {
        private readonly UserService userService = new UserService();

        readonly HttpClient client = new HttpClient();
        readonly string userApiUrl = Constants.apiUrl + "/user";
        

        // GET: Users
        public async Task<ActionResult> Index()
        {
            IList<BUser> bUsers = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(userApiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bUsers = JsonConvert.DeserializeObject<List<BUser>>(data);
                ViewBag.error = false;
            }
            catch (HttpRequestException)
            {
                ViewBag.error = true;
            }
            return View(bUsers);
        }


        // Get: User by id
        [Authorize]
        public async Task<ActionResult> GetUser(int id)
        {
            BUser bUser = null;
            
            try
            {
                string apiUrl = string.Format("{0}/{1}", userApiUrl, id);
                
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bUser = JsonConvert.DeserializeObject<BUser>(data);
                ViewBag.error = false;
            }
            catch (Exception)
            {
                return View("NotFound404");
            }
            return View(bUser);
        }


        // Post: add a new user
        public async Task<ActionResult> PostUser(BUser bUser)
        {
            if (ModelState.IsValid)
            {
                BUser newBUser = null;
                try
                {
                    // send a request to the API
                    var content = new StringContent(JsonConvert.SerializeObject(bUser), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(userApiUrl, content);
                    string data = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        // set cookies
                        newBUser = JsonConvert.DeserializeObject<BUser>(data);
                        FormsAuthentication.SetAuthCookie(newBUser.Email, true, "/");
                        return RedirectToAction("GetUser", new { id = newBUser.UserId });
                    }
                    else
                    {
                        ModelState.AddModelError("GroupId", "This group doesn't exist.");
                        return View("AddUser", bUser);
                    }
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
                
            }
            return View("AddUser", bUser);
        }


        [HttpGet]
        // Get add user form
        public async Task<ActionResult> AddUser()
        {
            // get a list of all groups
            IList<BGroup> bGroups = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(Constants.apiUrl + "/group");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bGroups = JsonConvert.DeserializeObject<List<BGroup>>(data);
                ViewBag.groups = bGroups;
            }
            catch (HttpRequestException)
            {
                // TODO: to be handled
                ViewBag.error = true;
            }
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UpdateUser(int id, BUser bUser)
        {
            try
            {
                string apiUrl = string.Format("{0}/{1}", userApiUrl, id);
                var content = new StringContent(JsonConvert.SerializeObject(bUser), Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var data = response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("GetUser", new {  id });
        }

        [Authorize]
        public async Task<ActionResult> EditUser(int id)
        {
            BUser bUser = null;
            IList<BGroup> bGroups = null;
            try
            {
                // get the user object
                string apiUrl = string.Format("{0}/{1}", userApiUrl, id);
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bUser = JsonConvert.DeserializeObject<BUser>(data);

                // get groups list
                HttpResponseMessage response2 = await client.GetAsync(Constants.apiUrl + "/group");
                response2.EnsureSuccessStatusCode();
                var data2 = await response2.Content.ReadAsStringAsync();
                bGroups = JsonConvert.DeserializeObject<List<BGroup>>(data2);
                ViewBag.groups = bGroups;
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return View(bUser);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(BUser bUser)
        {
            UserResult userResult = userService.GetByEmail(bUser.Email);
            if (userResult.IsValid)
            {
                // check password
                bool isPasswordCorrect = userService.CheckPasswork(userResult.bUsers.Single().UserId, bUser.Password);
                if (isPasswordCorrect)
                {
                    // set cookies
                    FormsAuthentication.SetAuthCookie(userResult.bUsers.Single().Email, true, "/");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Password", "This password is wrong please try again.");
                    return View(bUser);
                }
            }
            
            ModelState.AddModelError("Email", "This email doesn't exist.");
            return View(bUser);
        }


        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}