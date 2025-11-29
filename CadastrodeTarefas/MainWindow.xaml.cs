using Microsoft.UI.Xaml;
using CadastroDeTarefas.ViewModels;

namespace CadastroDeTarefas
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AdicionarTarefa();
        }
    }
}
