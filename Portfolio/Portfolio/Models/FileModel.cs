using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Portfolio.Models
{
    public class FileModel
    {
        //Properties
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? FileName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile File { get; set; }
    }
}
