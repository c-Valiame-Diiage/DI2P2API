using System.ComponentModel.DataAnnotations;

namespace Evaluation.Services.Contracts.DTO.Up
{
    public class EvenementUpDTO
    {
        [Required]
        public string Titre { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateEvent { get; set; }

        [Required]
        public DateTime TimeEvent { get; set; }

        [Required]
        public string Lieu { get; set; }
    }
}
