using System.ComponentModel.DataAnnotations;

namespace VillaApi.Models.Dto
{
    public class VillaDto
    {

        public int Id { get; set; }
        [Required] //DataAnnotation
        [MaxLength(30)]
        public string Name { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        

    }   
}