using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXPPP2
{
    public partial class StudentInfoForm : UserControl
    {
        public StudentInfoForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 获取文本框中的内容
            string name = textBox1.Text;
            string number = textBox2.Text;
            string classroom = textBox3.Text;
            string tele = textBox4.Text;
            // 将用户添加到Excel中
            AddUserToExcel(name, number,classroom,tele);

            // 提示用户已添加
            MessageBox.Show("User added to Excel successfully!");
        }
        private void AddUserToExcel(string name, string number,string classroom,string tele)
        {

            using (var excelPackage = new ExcelPackage(new System.IO.FileInfo("D:\\GitHub\\Fdsay\\XUANXUAN\\EXPPP2\\EXPPP2\\students.xlsx")))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                // 在下一个空行中添加用户
                worksheet.Cells[rowCount + 1, 1].Value = name;
                worksheet.Cells[rowCount + 1, 2].Value = number;
                worksheet.Cells[rowCount + 1, 3].Value = classroom;
                worksheet.Cells[rowCount + 1, 4].Value = tele;
                excelPackage.Save();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void StudentInfoForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // 获取文本框中的内容
            string name = textBox1.Text;
            string number = textBox2.Text;
            string classroom = textBox3.Text;
            string tele = textBox4.Text;
            // 将用户添加到Excel中
            AddUserToExcel(name, number, classroom, tele);

            // 提示用户已添加
            MessageBox.Show("User added to Excel successfully!");
        }
    }
}
