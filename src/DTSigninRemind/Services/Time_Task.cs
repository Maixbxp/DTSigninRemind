namespace DTSigninRemind.Services
{
    public class Time_Task
    {
        public event System.Timers.ElapsedEventHandler ExecuteTask;

        private static readonly Time_Task _task = null;
        private System.Timers.Timer _timer = null;


        //定义时间
        private int _interval = 1000;
        public int Interval
        {
            set
            {
                _interval = value;
            }
            get
            {
                return _interval;
            }
        }

        static Time_Task()
        {
            _task = new Time_Task();
        }

        public static Time_Task Instance()
        {
            return _task;
        }

        //开始
        public void Start()
        {
            if (_timer == null)
            {
                _timer = new System.Timers.Timer(_interval);
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                _timer.Enabled = true;
                _timer.Start();
            }
        }

        protected void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (null != ExecuteTask)
            {
                ExecuteTask(sender, e);
            }
        }

        //停止
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

    }

}
