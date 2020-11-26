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

        private bool remind;
        /// <summary>
        /// 是否提醒
        /// </summary>
        public bool Remind
        {
            get { return remind; }
            set { SetProperty(ref remind, value); }
        }

        private DateTime remindTime=DateTime.Now;
        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime RemindTime
        {
            get { return remindTime; }
            set { SetProperty(ref remindTime, value); }
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
                db.Things.Add(new Models.Thing { Content = parameter, CreatTime = DateTime.Now ,Remind=this.Remind,RemindTime=this.RemindTime});
                db.SaveChanges();
            }
           
            _eventAggregator.GetEvent<MainViewRefresh>().Publish();
        }

        #endregion 命令
    }
}