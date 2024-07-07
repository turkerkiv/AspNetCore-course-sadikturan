using FormsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string searchString, string category)
        {
            var products = Repository.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
                products = products.Where(prd => prd.Name!.ToLower().Contains(searchString.ToLower())).ToList();
            }
            if (category != "0" && !string.IsNullOrEmpty(category))
            {
                products = products.Where(prd => prd.CategoryId == int.Parse(category)).ToList();
            }

            var productViewModel = new ProductViewModel
            {
                Products = products,
                Categories = Repository.Categories,
                SelectedCategory = category,
            };

            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            string extension = string.Empty;

            if (imageFile != null)
            {
                extension = Path.GetExtension(imageFile.FileName);
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", $"Allowed image extensions: {string.Join(" ", allowedExtensions)}");
                }
            }

            if (!ModelState.IsValid) return View(product);

            string randomFileName = string.Format($"{Guid.NewGuid()}{extension}");
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
            product.ImageName = randomFileName;
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile!.CopyToAsync(stream);
            }

            Repository.AddProduct(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            Product? product = Repository.GetProductById(id.Value);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Product product, IFormFile? imageFile)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            string extension = string.Empty;

            if (imageFile != null)
            {
                extension = Path.GetExtension(imageFile.FileName);
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", $"Allowed image extensions: {string.Join(" ", allowedExtensions)}");
                }
            }

            if (!ModelState.IsValid) return View(product);

            //if imagefile is null then imageName stays same if not null then change name and copy image
            if (imageFile != null)
            {
                string randomFileName = string.Format($"{Guid.NewGuid()}{extension}");
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                product.ImageName = randomFileName;
                using var stream = new FileStream(path, FileMode.Create);
                await imageFile!.CopyToAsync(stream);
            }

            Repository.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id == null) return NotFound();
            Product? pr = Repository.GetProductById(id.Value);
            if(pr == null) return NotFound();
            return View("DeleteConfirmation", pr);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Repository.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditProducts(List<Product> products)
        {
            foreach(var pr in products)
            {
                Repository.UpdateProduct(pr);
            }

            return RedirectToAction("Index");
        }
    }
}