using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        AppDbContext _db;
       public CategoryController(AppDbContext db)
        {
            _db = db;   
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel cat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category cat1 = new Category()
                    {
                        Name = cat.Name,
                        IsActive=cat.IsActive
                    };
                    _db.Categories.Add(cat1);
                    _db.SaveChanges();

                    return Created("Create", cat1);
                }
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        ex.Message);
                }
            }

            return BadRequest("Please Check input data");
        }
        [HttpPut]
        public IActionResult Edit(int id,CategoryModel cat)
        {
            if(id!= cat.Id) {
             return BadRequest("Please Enter Correct  Category Id!!"); }
            if (ModelState.IsValid)
            {
                try
                {
                 //   var a = cat.Id;
                 //var b= _db.Categories.Find(cat.Id);//;.FirstOrDefault();

                    Category cat1 = new Category() {
                    Id= cat.Id,
                    Name=cat.Name,

                    IsActive=cat.IsActive};

                 
                        _db.Categories.Update(cat1);
                        _db.SaveChanges();
                  
                    return Ok(cat1);

                }
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        ex.Message);
                }

            }
            return BadRequest("Please Check Input Data!!");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if(id<=0 && id==null) { return BadRequest("Please Check Input ID!!"); }
            Category cat = _db.Categories.Find(id);
            if (cat != null)
            {
                try
                {
                    _db.Categories.Remove(cat);
                    _db.SaveChanges();

                    return Ok(cat);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        ex.Message);
                }
            }
            return BadRequest("Category is Not Existed..");
        }
        [HttpGet(Name ="CategoryList")]
        public IActionResult CategoryList()
        {
            List<Category> MyCategoryList= new List<Category>();
            MyCategoryList=_db.Categories.ToList(); 

            return Ok(MyCategoryList);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {

            if (id <= 0)
                return BadRequest("Please correct category id");

            Category cat = _db.Categories.Find(id);

            if (cat != null)
            {
                return Ok(cat);
            }

            return NotFound($"{id} category does not exists");
        }
    }
}
