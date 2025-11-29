using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TodoApp.Helpers;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly BandoDeDadosService _db;

        public ObservableCollection<Tarefa> Tarefas { get; } = new();

        private string _textoNovaTarefa;
        public string TextoNovaTarefa
        {
            get => _textoNovaTarefa;
            set
            {
                _textoNovaTarefa = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AdicionarTarefaCommand { get; }
        public RelayCommand ExcluirCommand { get; }
        public RelayCommand AlternarConcluidaCommand { get; }

        public MainViewModel()
        {
            _db = new BandoDeDadosService("tarefa.db");
            AdicionarTarefaCommand = new RelayCommand(_ => Adicionar());
            ExcluirCommand = new RelayCommand(t => Excluir((int)t));
            AlternarConcluidaCommand = new RelayCommand(t => Alternar((Tarefa)t));
            Carregar();
        }

        private void Carregar()
        {
            Tarefas.Clear();
            foreach (var tarefa in _db.Listar())
            {
                Tarefas.Add(tarefa);
            }
        }

        private void Adicionar()
        {
            var tarefa = new Tarefa { Descricao = TextoNovaTarefa, Concluida = false };
            _db.Atualizar(tarefa);
            TextoNovaTarefa = "";
            Carregar();
        }

        public void Excluir(int id)
        {
            _db.Excluir(id);
            Carregar();
        }

        public void Alternar(Tarefa tarefa)
        {
            tarefa.Concluida = !tarefa.Concluida;
            _db.Atualizar(tarefa);
            Carregar();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string n = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
        }

    }
}