using System;
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
            using var con = new SqliteConnection($"Data Source={_dbPath}");
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS Tarefas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Concluida INTEGER NOT NULL,
                    DataLimite TEXT,
                    HoraLimite TEXT
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
            @"INSERT INTO Tarefas (Nome, Concluida, DataLimite, HoraLimite)
              VALUES ($nome, $concluida, $data, $hora);";

            cmd.Parameters.AddWithValue("$nome", tarefa.Nome);
            cmd.Parameters.AddWithValue("$concluida", tarefa.Concluida ? 1 : 0);

            cmd.Parameters.AddWithValue("$data",
                tarefa.DataLimite.HasValue
                    ? tarefa.DataLimite.Value.ToString("yyyy-MM-dd")
                    : "");

            cmd.Parameters.AddWithValue("$hora",
                string.IsNullOrWhiteSpace(tarefa.HoraLimite)
                    ? ""
                    : tarefa.HoraLimite);

            cmd.ExecuteNonQuery();
        }

        public List<Tarefa> Listar()
        {
            var lista = new List<Tarefa>();

            using var con = new SqliteConnection($"Data Source={_dbPath}");
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Tarefas";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var dataString = reader.IsDBNull(3) ? null : reader.GetString(3);
                var horaString = reader.IsDBNull(4) ? null : reader.GetString(4);

                lista.Add(new Tarefa
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Concluida = reader.GetInt32(2) == 1,

                    DataLimite = string.IsNullOrWhiteSpace(dataString)
                        ? null
                        : DateTime.Parse(dataString),

                    HoraLimite = string.IsNullOrWhiteSpace(horaString)
                        ? null
                        : horaString
                });
            }

            return lista;
        }

        public void Atualizar(Tarefa tarefa)
        {
            using var con = new SqliteConnection($"Data Source={_dbPath}");
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText =
            @"UPDATE Tarefas
              SET Nome = $nome, Concluida = $concluida,
                  DataLimite = $data, HoraLimite = $hora
              WHERE Id = $id";

            cmd.Parameters.AddWithValue("$id", tarefa.Id);
            cmd.Parameters.AddWithValue("$nome", tarefa.Nome);
            cmd.Parameters.AddWithValue("$concluida", tarefa.Concluida ? 1 : 0);

            cmd.Parameters.AddWithValue("$data",
                tarefa.DataLimite.HasValue
                    ? tarefa.DataLimite.Value.ToString("yyyy-MM-dd")
                    : "");

            cmd.Parameters.AddWithValue("$hora",
                string.IsNullOrWhiteSpace(tarefa.HoraLimite)
                    ? ""
                    : tarefa.HoraLimite);

            cmd.ExecuteNonQuery();
        }

        public void Remover(int id)
        {
            using var con = new SqliteConnection($"Data Source={_dbPath}");
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM Tarefas WHERE Id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
