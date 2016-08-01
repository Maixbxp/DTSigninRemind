using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTSigninRemind.Models
{
    public class SigninRemindingContext : DbContext
    {

        public SigninRemindingContext(DbContextOptions<SigninRemindingContext> options)
            : base(options)
        { }
        public DbSet<SigninRemind> SigninReminds { get; set; }
        public DbSet<SendLogTb> SendLogTbs { get; set; }
        public DbSet<DailyOne> DailyOnes { get; set; }
    }
    public class SigninRemind
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 员工唯一标识ID（不可修改）
        /// </summary>
        [Required]
        public string Userid { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        [Required]
        [Display(Name ="姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 手机设备号,由钉钉在安装时随机产生
        /// </summary>
        [Display(Name = "手机设备号")]
        public string DeviceId { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        [Display(Name = "是否是管理员")]
        public bool Is_sys { get; set; }

        /// <summary>
        /// 级别，三种取值。0:非管理员 1：普通管理员 2：超级管理员
        /// </summary>
        [Display(Name = "级别")]
        public string Sys_level { get; set; }

        /// <summary>
        /// 手机号码（ISV不可见）
        /// </summary>
        [Display(Name ="手机号码")]
        public string Mobile { get; set; }

        /// <summary>
        /// 是否为企业的管理员, true表示是, false表示不是
        /// </summary>
        [Display(Name = "是否为企业的管理员")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否为企业的老板, true表示是, false表示不是
        /// </summary>
        [Display(Name = "是否为企业的老板")]
        public bool IsBoss { get; set; }

        /// <summary>
        /// 头像url
        /// </summary>
        [Display(Name = "头像url")]
        public string Avatar { get; set; }

        /// <summary>
        /// 提醒方式，App 或 Msg
        /// </summary>
        [Required]
        [Display(Name = "提醒方式")]
        public string RemindMode { get; set; }

        /// <summary>
        /// 上班时间，Time时间类型
        /// </summary>
        [Required]
        [Display(Name = "上班时间")]
        public TimeSpan? WorkTime { get; set; }

        /// <summary>
        /// 上班前5-30分钟提醒或者关闭
        /// </summary>
        [Required]
        [Display(Name = "上班提醒")]
        public Int32 WorkRemind { get; set; }

        [Required]
        public string _WorkRemind
        {
            get
            {
                switch (WorkRemind)
                {
                    case 5:
                        return "提前5分钟";
                    case 6:
                        return "提前6分钟";
                    case 7:
                        return "提前8分钟";
                    case 8:
                        return "提前8分钟";
                    case 9:
                        return "提前9分钟";
                    case 10:
                        return "提前10分钟";
                    case 11:
                        return "提前11分钟";
                    case 12:
                        return "提前12分钟";
                    case 13:
                        return "提前13分钟";
                    case 14:
                        return "提前14分钟";
                    case 15:
                        return "提前15分钟";
                    case 16:
                        return "提前16分钟";
                    case 17:
                        return "提前17分钟";
                    case 18:
                        return "提前18分钟";
                    case 19:
                        return "提前19分钟";
                    case 20:
                        return "提前20分钟";
                    case 21:
                        return "提前21分钟";
                    case 22:
                        return "提前22分钟";
                    case 23:
                        return "提前23分钟";
                    case 24:
                        return "提前24分钟";
                    case 25:
                        return "提前25分钟";
                    case 26:
                        return "提前26分钟";
                    case 27:
                        return "提前27分钟";
                    case 28:
                        return "提前28分钟";
                    case 29:
                        return "提前29分钟";
                    case 30:
                        return "提前30分钟";
                    default:
                        return "关闭";
                }
            }
            set
            {
                switch (value)
                {
                    case "提前5分钟":
                        WorkRemind = 5;
                        break;
                    case "提前6分钟":
                        WorkRemind = 6;
                        break;
                    case "提前7分钟":
                        WorkRemind = 7;
                        break;
                    case "提前8分钟":
                        WorkRemind = 8;
                        break;
                    case "提前9分钟":
                        WorkRemind = 9;
                        break;
                    case "提前10分钟":
                        WorkRemind = 10;
                        break;
                    case "提前11分钟":
                        WorkRemind = 11;
                        break;
                    case "提前12分钟":
                        WorkRemind = 12;
                        break;
                    case "提前13分钟":
                        WorkRemind = 13;
                        break;
                    case "提前14分钟":
                        WorkRemind = 14;
                        break;
                    case "提前15分钟":
                        WorkRemind = 15;
                        break;
                    case "提前16分钟":
                        WorkRemind = 16;
                        break;
                    case "提前17分钟":
                        WorkRemind = 17;
                        break;
                    case "提前18分钟":
                        WorkRemind = 18;
                        break;
                    case "提前19分钟":
                        WorkRemind = 19;
                        break;
                    case "提前20分钟":
                        WorkRemind = 20;
                        break;
                    case "提前21分钟":
                        WorkRemind = 21;
                        break;
                    case "提前22分钟":
                        WorkRemind = 22;
                        break;
                    case "提前23分钟":
                        WorkRemind = 23;
                        break;
                    case "提前24分钟":
                        WorkRemind = 24;
                        break;
                    case "提前25分钟":
                        WorkRemind = 25;
                        break;
                    case "提前26分钟":
                        WorkRemind = 26;
                        break;
                    case "提前27分钟":
                        WorkRemind = 27;
                        break;
                    case "提前28分钟":
                        WorkRemind = 28;
                        break;
                    case "提前29分钟":
                        WorkRemind = 29;
                        break;
                    case "提前30分钟":
                        WorkRemind = 30;
                        break;
                    default: WorkRemind = 0;
                        break;
                }
            }
        }

        [Required]
        [Display(Name = "下班时间")]
        public TimeSpan? OffTime { get; set; }

        /// <summary>
        /// 下班后5-30分钟提醒或者关闭
        /// </summary>
        [Required]
        [Display(Name = "下班提醒")]
        public Int32 OffRemind { get; set; }

        [Required]
        public string _OffRemind
        {
            get
            {
                switch (OffRemind)
                {
                    case 5:
                        return "延迟5分钟";
                    case 6:
                        return "延迟6分钟";
                    case 7:
                        return "延迟8分钟";
                    case 8:
                        return "延迟8分钟";
                    case 9:
                        return "延迟9分钟";
                    case 10:
                        return "延迟10分钟";
                    case 11:
                        return "延迟11分钟";
                    case 12:
                        return "延迟12分钟";
                    case 13:
                        return "延迟13分钟";
                    case 14:
                        return "延迟14分钟";
                    case 15:
                        return "延迟15分钟";
                    case 16:
                        return "延迟16分钟";
                    case 17:
                        return "延迟17分钟";
                    case 18:
                        return "延迟18分钟";
                    case 19:
                        return "延迟19分钟";
                    case 20:
                        return "延迟20分钟";
                    case 21:
                        return "延迟21分钟";
                    case 22:
                        return "延迟22分钟";
                    case 23:
                        return "延迟23分钟";
                    case 24:
                        return "延迟24分钟";
                    case 25:
                        return "延迟25分钟";
                    case 26:
                        return "延迟26分钟";
                    case 27:
                        return "延迟27分钟";
                    case 28:
                        return "延迟28分钟";
                    case 29:
                        return "延迟29分钟";
                    case 30:
                        return "延迟30分钟";
                    default:
                        return "关闭";
                }
            }
            set
            {
                switch (value)
                {
                    case "延迟5分钟":
                        OffRemind = 5;
                        break;
                    case "延迟6分钟":
                        OffRemind = 6;
                        break;
                    case "延迟7分钟":
                        OffRemind = 7;
                        break;
                    case "延迟8分钟":
                        OffRemind = 8;
                        break;
                    case "延迟9分钟":
                        OffRemind = 9;
                        break;
                    case "延迟10分钟":
                        OffRemind = 10;
                        break;
                    case "延迟11分钟":
                        OffRemind = 11;
                        break;
                    case "延迟12分钟":
                        OffRemind = 12;
                        break;
                    case "延迟13分钟":
                        OffRemind = 13;
                        break;
                    case "延迟14分钟":
                        OffRemind = 14;
                        break;
                    case "延迟15分钟":
                        OffRemind = 15;
                        break;
                    case "延迟16分钟":
                        OffRemind = 16;
                        break;
                    case "延迟17分钟":
                        OffRemind = 17;
                        break;
                    case "延迟18分钟":
                        OffRemind = 18;
                        break;
                    case "延迟19分钟":
                        OffRemind = 19;
                        break;
                    case "延迟20分钟":
                        OffRemind = 20;
                        break;
                    case "延迟21分钟":
                        OffRemind = 21;
                        break;
                    case "延迟22分钟":
                        OffRemind = 22;
                        break;
                    case "延迟23分钟":
                        OffRemind = 23;
                        break;
                    case "延迟24分钟":
                        OffRemind = 24;
                        break;
                    case "延迟25分钟":
                        OffRemind = 25;
                        break;
                    case "延迟26分钟":
                        OffRemind = 26;
                        break;
                    case "延迟27分钟":
                        OffRemind = 27;
                        break;
                    case "延迟28分钟":
                        OffRemind = 28;
                        break;
                    case "延迟29分钟":
                        OffRemind = 29;
                        break;
                    case "延迟30分钟":
                        OffRemind = 30;
                        break;
                    default:
                        WorkRemind = 0;
                        break;
                }
            }
        }

        /// <summary>
        /// 是否启用提醒
        /// </summary>
        [Required]
        [Display(Name = "启用")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [Display(Name = "注册时间")]
        public DateTime? RegDateTime { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [Display(Name ="职位")]
        public string Position { get; set; }

        /// <summary>
        /// 所属部门id列表
        /// </summary>
        [Display(Name = "所属部门列表")]
        public string Department { get; set; }

        /// <summary>
        /// 在对应的部门中是否为主管, Map结构的json字符串, key是部门的Id, value是人员在这个部门中是否为主管, true表示是, false表示不是
        /// </summary>
        [Display(Name = "是否对应部门主管")]
        public string IsLeaderInDepts { get; set; }
    }

    public class SendLogTb
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 员工在企业内的UserID
        /// </summary>
        [Required]
        public string Userid { get; set; }

        /// <summary>
        /// 手机号码（ISV不可见）
        /// </summary>
        [Display(Name = "手机号码")]
        public string Mobile { get; set; }

        /// <summary>
        /// CorpID是企业在钉钉中的标识，每个企业拥有一个唯一的CorpID；
        /// </summary>
        [Required]
        [Display(Name ="企业ID")]
        public string CorpID { get; set; }

        /// <summary>
        /// 上班 或 下班
        /// </summary>
        [Required]
        [Display(Name ="消息类型")]
        public string Msgtype { get; set; }

        /// <summary>
        /// App 或 Msg
        /// </summary>
        [Required]
        [Display(Name =("推送方式"))]
        public string PushType { get; set; }

        [Required]
        [Display(Name = "发送内容")]
        public string Content { get; set; }

        [Display(Name = "发送时间")]
        public DateTime? SendTime { get; set; }

        [Display(Name = "发送状态")]
        public bool? SendState { get; set; }

    }

    public class DailyOne
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20,MinimumLength =5,ErrorMessage ="最大允许20个字")]
        [Display(Name ="内容")]
        public string Conent { get; set; }

        [Required]
        [Display(Name ="类型")]
        public string DOtype { get; set; }

        [Required]
        [Display(Name = "创建人")]
        public string CreateBy { get; set; }

        [Required]
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }


        [Display(Name = "修改人")]
        public string UpdateBy { get; set; }


        [Display(Name = "修改时间")]
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// 将获得全部数据跟总条数
    /// </summary>
    public class SigninRemindList_Count
    {
        /// <summary>
        /// 全部数据
        /// </summary>
        public List<SigninRemind> SigninRemindList { get; set; }
        /// <summary>
        /// 数据的总条数
        /// </summary>
        public int SigninRemindCount { get; set; }
    }
    /// <summary>
    /// 分布参数
    /// </summary>
    public class SigninRemind_FY:SigninRemind
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

}
