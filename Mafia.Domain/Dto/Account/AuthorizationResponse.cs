using Mafia.Domain.Entities;
using System.Collections.Generic;

namespace Mafia.Domain.Dto.Account
{
    public class AuthorizationResponse
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Doljnost { get; set; }
        public string Fio { get; set; }
        public string Roles { get; set; }
        public List<string> Privileges { get; set; }
        public bool IsAdmin { get; set; }
        public ApplicationUser currentUser { get; set; }
        public string JwtToken { get; set; }

        public AuthorizationResponse(string name, string login, string doljnost, string fio, string roles,
            List<string> privileges, string jwtToken,
            bool admin, ApplicationUser currentUser)
        {
            this.Name = name;
            this.Login = login;
            this.Doljnost = doljnost;
            this.Fio = fio;
            this.Roles = roles;
            this.Privileges = privileges;
            this.JwtToken = jwtToken;
            this.IsAdmin = admin;
            this.currentUser = currentUser;
        }
        public AuthorizationResponse() { }
    }
}