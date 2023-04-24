using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Recipe_Website.Data;
using Online_Recipe_Website.Models;

namespace Online_Recipe_Website.Controllers
{
    public class MainController : Controller
    {
        private readonly AppDbContext db;
        public MainController(AppDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> MyRecipes()
        {

            string userEmail = User.Identity?.Name;
            return View((await db.Recipes.Where(x => x.email == userEmail).ToListAsync()));
        }

        // GET: Main
        public async Task<IActionResult> Index()
        {
            return db.Recipes != null ? 
                          View(await db.Recipes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Recipes'  is null.");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection collection)
        {
            string formname = collection["form_name"];
            Console.WriteLine("formname :" + formname);
            if (formname.Equals("search_form"))
            {
                string keyword = collection["search_by_recipe_name"];
                return View(await db.Recipes.Where(x => x.title.Contains(keyword)).ToListAsync());
            }
            else if (formname.Equals("cusine_search_form"))
            {
                string cusine = collection["cusine"];
                return View(await db.Recipes.Where(x => x.cuisine==cusine).ToListAsync());
            }
            else if (formname.Equals("category_search_form"))
            {
                string category = collection["category"];
                return View(await db.Recipes.Where(x => x.category == category).ToListAsync());
            }
            else if (formname.Equals("difficulty_search_form"))
            {
                string difficulty = collection["difficulty"];
                return View(await db.Recipes.Where(x => x.difficulty_level == difficulty).ToListAsync());
            }
            else
            {
                return db.Recipes != null ?
                          View(await db.Recipes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Recipes'  is null.");
            }
        }

        // GET: Main/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Recipes == null)
            {
                return NotFound();
            }

            var recipeModel = await db.Recipes
                .FirstOrDefaultAsync(m => m.recipe_id == id);
            if (recipeModel == null)
            {
                return NotFound();
            }

            return View(recipeModel);
        }

        // GET: Main/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Main/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recipe_id,email,title,image_url,description,ingredients,instruction,category,difficulty_level,cuisine,cooking_time")] RecipeModel recipeModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(recipeModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipeModel);
        }

        // GET: Main/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Recipes == null)
            {
                return NotFound();
            }

            var recipeModel = await db.Recipes.FindAsync(id);
            if (recipeModel == null)
            {
                return NotFound();
            }
            return View(recipeModel);
        }

        // POST: Main/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recipe_id,email,title,image_url,description,ingredients,instruction,category,difficulty_level,cuisine,cooking_time")] RecipeModel recipeModel)
        {
            if (id != recipeModel.recipe_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(recipeModel);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeModelExists(recipeModel.recipe_id))
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
            return View(recipeModel);
        }

        // GET: Main/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Recipes == null)
            {
                return NotFound();
            }

            var recipeModel = await db.Recipes
                .FirstOrDefaultAsync(m => m.recipe_id == id);
            if (recipeModel == null)
            {
                return NotFound();
            }

            return View(recipeModel);
        }

        // POST: Main/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Recipes == null)
            {
                return Problem("Entity set 'AppDbContext.Recipes'  is null.");
            }
            var recipeModel = await db.Recipes.FindAsync(id);
            if (recipeModel != null)
            {
                db.Recipes.Remove(recipeModel);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeModelExists(int id)
        {
          return (db.Recipes?.Any(e => e.recipe_id == id)).GetValueOrDefault();
        }
    }
}
