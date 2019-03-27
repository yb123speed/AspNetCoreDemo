using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResolveConcurrencyCconflictsWithEFCore.Models;

namespace ResolveConcurrencyCconflictsWithEFCore.Controllers
{
    public class PostsController : Controller
    {
        private readonly MyDbContext _context;

        public PostsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,PublishTime,RowVersion")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,PublishTime")] Post post,byte[] rowVersion)
        //{
        //    if (id != post.Id)
        //    {
        //        return NotFound();
        //    }
        //    _context.Entry(post).Property("RowVersion").OriginalValue = rowVersion;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(post);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException ex)
        //        {
        //            //if (!PostExists(post.Id))
        //            //{
        //            //    return NotFound();
        //            //}

        //            var exceptionEntry = ex.Entries.Single();
        //            var clientValues = (Post)exceptionEntry.Entity;
        //            var databaseEntry = exceptionEntry.GetDatabaseValues();
        //            if (databaseEntry == null)
        //            {
        //                ModelState.AddModelError(string.Empty,
        //                    "Unable to save changes. The post was deleted by another user.");
        //            }
        //            else
        //            {
        //                var databaseValues = (Post)databaseEntry.ToObject();

        //                if (databaseValues.Title != clientValues.Title)
        //                {
        //                    ModelState.AddModelError("Title", $"Current value: {databaseValues.Title}");
        //                }
        //                if (databaseValues.Content != clientValues.Content)
        //                {
        //                    ModelState.AddModelError("Content", $"Current value: {databaseValues.Content}");
        //                }
        //                if (databaseValues.PublishTime != clientValues.PublishTime)
        //                {
        //                    ModelState.AddModelError("PublishTime", $"Current value: {databaseValues.PublishTime:d}");
        //                }

        //                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
        //                        + "was modified by another user after you got the original value. The "
        //                        + "edit operation was canceled and the current values in the database "
        //                        + "have been displayed. If you still want to edit this record, click "
        //                        + "the Save button again. Otherwise click the Back to List hyperlink.");
        //                post.RowVersion = (byte[])databaseValues.RowVersion;
        //                ModelState.Remove("RowVersion");
        //            }
        //            return View(post);

        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(post);
        //}

        // GET: Posts/Delete/5


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                Post deletedPost = new Post();
                await TryUpdateModelAsync(deletedPost);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The post was deleted by another user.");
                return View(deletedPost);
            }

            _context.Entry(post).Property("RowVersion").OriginalValue = rowVersion;
            if (await TryUpdateModelAsync<Post>(
                post,
                "",
                s => s.Title, s => s.Content, s => s.PublishTime))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    //if (!PostExists(post.Id))
                    //{
                    //    return NotFound();
                    //}

                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Post)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The post was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Post)databaseEntry.ToObject();

                        if (databaseValues.Title != clientValues.Title)
                        {
                            ModelState.AddModelError("Title", $"Current value: {databaseValues.Title}");
                        }
                        if (databaseValues.Content != clientValues.Content)
                        {
                            ModelState.AddModelError("Content", $"Current value: {databaseValues.Content}");
                        }
                        if (databaseValues.PublishTime != clientValues.PublishTime)
                        {
                            ModelState.AddModelError("PublishTime", $"Current value: {databaseValues.PublishTime:d}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        post.RowVersion = databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
                return View(post);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool PostExists(int id)
        //{
        //    return _context.Posts.Any(e => e.Id == id);
        //}
    }
}
