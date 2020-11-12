using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoList.Db;
using ToDoList.Models;
using ToDoList.Services;

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

        private DelegateCommand command1;

        public DelegateCommand Command1 =>
            command1 ?? (command1 = new DelegateCommand(ExecuteCommandName, CanExecuteCommandName));

        private void ExecuteCommandName()
        {
            
            var a = from b in db.Things where b.Done == true select b.Content;
           
            //db.Things.Add(new Thing { Content = "666" ,DateTime=DateTime.Now});
            //db.SaveChanges();
        }

        private bool CanExecuteCommandName()
        {
            return true;
        }

        private DelegateCommand command2;

        public DelegateCommand Command2 =>
            command2 ?? (command2 = new DelegateCommand(ExecuteCommand2));

        private void ExecuteCommand2()
        {
            db.SaveChanges();
        }

        #endregion 属性定义

        #region 命令

        #endregion
    }
}