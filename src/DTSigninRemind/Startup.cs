using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DTSigninRemind.Models;
using Microsoft.EntityFrameworkCore;
using System.Timers;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace DTSigninRemind
{
    public class Startup
    {
        private SigninRemindingContext _context;

        public static ConfigOptions settings;

        private Services.HttpHelper httpx = new Services.HttpHelper();
        public Startup(IHostingEnvironment env)
        {

            Services.Time_Task.Instance().ExecuteTask += new System.Timers.ElapsedEventHandler(Global_ExecuteTask);
            Services.Time_Task.Instance().Interval = 1000 * 60;//表示间隔:默认60秒
            Services.Time_Task.Instance().Start();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private void Global_ExecuteTask(object sender, ElapsedEventArgs e)
        {
            DateTime dtm = DateTime.Now;

            Services.Token.GetNewToken();


            TimeSpan dtmsp = TimeSpan.Parse(string.Format("{0}:{1}:{2}", dtm.TimeOfDay.Hours, dtm.TimeOfDay.Minutes, 00));
            if (_context != null)
            {

                var workData = _context.SigninReminds.Where(m => m.IsEnable == true && m.WorkRemind > 0 && TimeSpan.FromMinutes(m.WorkTime.Value.TotalMinutes - m.WorkRemind) == dtmsp).ToList();
                var offData = _context.SigninReminds.Where(m => m.IsEnable == true && m.OffRemind > 0 && TimeSpan.FromMinutes(m.OffTime.Value.TotalMinutes + m.OffRemind) == dtmsp).ToList();

                string _mobile = string.Empty;
                string _userid = string.Empty;
                Dictionary<string, string> myDictionary;

                if (workData.Count > 0)//开始上班提醒代码
                {
                    var appworkData = workData.Where(p => p.RemindMode == "App");
                    if (appworkData.Count() > 0)
                    {
                        _mobile = string.Empty;

                        Services.DTServerClass.DTSC_SendEnterpriseTextMsg sendtextMsg = new Services.DTServerClass.DTSC_SendEnterpriseTextMsg();
                        sendtextMsg.agentid = settings.AgentID;
                        sendtextMsg.msgtype = "text";
                        sendtextMsg.text.content = String.Format("新的一天，从良好的工作习惯开始。快要上班了，赶快签个到吧！");

                        foreach (var item in appworkData)
                        {
                            if (!string.IsNullOrEmpty(item.Userid))
                            {
                                if (string.IsNullOrEmpty(sendtextMsg.touser))
                                {
                                    sendtextMsg.touser = item.Userid;
                                    _mobile = item.Mobile;
                                }
                                else
                                {
                                    sendtextMsg.touser = string.Format("{0}|{1}", sendtextMsg.touser, item.Userid);
                                    _mobile = string.Format("{0}|{1}", _mobile, item.Mobile);
                                }
                            }
                        }
                        myDictionary = new Dictionary<string, string>();
                        myDictionary.Add("access_token", Services.Token.GetToken());

                        Services.DTServerClass.GetErrInfo getErrInfo = JsonConvert.DeserializeObject<Services.DTServerClass.GetErrInfo>(httpx.PostJson("https://oapi.dingtalk.com/message/send", JsonConvert.SerializeObject(sendtextMsg), myDictionary));
                        if (getErrInfo.errmsg == "ok")
                        {
                            _context.SendLogTbs.Add(new SendLogTb() { CorpID = settings.CorpID, PushType = "App", Mobile = _mobile, Msgtype = "上班", Content = sendtextMsg.text.content, SendState = true, SendTime = DateTime.Now, Userid = sendtextMsg.touser });
                            _context.SaveChanges();
                        }
                        myDictionary.Clear();
                        getErrInfo = null;
                        sendtextMsg = null;
                    }
                    //---------短信发送代码开始------------

                    var msgworkData = workData.Where(p => p.RemindMode == "Msg");
                    if (msgworkData.Count() > 0)
                    {
                        _mobile = string.Empty;
                        _userid = string.Empty;
                        foreach (var item in msgworkData)
                        {
                            if (!string.IsNullOrEmpty(item.Userid) && !string.IsNullOrEmpty(item.Mobile))
                            {
                                if (string.IsNullOrEmpty(_mobile))
                                {
                                    _mobile = item.Mobile;
                                    _userid = item.Userid;
                                }
                                else
                                {
                                    _mobile = string.Format("{0};{1}", _mobile, item.Mobile);
                                    _userid = string.Format("{0};{1}", _userid, item.Userid);
                                }
                            }

                        }
                        myDictionary = new Dictionary<string, string>();
                        //企业ID
                        myDictionary.Add("CorpID", settings.SmsCorpID);
                        //登录帐号
                        myDictionary.Add("LoginName", settings.SmsLoginName);
                        //登录密码
                        myDictionary.Add("Passwd", settings.SmsPasswd);
                        //手机号
                        myDictionary.Add("send_no", _mobile);
                        //短信平台中文发送一般会乱码，所以要将中文转为GBK格式
                        myDictionary.Add("msg", HttpUtility.UrlEncode("新的一天，从良好的工作习惯开始。快要上班了，赶快签个到吧！", Encoding.GetEncoding("GBK")));
                        var remsg = httpx.GetJson("http://sms.mobset.com/SDK/Sms_Send.asp", myDictionary);
                        if (remsg != null)
                        {
                            //短信平台第一位返回的是数字，大于0代表发送成功
                            if (remsg[0] > 0)
                            {
                                //发送成功后添加到日志
                                _context.SendLogTbs.Add(new SendLogTb() { CorpID = settings.CorpID, PushType = "Msg", Mobile = _mobile, Msgtype = "上班", Content = "新的一天，从良好的工作习惯开始。快要上班了，赶快签个到吧！", SendState = true, SendTime = DateTime.Now, Userid = _userid });
                                _context.SaveChanges();

                            }
                        }


                        myDictionary.Clear();
                    }
                    //--------短信发送代码结束-----------
                    msgworkData = null;
                    _mobile = string.Empty;
                    _userid = string.Empty;
                    appworkData = null;
                }
                if (offData.Count > 0)//开始下班提醒代码
                {
                    var appoffData = offData.Where(p => p.RemindMode == "App");
                    if (appoffData.Count() > 0)
                    {
                        _mobile = string.Empty;

                        Services.DTServerClass.DTSC_SendEnterpriseTextMsg sendtextMsg = new Services.DTServerClass.DTSC_SendEnterpriseTextMsg();
                        sendtextMsg.agentid = settings.AgentID;
                        sendtextMsg.msgtype = "text";
                        sendtextMsg.text.content = String.Format("今天太宝贵，但它一去不复返！已经下班了，赶快签个到吧！");

                        foreach (var item in appoffData)
                        {
                            if (!string.IsNullOrEmpty(item.Userid))
                            {
                                if (string.IsNullOrEmpty(sendtextMsg.touser))
                                {
                                    sendtextMsg.touser = item.Userid;
                                    _mobile = item.Mobile;
                                }
                                else
                                {
                                    sendtextMsg.touser = string.Format("{0}|{1}", sendtextMsg.touser, item.Userid);
                                    _mobile = string.Format("{0}|{1}", _mobile, item.Mobile);
                                }
                            }
                        }
                        myDictionary = new Dictionary<string, string>();
                        myDictionary.Add("access_token", Services.Token.GetToken());

                        Services.DTServerClass.GetErrInfo getErrInfo = JsonConvert.DeserializeObject<Services.DTServerClass.GetErrInfo>(httpx.PostJson("https://oapi.dingtalk.com/message/send", JsonConvert.SerializeObject(sendtextMsg), myDictionary));
                        if (getErrInfo.errmsg == "ok")
                        {
                            _context.SendLogTbs.Add(new SendLogTb() { CorpID = settings.CorpID, PushType = "App", Mobile = _mobile, Msgtype = "下班", Content = sendtextMsg.text.content, SendState = true, SendTime = DateTime.Now, Userid = sendtextMsg.touser });
                            _context.SaveChanges();
                        }
                        myDictionary.Clear();
                        getErrInfo = null;
                        sendtextMsg = null;
                    }
                    appoffData = null;

                    //---------短信发送代码开始------------

                    var msgoffData = offData.Where(p => p.RemindMode == "Msg");
                    if (msgoffData.Count() > 0)
                    {
                        _mobile = string.Empty;
                        _userid = string.Empty;
                        foreach (var item in msgoffData)
                        {
                            if (!string.IsNullOrEmpty(item.Userid) && !string.IsNullOrEmpty(item.Mobile))
                            {
                                if (string.IsNullOrEmpty(_mobile))
                                {
                                    _mobile = item.Mobile;
                                    _userid = item.Userid;
                                }
                                else
                                {
                                    _mobile = string.Format("{0};{1}", _mobile, item.Mobile);
                                    _userid = string.Format("{0};{1}", _userid, item.Userid);
                                }
                            }
                        }
                        myDictionary = new Dictionary<string, string>();
                        myDictionary.Add("CorpID", settings.SmsCorpID);
                        myDictionary.Add("LoginName", settings.SmsLoginName);
                        myDictionary.Add("Passwd", settings.SmsPasswd);
                        myDictionary.Add("send_no", _mobile);
                        myDictionary.Add("msg", HttpUtility.UrlEncode("今天太宝贵，但它一去不复返！已经下班了，赶快签个到吧！", Encoding.GetEncoding("GBK")));
                        var remsg = httpx.GetJson("http://sms.mobset.com/SDK/Sms_Send.asp", myDictionary);
                        if (remsg != null)
                        {
                            if (remsg[0] > 0)
                            {
                                _context.SendLogTbs.Add(new SendLogTb() { CorpID = settings.CorpID, PushType = "Msg", Mobile = _mobile, Msgtype = "下班", Content = "今天太宝贵，但它一去不复返！已经下班了，赶快签个到吧！", SendState = true, SendTime = DateTime.Now, Userid = _userid });
                                _context.SaveChanges();

                            }
                        }


                        myDictionary.Clear();
                    }
                    //--------短信发送代码结束-----------
                    msgoffData = null;
                    _mobile = string.Empty;
                    _userid = string.Empty;
                }
                workData = null;
                offData = null;
            }

        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalMinutes;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<SigninRemindingContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
    );

            services.AddMvc();

            services.AddOptions();

            services.Configure<ConfigOptions>(Configuration.GetSection("DingTalkSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            _context = app.ApplicationServices.GetService<SigninRemindingContext>();
            settings = new ConfigOptions();
            settings.AgentID = Configuration.GetSection("DingTalkSettings").GetValue<string>("AgentID");
            settings.CorpID = Configuration.GetSection("DingTalkSettings").GetValue<string>("CorpID");
            settings.CorpName = Configuration.GetSection("DingTalkSettings").GetValue<string>("CorpName");
            settings.CorpSecret = Configuration.GetSection("DingTalkSettings").GetValue<string>("CorpSecret");
            settings.UserId = Configuration.GetSection("DingTalkSettings").GetValue<string>("UserId");
            settings.Url = Configuration.GetSection("DingTalkSettings").GetValue<string>("Url");
            settings.UserName = Configuration.GetSection("DingTalkSettings").GetValue<string>("UserName");
            settings.SmsCorpID = Configuration.GetSection("DingTalkSettings").GetValue<string>("SmsCorpID");
            settings.SmsLoginName = Configuration.GetSection("DingTalkSettings").GetValue<string>("SmsLoginName");
            settings.SmsPasswd = Configuration.GetSection("DingTalkSettings").GetValue<string>("SmsPasswd");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
