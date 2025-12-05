using System;

namespace CadastroDeTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Concluida { get; set; }
        public DateTimeOffset? DataLimite { get; set; }
        public string? HoraLimite { get; set; }

    }
}