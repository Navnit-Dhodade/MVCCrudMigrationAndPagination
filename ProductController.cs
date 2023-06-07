using NimapTask.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NimapTask.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();

        public ActionResult ProductIndex(int? PageNumber)
        {
            
            var user = productRepository.ShowAllProduct();
            ViewBag.TotalPages = Math.Ceiling(user.Count() / 10.0);
            user = user.Skip(Convert.ToInt32((PageNumber - 1) * 10)).Take(10).ToList();
            //return View(productRepository.ShowAllProduct().ToPagedList(i ?? 1, 5));
            return View(user);


        }

        public ActionResult ProductDetails(int? id)
        {
            DataSet ds = productRepository.GetProduct(id.Value);
            ProductMaster product = new ProductMaster();
            CategoryMaster categ = new CategoryMaster();
            product.ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductID"].ToString());
            product.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
            product.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryID"].ToString());
            product.CategoryMaster = categ;
            product.CategoryMaster.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            return View(product);
        }

        [HttpGet]
        public ActionResult ProductCreate()
        {
            ProductMaster productMaster = new ProductMaster();
            ViewBag.CategoryId = new SelectList(productRepository.FillCategory(), "CategoryID", "CategoryName", productMaster.CategoryID);
            return View();
        }

        [HttpPost]
        public ActionResult ProductCreate(ProductMaster productMaster)
        {
            if (ModelState.IsValid)
            {
                productRepository.CreateProduct(productMaster.ProductId, productMaster.ProductName, productMaster.CategoryID);
                return RedirectToAction("ProductIndex");
            }
            return View(productMaster);
        }
        public ActionResult ProductEdit(int? id)
        {
            ProductMaster productMaster = new ProductMaster();
            ViewBag.CategId = new SelectList(productRepository.FillCategory(), "CategoryID", "CategoryName", productMaster.CategoryID);

            ProductMaster product = new ProductMaster();
            CategoryMaster categ = new CategoryMaster();
            if (id != null)
            {
                DataSet ds = productRepository.GetProduct(id.Value);
                product.ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductID"].ToString());
                product.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                product.CategoryID = int.Parse(ds.Tables[0].Rows[0]["CategoryID"].ToString());
                product.CategoryMaster = categ;
                product.CategoryMaster.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            }

            return View(product);
        }
        [HttpPost]
        public ActionResult ProductEdit(ProductMaster productMaster)
        {
            if (ModelState.IsValid)
            {
                productRepository.UpdateProduct(productMaster.ProductId, productMaster.ProductName, productMaster.CategoryID);
                return RedirectToAction("ProductIndex");
            }
            return View(productMaster);
        }

        public ActionResult ProductDelete(int? id)
        {
            ProductMaster product = new ProductMaster();
            CategoryMaster categ = new CategoryMaster();
            if (id != null)
            {
                DataSet ds = productRepository.GetProduct(id.Value);
                product.ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductId"].ToString());
                product.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                product.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryID"].ToString());
                product.CategoryMaster = categ;
                product.CategoryMaster.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult ProductDelete(int id)
        {
            ProductMaster productMaster = new ProductMaster();
            if (ModelState.IsValid)
            {
                productRepository.DeleteProduct(id);
                return RedirectToAction("ProductIndex");
            }
            return View(productMaster);
        }
    }
}