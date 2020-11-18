using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoList.Db;
using ToDoList.Models;
using ToDoList.Services.EventType;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IEventAggregator ea)

        {
            ea.GetEvent<MainViewRefresh>().Subscribe(Refresh);

            Things = new ObservableCollection<Thing>();

            Refresh();
        }

        #region 属性定义

        private readonly ThingsContext db = new ThingsContext();
        public ObservableCollection<Thing> Things { get; set; }

        #endregion 属性定义

        #region 命令方法

        private DelegateCommand _newThingCmd;

        public DelegateCommand NewThingViewCmd =>
            _newThingCmd ?? (_newThingCmd = new DelegateCommand(ExecuteNewThingViewCmd));

        private void ExecuteNewThingViewCmd()
        {
            NewThingView newThingView = new NewThingView();
            newThingView.Show();
        }

        private DelegateCommand _testCmd;

        public DelegateCommand TestCmd =>
            _testCmd ?? (_testCmd = new DelegateCommand(Test));

        private DelegateCommand _RefreshCmd;

        public DelegateCommand RefreshCmd =>
            _RefreshCmd ?? (_RefreshCmd = new DelegateCommand(Refresh));

        #endregion 命令方法

        #region 内部方法

        private void Test()
        {
           // db.SaveChanges();
        }

        private void Refresh()
        {
            db.SaveChanges();
            var ThingsLst = from Thing in db.Things where (Thing.Done == false) select Thing;

            Things.Clear();
            foreach (var item in ThingsLst)
            {
                Things.Add(item);
            }
        }

        #endregion 内部方法
    }
}