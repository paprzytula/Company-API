using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company_API.Data;
using Company_API.Data.Entities;

namespace Company_API.Controllers
{
    public class EmployeeProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeProjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeProject.Include(e => e.Employee).Include(e => e.Project);
            return (IActionResult)await applicationDbContext.ToListAsync();
        }

        // GET: EmployeeProjects/Details/5
        public async Task<IActionResult> Details(Guid emp, Guid proj)
        {

                return (IActionResult)await  _context.EmployeeProject
                .Where(e => e.IdEmployee == emp)
                .Where(p => p.IdProject == proj)
                .ToListAsync();
           }

        // GET: EmployeeProjects/Create
        public IActionResult Create()
        {
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "FirstName");
            ViewData["IdProject"] = new SelectList(_context.Projects, "IdProject", "Name");
            return View();
        }

        // POST: EmployeeProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProject,IdEmployee")] EmployeeProject employeeProject)
        {
            if (ModelState.IsValid)
            {
                employeeProject.IdEmployee = Guid.NewGuid();
                _context.Add(employeeProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "FirstName", employeeProject.IdEmployee);
            ViewData["IdProject"] = new SelectList(_context.Projects, "IdProject", "Name", employeeProject.IdProject);
            return View(employeeProject);
        }

        // GET: EmployeeProjects/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeProject = await _context.EmployeeProject.FindAsync(id);
            if (employeeProject == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "FirstName", employeeProject.IdEmployee);
            ViewData["IdProject"] = new SelectList(_context.Projects, "IdProject", "Name", employeeProject.IdProject);
            return View(employeeProject);
        }

        // POST: EmployeeProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdProject,IdEmployee")] EmployeeProject employeeProject)
        {
            if (id != employeeProject.IdEmployee)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeProjectExists(employeeProject.IdEmployee))
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
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "FirstName", employeeProject.IdEmployee);
            ViewData["IdProject"] = new SelectList(_context.Projects, "IdProject", "Name", employeeProject.IdProject);
            return View(employeeProject);
        }

        // GET: EmployeeProjects/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeProject = await _context.EmployeeProject
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.IdEmployee == id);
            if (employeeProject == null)
            {
                return NotFound();
            }

            return View(employeeProject);
        }

        // POST: EmployeeProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employeeProject = await _context.EmployeeProject.FindAsync(id);
            _context.EmployeeProject.Remove(employeeProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeProjectExists(Guid id)
        {
            return _context.EmployeeProject.Any(e => e.IdEmployee == id);
        }
    }
}
