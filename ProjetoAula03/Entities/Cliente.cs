using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAula03.Entities;
/// <summary>
/// Modelo de entidade para cliente
/// </summary>
public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public Endereco Endereco { get; set; }
}
