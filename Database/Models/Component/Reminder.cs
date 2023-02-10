using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Database.Models.Component
{
    public class Reminder:BindableBase
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string context;
        public string Context
        {
            get { return context; }
            set { SetProperty(ref context, value); }
        }


    }
}
