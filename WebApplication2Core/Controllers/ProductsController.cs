using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WebApplication2Core.Models;
using WebApplication2Core.ViewModel;

namespace WebApplication2Core.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductReposetory _context;
        private readonly IHostingEnvironment hostingenvironment;

        public ProductsController(IProductReposetory context,
            IHostingEnvironment hostingenvironment)
        {
            _context = context;
            this.hostingenvironment = hostingenvironment;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View("IndexCard", _context.GetProducts());
        }

        // GET: Products/Details/5
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var product = _context.GetProduct(Id ?? 1);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var create = new ProductCreateViewModel();
            return View(create);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,Photopath,IsActive")]
                                     ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqeFileName = FilePath(model);
                var newProduct = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Photopath = uniqeFileName,
                    IsActive = model.IsActive
                };
                _context.create(newProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private string FilePath(ProductCreateViewModel model)
        {
            string uniqeFileName = null;
            if (model.Photopath != null)
            {
                string uploadsFile = Path.Combine(hostingenvironment.WebRootPath, "images");
                uniqeFileName = Guid.NewGuid().ToString() + "_" + model.Photopath.FileName;
                string filePath = Path.Combine(uploadsFile, uniqeFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                    model.Photopath.CopyTo(filestream);
            }

            return uniqeFileName;
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.GetProduct(id ?? 1);
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = new ProductUpdateViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                ExistingPhotoPath = product.Photopath,
                Description = product.Description,
                IsActive = product.IsActive
            };
            return View(viewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Name,Description,Photopath,IsActive")] ProductUpdateViewModel model)
        {


            if (ModelState.IsValid)
            {

                var changeProduct = new Product()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive,

                };
                if (model.Photopath != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filepath = Path.Combine(hostingenvironment.WebRootPath
                            , "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filepath);
                    }
                    changeProduct.Photopath = FilePath(model);
                }

                _context.Update(changeProduct);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var product = _context.GetProduct(Id ?? 1);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
