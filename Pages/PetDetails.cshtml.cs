using listademascotas.DAL;
using listademascotas.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace listademascotas.Pages
{
    public class PetDetailsModel : PageModel
    {
        private readonly WpmDBContext _dbcontext;
        public Pet Pet { get; set; }
        public PetDetailsModel(WpmDBContext dBContext) 
        {
            _dbcontext = dBContext;
        }
        public void OnGet(int? id)
        {
            var pet = _dbcontext.Pets
                .Where(p => p.Id == id)
                .Include(p => p.Owners)
                .Include(p => p.Breed)
                .ThenInclude(b => b.Species)
                .First();

            Pet = pet;
        }

        
    }
}
