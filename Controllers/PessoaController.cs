using app_test_jmeter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace app_test_jmeter.Controllers
{
    [Route("api/[controller]")]
    public class PessoaController: Controller
    {
        [HttpPost]
        public IActionResult Process([FromBody] PessoaViewModel pessoa)
        {
            return Ok(new { Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });
        }
    }
}