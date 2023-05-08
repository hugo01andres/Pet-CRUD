using listademascotas.DAL;
using listademascotas.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace listademascotas.Pages
{
    public class PetEditModel : PageModel
    {
        public readonly WpmDBContext _dbContext;
        [BindProperty]
        public Pet Pet { get; set; }
        public SelectList Breeds { get; set; }
        public PetEditModel(WpmDBContext dbContext)
        {
            _dbContext = dbContext;
            var breeds = dbContext
                .Breeds
                .Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();
            Breeds = new SelectList(breeds, "Value", "Text");
        }
        public void OnGet(int id)
        {
            Pet = _dbContext.Pets
                .Where(p => p.Id == id)
                .First();
        }
        public IActionResult OnPost()
        {
            _dbContext.Update(Pet);
            _dbContext.SaveChanges();
            return RedirectToPage("Pets");
        }

    }
}
