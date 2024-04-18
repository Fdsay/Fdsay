using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OfficeOpenXml; // 添加EPPlus命名空间

namespace UID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
        }

        private int loginAttempts = 0;

        private void Button1_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            // 读取Excel文件并检查用户名和密码
            if (CheckCredentials(username, password))
            {
                Form2 secondForm = new Form2();
                secondForm.Show();
                this.Hide();
            }
            else
            {
                loginAttempts++;
                if (loginAttempts < 3)
                {
                    MessageBox.Show("账号或密码错误，请重试。您还有 " + (3 - loginAttempts) + " 次尝试机会。");
                }
                else
                {
                    TimerForm timerForm = new TimerForm();
                    timerForm.Show();
                    this.Hide();
                }
            }
        }

        private bool CheckCredentials(string username, string password)
        {
             
            // 读取Excel文件
            using (var excelPackage = new ExcelPackage(new System.IO.FileInfo("D:\\code\\C++\\UID\\users.xlsx")))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0]; // 假设用户名和密码在第一个工作表中

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // 遍历Excel表格中的数据
                for (int row = 1; row <= rowCount; row++)
                {
                    string excelUsername = worksheet.Cells[row, 1].Value?.ToString(); // 第一列是用户名
                    string excelPassword = worksheet.Cells[row, 2].Value?.ToString(); // 第二列是密码

                    if (username == excelUsername && password == excelPassword)
                    {
                        return true; // 找到匹配的用户名和密码
                    }
                }
            }

            return false; // 没有找到匹配的用户名和密码
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 ThirdForm = new Form3();
            ThirdForm.Show();
            this.Hide();
        }
    }
}
