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

namespace Train2.GroupsMVC.Controllers
{
    public class GroupController : Controller
    {
        readonly HttpClient client = new HttpClient();
        readonly string groupApiUrl = Constants.apiUrl + "/group";


        // GET: Group
        public async Task<ActionResult> Index()
        {
            IList<BGroup> bGroups = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(groupApiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bGroups = JsonConvert.DeserializeObject<List<BGroup>>(data);
                ViewBag.error = false;
            }
            catch (HttpRequestException)
            {
                ViewBag.error = true;
            }
            return View(bGroups);
        }


        // Get: Group by id
        public async Task<ActionResult> GetGroup(int id)
        {
            BGroup bGroup = null;

            try
            {
                string apiUrl = string.Format("{0}/{1}", groupApiUrl, id);
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bGroup = JsonConvert.DeserializeObject<BGroup>(data);
                ViewBag.error = false;
            }
            catch (Exception)
            {
                ViewBag.error = true;
            }
            return View(bGroup);
        }


        // Post: Add a new group
        public async Task<ActionResult> PostGroup(BGroup bGroup)
        {
            BGroup newBGroup = null;
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(bGroup), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(groupApiUrl, content);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                newBGroup = JsonConvert.DeserializeObject<BGroup>(data);
            }
            catch (Exception e)
            {
                ViewBag.error = true;
                return Content(e.Message);
            }
            return RedirectToAction("GetGroup", new { id = newBGroup.GroupId });
        }


        [HttpGet]
        // Get add group form
        public ActionResult AddGroup()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> UpdateGroup(int id, BGroup bGroup)
        {
            try
            {
                string apiUrl = string.Format("{0}/{1}", groupApiUrl, id);
                var content = new StringContent(JsonConvert.SerializeObject(bGroup), Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                var data = response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("GetGroup", new { id });
        }


        public async Task<ActionResult> EditGroup(int id)
        {
            BGroup bGroup = null;
            try
            {
                string apiUrl = string.Format("{0}/{1}", groupApiUrl, id);
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                bGroup = JsonConvert.DeserializeObject<BGroup>(data);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return View(bGroup);
        }
    }
}