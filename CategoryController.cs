using NimapTask.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NimapTask.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();

        public ActionResult CategoryIndex(int? i)
        {
            return View(categoryRepository.ShowAllCategory().ToPagedList(i ?? 1, 5));
        }

        public ActionResult CategoryDetails(int? id)
        {
            DataSet ds = categoryRepository.GetCategory(id.Value);
            CategoryMaster categ = new CategoryMaster();
            categ.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryID"].ToString());
            categ.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            return View(categ);
        }

        [HttpGet]
        public ActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CategoryCreate(CategoryMaster categoryMaster)
        {
            //categoryMaster = new CategoryMaster();
            if (ModelState.IsValid)
            {
                categoryRepository.CreateCategory(categoryMaster.CategoryName);
                return RedirectToAction("CategoryIndex");
            }
            return View(categoryMaster);
        }
        public ActionResult CategoryEdit(int? id)
        {
            DataSet ds = categoryRepository.GetCategory(id.Value);
            CategoryMaster categ = new CategoryMaster();
            categ.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryID"].ToString());
            categ.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            return View(categ);
        }
        [HttpPost]
        public ActionResult CategoryEdit(CategoryMaster categoryMaster)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.UpdateCategory(categoryMaster.CategoryID, categoryMaster.CategoryName);
                return RedirectToAction("CategoryIndex");
            }
            return View(categoryMaster);
        }

        public ActionResult CategoryDelete(int? id)
        {
            DataSet ds = categoryRepository.GetCategory(id.Value);
            CategoryMaster categ = new CategoryMaster();
            categ.CategoryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryID"].ToString());
            categ.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            return View(categ);
        }
        [HttpPost]
        public ActionResult CategoryDelete(int id)
        {
            CategoryMaster categoryMaster = new CategoryMaster();
            if (ModelState.IsValid)
            {
                categoryRepository.DeleteCategory(id);
                return RedirectToAction("CategoryIndex");
            }
            return View(categoryMaster);
        }
    }
}