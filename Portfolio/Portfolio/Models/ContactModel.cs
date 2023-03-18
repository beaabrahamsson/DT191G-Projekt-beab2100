using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class ContactModel
    {
        //Properties
        [DisplayName("Namn")]
        [Required(ErrorMessage = "Vänligen ange ett namn")]
        public string? Name { get; set; }
        [DisplayName("E-post")]
        [Required(ErrorMessage = "Vänligen ange en e-post")]
        public string? Email { get; set; }
        [DisplayName("Ämne")]
        [Required(ErrorMessage = "Vänligen ange ett ämne")]
        public string? Subject { get; set; }
        [DisplayName("Meddelande")]
        [Required(ErrorMessage = "Vänligen ange ett meddelande")]
        public string? Message { get; set; }
    }
}
