using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.ViewModel
{
    //Detta visas i vyn då man registrerar ny konto
    public class KundViewModel
    {
        public string Namn { get; set; }
        public string Gatuadress { get; set; }
        public string Postnr { get; set; }
        public string Postort { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string AnvandarNamn { get; set; }
        public string Losenord { get; set; }
    }
}
