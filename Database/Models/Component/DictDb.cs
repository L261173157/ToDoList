using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Prism.Mvvm;
namespace Database.Models.Component
{
  public  class DictDb:Dict
    {

        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }


    }
}
