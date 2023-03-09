using EntidadesPro;
using LogNeg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentacion.Pages
{
    public class IndexModel : PageModel
    {
        public bool created = false;
        public ServicioLogicaN LogN = new ServicioLogicaN();
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public Usuario persona { get; set; } = new Usuario();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            Console.WriteLine(persona);
            LogN.Create(persona);
            created= true;
            return Page();
        }
    }
}