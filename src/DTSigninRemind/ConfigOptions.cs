namespace DTSigninRemind
{
    public class ConfigOptions
    {
        public string CorpID { get; set; }
        public string CorpSecret { get; set; }
        public string AgentID { get; set; }
        public string CorpName { get; set; }
        /// <summary>
        /// 用于帮助页面人工服务调用电话，被叫方的钉钉userid
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 钉钉后台微应用设置中的首页地址完全一致
        /// 默认应为 http://你的网址/SigninRemind?id=你的CorpID&dd_nav_bgcolor=FF5E97F6
        /// nav_bgcolor参数为设置导航栏颜色
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 新用户注册向管理员推送oa消息的默认用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 短信平台企业ID
        /// </summary>
        public string SmsCorpID { get; set; }
        /// <summary>
        /// 短信平台用户名
        /// </summary>
        public string SmsLoginName { get; set; }
        /// <summary>
        /// 短信平台密码
        /// </summary>
        public string SmsPasswd { get; set; }
    }
}
