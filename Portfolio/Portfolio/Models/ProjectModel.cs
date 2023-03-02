using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    public class ProjectModel
    {
        //Properties
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? GitHub { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? Image { get; set; }

    }
}
