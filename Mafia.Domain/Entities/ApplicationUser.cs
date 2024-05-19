using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mafia.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Pin { get; set; }
        public Organisation Organisation { get; set; }
        public int? OrganisationId { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }

        public string GetFIO()
        {
            if(FIO == null)
            return $"{Surname} {FirstName} {Patronymic} ";
            else
            {
                return FIO;
            }
        }
    }
}
