using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ICSproj.App.ViewModels.Interfaces;

namespace ICSproj.App.ViewModels
{
    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected ViewModelBase()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                // ReSharper disable once VirtualMemberCallInConstructor
                LoadInDesignMode();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // is called only in designe mode, can be overwrotten in order to put some extern data to design
        public virtual void LoadInDesignMode()
        {
        }
    }
}
