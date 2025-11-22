using Microsoft.AspNetCore.Mvc;
using ASPcore_WebAPP.Models;
using System.Net.Http;

namespace ASPcore_WebAPP.Controllers
{
    public class InsertController : Controller
    {
        public IActionResult Insert_PageLoad()
        {
            return View();
        }


        // On Button Click - Insert Data
        [HttpPost]
        public ActionResult Index_click(Employee obcls)
        {
            using (var client = new HttpClient())
            {
                                                                               
                client.BaseAddress = new Uri("http://localhost:5117/Test/");   //API Adding To mvc


                // HTTP POST
                var postTask = client.PostAsJsonAsync("posttab", obcls);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    // Redirect after successful insert
                    return RedirectToAction("DisplayAll_PageLoad", "All_Methods");
                }
            }
            return RedirectToAction("DisplayAll_PageLoad", "All_Methods");

        }
    }
}
