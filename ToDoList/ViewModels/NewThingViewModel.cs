using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;

using System.Collections.ObjectModel;
using ToDoList.Db;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class NewThingViewModel : BindableBase
    {
        public NewThingViewModel()
        {
            Things = db.Things.Local.ToObservableCollection();
            db.Things.Load();
        }

        #region 属性定义

        private ThingsContext db = new ThingsContext();
        private string thing;
        public ThingsContext DB
        {
            get { return db; }
            set { SetProperty(ref db, value); }
        }

        public string Thing
        {
            get { return thing; }
            set { SetProperty(ref thing, value); }
        }

        public ObservableCollection<Thing> Things { get; set; } = new ObservableCollection<Thing>();
        #endregion 属性定义

        #region 命令

        private DelegateCommand<string> _saveCmd;

        public DelegateCommand<string> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<string>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(string parameter)
        {
            db.Things.Add(new Models.Thing { Content = parameter });
            db.SaveChanges();
        }

        #endregion 命令
    }
}