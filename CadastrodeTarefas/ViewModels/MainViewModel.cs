using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CadastroDeTarefas.Models;
using CadastroDeTarefas.Helpers;

namespace CadastroDeTarefas.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tarefa> Tarefas { get; } = new();
        public ObservableCollection<Tarefa> TarefasFeitas { get; } = new();

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
                Nome = NomeTarefa.Trim(),
                Concluida = false
            });

            NomeTarefa = string.Empty;
        }

        public ICommand ExcluirCommand => new RelayCommand(param =>
        {
            if (param is Tarefa tarefa)
            {
                Tarefas.Remove(tarefa);
                TarefasFeitas.Remove(tarefa); 
            }
        });

        public ICommand JaFizCommand => new RelayCommand(param =>
        {
            if (param is Tarefa tarefa)
            {
                Tarefas.Remove(tarefa);
                tarefa.Concluida = true;
                TarefasFeitas.Add(tarefa);
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
