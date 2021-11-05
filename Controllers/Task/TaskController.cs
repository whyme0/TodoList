using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace TodoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public TaskController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [NonAction]
        private async Task<User> GetCurrentUser() => await _userManager.FindByNameAsync(User.Identity.Name);

        // GET: Task
        [Route("/task/list")]
        [Authorize]
        public async Task<IActionResult> List()
        {
            var user = await GetCurrentUser();
            var userTasks = await _context.Tasks.Where(t => t.User == user).OrderByDescending(t => t.CreationDate).ToListAsync();
            List<TaskModel> activeTasks = new List<TaskModel>();
            List<TaskModel> inactiveTasks = new List<TaskModel>();
            foreach (TaskModel task in userTasks)
            {
                if(task.IsActive)
                {
                    activeTasks.Add(task);
                }
                else
                {
                    inactiveTasks.Add(task);
                }

            }
            ViewData["ActiveTasks"] = activeTasks;
            ViewData["InactiveTasks"] = inactiveTasks;
            return View(userTasks);
        }

        // GET: Task/Create
        [HttpGet]
        [Authorize]
        [Route("/task/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [Route("/task/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShortDescription,DetailedDescription,CreationDate,CompletionDate")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                taskModel.User = await GetCurrentUser();
                taskModel.UserId = taskModel.User.Id;
                
                _context.Tasks.Add(taskModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(taskModel);
        }

        // GET: Task/Edit/5
        [Authorize]
        [Route("/task/edit/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskModel = await _context.Tasks.FindAsync(id);
            var currentUser = await GetCurrentUser();
            if (!taskModel.IsActive || taskModel.User != currentUser)
            {
                return BadRequest();
            }
            
            if (taskModel == null)
            {
                return NotFound();
            }
            return View(taskModel);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Route("/task/edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, UserId, ShortDescription, DetailedDescription,CreationDate,CompletionDate")] TaskModel taskModel)
        {
            if (id != taskModel.Id)
            {
                return NotFound();
            }

            var currentUser = await GetCurrentUser();
            
            if (!taskModel.IsActive || taskModel.UserId != currentUser.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(taskModel).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskModelExists(taskModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(taskModel);
        }

        // GET: Task/Delete/5
        [Authorize]
        [Route("/task/delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var taskModel = await _context.Tasks.FindAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            var currentUser = await GetCurrentUser();
            if (taskModel.User != currentUser)
            {
                return BadRequest();
            }

            return View(taskModel);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("/task/delete/{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskModel = await _context.Tasks.FindAsync(id);
            
            if (taskModel == null)
            {
                return NotFound();
            }
            
            var currentUser = await GetCurrentUser();
            if (taskModel.User != currentUser)
            {
                return BadRequest();
            }
            
            _context.Tasks.Remove(taskModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool TaskModelExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }

        [HttpGet]
        [Authorize]
        [Route("/task/make-done/{id:int}")]
        public async Task<IActionResult> MakeTaskDone(int? id)
        {

            var taskModel = await _context.Tasks.FindAsync(id);
            
            if (taskModel == null)
            {
                return NotFound();
            }
            
            var user = await GetCurrentUser();
            
            if (taskModel.User != user)
            {
                return BadRequest();
            }
            
            if (taskModel.IsActive)
            {
                taskModel.IsDone = true;
                _context.Update(taskModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
