using CadastroDeTarefas.Helpers;
using CadastroDeTarefas.Models;
using CadastroDeTarefas.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.IO;

namespace CadastroDeTarefas.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tarefa> Tarefas { get; } = new();
        public ObservableCollection<Tarefa> TarefasFeitas { get; } = new();

        private readonly BancoDeDadosService _db;

        public MainViewModel()
        {
            string caminho = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "tarefas.db"
            );

            _db = new BancoDeDadosService(caminho);
            CarregarTarefas();
        }

        public void CarregarTarefas()
        {
            var dados = _db.Listar();

            foreach (var t in dados)
            {
                if (t.Concluida)
                    TarefasFeitas.Add(t);
                else
                    Tarefas.Add(t);
            }
        }

        private string _nomeTarefa;
        public string NomeTarefa
        {
            get => _nomeTarefa;
            set
            {
                _nomeTarefa = value;
                OnPropertyChanged(nameof(NomeTarefa));
            }
        }

        private DateTimeOffset? _dataLimite;
        public DateTimeOffset? DataLimite
        {
            get => _dataLimite;
            set
            {
                _dataLimite = value;
                OnPropertyChanged(nameof(DataLimite));
            }
        }

        private string _horaLimite;
        public string HoraLimite
        {
            get => _horaLimite;
            set
            {
                _horaLimite = value;
                OnPropertyChanged(nameof(HoraLimite));
            }
        }

        public void AdicionarTarefa()
        {
            if (string.IsNullOrWhiteSpace(NomeTarefa))
                return;

            var novaTarefa = new Tarefa
            {
                Nome = NomeTarefa.Trim(),
                Concluida = false,
                DataLimite = DataLimite,
                HoraLimite = HoraLimite
            };

            _db.Adicionar(novaTarefa);

            Tarefas.Add(novaTarefa);

            NomeTarefa = string.Empty;
            DataLimite = null;
            HoraLimite = string.Empty;
        }


        public ICommand ExcluirCommand => new RelayCommand(param =>
        {
            if (param is Tarefa tarefa)
            {
                _db.Remover(tarefa.Id);

                Tarefas.Remove(tarefa);
                TarefasFeitas.Remove(tarefa);
            }
        });

        public ICommand JaFizCommand => new RelayCommand(param =>
        {
            if (param is Tarefa tarefa)
            {
                tarefa.Concluida = true;
                _db.Atualizar(tarefa);

                Tarefas.Remove(tarefa);
                TarefasFeitas.Add(tarefa);
            }
        });
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
