using TodoApp.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace TodoApp.Services
{
    public class  BandoDeDadosService
    {
        private readonly string _dbPath;

        public BandoDeDadosService(string dbPath)
        {
            _dbPath = dbPath;
            CriarBanco();
        }

        private void CriarBanco()
        {
           using var conexao = new SqliteConnection($"Data Source={_dbPath}");
           string sql = @"
                CREATE TABLE IF NOT EXISTS Tarefas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Descricao TEXT NOT NULL,
                    Concluida INTEGER NOT NULL
                );";

           var  comando = new SqliteCommand(sql, conexao);
            comando.ExecuteNonQuery();
        }

        public List<Tarefa> Listar()
        {
            var lista = new List<Tarefa>();
            using var conexao = new SqliteConnection($"Data Source={_dbPath}");
            conexao.Open();
            string sql = "SELECT Id, Descricao, Concluida FROM Tarefas;";

            var comando = new SqliteCommand(sql, conexao);
            var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                var tarefa = new Tarefa
                {
                    Id = reader.GetInt32(0),
                    Descricao = reader.GetString(1),
                    Concluida = reader.GetInt32(2) == 1
                };
                lista.Add(tarefa);
            }

            return lista;
        }

        public void Atualizar(Tarefa tarefa)
        {
            using var conexao = new SqliteConnection($"Data Source={_dbPath}");
            conexao.Open();

            string sql = "UPDATE Tarefas SET Descricao = @Descricao, Concluida = @Concluida WHERE Id = @Id;";
            var comando = new SqliteCommand(sql, conexao);
            comando.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
            comando.Parameters.AddWithValue("@Concluida", tarefa.Concluida ? 1 : 0);
            comando.Parameters.AddWithValue("@Id", tarefa.Id);
            comando.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conexao = new SqliteConnection($"Data Source={_dbPath}");
            conexao.Open();

            string sql = "DELETE FROM Tarefas WHERE Id = @Id;";
            var comando = new SqliteCommand(sql, conexao);
            comando.Parameters.AddWithValue("@Id", id);
            comando.ExecuteNonQuery();
        }
    }
}