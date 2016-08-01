using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DTSigninRemind.Services
{
    public class Token
    {

        public static Token _Token;
        public string access_token { get; set; }
        public static int expires_in = 7200;//默认为7200秒

        private DateTime createTokenTimme = DateTime.Now;

        /// <summary>
        /// 到期时间(防止时间差，提前1分钟到期)
        /// </summary>
        public DateTime TookenOverdueTime
        {
            get { return createTokenTimme.AddSeconds(expires_in - 60); }
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public static void Renovate()
        {
            if (_Token == null)
            {
                GetNewToken();
            }
            Token._Token.createTokenTimme = DateTime.Now;
        }

        public static bool IsTimeOut()
        {
            if (_Token == null)
            {
                GetNewToken();
            }
            return DateTime.Now >= Token._Token.TookenOverdueTime;
        }
        public static Token GetNewToken()
        {
            try
            {
                HttpHelper http = new HttpHelper();
                string strulr = "https://oapi.dingtalk.com/gettoken";
                Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                myDictionary.Add("corpid", Startup.settings.CorpID);
                myDictionary.Add("corpsecret", Startup.settings.CorpSecret);
                string respone = http.GetJson(strulr, myDictionary);

                var token = JsonConvert.DeserializeObject<Token>(respone);
                Token._Token = token;

                strulr = string.Empty;
                myDictionary.Clear();
                respone = string.Empty;
                http = null;
                return token;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static string GetToken()
        {
            if (_Token == null)
            {
                GetNewToken();
            }
            try
            {
                return _Token.access_token;
            }
            catch (Exception)
            {

                GetNewToken();
                return _Token.access_token;
            }

        }
    }

    public class Jsapi_ticket
    {

        public static Jsapi_ticket _Jsapi_ticket;
        public string ticket { get; set; }
        public int expires_in { get; set; }//正常情况下有效期为7200秒

        private DateTime createTokenTimme = DateTime.Now;
        /// <summary>
        /// 到期时间(防止时间差，提前1分钟到期)
        /// </summary>
        public DateTime TookenOverdueTime
        {
            get { return createTokenTimme.AddSeconds(expires_in - 60); }
        }
        /// <summary>
        /// 刷新Jsapi_ticket
        /// </summary>
        public static void Renovate()
        {
            if (_Jsapi_ticket == null)
            {
                GetNewJsapi_ticket();
            }
            Jsapi_ticket._Jsapi_ticket.createTokenTimme = DateTime.Now;
        }

        public static Jsapi_ticket GetNewJsapi_ticket()
        {
            HttpHelper httpx = new HttpHelper();
            //if(Token.IsTimeOut())
            //{
            //    Token.GetNewToken();
            //}
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary.Add("access_token", Services.Token.GetToken());
            var info1 = httpx.GetJson("https://oapi.dingtalk.com/get_jsapi_ticket", myDictionary);
            Jsapi_ticket._Jsapi_ticket = JsonConvert.DeserializeObject<Jsapi_ticket>(info1);

            httpx = null;
            myDictionary.Clear();
            info1 = null;
            return Jsapi_ticket._Jsapi_ticket;
        }

        public static bool IsTimeOut()
        {
            if (_Jsapi_ticket == null)
            {
                GetNewJsapi_ticket();
            }
            return DateTime.Now >= Jsapi_ticket._Jsapi_ticket.TookenOverdueTime;
        }
        public static string GetJsapi_ticket()
        {
            if (_Jsapi_ticket == null)
            {
                GetNewJsapi_ticket();
            }
            return _Jsapi_ticket.ticket;
        }

    }
}
