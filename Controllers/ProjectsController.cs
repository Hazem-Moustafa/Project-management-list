﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Models;
using ProjectManagement.Models.ViewModels;

namespace ProjectManagement.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectsContext _context;
       // private readonly UserManager<IdentityUser> _userManager;

        public ProjectsController(ProjectsContext context) //UserManager<IdentityUser> userManager
        {
            _context = context;
         //   _userManager = userManager;

        }

        // To be deleted
        // GET: Projects/Board/5
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Board(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
              .Include(b => b.Boards) // Relation!
              .FirstOrDefaultAsync(m => m.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Index(string title, int page = 1)
        {

            Member member = await _context.Member.FirstOrDefaultAsync(m => m.Username == User.Identity.Name); // getting member
            if (member == null)
            {
                return NotFound();
            }


            var memberProjects = await _context.ProjectMember
             .Where(p => p.MemberId == member.MemberId)
             .Include(p => p.Member) 
             .Include(p => p.Project.Manager) // include manager (many - (to) - many)
             .ToListAsync();

            if (memberProjects.Count == 0) 
            {
                return View("Create");
            }

            var memberProjectsSearched = memberProjects.Where(p => title == null || p.Project.Name.Contains(title)); // search

            return View( new ProjectsListViewModel
            {
                ProjectMember = memberProjectsSearched,
                TitleSearched = title
            });
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            var ProjectMembers = await _context.ProjectMember
                .Where(x => x.Project == project)
                .Include(u => u.Member)
                .Include(u => u.Project.Manager)
                .ToListAsync();

            ProjectProjectMembers ProjectProjectMembers = new ProjectProjectMembers
            {
                Project = project,
                ProjectMembers = ProjectMembers
            };

            return View(ProjectProjectMembers);
        }

        // GET: Projects/Create
        [Authorize(Roles = "member")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Create([Bind("ProjectId,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                //var user = await _userManager.GetUserAsync(User);
                //var userId = user?.Id;

                // user ekle seeddata da ayrica register oldugunda da member class a baglanmali?
         
                Member member = await _context.Member.FirstOrDefaultAsync(m => m.Username == User.Identity.Name) ;
                if(member == null)
                {
                    return NotFound();
                }

                project.ManagerId = member.MemberId;

                _context.Add(project);

                await _context.SaveChangesAsync(); // save, before to use new id's

                // find new project's id (it is important!)
                Project projectAdded = await _context.Project.FirstOrDefaultAsync(m => m.Name == project.Name);
                if (projectAdded == null)
                {
                    return NotFound();
                }

                _context.Add(new ProjectMember { 
                    MemberId = member.MemberId , 
                    ProjectId = projectAdded.ProjectId // you can't use "project.ProjectId" because id doesnt know (before saving db) that will give error
                });; // add person to many to many


                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Name,ManagerId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Member member = await _context.Member.FirstOrDefaultAsync(m => m.Username == User.Identity.Name);
            if (member == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            if (member.MemberId != project.ManagerId) // Non-Authorized Access 
            {
                return NotFound();
            }

            var ProjectMembers = await _context.ProjectMember
                .Where(x => x.Project == project)
                .Include(u => u.Member)
                .Include(u => u.Project.Manager)
                .ToListAsync();

            ProjectProjectMembers ProjectProjectMembers = new ProjectProjectMembers
            {
                Project = project,
                ProjectMembers = ProjectMembers
            };

            return View(ProjectProjectMembers);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "member")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            // Delete Board Foreign Keys
            var Boards = await _context.Board.Where(x => x.ProjectId == id).ToListAsync();
            foreach (Board b in Boards)
            {
                if (b != null)
                {
                    _context.Board.Remove(b);
                }
            }

            // Delete Matched Foreign Key (ProjectId) Rows on ProjectMember Before deleting a project row
            var projectMembers = await _context.ProjectMember.Where(x => x.ProjectId == id).ToListAsync();
            foreach(ProjectMember pm in projectMembers)
            {
                if(pm != null)
                {
                    _context.ProjectMember.Remove(pm);
                }
            }

            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectId == id);
        }
    }
}
