using Dapper;
using ProjetoAula03.Entities;
using System.Data.SqlClient;

namespace ProjetoAula03.Repositories;

/// <summary>
/// classe de repositorio de bd para clientes
/// </summary>
public class ClienteRepository
{
    /// <summary>
    /// Método para inserir um registro de clientes no bd
    /// </summary>
    /// <param name="cliente">objeto da classe de entidade de cliente</param>
    public void Inserir(Cliente cliente) //atividade - gravar no banco com commit roolback
    {
        //abrir conexão com o banco de dados
        //Esse using é um disposable, quando sai do using, a conexão do banco fecha automaticamente
        using (var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDProjetoAula03;Integrated Security=True;"))
        {
            connection.Open(); //abre a conexão com o bd

            //abre a transaction - faz um rollback caso dê erro de execução
            using (var transaction = connection.BeginTransaction())
            {
                #region Gravar os dados do endereço do banco de dados

                var sqlEndereco = @"insert into endereco(logradouro, complemento, numero, bairro, localidade, uf, cep)
                            output inserted.id
                        values(@logradouro, @complemento, @numero, @bairro, @localidade, @uf, @cep)";

                var enderecoId = connection.ExecuteScalar<int>(sqlEndereco, new
                {
                    @logradouro = cliente.Endereco.Logradouro,
                    @complemento = cliente.Endereco.Complemento,
                    @numero = cliente.Endereco.Numero,
                    @bairro = cliente.Endereco.Bairro,
                    @localidade = cliente.Endereco.Localidade,
                    @uf = cliente.Endereco.Uf,
                    @cep = cliente.Endereco.Cep
                }, transaction);

                #endregion

                #region Gravar os dados do cliente

                var sqlCliente = @"insert into cliente(nome, cpf, datanascimento, endereco_id)
                        values(@nome, @cpf, @datanascimento, @endereco_id)";

                connection.Execute(sqlCliente, new
                {
                    @nome = cliente.Nome,
                    @cpf = cliente.Cpf,
                    @datanascimento = cliente.DataDeNascimento,
                    @endereco_id = enderecoId
                }, transaction);

                #endregion

                transaction.Commit();
            }
        }
        //gravando o endereço no banco de dados
        //connection.Query(); //continuar

        //gravando os dados do cliente no bd





    }
}
