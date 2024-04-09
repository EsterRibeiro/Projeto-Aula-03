using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAula03.Services;
/// <summary>
/// Classe de serviço para solicitações de consulta de endereço por Cep
/// </summary>
public class ViaCepService
{
    /// <summary>
    /// Método para consulta de dados de endereço baseado em cep
    /// </summary>
    /// <param name="cep">Numero do cep desejado</param>
    /// <returns>Endereço baseado no cep informado</returns>
    public string GetData(string cep)
    {
        //fazer uma consulta HTTP
        //instanciando o objeto HTTP CLIENT
        var httpClient = new HttpClient();

        //fazer uma requisição na API do ViaCep para consultar o endereço - já pegando a resposta
        var response = httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/").Result;

        //retorna a resposta com validação
        if (response.IsSuccessStatusCode) {
            return response.Content.ReadAsStringAsync().Result;
        }
        else
        {
            return "\nErro. Verifique o CEP informado.";
        }

    }
}
