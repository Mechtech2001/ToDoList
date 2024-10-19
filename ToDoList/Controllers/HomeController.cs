using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext context;
        public HomeController(ToDoContext context)
        {
            this.context = context;
        }

        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = context.Status.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;

            IQueryable<ToDo> query = context.ToDos
                .Include(t => t.Status);
            if (filters.HasStatue)
            {
                query = query.Where(t => t.StatusID == filters.StatusId);
            }
            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueDate <today);
                }
                else if (filters.isFuture)
                {
                    query = query.Where(t => t.DueDate > today);
                }
                else if (filters.isToday)
                {
                    query = query.Where(t => t.DueDate == today);
                }
            }
            var tasks = query.OrderBy(t => t.DueDate).ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Statuses = context.Status.ToList();
            var task = new ToDo { StatusID = "to do" };
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Statuses = context.Status.ToList();
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new {Id = id});
        }

     
        
        [HttpPost]
        public IActionResult UpdateStatus(int id, string action)
        {
            var selected = context.ToDos.Include(t => t.Status).FirstOrDefault(t => t.Id == id);

            if (selected == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            switch (action)
            {
                case "to do":
                    selected.StatusID = "to do";
                    break;
                case "in progress":
                    selected.StatusID = "in progress";
                    break;
                case "quality assurance":
                    selected.StatusID = "quality assurance";
                    break;
                case "done":
                    selected.StatusID = "done";
                    break;
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            var toDelete = context.ToDos.Where(t => t.StatusID == "done").ToList();
            foreach (var task in toDelete)
            {
                context.ToDos.Remove(task);
            }
            context.SaveChanges();
            return RedirectToAction("Index", new { ID = id });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
