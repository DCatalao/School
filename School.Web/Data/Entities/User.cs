using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data.Entities
{
    public class User : IdentityUser //Ao nossa classe de usuario herdar da classe ja implementada da microsoft, automaticamente já possuiremos métodos e propriedades herdadas.
                                     //Verificar a pagina de documentação do identityUser para saber quais as propriedades e métodos já implementadas.
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
