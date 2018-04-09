using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app_test_jmeter.Data;
using app_test_jmeter.Models;
using Microsoft.AspNetCore.Authorization;
using app_test_jmeter.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace app_test_jmeter.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly AdmContext _context;
        private readonly IMapper _mapper;

        private string filePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar");

        public UserController(AdmContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            ViewBag.filePath = Path.Combine("images", "avatar");
            return View(await _context.AdmUsers.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.AdmUsers
                .SingleOrDefaultAsync(m => m.Id == id);

            user.Password = new String('*', user.Password.Length);
            
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.filePath = Path.Combine("images", "avatar");

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Username,Password,PerfilImage")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userViewModel);

                if(await saveFileAsync(userViewModel.PerfilImage)){
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                    ModelState.AddModelError("PerfilImage", "Arquivo não salvo");

            }
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Download(string PerfilImage)
        {
            if (PerfilImage == null)
                return Content("Filename not present");

            var path = Path.Combine(filePath, PerfilImage);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        private bool UserExists(int id)
        {
            return _context.AdmUsers.Any(e => e.Id == id);
        }

        private async Task<bool> saveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var path = Path.Combine(filePath,
                                    file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);

                return true;
            }
        }
    }
}
