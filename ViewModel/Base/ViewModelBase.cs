using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Upz.Cms.DesktopClient.ViewModel.Base;

public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Устанавливаем значение для свойства с вызовом OnPropertyChanged. Чтобы не писать это постоянно
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field">Поле свойства, которое обновляем</param>
    /// <param name="value">Новое значение</param>
    /// <param name="propertyName">Название свойства</param>
    /// <returns></returns>
    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private bool _disposed;
    public void Dispose(bool disposing)
    {
        if (!disposing || _disposed)
            return;

        _disposed = true;
    }
}
