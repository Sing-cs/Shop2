using MyShop2.Core.Contracts;
using MyShop2.Core.Models;
using MyShop2.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop2.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        IRepository<Product> ProductContext;
        IRepository<ProductCategory> ProductCategoryContext;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this.ProductContext = productContext;
            this.ProductCategoryContext = productCategoryContext;
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = ProductContext.Collection().ToList();
            return View(products);
        }

        // GET: ProductManager/Create
        public ActionResult Create()
        {
            Product p = new Product();
            IEnumerable<ProductCategory> productCategories = ProductCategoryContext.Collection().ToList();

            ProductEditViewModel viewModel = new ProductEditViewModel(p, productCategories);
            return View(viewModel);
        }

        // POST: ProductManager/Create
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase uploadedImage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (uploadedImage != null)
                {
                    product.Image = product.Id + Path.GetExtension(uploadedImage.FileName);
                    uploadedImage.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }


                ProductContext.Insert(product);
                ProductContext.Commit();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductManager/Edit/5
        public ActionResult Edit(string Id)
        {            
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            Product productToEdit = ProductContext.Find(Id);

            if (productToEdit == null)
            {
                return RedirectToAction("Index");
            }

            IEnumerable<ProductCategory> productCategories = ProductCategoryContext.Collection().ToList();
            ProductEditViewModel viewModel = new ProductEditViewModel(productToEdit, productCategories);
            return View(viewModel);
        }

        // POST: ProductManager/Edit/5
        [HttpPost]
        public ActionResult Edit(string Id, Product product, HttpPostedFileBase uploadedImage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                Product productToEdit = ProductContext.Find(Id);

                if (productToEdit != null)
                {
                    productToEdit.Name = product.Name;
                    productToEdit.Description = product.Description;
                    productToEdit.Price = product.Price;
                    productToEdit.ProductCategory = product.ProductCategory;

                    if (uploadedImage != null)
                    {
                        uploadedImage.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                    }
                    ProductContext.Commit();
                }
                return RedirectToAction("Index");

            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductManager/Delete/5
        public ActionResult Delete(string Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }
            Product productToDelete = ProductContext.Find(Id);

            return View(productToDelete);
        }

        // POST: ProductManager/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(String Id)
        {
            Product productToDelete = ProductContext.Find(Id);
            string imagePathToDelete = Server.MapPath("//Content//ProductImages//") + productToDelete.Image;
            try
            {
                ProductContext.Delete(Id);
                if (System.IO.File.Exists(imagePathToDelete))
                {
                    System.IO.File.Delete(imagePathToDelete);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                
                return View(productToDelete);
            }
        }
    }
}
