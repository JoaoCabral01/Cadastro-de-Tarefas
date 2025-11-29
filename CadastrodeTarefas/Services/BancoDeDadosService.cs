using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using CadastroDeTarefas.Models;

namespace CadastroDeTarefas.Services
{
    public class BancoDeDadosService
    {
        private readonly string _dbPath;

        public BancoDeDadosService(string dbPath)
        {
            _dbPath = dbPath;
            CriarBancoSeNaoExistir();
        }

        private void CriarBancoSeNaoExistir()
        {
            using var conexao = new SqliteConnection($"Data Source={_dbPath}");
            conexao.Open();

            var cmd = conexao.CreateCommand();
            cmd.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS Tarefas (
	                Id INTEGER PRIMARY KEY AUTOINCREMENT,
	                Nome TEXT NOT NULL,
	                Concluida INTEGER NOT NULL
                );
            ";
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Tarefa tarefa)
        {
            using var con = new SqliteConnection($"Data Source={_dbPath}");
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText =
            @"INSERT INTO Tarefas (Nome, Concluida)
              VALUES ($nome, $concluida);";

            cmd.Parameters.AddWithValue("$nome", tarefa.Nome);
            cmd.Parameters.AddWithValue("$concluida", tarefa.Concluida ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        public List<Tarefa> Listar()
        {
            List<Tarefa> lista = new();

            using var con = new SqliteConnection($"Data Source={_dbPath}");
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Id, Nome, Concluida FROM Tarefas;";

            using var leitor = cmd.ExecuteReader();
            while (leitor.Read())
            {
                lista.Add(new Tarefa
                {
                    Id = leitor.GetInt32(0),
                    Nome = leitor.GetString(1),
                    Concluida = leitor.GetInt32(2) == 1
                });
            }

            return lista;
        }

        public void Remover(int id)
        {
            using var con = new SqliteConnection($"Data Source={_dbPath}");
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = @"DELETE FROM Tarefas WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }
    }
}