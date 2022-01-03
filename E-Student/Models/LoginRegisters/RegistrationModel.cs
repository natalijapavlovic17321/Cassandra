using System.ComponentModel.DataAnnotations;

namespace E_Student.Models.LoginRegisters
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Email is required")]
        public String? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public String? Password { get; set; }


        [Required(ErrorMessage = " Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Paswords doesn't match")]
        public String? ConfirmPassword { get; set; }

        [Required(ErrorMessage = " GodinaUpisa is required")]
        public String? GodinaUpisa { get; set; }

        [Required(ErrorMessage = " Ime is required")]
        public String? Ime { get; set; }

        [Required(ErrorMessage = " Indeks is required")]
        public String? Indeks { get; set; }

        [Required(ErrorMessage = " Prezime is required")]
        public String? Prezime { get; set; }

        [Required(ErrorMessage = " Semestar is required")]
        public String? Semestar { get; set; }

        [Required(ErrorMessage = " Smer is required")]
        public String? Smer { get; set; }

        // [Required(ErrorMessage = "Email is required")]
        // public String? Email { get; set; }
        // [Required(ErrorMessage = "Ime is required")]
        // public String? Ime { get; set; }


        // [Required(ErrorMessage = "Password is required")]
        // public String? Password { get; set; }


        // [Required(ErrorMessage = " Password is required")]
        // [DataType(DataType.Password)]
        // [Display(Name = "Potvrdi lozinku")]
        // [Compare("Password", ErrorMessage = "Paswords doesn't match")]
        // public String? ConfirmPassword { get; set; }


        // [Required(ErrorMessage = "Name is required")]
        // public String? Name { get; set; }


        // [Required(ErrorMessage = "LastName is required")]
        // public String? LastName { get; set; }
        // [Required(ErrorMessage = "Description is required")]

        // public String? Description { get; set; }
    }
}