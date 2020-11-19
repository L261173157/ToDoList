using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using ToDoList.Services;
using ToDoList.Services.EventType;
using System.Collections.ObjectModel;
using ToDoList.Db;
using ToDoList.Models;
using ToDoList.Views;
using Prism.Events;
using System;

namespace ToDoList.ViewModels
{
    public class NewThingViewModel : BindableBase
    {
        public NewThingViewModel(IEventAggregator ea)
        {

            _eventAggregator = ea;
            
        }

        #region 属性定义
        IEventAggregator _eventAggregator;
        
        private string thing;
        public string Thing
        {
            get { return thing; }
            set { SetProperty(ref thing, value); }
        }

        
        #endregion 属性定义

        #region 命令

        private DelegateCommand<string> _saveCmd;

        public DelegateCommand<string> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<string>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(string parameter)
        {
            using (var db = new ThingsContext())
            {
                db.Things.Add(new Models.Thing { Content = parameter, CreatTime = DateTime.Now });
                db.SaveChanges();
            }
           
            _eventAggregator.GetEvent<MainViewRefresh>().Publish();
        }

        #endregion 命令
    }
}