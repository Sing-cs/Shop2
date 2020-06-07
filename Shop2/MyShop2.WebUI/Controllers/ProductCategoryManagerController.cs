using MyShop2.Core.Contracts;
using MyShop2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop2.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryManagerController : Controller
    {

        IRepository<ProductCategory> ProductCategoryContext;

        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryContext)
        {
            this.ProductCategoryContext = productCategoryContext;
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = ProductCategoryContext.Collection().ToList();
            return View(productCategories);
        }


        // GET: ProductCategoryManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategoryManager/Create
        [HttpPost]
        public ActionResult Create(ProductCategory productCategories)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategories);
                }

                ProductCategoryContext.Insert(productCategories);
                ProductCategoryContext.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCategoryManager/Edit/5
        public ActionResult Edit(string Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            ProductCategory productCategory = ProductCategoryContext.Find(Id);
            return View(productCategory);
        }

        // POST: ProductCategoryManager/Edit/5
        [HttpPost]
        public ActionResult Edit(string Id, ProductCategory productCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }

                ProductCategory productCategoryToEdit = ProductCategoryContext.Find(Id);

                if (productCategoryToEdit != null)
                {
                    productCategoryToEdit.Name = productCategory.Name;
                    ProductCategoryContext.Commit();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCategoryManager/Delete/5
        public ActionResult Delete(string Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }
            ProductCategory productCategoryToDelete = ProductCategoryContext.Find(Id);

            return View(productCategoryToDelete);
        }

        // POST: ProductCategoryManager/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(String Id)
        {
            try
            {
                ProductCategoryContext.Delete(Id);
                return RedirectToAction("Index");
            }
            catch
            {
                ProductCategory productCategoryToDelete = ProductCategoryContext.Find(Id);
                return View(productCategoryToDelete);
            }
        }
    }
}
