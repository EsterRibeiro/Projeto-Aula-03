using Newtonsoft.Json;
using ProjetoAula03.Entities;
using ProjetoAula03.Repositories;
using ProjetoAula03.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjetoAula03.Controllers;
/// <summary>
/// Classe de controle para realização do fluxo de cadastro de clientes
/// </summary>
public class ClienteController
{
    /// <summary>
    /// Método para executar o passao a passo do cadastro de um cliente
    /// </summary>
    public void CadastrarCliente()
    {
        #region Consultar os dados do endereço baseado no CEP informado

        Console.Write("Informe o CEP:");
        var cep = Console.ReadLine();

        
        var viaCepService = new ViaCepService();
        var dadosCep = viaCepService.GetData(cep);

        #endregion

        #region Confirmando o endereço obtido

        Console.WriteLine(dadosCep);

        Console.WriteLine("\nconfirmar o endereço obtido? s/n");
        var opcao = Console.ReadLine();

        if(!opcao.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            CadastrarCliente(); //recursividade
        }
        else
        {
            var cliente = new Cliente()
            {
                Endereco = JsonConvert.DeserializeObject<Endereco>(dadosCep)
            };

            Console.Write("\nInforme o nome do cliente: ");
            cliente.Nome = Console.ReadLine();

            Console.Write("\nInforme o CPF do cliente: ");
            cliente.Cpf = Console.ReadLine();

            Console.Write("\nInforme a data de nascimento do cliente: ");
            cliente.DataDeNascimento = Convert.ToDateTime(Console.ReadLine()); //ou DateTime.Parse() - apenas para strings

            Console.Write("\nNúmero do endereço: ");
            cliente.Endereco.Numero = Console.ReadLine();

            #region Gravando o cliente no banco de dados

            var clienteRepository = new ClienteRepository();
            clienteRepository.Inserir(cliente);

            Console.WriteLine("Cliente cadastrado com sucesso");

            #endregion


        }

        #endregion

    }
}
