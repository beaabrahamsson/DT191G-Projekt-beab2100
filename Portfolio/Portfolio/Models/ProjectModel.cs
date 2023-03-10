using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    public class ProjectModel
    {
        //Properties
        public int ID { get; set; }
        [DisplayName("Titel")]
        [Required(ErrorMessage = "Vänligen ange en titel")]
        public string? Title { get; set; }
        [DisplayName("Beskrivning")]
        [Required(ErrorMessage = "Vänligen ange en beskrivning")]
        public string? Description { get; set; }
        [DisplayName("Länk")]
        [Required(ErrorMessage = "Vänligen ange en länk")]
        public string? Link { get; set; }
        [DisplayName("GitHub länk")]
        [Required(ErrorMessage = "Vänligen ange en GitHub länk")]
        public string? GitHub { get; set; }
        public string? ImageName { get; set; }

        [NotMapped]
        [DisplayName("Ladda upp bild")]
        [Required(ErrorMessage = "Vänligen välj en bild")]
        public IFormFile? Image { get; set; }

    }
}
