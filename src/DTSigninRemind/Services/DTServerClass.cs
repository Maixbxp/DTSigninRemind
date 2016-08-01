using System;

namespace DTSigninRemind.Services
{
    public class DTServerClass
    {
        public enum DTRemindMode
        {
            App,
            Msg
        }

        public class GetErrInfo
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string invaliduser { get; set; }
            public string invalidparty { get; set; }
            public string messageId { get; set; }

        }
        public class CodeGetUserInfo
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string userid { get; set; }
            public string deviceId { get; set; }
            public string is_sys { get; set; }
            public string sys_level { get; set; }
        }

        public class GetUserDetail
        {
            public int errocde { get; set; }
            public string errmsg { get; set; }
            public string userid { get; set; }
            public string name { get; set; }
            public string tel { get; set; }
            public string workPlace { get; set; }
            public string remark { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public Boolean active { get; set; }
            public string orderInDepts { get; set; }
            public Boolean isAdmin { get; set; }
            public Boolean isBoss { get; set; }
            public string dingId { get; set; }
            public string isLeaderInDepts
            {
                get; set;
            }
            public Boolean isHide { get; set; }
            public int[] department { get; set; }
            public string position { get; set; }
            public string avatar { get; set; }
            public string jobnumber { get; set; }

        }

        public class Getsimplelist
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public bool hasMore { get; set; }

            public Userlist[] userlist;
            public class Userlist
            {
                public string userid { get; set; }
                public string name { get; set; }
            }
        }

        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        public class GetTicket
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string ticket { get; set; }
            public int expires_in { get; set; }
        }

        /// <summary>
        /// 发送企业消息头
        /// </summary>
        public class DTSC_SendMsgHeader
        {
            private string Touser;
            private string Toparty;
            private string Agentid;
            private string Msgtype;
            /// <summary>
            /// 员工ID列表（消息接收者，多个接收者用’ | '分隔）。特殊情况：指定为@all，则向该企业应用的全部成员发送
            /// </summary>
            public string touser { get { return Touser; } set { Touser = value; } }
            /// <summary>
            /// 部门id列表，多个接收者用’ | '分隔。当touser为@all时忽略本参数 touser或者toparty 二者有一个必填
            /// </summary>
            public string toparty { get { return Toparty; } set { Toparty = value; } }
            /// <summary>
            /// 企业应用id，这个值代表以哪个应用的名义发送消息
            /// </summary>
            public string agentid { get { return Agentid; } set { Agentid = value; } }
            /// <summary>
            /// 消息类型:text|image|voice|file|link|oa
            /// </summary>
            public string msgtype { get { return Msgtype; } set { Msgtype = value; } }
        }

        /// <summary>
        /// 发送企业text消息
        /// </summary>
        public class DTSC_SendEnterpriseTextMsg : DTSC_SendMsgHeader
        {

            public Content text = new Content();
            public class Content
            {
                private string Text;
                /// <summary>
                /// 消息内容
                /// </summary>
                public string content { get { return Text; } set { Text = value; } }
            }
        }

        /// <summary>
        /// 发送企业image消息
        /// </summary>
        public class DTSC_SendEnterpriseImageMsg : DTSC_SendMsgHeader
        {
            public Content image = new Content();
            public class Content
            {
                private string Image;
                /// <summary>
                /// 图片媒体文件id，可以调用上传媒体文件接口获取。建议宽600像素 x 400像素，宽高比3：2
                /// </summary>
                public string media_id { get { return Image; } set { Image = value; } }
            }
        }

        /// <summary>
        /// 发送企业voice消息
        /// </summary>
        public class DTSC_SendEnterpriseVoiceMsg : DTSC_SendMsgHeader
        {
            public Content voice = new Content();
            public class Content
            {
                private string Voice;
                /// <summary>
                /// 语音媒体文件id，可以调用上传媒体文件接口获取。2MB，播放长度不超过60s，AMR格式
                /// </summary>
                public string media_id { get { return Voice; } set { Voice = value; } }
            }
        }

        /// <summary>
        /// 发送企业file消息
        /// </summary>
        public class DTSC_SendEnterpriseFileMsg : DTSC_SendMsgHeader
        {
            public Content file = new Content();
            public class Content
            {
                private string File;
                /// <summary>
                /// 媒体文件id，可以调用上传媒体文件接口获取。10MB
                /// </summary>
                public string media_id { get { return File; } set { File = value; } }
            }
        }

        /// <summary>
        /// 发送企业link消息
        /// </summary>
        public class DTSC_SendEnterpriseLinkMsg : DTSC_SendMsgHeader
        {
            public Content link = new Content();
            public class Content
            {
                private string MessageUrl;
                private string PicUrl;
                private string Title;
                private string Text;
                /// <summary>
                /// 消息点击链接地址
                /// </summary>
                public string messageUrl { get { return MessageUrl; } set { MessageUrl = value; } }
                /// <summary>
                /// 图片媒体文件id，可以调用上传媒体文件接口获取
                /// </summary>
                public string picUrl { get { return PicUrl; } set { PicUrl = value; } }
                /// <summary>
                /// 消息标题
                /// </summary>
                public string title { get { return Title; } set { Title = value; } }
                /// <summary>
                /// 消息描述
                /// </summary>
                public string text { get { return Text; } set { Text = value; } }
            }
        }

        /// <summary>
        /// 发送企业oa消息
        /// </summary>
        public class DTSC_SendEnterpriseOaMsg : DTSC_SendMsgHeader
        {

            public OaContent oa = new OaContent();
            public class OaContent
            {
                private string Message_url;
                /// <summary>
                /// 客户端点击消息时跳转到的H5地址
                /// </summary>
                public string message_url { get { return Message_url; } set { Message_url = value; } }

                private string Pc_message_url;
                /// <summary>
                /// PC端点击消息时跳转到的H5地址
                /// </summary>
                public string pc_message_url { get { return Pc_message_url; } set { Pc_message_url = value; } }

                /// <summary>
                /// 消息头部内容
                /// </summary>

                public OaHeadContent head = new OaHeadContent();

                public class OaHeadContent
                {
                    private string Bgcolor;
                    /// <summary>
                    /// 消息头部的背景颜色。长度限制为8个英文字符，其中前2为表示透明度，后6位表示颜色值。不要添加0x
                    /// </summary>
                    public string bgcolor { get { return Bgcolor; } set { Bgcolor = value; } }
                    private string Text;
                    /// <summary>
                    /// 消息的头部标题
                    /// </summary>
                    public string text { get { return Text; } set { Text = value; } }
                }
                /// <summary>
                /// 消息体
                /// </summary>
                public OaBodyContent body = new OaBodyContent();
                public class OaBodyContent
                {
                    /// <summary>
                    /// 消息体的标题
                    /// </summary>
                    public string title { get; set; }
                    /// <summary>
                    /// 消息体的表单，最多显示6个，超过会被隐藏
                    /// </summary>
                    public OaBodyFormContent[] form;
                    /// <summary>
                    /// 单行富文本信息
                    /// </summary>
                    public OaBodyRichContent rich = new OaBodyRichContent();
                    private string Content;
                    /// <summary>
                    /// 消息体的内容,最多显示3行
                    /// </summary>
                    public string content { get { return Content; } set { Content = value; } }
                    private string Image;
                    /// <summary>
                    /// 消息体中的图片media_id
                    /// </summary>
                    public string image { get { return Image; } set { Image = value; } }
                    private string File_count;
                    /// <summary>
                    /// 自定义的附件数目。此数字仅供显示，钉钉不作验证
                    /// </summary>
                    public string file_count { get { return File_count; } set { File_count = value; } }
                    private string Author;
                    /// <summary>
                    /// 自定义的作者名字
                    /// </summary>
                    public string author { get { return Author; } set { Author = value; } }
                    public class OaBodyFormContent
                    {
                        private string Key;
                        /// <summary>
                        /// 消息体的关键字
                        /// </summary>
                        public string key { get { return Key; } set { Key = value; } }
                        private string Value;
                        /// <summary>
                        /// 消息体的关键字对应的值
                        /// </summary>
                        public string value { get { return Value; } set { Value = value; } }
                    }
                    public class OaBodyRichContent
                    {
                        private string Num;
                        /// <summary>
                        /// 单行富文本信息的数目
                        /// </summary>
                        public string num { get { return Num; } set { Num = value; } }
                        private string Unit;
                        /// <summary>
                        /// 单行富文本信息的单位
                        /// </summary>
                        public string unit { get { return Unit; } set { Unit = value; } }
                    }
                }


            }
        }

    }
}
