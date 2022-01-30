using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wemail.DAL.DTOs;

namespace Wemail.DAL
{
    public enum HttpMethod
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        TRACE = 3,
        DELETE = 4
    }

    public class HttpHelper
    {
        public static UserDTO Login(string account,string passworld) 
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(passworld)) return null;

            if(account.Equals("juster") && passworld.Equals("123456")) return new UserDTO() { Token = "0ca175b9c0f726a831d895e269332461" };

            return null;
        }

        private static HttpHelper _instance;
        private static readonly object _lock = new object();
        //当服务端的web api 部署到不同的服务器上时需要请求不同地址
        public static readonly string Address = @"http://127.0.0.1:6668/";
        //public static readonly string Address = @"http://192.1.45.1:6268/";

        public static HttpHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            return _instance = new HttpHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        private HttpHelper() { }

        public async Task HttpHandle<T>(HttpMethod method, string address, Dictionary<string, object> prameters, Action<Exception> onExcption, Action<T> onResult) where T : class
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            StreamReader streamReader = null;
            try
            {
                string methodConfig = string.Empty;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(address);

                httpWebRequest.ContentType = "application/json";
                switch (method)
                {
                    case HttpMethod.GET:
                        methodConfig = "GET";
                        break;
                    case HttpMethod.POST:
                        methodConfig = "POST";
                        break;
                    case HttpMethod.PUT:
                        methodConfig = "PUT";
                        break;
                    case HttpMethod.TRACE:
                        methodConfig = "TRACE";
                        break;
                    case HttpMethod.DELETE:
                        methodConfig = "DELETE";
                        break;
                }
                //设置请求类型
                httpWebRequest.Method = methodConfig;
                //设置超时时间
                httpWebRequest.Timeout = 10 * 1000;
                if (prameters != null)
                {
                    //字符串转换为字节码
                    byte[] prameterBytes = prameters == null ? new byte[0] : Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(prameters));
                    //参数数据长度
                    httpWebRequest.ContentLength = prameterBytes.Length;
                    //将参数写入请求地址中
                    httpWebRequest.GetRequestStream().Write(prameterBytes, 0, prameterBytes.Length);
                }
                //发送请求
                httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                //读取返回数据
                streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                string responseContent = await streamReader.ReadToEndAsync();
                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    onResult(default(T));
                }
                else
                {
                    var resultDto = JsonConvert.DeserializeObject<T>(responseContent);
                    if (resultDto != null)
                    {
                        onResult(resultDto);
                    }
                }
            }
            catch (Exception ex)
            {
                onExcption.Invoke(ex);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
            }
        }
    }
}
