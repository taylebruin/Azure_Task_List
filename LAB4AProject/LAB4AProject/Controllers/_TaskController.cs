using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB4AProject.Data;
using LAB4AProject.Models;
using Microsoft.AspNetCore.Authorization;
using LAB4AProject.Data.Dao;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LAB4AProject.Controllers
{
    [Authorize]
    public class _TaskController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ITaskDao _dao;

        public _TaskController(IAtlasSettings settings)
        {
            _dao = new TaskDao(settings);
        }

        // GET: _Task
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserID"] = userId;
            var userTasks = await _dao.Read(userId);
            ViewData["CurrentTasks"] = userTasks;
            return View();
        }

        // POST: _Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Text,Date")] _Task _Task)
        {
            if (ModelState.IsValid)
            {
                await _dao.Create(_Task);
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        // POST: _Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserID,Text,Done,Date")] _Task _Task)
        {
            if (id != _Task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Task.Done = !_Task.Done;
                    await _dao.Update(_Task);
                   
                }
                catch (DbUpdateConcurrencyException)
                {               
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_Task);
        }

        // POST: _Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
           await _dao.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
