using System.Collections.ObjectModel;
using System.ComponentModel;
using CadastroDeTarefas.Models;

namespace CadastroDeTarefas.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tarefa> Tarefas { get; } = new();

        private string _nomeTarefa;
        public string NomeTarefa
        {
            get => _nomeTarefa;
            set
            {
                if (_nomeTarefa == value) return;
                _nomeTarefa = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomeTarefa)));
            }
        }

        private int _id = 1;

        public void AdicionarTarefa()
        {
            if (string.IsNullOrWhiteSpace(NomeTarefa))
                return;

            Tarefas.Add(new Tarefa
            {
                Id = _id++,
                Nome = NomeTarefa.Trim()
            });

            NomeTarefa = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
