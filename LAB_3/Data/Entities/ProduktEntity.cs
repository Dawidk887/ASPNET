using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Products")]
    public class ProduktEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwę produktu")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Cena musi być większa lub równa 0")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Proszę wpisać producenta!")]
        [Column(TypeName = "nvarchar(50)")] // Mapowanie enum jako string
        public ProducentType Producent { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Produktdate { get; set; }

        [MinLength(5, ErrorMessage = "Opis musi mieć co najmniej 5 znaków!")]
        [MaxLength(500)]
        [Required(ErrorMessage = "Proszę wpisać opis!")]
        public string Description { get; set; }
    }

    public enum ProducentType
    {
        [Display(Name = "Producent A")]
        ProducentA,
        [Display(Name = "Producent B")]
        ProducentB,
        [Display(Name = "Producent C")]
        ProducentC
    }
}