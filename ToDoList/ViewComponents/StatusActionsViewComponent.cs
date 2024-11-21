using Microsoft.AspNetCore.Mvc;
using ToDoList.Models; 

namespace ToDoList.ViewComponents
{
    public class TaskStatusActionsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ToDo task)
        {
            return View(task);
        }
    }
}
