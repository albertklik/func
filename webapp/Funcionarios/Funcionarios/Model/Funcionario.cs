using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Funcionarios.Model;

public class Funcionario
{
    public int Codigo { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;
    [Required]
    public int CodigoCargo { get; set; }
    public string Cargo { get; set; } = string.Empty;
    [Required]
    public decimal ValorSalario { get; set; } = 0;
    public string ValorSalarioStr => $"{ValorSalario:C}";
}