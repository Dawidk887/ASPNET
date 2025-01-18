using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class Produkt
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Musisz podać nazwe produkt")]
    public string Name { get; set; }

    [Required] public int Price { get; set; }
    
    [Required(ErrorMessage ="Proszę wpisz producenta!")]
    public ProducentType Producent { get; set; }
    
    [DataType(DataType.Date)] public DateTime Produktdate { get; set; }
    
    [MinLength(length:5, ErrorMessage ="Wiadomość musi mieć co najmniej 5 znaków!")]
    [Required(ErrorMessage ="Proszę wpisz wiadomość!")]
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