using System.ComponentModel.DataAnnotations;

namespace AppleStoreTupinikim.Models
{
    public class Produtos
    {
        public string? Id { get; set; } //id pro firebase

        [Required(ErrorMessage = "O campo não pode ficar vazio!")] public int? Codigo { get; set; } //id pra view

        [Required(ErrorMessage = "O campo não pode ficar vazio!")] public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo não pode ficar vazio!")] [Range(0, 10000000, ErrorMessage = "O valor não pode ser negativo!")] public decimal? Preco { get; set; }
        [Required(ErrorMessage = "O campo não pode ficar vazio!")] public int? Estoque { get; set; }
    }
}
