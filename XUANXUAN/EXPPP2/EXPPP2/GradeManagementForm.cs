using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EXPPP2
{
    public partial class GradeManagementForm : UserControl
    {
        private DataTable originalDataTable; // 存储原始数据的 DataTable

        public GradeManagementForm()
        {
            InitializeComponent();
            LoadExcelData();
            // 将DataBindingComplete事件与处理函数关联
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
        }

        private void LoadExcelData()
        {
            // 获取 Excel 文件路径
            string filePath = @"D:\GitHub\Fdsay\XUANXUAN\EXPPP2\EXPPP2\students.xlsx";

            // 创建一个 DataTable 用于存储 Excel 数据
            originalDataTable = new DataTable();

            try
            {
                // 读取 Excel 文件
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // 假设数据在第一个工作表中

                    // 设置需要读取的列
                    int[] columnsToRead = { 1, 2, 9, 10, 11, 12, 13, 14 };

                    // 设置列头
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        if (columnsToRead.Contains(col))
                        {
                            string columnName = worksheet.Cells[1, col].Value?.ToString();
                            originalDataTable.Columns.Add(columnName);
                        }
                    }

                    // 从第二行开始读取数据
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        DataRow newRow = originalDataTable.NewRow();
                        int columnIndex = 0;
                        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        {
                            if (columnsToRead.Contains(col))
                            {
                                newRow[columnIndex++] = worksheet.Cells[row, col].Value?.ToString();
                            }
                        }
                        originalDataTable.Rows.Add(newRow);
                    }
                }

                // 将 DataTable 绑定到 DataGridView
                dataGridView1.DataSource = originalDataTable;
                dataGridView1.AllowUserToAddRows = false; // 禁止添加新行
                dataGridView1.ReadOnly = false; // 允许编辑
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误: " + ex.Message);
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

        private void button2_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                // 执行模糊查询并过滤 DataGridView
                DataView dv = originalDataTable.DefaultView;
                dv.RowFilter = string.Format("{0} LIKE '%{1}%' OR {2} LIKE '%{1}%'", originalDataTable.Columns[0].ColumnName, searchText, originalDataTable.Columns[1].ColumnName);
                dataGridView1.DataSource = dv.ToTable();
            }
            else
            {
                LoadExcelData();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }


    }
}
