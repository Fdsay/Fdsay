using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace EXPPP2
{
    public partial class StudentInfoForm : UserControl
    {
        private DataTable dt; // 用于存储DataGridView的数据

        public StudentInfoForm()
        {
            InitializeComponent();
            // 刷新DataGridView显示最新数据
            RefreshDataGridView();
            // 将DataBindingComplete事件与处理函数关联
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // 获取文本框中的内容
            string name = textBox1.Text;
            string number = textBox2.Text;
            string sex = textBox3.Text;
            string nation = textBox4.Text;
            string age = textBox6.Text;
            string time = textBox7.Text;
            string classroom = textBox8.Text;
            string tele = textBox9.Text;
            // 将用户添加到Excel中
            AddUserToExcel(name, number,sex,nation,age,time, classroom, tele);

            // 提示用户已添加
            MessageBox.Show("添加成功!");
            // 刷新DataGridView显示最新数据
            RefreshDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 保存DataGridView中的修改到Excel
            UpdateExcelFromDataGridView();
            // 提示用户已更新
            MessageBox.Show("保存成功！");
            RefreshDataGridView();
            PerformFuzzySearch(textBox5.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // 进行模糊查询
            PerformFuzzySearch(textBox5.Text);
        }
        private void AddUserToExcel(string name, string number,string sex,string nation,string age,string time, string classroom, string tele)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo("D:\\GitHub\\Fdsay\\XUANXUAN\\EXPPP2\\EXPPP2\\students.xlsx")))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                // 在下一个空行中添加用户
                worksheet.Cells[rowCount + 1, 1].Value = name;
                worksheet.Cells[rowCount + 1, 2].Value = number;
                worksheet.Cells[rowCount + 1, 3].Value = sex;
                worksheet.Cells[rowCount + 1, 4].Value = nation;
                worksheet.Cells[rowCount + 1, 5].Value = age;
                worksheet.Cells[rowCount + 1, 6].Value = time;
                worksheet.Cells[rowCount + 1, 7].Value = classroom;
                worksheet.Cells[rowCount + 1, 8].Value = tele;
                excelPackage.Save();
            }
        }

        private void RefreshDataGridView()
        {
            using (var excelPackage = new ExcelPackage(new FileInfo("D:\\GitHub\\Fdsay\\XUANXUAN\\EXPPP2\\EXPPP2\\students.xlsx")))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int columnCount = 8; // 只显示前八列

                // 创建一个DataTable来存储Excel中的数据
                dt = new DataTable();
                for (int col = 1; col <= columnCount; col++)
                {
                    dt.Columns.Add(worksheet.Cells[1, col].Value?.ToString()); // 使用Excel表头作为列名
                }

                // 将Excel中的数据填充到DataTable中
                for (int row = 2; row <= rowCount; row++) // 从第二行开始遍历
                {
                    // 跳过前两列为空的行
                    if (worksheet.Cells[row, 1].Value == null && worksheet.Cells[row, 2].Value == null)
                        continue;

                    DataRow dr = dt.NewRow();
                    for (int col = 1; col <= columnCount; col++)
                    {
                        dr[col - 1] = worksheet.Cells[row, col].Value?.ToString();
                    }
                    dt.Rows.Add(dr);
                }

                // 移除已存在的删除按钮列
                var existingDeleteButtonColumn = dataGridView1.Columns["DeleteButtonColumn"];
                if (existingDeleteButtonColumn != null)
                {
                    dataGridView1.Columns.Remove(existingDeleteButtonColumn);
                }

                // 将DataTable绑定到DataGridView中
                dataGridView1.DataSource = dt;

                // 设置DataGridView的Padding为0
                dataGridView1.Padding = new Padding(0);

                // 设置RowHeadersVisible为true
                dataGridView1.RowHeadersVisible = true;
                // 设置行头列的宽度
                dataGridView1.RowHeadersWidth = 70; // 设置为适当的值，例如50像素

                // 添加删除按钮列
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                deleteButtonColumn.HeaderText = "操作";
                deleteButtonColumn.Text = "删除";
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                deleteButtonColumn.Name = "DeleteButtonColumn";
                
                
                                              // 使用单元格样式来调整按钮的大小
                deleteButtonColumn.DefaultCellStyle.Padding = new Padding(0); // 设置按钮的内边距为10像素
                dataGridView1.Columns.Add(deleteButtonColumn);

                
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // 手动添加行号到行头
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void UpdateExcelFromDataGridView()
        {
            using (var excelPackage = new ExcelPackage(new FileInfo("D:\\GitHub\\Fdsay\\XUANXUAN\\EXPPP2\\EXPPP2\\students.xlsx")))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];

                // 遍历 DataGridView 中的数据
                foreach (DataRow row in dt.Rows)
                {
                    // 获取第一列和第二列的值
                    string name = row[0].ToString();
                    string number = row[1].ToString();

                    // 在 Excel 中查找对应的行
                    int rowIndex = FindRowIndexByNameAndNumber(worksheet, name, number);

                    if (rowIndex != -1)
                    {
                        // 如果找到对应的行，则更新该行的信息
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            worksheet.Cells[rowIndex + 1, j + 1].Value = row[j].ToString();
                        }
                    }
                    else
                    {
                        // 如果没有找到对应的行，则在 Excel 中新增一行并填入相应的信息
                        int rowCount = worksheet.Dimension.Rows;
                        worksheet.Cells[rowCount + 1, 1].Value = name;
                        worksheet.Cells[rowCount + 1, 2].Value = number;
                        for (int j = 0; j < dt.Columns.Count - 2; j++)
                        {
                            worksheet.Cells[rowCount + 1, j + 3].Value = row[j + 2].ToString();
                        }
                    }
                }
                excelPackage.Save();
            }
        }

        // 根据姓名和学号查找对应行的索引
        private int FindRowIndexByNameAndNumber(ExcelWorksheet worksheet, string name, string number)
        {
            int rowCount = worksheet.Dimension.Rows;
            for (int i = 2; i <= rowCount; i++) // 从第二行开始查找，因为第一行是标题行
            {
                if (worksheet.Cells[i, 1].Value?.ToString() == name && worksheet.Cells[i, 2].Value?.ToString() == number)
                {
                    return i - 1; // 返回 Excel 中的行索引（从0开始）
                }
            }
            return -1; // 如果未找到对应行，返回-1
        }

        private void PerformFuzzySearch(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                // 如果关键字为空，直接显示所有数据
                RefreshDataGridView();
                return;
            }
            RefreshDataGridView();
            // 根据关键字进行模糊查询
            var query = dt.AsEnumerable().Where(row => row.ItemArray.Any(field => field.ToString().Contains(keyword)));

            // 创建一个新的 DataTable 来存储查询结果
            DataTable filteredTable = dt.Clone(); // 复制原始 DataTable 的结构

            // 将查询结果添加到新的 DataTable 中
            foreach (DataRow row in query)
            {
                filteredTable.ImportRow(row);
            }

            // 更新 DataGridView 显示查询结果
            dataGridView1.DataSource = filteredTable;
            dt = filteredTable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断点击的是否是删除按钮列
            if (e.ColumnIndex == dataGridView1.Columns["DeleteButtonColumn"].Index && e.RowIndex >= 0)
            {
                // 获取要删除的行
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // 获取对应行的数据
                DataRowView drv = (DataRowView)row.DataBoundItem;
                DataRow dr = drv.Row;

                // 获取要删除的行的姓名和学号
                string name = dr[0].ToString();
                string number = dr[1].ToString();

                // 从 DataTable 中删除对应行的数据
                dt.Rows.Remove(dr);

                // 从 Excel 中删除对应行
                DeleteRowFromExcel(name, number);

                // 刷新 DataGridView
                RefreshDataGridView();
            }
        }

        private void DeleteRowFromExcel(string name, string number)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo("D:\\GitHub\\Fdsay\\XUANXUAN\\EXPPP2\\EXPPP2\\students.xlsx")))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0];

                // 寻找要删除的行
                int rowIndex = FindRowIndexByNameAndNumber(worksheet, name, number);

                // 如果找到对应的行，则删除该行
                if (rowIndex != -1)
                {
                    worksheet.DeleteRow(rowIndex + 1, 1); // Excel 行索引从 1 开始，但我们的 FindRowIndexByNameAndNumber 返回的是从 0 开始的索引
                    excelPackage.Save();
                }
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

        

    }
}
