using System.ComponentModel;

namespace AspectMVVM
{
    public interface INotifyPropertyChangedWithMethod : INotifyPropertyChanged
    {
        void NotifyPropertyChanged(string propertyName);
    }
}
