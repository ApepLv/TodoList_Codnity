using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList_Codnity.Models;
using TodoList_Codnity.Services;

namespace TodoList_Codnity.Controllers
{
    public class TodoListController : Controller
    {
        private readonly IEntityService<TodoList> _todoListService;

        public TodoListController(IEntityService<TodoList> todoListService)
        {
            _todoListService = todoListService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _todoListService.GetAllItemsAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var todoList = await _todoListService.GetItemByIdAsync(id.Value);
            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoList todoList)
        {
            if (ModelState.IsValid)
            {
                await _todoListService.AddItemAsync(todoList);
                return RedirectToAction(nameof(Index));
            }

            return View(todoList);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoList = await _todoListService.GetItemByIdAsync(id.Value);
            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TodoList todoList)
        {
            if (id != todoList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _todoListService.UpdateItemAsync(todoList);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _todoListService.ExistsAsync(todoList.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(todoList);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoList = await _todoListService.GetItemByIdAsync(id.Value);
            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoList = await _todoListService.GetItemByIdAsync(id);
            if (todoList != null)
            {
                await _todoListService.DeleteItemAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}