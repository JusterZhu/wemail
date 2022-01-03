using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wemail.Common.User;

namespace Wemail.Common
{
    public class UserModel : IUser
    {
        private bool IsLoginStatus { get; set; }

        public string Account { get; set; }

        public string Passworld { get; set; }

        public bool IsLogin()
        {
            return IsLoginStatus;
        }

        public void SetUserLoginState(bool state)
        {
            IsLoginStatus = state;
        }
    }
}
