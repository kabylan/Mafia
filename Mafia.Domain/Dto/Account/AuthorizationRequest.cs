using Mafia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Domain.Dto.Account
{
    public class AuthorizationRequest
    {
        public string Login { get; set; }
        public int CurrentOrganisation { get; set; }

        public AuthorizationRequest( string login, int currentOrganisation)
        {
            this.Login = login;
            this.CurrentOrganisation = currentOrganisation;
        }

        public AuthorizationRequest()
        {
        }
    }
}
