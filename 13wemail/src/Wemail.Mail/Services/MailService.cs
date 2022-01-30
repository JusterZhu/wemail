using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wemail.DAL;
using Wemail.DAL.DTOs;

namespace Wemail.Mail.Services
{
    public class MailService
    {
        public static async Task GetMails(Action<Exception> onExcption, Action<List<MailDTO>> onResult)
        {
            try
            {
                await HttpHelper.Instance.HttpHandle(HttpMethod.GET, HttpHelper.Address + "getmails", null, onExcption, onResult);
            }
            catch (Exception ex)
            {
                onExcption(ex);
            }
        }
    }
}
