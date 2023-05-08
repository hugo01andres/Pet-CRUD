using listademascotas.DAL;
using listademascotas.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace listademascotas.Pages
{
    public class PetsModel : PageModel
    {
        private readonly WpmDBContext _dBContext;

        public IEnumerable<Pet> Pets { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public PetsModel(WpmDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public void OnGet()
        {
            Pets = _dBContext.Pets
                .Include(p => p.Breed)
                .ThenInclude(b => b.Species)
                .Where(p => string.IsNullOrWhiteSpace(Search) ? true:
                p.Name.ToLowerInvariant().Contains(Search))
                .ToList(); 
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var petToDelete = await _dBContext.Pets.SingleOrDefaultAsync(p => p.Id == id);

            if (petToDelete == null)
            {
                return NotFound();
            }

            _dBContext.Pets.Remove(petToDelete);
            await _dBContext.SaveChangesAsync();

            return RedirectToPage("./Pets");
        }


    }
}
