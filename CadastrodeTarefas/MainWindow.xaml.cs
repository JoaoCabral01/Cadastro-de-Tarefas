using CadastroDeTarefas.Models;
using CadastroDeTarefas.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

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

            ImagemAlbon.PointerEntered += ImagemAlbon_PointerEntered;
            ImagemAlbon.PointerExited += ImagemAlbon_PointerExited;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AdicionarTarefa();
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Models.Tarefa tarefa)
            {
                ViewModel.Tarefas.Remove(tarefa);
            }
        }

        private void ImagemAlbon_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ScaleImagem.ScaleX = 1.2;
            ScaleImagem.ScaleY = 1.2;
        }

        private void ImagemAlbon_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ScaleImagem.ScaleX = 1;
            ScaleImagem.ScaleY = 1;
        }
    }
}
