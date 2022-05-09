using Prism.Events;
using Prism.Mvvm;

namespace ToDoList.ViewModels;

public class MainViewModel : BindableBase
{
    /// <summary>
    ///     事件聚合器
    /// </summary>
    private IEventAggregator _eventAggregator;

    public MainViewModel(IEventAggregator ea)

    {
        _eventAggregator = ea;
    }
}