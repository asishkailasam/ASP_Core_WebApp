using Microsoft.AspNetCore.Mvc;
using ASPcore_WebAPP.Models;


namespace ASPcore_WebAPP.Controllers
{


    public class All_MethodsController : Controller
    {

        //----------------------------------------------------Display all details-----------------------------------
        public IActionResult DisplayAll_PageLoad()
        {
            List<Employee> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5117/Test/");    //Api Connect


                var responseTask = client.GetAsync("GetAlltab");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Employee>>();
                    readTask.Wait();

                    employees = readTask.Result;
                }
            }

            return View(employees);



        }



        //----------------------------------Delete----------------------------------

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("http://localhost:5117/Test/");
                var deleteTask = client.DeleteAsync($"DeleteTab/{id}");
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("DisplayAll_PageLoad");
                }
            }
            return RedirectToAction("DisplayAll_PageLoad");
        }

        //-----------------------------------------Edit--------------------------------------------------------------------

        public IActionResult EditGet(int? id)
        {
            Employee emp = null;

            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("http://localhost:5117/Test/");
                var responseTask = client.GetAsync($"UpdateTab/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employee>();
                    readTask.Wait();

                    emp = readTask.Result;
                }
                else
                {
                    emp = new Employee(); // fallback if API fails
                }
            }

            return View(emp);
        }
        [HttpPost]
        public ActionResult EditPost(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = new Uri("http://localhost:5117/Test/");
                    var postTask = client.PutAsJsonAsync<Employee>($"UpdateTab/{empobj.Id}", empobj);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("DisplayAll_PageLoad");
                    }
                }
            }

            return View(empobj);
        }


        //-------------------------------------------Get with ID---------------------------------

        public ActionResult Detailstab(int? id)
        {
            Employee emp = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5117/Test/");
                var responseTask = client.GetAsync($"GetWithIdTab/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employee>();
                    readTask.Wait();

                    emp = readTask.Result;
                }
                else
                {
                    emp = new Employee(); // fallback if API fails
                }
            }

            return View(emp);
        }







    }
}
