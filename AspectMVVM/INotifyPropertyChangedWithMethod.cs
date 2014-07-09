using System.ComponentModel;

namespace AspectMVVM
{
    public interface INotifyPropertyChangedWithMethod : INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }
}
