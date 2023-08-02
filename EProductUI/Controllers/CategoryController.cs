using EProductUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;

namespace EProductUI.Controllers
{
    [Route("[Controller]/[Action]")]
    public class CategoryController : Controller
    {
        //HttpClient client;
        //string baseAddress;
        ////public CategoryController(IConfiguration con)
        //{
        //    baseAddress=con["apiAddress"];
        //   this.client = new HttpClient();
        //    this.client.BaseAddress=new Uri("https://localhost:7139/api/");


        //}
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // [ActionName("Create")]
        public IActionResult Create(CategoryModel cat)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress=new Uri("https://localhost:7139/api/");
            try
            {

                string catdata = JsonSerializer.Serialize(cat);
                StringContent data = new StringContent(catdata, System.Text.Encoding.UTF8, "application/json");
                //           string result = client.PostAsync(baseAddress + $"CreateCategory",data).Result;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + $"Category", data).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"]="Category Created Successfully!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
                TempData["ErrorMessage"]=ex.Message;
            }
            return View();

        }

        public IActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress=new Uri("https://localhost:7139/api/");
            try
            {
                string catdata = JsonSerializer.Serialize(id);
                StringContent data = new StringContent(catdata, System.Text.Encoding.UTF8, "application/json");

                var options = new JsonSerializerOptions();
                options.PropertyNamingPolicy =JsonNamingPolicy.CamelCase;

                HttpResponseMessage res = client.PostAsync(client.BaseAddress +$"Category", data).Result;
            }
            catch (Exception ex) { }
            return View();
        }
        [HttpGet]
            //[ActionName("Index")]
            public IActionResult Index()
            {
                HttpClient client = new HttpClient();
                client.BaseAddress=new Uri("https://localhost:7139/api/");

                string result = client.GetStringAsync(client.BaseAddress +$"Category").Result;

                var options = new JsonSerializerOptions();
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                List<CategoryModel> categories = JsonSerializer.Deserialize<List<CategoryModel>>(result, options);

                return View(categories);
            }

            [HttpGet]
            [ActionName("Details")]
            public IActionResult Details(int? id)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress=new Uri("https://localhost:7139/api/");

                var options = new JsonSerializerOptions();
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                string result = client.GetStringAsync(client.BaseAddress+$"Category/{id}").Result;
                CategoryModel cat = JsonSerializer.Deserialize<CategoryModel>(result, options);
                return View(cat);
            }
          


    }
}

