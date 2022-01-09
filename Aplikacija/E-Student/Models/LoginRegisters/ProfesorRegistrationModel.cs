using System.ComponentModel.DataAnnotations;

namespace E_Student.Models.LoginRegisters
{
    public class ProfesorRegistrationModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public String? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public String? Password { get; set; }

        [Required(ErrorMessage = " Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Paswords doesn't match")]
        public String? ConfirmPassword { get; set; }

        [Required(ErrorMessage = " Ime is required")]
        public String? Ime { get; set; }

        [Required(ErrorMessage = " Broj telefona is required")]
        [DataType(DataType.PhoneNumber)]
        public String? Br_telefona { get; set; }

        [Required(ErrorMessage = " Prezime is required")]
        public String? Prezime { get; set; }

        [Required(ErrorMessage = " Kancelarija is required")]
        public String? Kancelarija { get; set; }
    }
}