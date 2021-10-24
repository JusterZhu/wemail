using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wemail.Schedule.ViewModels
{
    public class ScheduleViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ScheduleViewModel()
        {
            Message = "Wemail.Schedule Prism Module";
        }
    }
}
