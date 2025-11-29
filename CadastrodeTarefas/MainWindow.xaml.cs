using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using CadastroDeTarefas.ViewModels;

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
            {
                root.DataContext = ViewModel;
            }
            else
            {
                var grid = new Grid();
                this.Content = grid;
                grid.DataContext = ViewModel;
            }
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AdicionarTarefa();
        }
    }
}
