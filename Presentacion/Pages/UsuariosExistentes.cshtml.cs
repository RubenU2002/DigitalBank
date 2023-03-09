using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LogNeg;
using EntidadesPro;
using Microsoft.VisualBasic.FileIO;

namespace Presentacion.Pages
{
    public class UsuariosExistentesModel : PageModel
    {
        [BindProperty]
        public Usuario Editing { get; set; } = new Usuario { IdUsuario = -10 };
        public bool deleted = false;
        public bool updated = false;
        public ServicioLogicaN LogN = new ServicioLogicaN();
        public List<Usuario> totalPersonas = new();
        public void OnGet()
        {
                totalPersonas =  LogN.ReadAll();
        }
        public IActionResult OnPost(int id)
        {
            Editing.IdUsuario = id;
            Console.WriteLine(Editing);
            LogN.Update(Editing);
            Editing.IdUsuario = -10;
            updated= true;
            totalPersonas=LogN.ReadAll();
            return Page();
        }
        public IActionResult OnPostEdit(int id)
        {
            deleted = false;
            Editing = LogN.Read(id);
            totalPersonas= LogN.ReadAll();
            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            Console.WriteLine(id);
            if (LogN.Read(id).IdUsuario!=-10)
            {
                LogN.DeleteById(id);
                deleted = true;
                Editing.IdUsuario = -10;
                totalPersonas = LogN.ReadAll();
                return Page();
            }
            return RedirectToAction("Get");
        }
    }
}
