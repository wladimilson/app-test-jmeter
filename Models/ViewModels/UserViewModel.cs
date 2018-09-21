using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace app_test_jmeter.Models.ViewModels
{
    public class UserViewModel {
       public int Id { get; set; }

       [DisplayName("Nome: ")]
       public string Name { get; set; }

       [DisplayName("Idade: ")]
       public int Age { get; set; }
       
       [DisplayName("Usuário: ")]
       public string Username { get; set; }

       [DisplayName("Senha: ")]
       public string Password { get; set; }

       [DisplayName("Imagem: ")]
       public string Filename { get; set; }

       [DisplayName("Avatar: ")]
       public IFormFile PerfilImage { get; set; }
    }
}