using CornerShopBooksWeb.Data;
using CornerShopBooksWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CornerShopBooksWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        //Action to get category list
        public IActionResult Index()
        {
            IEnumerable<CategoryModel> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        
        // GET
        public IActionResult Create()
        {
            return View();
        }
        
        // Create POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel obj)
        {
            if(obj.Name.Length <= 2)
            {
                ModelState.AddModelError("Name", "Name is too short");
            }

            if (!ModelState.IsValid) return View(obj);
            
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully!";
            return RedirectToAction("Index");
        }
        
        // Action to display the category that we want to edit
        // In order to send the data over to the view, we use
        // C# to find the category that has been clicked
        public IActionResult Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            
            return View(categoryFromDb);
        }
        
        // Edit POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel obj)
        {
            if(obj.Name.Length <= 2)
            {
                ModelState.AddModelError("Name", "Name is too short");
            }

            if (!ModelState.IsValid) return View(obj);
            
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category edit successfully!";
            return RedirectToAction("Index");
        }
        
        // Action to display the category that we want to delete
        // In order to send the data over to the view, we use
        // C# to find the category that has been clicked
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            
            return View(categoryFromDb);
        }
        
        // Edit POST Action
        [HttpPost,ActionName("Delete")]
        // This prevent cross-site request forgery
        // https://docs.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-6.0#:~:text=ValidateAntiForgeryToken%20is%20an%20action%20filter,includes%20a%20valid%20antiforgery%20token.
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var idFromObjFromForm = _db.Categories.Find(id);
            if (idFromObjFromForm == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(idFromObjFromForm);
            _db.SaveChanges();
            TempData["success"] = "Category delete successfully!";
            return RedirectToAction("Index");
        }
        
    }
}
