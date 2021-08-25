using Prism.Events;
using Prism.Mvvm;

namespace ToDoList.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IEventAggregator ea)

        {
            _eventAggregator = ea;
        }


        /// <summary>
        /// 事件聚合器
        /// </summary>
        private IEventAggregator _eventAggregator;
    }
}