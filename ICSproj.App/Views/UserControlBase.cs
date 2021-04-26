using System.Windows;
using System.Windows.Controls;
using ICSproj.App.ViewModels.Interfaces;

namespace ICSproj.App.Views
{
    // zabezpečuje load dát do View modelu v momente keď sa View model zobrazí

    public abstract class UserControlBase : UserControl
    {
        protected UserControlBase()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IListViewModel viewModel)
            {
                viewModel.Load();
            }
        }
    }
}
