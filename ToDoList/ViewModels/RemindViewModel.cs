using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class RemindViewModel : BindableBase
    {
        public RemindViewModel()
        {

        }

        #region 属性定义

        private Thing thing;
        public Thing Thing
        {
            get { return thing; }
            set { SetProperty(ref thing, value); }
        }
        #endregion
    }
}
