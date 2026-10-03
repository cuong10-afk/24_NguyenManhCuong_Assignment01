using _24_NguyenManhCuong_Assignment01_FrontEnd.Models;
using _24_NguyenManhCuong_Assignment01_FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _24_NguyenManhCuong_Assignment01_FrontEnd.Controllers
{
    [Authorize(Roles = "Staff")]
    public class CategoriesController : Controller
    {
        private readonly IApiService _apiService;

        public CategoriesController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _apiService.GetCategoriesAsync();
            return View(categories);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            await PopulateParentsDropDownList();
            return View(new CategoryViewModel { IsActive = true });
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateParentsDropDownList(model.ParentCategoryID);
                return View(model);
            }

            var (success, error) = await _apiService.CreateCategoryAsync(model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, error ?? "Failed to create category.");
                await PopulateParentsDropDownList(model.ParentCategoryID);
                return View(model);
            }

            TempData["SuccessMessage"] = $"Category '{model.CategoryName}' created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(short id)
        {
            var category = await _apiService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = $"Category with ID {id} not found.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateParentsDropDownList(category.ParentCategoryID, id);
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, CategoryViewModel model)
        {
            if (id != model.CategoryID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                await PopulateParentsDropDownList(model.ParentCategoryID, id);
                return View(model);
            }

            var (success, error) = await _apiService.UpdateCategoryAsync(id, model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, error ?? "Failed to update category.");
                await PopulateParentsDropDownList(model.ParentCategoryID, id);
                return View(model);
            }

            TempData["SuccessMessage"] = $"Category '{model.CategoryName}' updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(short id)
        {
            var category = await _apiService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = $"Category with ID {id} not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var (success, error) = await _apiService.DeleteCategoryAsync(id);
            if (!success)
            {
                TempData["ErrorMessage"] = error ?? "Cannot delete this category because it has associated news articles.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Category deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateParentsDropDownList(short? selectedId = null, short? excludeId = null)
        {
            var all = await _apiService.GetCategoriesAsync();
            var list = excludeId.HasValue ? all.Where(c => c.CategoryID != excludeId.Value) : all;
            ViewBag.ParentCategoryID = new SelectList(list, "CategoryID", "CategoryName", selectedId);
        }
    }
}
