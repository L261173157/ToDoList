using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoList.Db;
using ToDoList.Models;
using ToDoList.Views;
using ToDoList.Services;
using ToDoList.Services.EventType;
using Prism.Events;

namespace ToDoList.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IEventAggregator ea)

        {
            ea.GetEvent<MainViewRefresh>().Subscribe(Refresh);
            var ThingsList = db.Things.Where(b => b.Done == false).ToList();
            Things = new ObservableCollection<Thing>(ThingsList);
            db.Things.Load();
        }

        #region 属性定义
        
        public ObservableCollection<Thing> Things { get; set; }

        private ThingsContext db = new ThingsContext();

        public ThingsContext DB
        {
            get { return db; }
            set { SetProperty(ref db, value); }
        }

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

        #endregion 命令方法

        #region 内部方法

        private void Test()
        {
        }

        private void Refresh()
        {
            db.Things.Load();
            var ThingsList = db.Things.Where(b => b.Done == false).ToList();
            Things.Clear();
            foreach (var item in ThingsList)
            {
                Things.Add(item);
            }
        }

        #endregion 内部方法
    }
}