using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoList.Db;
using ToDoList.Models;
using ToDoList.Services;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private IExample _example = null;

        public MainViewModel(IExample example)

        {
            _example = example;
            Things = db.Things.Local.ToObservableCollection();
            db.Things.Load();
        }

        #region 属性定义

        public ObservableCollection<Thing> Things { get; set; } = new ObservableCollection<Thing>();

        private ThingsContext db = new ThingsContext();

        public ThingsContext DB
        {
            get { return db; }
            set { SetProperty(ref db, value); }
        }

        #endregion 属性定义

        #region 命令

        private DelegateCommand _newThingCmd;

        public DelegateCommand NewThingViewCmd =>
            _newThingCmd ?? (_newThingCmd = new DelegateCommand(ExecuteNewThingViewCmd));

        private void ExecuteNewThingViewCmd()
        {
            var a = from b in db.Things where b.Done == true select b.Content;
            NewThingView newThingView = new NewThingView();
            newThingView.Show();
        }

        #endregion 命令
    }
}