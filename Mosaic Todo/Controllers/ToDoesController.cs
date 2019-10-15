using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mosaic_Todo.Models;

namespace Mosaic_Todo.Controllers
{
    public class ToDoesController : Controller
    {
        private ToDoDbContext db = new ToDoDbContext();

        private IToDoRepositoryInterface _toDoRepo = new ToDoRepository();

        public ActionResult Index()
        {
           
            this.Session["ToDoes"] = _toDoRepo.GetAll();
            ViewBag.ToDoes = _toDoRepo.GetAll();
            return View(db.ToDoes.ToList());
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Completed")] ToDo toDo)
        {

            _toDoRepo.Add(toDo);
            ViewBag.ToDoes = _toDoRepo.GetAll();
            //return RedirectToAction("Index");

            if (ModelState.IsValid)
            {

                db.ToDoes.Add(toDo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();

        }
        
        // POST: ToDoes/Complete/5
        [HttpPost, ActionName("Complete")]
        [ValidateAntiForgeryToken]
        public ActionResult Complete(int id, bool completed)
        {
            ToDo toDo = db.ToDoes.Find(id);

            if (ModelState.IsValid)
            {
                toDo.Completed = completed;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ToDo toDo = db.ToDoes.Find(id);
            db.ToDoes.Remove(toDo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
