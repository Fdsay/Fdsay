using OfficeOpenXml;
using System;

using System.ComponentModel;

using System.Windows.Forms;


namespace EXPPP2

{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string confirmPassword = textBox3.Text;

            if (password == confirmPassword)
            {

                // 密码匹配，将账号和密码存储到Excel表中
                AddUserToExcel(username, password);
                MessageBox.Show("注册成功！");
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("密码和确认密码不匹配，请重新输入。");
            }
        }
        private void AddUserToExcel(string username, string password)
        {

            using (var excelPackage = new ExcelPackage(new System.IO.FileInfo("D:\\GitHub\\Fdsay\\XUANXUAN\\EXPPP2\\EXPPP2\\users.xlsx")))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                // 在下一个空行中添加用户
                worksheet.Cells[rowCount + 1, 1].Value = username;
                worksheet.Cells[rowCount + 1, 2].Value = password;

                excelPackage.Save();
            }
        }

    }
}
