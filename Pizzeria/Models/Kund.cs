using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        public string Namn { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        public string Gatuadress { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        public string Postnr { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        public string Postort { get; set; }
       public string Email { get; set; }
       public string Telefon { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        public string AnvandarNamn { get; set; }
        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        [Display(Name = "Lösenord")]
        [MaxLength(20, ErrorMessage = "Lösenordet får max vara 20 karaktärer.")]
        [DataType(DataType.Password)]
        public string Losenord { get; set; }

        public ICollection<Bestallning> Bestallning { get; set; }
    }
}
