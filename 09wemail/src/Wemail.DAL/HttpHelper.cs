using System;
using Wemail.DAL.DTOs;

namespace Wemail.DAL
{
    public class HttpHelper
    {
        public static UserDto Login(string account,string passworld) 
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(passworld)) return null;

            if(account.Equals("juster") && passworld.Equals("123456")) return new UserDto() { Token = "0ca175b9c0f726a831d895e269332461" };

            return null;
        }
    }
}
