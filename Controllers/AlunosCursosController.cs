using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using entity_framework_lab_4.DAL;
using entity_framework_lab_4.Models;

namespace entity_framework_lab_4.Controllers
{
    public class AlunosCursosController : Controller
    {
        private readonly Contexto _context;

        public AlunosCursosController(Contexto context)
        {
            _context = context;
        }

        // GET: AlunosCursos
        public async Task<IActionResult> Index()
        {
            var contexto = _context.AlunosCursos.Include(a => a.Aluno).Include(a => a.Curso);
            return View(await contexto.ToListAsync());
        }

        // GET: AlunosCursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunosCursos = await _context.AlunosCursos
                .Include(a => a.Aluno)
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(m => m.AlunosCursosId == id);
            if (alunosCursos == null)
            {
                return NotFound();
            }

            return View(alunosCursos);
        }

        // GET: AlunosCursos/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "Nome");
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "Nome");
            return View();
        }

        // POST: AlunosCursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunosCursosId,AlunoId,CursoId")] AlunosCursos alunosCursos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alunosCursos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId", alunosCursos.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", alunosCursos.CursoId);
            return View(alunosCursos);
        }

        // GET: AlunosCursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunosCursos = await _context.AlunosCursos.FindAsync(id);
            if (alunosCursos == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "Nome", alunosCursos.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "Nome", alunosCursos.CursoId);
            return View(alunosCursos);
        }

        // POST: AlunosCursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlunosCursosId,AlunoId,CursoId")] AlunosCursos alunosCursos)
        {
            if (id != alunosCursos.AlunosCursosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alunosCursos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunosCursosExists(alunosCursos.AlunosCursosId))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId", alunosCursos.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", alunosCursos.CursoId);
            return View(alunosCursos);
        }

        // GET: AlunosCursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunosCursos = await _context.AlunosCursos
                .Include(a => a.Aluno)
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(m => m.AlunosCursosId == id);
            if (alunosCursos == null)
            {
                return NotFound();
            }

            return View(alunosCursos);
        }

        // POST: AlunosCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alunosCursos = await _context.AlunosCursos.FindAsync(id);
            if (alunosCursos != null)
            {
                _context.AlunosCursos.Remove(alunosCursos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunosCursosExists(int id)
        {
            return _context.AlunosCursos.Any(e => e.AlunosCursosId == id);
        }
    }
}
