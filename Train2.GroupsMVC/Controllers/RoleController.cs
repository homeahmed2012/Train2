using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Train2.Base;
using Train2.GroupsMVC.Filters;

namespace Train2.GroupsMVC.Controllers
{
    public class RoleController : Controller
    {
        readonly HttpClient client = new HttpClient();
        readonly string roleApiUrl = Constants.apiUrl + "/role";


        // GET: Roles
        [Authorize]
        [CustomRoleFilter(Role = "admin")]
        public async Task<ActionResult> Index()
        {
            IList<BRole> bRoles = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(roleApiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bRoles = JsonConvert.DeserializeObject<List<BRole>>(data);
            }
            catch (HttpRequestException)
            {
                // TODO: handle this error in frontend
                ViewBag.error = true;
            }
            return View(bRoles);
        }


        // Get: Role by id
        [Authorize]
        [CustomRoleFilter(Role = "admin")]
        public async Task<ActionResult> GetRole(int id)
        {
            BRole bRole = null;

            try
            {
                string apiUrl = string.Format("{0}/{1}", roleApiUrl, id);
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bRole = JsonConvert.DeserializeObject<BRole>(data);
            }
            catch (Exception e)
            {
                // TODO: handel this error properly
                return Content(e.Message);
            }
            return View(bRole);
        }


        public async Task<ActionResult> PostRole(BRole bRole)
        {
            BRole newBRole = null;
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(bRole), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(roleApiUrl, content);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                newBRole = JsonConvert.DeserializeObject<BRole>(data);
            }
            catch (Exception e)
            {
                ViewBag.error = true;
                return Content(e.Message);
            }
            return RedirectToAction("GetRole", new { id = newBRole.RoleId });
        }


        [HttpGet]
        // Get: 'add role' form
        public ActionResult AddRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UpdateRole(int id, BRole bRole)
        {
            try
            {
                string apiUrl = string.Format("{0}/{1}", roleApiUrl, id);
                var content = new StringContent(JsonConvert.SerializeObject(bRole), Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var data = response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("GetRole", new { id });
        }


        public async Task<ActionResult> EditRole(int id)
        {
            BRole bRole = null;
            try
            {
                string apiUrl = string.Format("{0}/{1}", roleApiUrl, id);
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bRole = JsonConvert.DeserializeObject<BRole>(data);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return View(bRole);
        }

    }
}