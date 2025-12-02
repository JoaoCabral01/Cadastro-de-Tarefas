using CadastroDeTarefas.Models;
using CadastroDeTarefas.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CadastroDeTarefas
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();

            if (this.Content is FrameworkElement root)
                root.DataContext = ViewModel;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AdicionarTarefa();
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Tarefa tarefa)
            {
                ViewModel.Tarefas.Remove(tarefa);
            }
        }

    }

}
