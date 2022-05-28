using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAppWeb.Data;
using ToDoAppWeb.Models;

namespace ToDoAppWeb.Controllers
{
    public class TaskToDoController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TaskToDoController(ApplicationDbContext db)
        {
            _db = db;
        }

        /*public IActionResult Index()
        {
            IEnumerable<TaskToDo> objTaskToDoList = _db.TasksToDo.ToList();
            //IEnumerable<Priority> objPriorityList = _db.Priority.ToList();
            return View(objTaskToDoList);
        }*/
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var tasksToDo = from s in _db.TasksToDo
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                tasksToDo = tasksToDo.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    tasksToDo = tasksToDo.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    tasksToDo = tasksToDo.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    tasksToDo = tasksToDo.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    tasksToDo = tasksToDo.OrderBy(s => s.Title);
                    break;
            }
            return View(await tasksToDo.AsNoTracking().ToListAsync());
        }
        //GET
        public IActionResult Add()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TaskToDo obj)
        {
            if (ModelState.IsValid)
            {
                _db.TasksToDo.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var taskToDoFromDb = _db.TasksToDo.Find(id);
            if (taskToDoFromDb == null)
            {
                return NotFound();
            }

            return View(taskToDoFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskToDo obj)
        {
            if (ModelState.IsValid)
            {
                _db.TasksToDo.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var taskToDoFromDb = _db.TasksToDo.Find(id);
            if (taskToDoFromDb == null)
            {
                return NotFound();
            }

            return View(taskToDoFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TaskToDo obj)
        {
            _db.TasksToDo.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
