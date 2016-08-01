using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DTSigninRemind.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DTSigninRemind.Controllers
{
    public class SigninRemindController : Controller
    {
        private SigninRemindingContext _context;
        private Services.HttpHelper httpx = new Services.HttpHelper();
        private IOptions<ConfigOptions> settings;


        public SigninRemindController(SigninRemindingContext context, IOptions<ConfigOptions> settings)
        {
            _context = context;
            this.settings = settings;
        }

        [HttpGet]
        public System.Collections.IEnumerable GetUserContent(string userid)
        {
            var _userContent = _context.SigninReminds.Where(p => p.Userid == userid).ToList();
            return _userContent;
        }
        [HttpGet]
        public string GetUserContentUrl()
        {
            return settings.Value.Url;
        }

        public IActionResult UserContent(string userid)
        {
            ViewBag.getuserid = userid;
            return View();
        }


        public IActionResult AdvSettings()
        {
            return View();
        }

        public IActionResult StatisContent(string type)
        {

            return View(_context.SigninReminds.OrderBy(p => p.RegDateTime).ToList());
        }



        public IActionResult EChart()
        {
            return View(GetRecentPush());
        }

        public IActionResult Help()
        {

            return View("Help1");
        }

        // GET: /<controller>/
        public IActionResult Index(string userid)
        {
            try
            {
                if (Request.QueryString.HasValue)
                {
                    if (Request.Query["id"].ToString() != settings.Value.CorpID)
                    {
                        return View();
                    }
                    if (!String.IsNullOrEmpty(Request.Query["userid"].ToString()))
                    {
                        ViewBag.getuserid = Request.Query["userid"].ToString();
                        return View("UserContent");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        [HttpGet]
        public System.Collections.IEnumerable GetRecentPush()
        {
            if (_context.SigninReminds.Count() > 0)
            {
                ViewData["TotalNumber"] = _context.SigninReminds.Count();
                ViewData["RunPush"] = _context.SigninReminds.Where(p => p.IsEnable == true).Count();
                ViewData["StopPush"] = _context.SigninReminds.Where(p => p.IsEnable != true).Count();
                ViewData["EnableApp"] = _context.SigninReminds.Where(p => p.RemindMode == "App").Count();
                ViewData["EnableMsg"] = _context.SigninReminds.Where(p => p.RemindMode == "Msg").Count();
            }
            else
            {
                ViewData["TotalNumber"] = 0;
                ViewData["RunPush"] = 0;
                ViewData["StopPush"] = 0;
                ViewData["EnableApp"] = 0;
                ViewData["EnableMsg"] = 0;
            }
            if (_context.SendLogTbs.Count() > 0)
            {
                ViewData["TotalPushMsg"] = _context.SendLogTbs.Count();
                //今日推送消息
                ViewData["TodayPushMsg"] = _context.SendLogTbs.Where(p => p.SendTime.Value.Date == DateTime.Now.Date).Count();
                //昨日推送消息
                ViewData["YesterdayPushMsg"] = _context.SendLogTbs.Where(p => p.SendTime.Value.Date == DateTime.Now.AddDays(-1).Date).Count();

                ViewData["PushWorkMsg"] = _context.SendLogTbs.Where(p => p.Msgtype == "上班").Count();
                ViewData["PushOffMsg"] = _context.SendLogTbs.Where(p => p.Msgtype == "下班").Count();

                ViewData["RecentPush"] = _context.SendLogTbs.Max(p => p.SendTime).Value.ToString("yyyy-MM-dd HH:mm:ss");

                //今日App消息
                ViewData["TodayPushAppMsg"] = _context.SendLogTbs.Where(p => p.SendTime.Value.Date == DateTime.Now.Date && p.PushType == "App").Count();
                //今日Msg消息
                ViewData["TodayPushSmsMsg"] = _context.SendLogTbs.Where(p => p.SendTime.Value.Date == DateTime.Now.Date && p.PushType == "Msg").Count();

                //昨日App消息
                ViewData["YesterdayPushAppMsg"] = _context.SendLogTbs.Where(p => p.SendTime.Value.Date == DateTime.Now.AddDays(-1).Date && p.PushType == "App").Count();
                //昨日Msg消息
                ViewData["YesterdayPushSmsMsg"] = _context.SendLogTbs.Where(p => p.SendTime.Value.Date == DateTime.Now.AddDays(-1).Date && p.PushType == "Msg").Count();
            }
            else
            {
                ViewData["TotalPushMsg"] = 0;
                ViewData["YesterdayPushMsg"] = 0;
                ViewData["TodayPushMsg"] = 0;
                ViewData["PushWorkMsg"] = 0;
                ViewData["PushOffMsg"] = 0;
                ViewData["RecentPush"] = null;
                ViewData["TodayPushAppMsg"] = 0;
                ViewData["TodayPushSmsMsg"] = 0;
                ViewData["YesterdayPushAppMsg"] = 0;
                ViewData["YesterdayPushSmsMsg"] = 0;
            }

            return ViewData;
        }

        [HttpGet]
        public System.Collections.IEnumerable GetDtConfig()
        {

            Services.Jsapi_ticket.GetNewJsapi_ticket();

            string jsurl;
            if (!String.IsNullOrEmpty(Request.Query["userid"].ToString()))
            {
                ViewBag.userid = Request.Query["userid"].ToString();
                jsurl = settings.Value.Url + "&userid=" + Request.Query["userid"];
            }
            else
            {
                jsurl = settings.Value.Url;
            }


            int timestamp = Services.DingTalkEncrypt.ConvertDateTimeInt(DateTime.Now);
            string nonceStr = Services.DingTalkEncrypt.createNonceStr();

            string[] paramArr = new String[] { "jsapi_ticket=" + Services.Jsapi_ticket.GetJsapi_ticket(), "timestamp=" + timestamp, "noncestr=" + nonceStr, "url=" + jsurl };
            // 这里参数的顺序要按照 key 值 ASCII 码升序排序 
            Array.Sort(paramArr);
            // 将排序后的结果拼接成一个字符串
            string content = string.Concat(paramArr[0], "&", paramArr[1], "&", paramArr[2], "&", paramArr[3]);

            string signature = Services.DingTalkEncrypt.GetSwcSH1(content).ToLower();

            ViewData["access_token"] = Services.Token.GetToken();
            ViewData["agentId"] = settings.Value.AgentID;
            ViewData["corpId"] = settings.Value.CorpID;
            ViewData["timeStamp"] = timestamp;
            ViewData["nonceStr"] = nonceStr;
            ViewData["signature"] = signature;
            ViewData["corpName"] = settings.Value.CorpName;
            ViewData["userId"] = settings.Value.UserId;
            ViewData["url"] = settings.Value.Url;
            ViewData["userName"] = settings.Value.UserName;
            return ViewData;
        }

        [HttpGet]
        public string userinfo(string code)
        {

            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary.Add("access_token", Services.Token.GetToken());
            myDictionary.Add("code", code);
            ViewBag.info = httpx.GetJson("https://oapi.dingtalk.com/user/getuserinfo", myDictionary);

            myDictionary.Clear();


            return ViewBag.info;
        }

        [HttpGet]
        public string GetUserInfoDep(string userid)
        {

            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary.Add("access_token", Services.Token.GetToken());
            myDictionary.Add("userid", userid);
            ViewBag.GetUserInfoDep = httpx.GetJson("https://oapi.dingtalk.com/user/get", myDictionary);

            myDictionary.Clear();

            return ViewBag.GetUserInfoDep;
        }

        [HttpGet]
        public System.Collections.IEnumerable GetSigninRemind(string userid)
        {
            var reSR = _context.SigninReminds.Where(e => e.Userid == userid).ToList();
            if (reSR.Count == 0)
            {
                return null;
            }
            else
            {
                return reSR;
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SigninRemind signinremind)
        {
            if (ModelState.IsValid)
            {

                if (_context.SigninReminds.Where(m => m.Userid == signinremind.Userid).Count() > 0)//如果存在数据，则更新
                {
                    SigninRemind sr = _context.SigninReminds.Single(m => m.Userid == signinremind.Userid);
                    sr.IsEnable = signinremind.IsEnable;
                    sr.Name = signinremind.Name;
                    sr.OffTime = signinremind.OffTime;
                    sr.RemindMode = signinremind.RemindMode;
                    sr.WorkTime = signinremind.WorkTime;
                    sr._OffRemind = signinremind._OffRemind;
                    sr._WorkRemind = signinremind._WorkRemind;

                    sr.Avatar = signinremind.Avatar;
                    sr.DeviceId = signinremind.DeviceId;
                    sr.IsAdmin = signinremind.IsAdmin;
                    sr.IsBoss = signinremind.IsBoss;
                    sr.Is_sys = signinremind.Is_sys;
                    sr.Mobile = signinremind.Mobile;
                    sr.Sys_level = signinremind.Sys_level;
                    sr.Position = signinremind.Position;
                    sr.Department = signinremind.Department;
                    sr.IsLeaderInDepts = signinremind.IsLeaderInDepts;

                    _context.SaveChanges();
                }
                else
                {
                    signinremind.RegDateTime = DateTime.Now;
                    _context.SigninReminds.Add(signinremind);
                    _context.SaveChanges();
                    //通知管理员
                    Services.DTServerClass.DTSC_SendEnterpriseOaMsg sendoaMsg = new Services.DTServerClass.DTSC_SendEnterpriseOaMsg();
                    sendoaMsg.agentid = Startup.settings.AgentID;
                    sendoaMsg.msgtype = "oa";
                    sendoaMsg.touser = Startup.settings.UserId;
                    sendoaMsg.oa.head.bgcolor = "FFBBBBBB"; 
                    sendoaMsg.oa.head.text = "签到提醒";

                    sendoaMsg.oa.body.title = "新用户注册";
                    sendoaMsg.oa.body.image = "@lADOADmaWMzazQKA";
                    sendoaMsg.oa.message_url = settings.Value.Url + "&userid=" + signinremind.Userid;
                    sendoaMsg.oa.pc_message_url = settings.Value.Url + "&userid=" + signinremind.Userid;

                    sendoaMsg.oa.body.form = new Services.DTServerClass.DTSC_SendEnterpriseOaMsg.OaContent.OaBodyContent.OaBodyFormContent[5];
                    sendoaMsg.oa.body.form[0] = new Services.DTServerClass.DTSC_SendEnterpriseOaMsg.OaContent.OaBodyContent.OaBodyFormContent();
                    sendoaMsg.oa.body.form[0].key = "姓名：";
                    sendoaMsg.oa.body.form[0].value = signinremind.Name;

                    sendoaMsg.oa.body.form[1] = new Services.DTServerClass.DTSC_SendEnterpriseOaMsg.OaContent.OaBodyContent.OaBodyFormContent();
                    sendoaMsg.oa.body.form[1].key = "上班时间：";
                    sendoaMsg.oa.body.form[1].value = signinremind.WorkTime.HasValue ? signinremind.WorkTime.Value.ToString() : null;

                    sendoaMsg.oa.body.form[2] = new Services.DTServerClass.DTSC_SendEnterpriseOaMsg.OaContent.OaBodyContent.OaBodyFormContent();
                    sendoaMsg.oa.body.form[2].key = "下班时间：";
                    sendoaMsg.oa.body.form[2].value = signinremind.OffTime.HasValue ? signinremind.OffTime.Value.ToString() : null;

                    sendoaMsg.oa.body.form[3] = new Services.DTServerClass.DTSC_SendEnterpriseOaMsg.OaContent.OaBodyContent.OaBodyFormContent();
                    sendoaMsg.oa.body.form[3].key = "提醒方式：";
                    sendoaMsg.oa.body.form[3].value = signinremind.RemindMode == "App" ? "应用内" : "短信";

                    sendoaMsg.oa.body.form[4] = new Services.DTServerClass.DTSC_SendEnterpriseOaMsg.OaContent.OaBodyContent.OaBodyFormContent();
                    sendoaMsg.oa.body.form[4].key = "是否启用：";
                    sendoaMsg.oa.body.form[4].value = signinremind.IsEnable ? "是" : "否";

                    sendoaMsg.oa.body.rich.num = _context.SigninReminds.Count().ToString();
                    sendoaMsg.oa.body.rich.unit = "名";
                    sendoaMsg.oa.body.author = settings.Value.UserName;

                    Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                    myDictionary.Add("access_token", Services.Token.GetToken());

                    Services.DTServerClass.GetErrInfo getErrInfo = JsonConvert.DeserializeObject<Services.DTServerClass.GetErrInfo>(httpx.PostJson("https://oapi.dingtalk.com/message/send", JsonConvert.SerializeObject(sendoaMsg), myDictionary));


                }
                return View("Index1");
            }
            else
            {
                return View("Index2");
            }
        }

        public SigninRemindList_Count SigninRemindList(int pageIndex, int pageSize)
        {
            SigninRemindList_Count _DatasSigList = new SigninRemindList_Count();
            //数据总条数
            _DatasSigList.SigninRemindCount = _context.SigninReminds.Count();
            //分页数据
            _DatasSigList.SigninRemindList = _context.SigninReminds.Count() > 0 ? _context.SigninReminds.OrderBy(p => p.Id).ToList<SigninRemind>().OrderBy(p => p.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList<SigninRemind>() : null;
            return _DatasSigList;
        }

        public IActionResult Index3()
        {
            return View();
        }
    }
}
