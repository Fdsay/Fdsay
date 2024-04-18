using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UID
{
    public partial class TimerForm : Form
    {
        private int countdown = 180; // 3 minutes
        private Timer timer;

        public TimerForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countdown--;
            if (countdown <= 0)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
                timer.Stop();
            }
            else
            {
                label1.Text = "请等待 " + (countdown / 60) + " 分钟 " + (countdown % 60) + " 秒后再尝试登录。";
            }
        }

        private void TimerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
