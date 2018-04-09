using System;
using System.ComponentModel;

namespace app_test_jmeter.Models
{
    public class User {
       public int Id { get; set; }

       [DisplayName("Nome: ")]
       public string Name { get; set; }

       [DisplayName("Idade: ")]
       public int Age { get; set; }
       
       [DisplayName("Usuário: ")]
       public string Username { get; set; }

       [DisplayName("Senha: ")]
       public string Password { get; set; }

       [DisplayName("Avatar: ")]
       public string PerfilImage { get; set; }
    }
}