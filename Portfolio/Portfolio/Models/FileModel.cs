using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Portfolio.Controllers.FileModelsController;

namespace Portfolio.Models
{
    public class FileModel
    {
        //Properties
        public int ID { get; set; }
        [DisplayName("Titel")]
        [Required(ErrorMessage = "Vänligen ange titel")]
        public string? Title { get; set; }
        [DisplayName("Filnamn")]
        public string? FileName { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Vänligen välj en fil")]
        [DisplayName("Ladda upp fil")]
        [AllowedExtensions(new string[] { ".pdf" })]
        public IFormFile File { get; set; }
    }
}
