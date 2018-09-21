using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace app_test_jmeter.Models.ViewModels
{
    public class PessoaViewModel {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }

        public List<SelectListItem> Sexos { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Masculino", Text = "Masculino" },
            new SelectListItem { Value = "Feminino", Text = "Feminino" },
            new SelectListItem { Value = "Indefinido", Text = "Indefinido"  },
        };
    }
}