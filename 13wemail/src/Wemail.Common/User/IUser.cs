using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wemail.Common.User
{
    public interface IUser
    {
        bool IsLogin();

        void SetUserLoginState(bool state);
    }
}
