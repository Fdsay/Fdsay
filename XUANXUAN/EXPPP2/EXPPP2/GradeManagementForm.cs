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
            UpdateExcelFromDataGridView();
        }
        private int MapDataGridViewColumnIndexToExcel(int dataGridViewColumnIndex)
        {
            // 映射规则
            // DataGridView 的第1、2、3、4、5、6、7、8列 对应 Excel 的第1、2、9、10、11、12、13、14列
            switch (dataGridViewColumnIndex)
            {
                case 0:
                case 1:
                    return dataGridViewColumnIndex + 1; // 第1和第2列直接映射到Excel的第1和第2列
                default:
                    return dataGridViewColumnIndex + 7; // 其他列需要加上偏移量6
            }
        }

        private void UpdateExcelFromDataGridView()
        {
            try
            {
                using (var excelPackage = new ExcelPackage(new FileInfo("D:\\GitHub\\Fdsay\\XUANXUAN\\EXPPP2\\EXPPP2\\students.xlsx")))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[0];

                    // 遍历 DataGridView 中的数据
                    foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
                    {
                        // 获取第一列和第二列的值
                        string name = dataGridViewRow.Cells[0].Value?.ToString();
                        string number = dataGridViewRow.Cells[1].Value?.ToString();

                        // 在 Excel 中查找对应的行
                        int rowIndex = FindRowIndexByNameAndNumber(worksheet, name, number);

                        if (rowIndex != -1)
                        {
                            // 如果找到对应的行，则更新该行的信息
                            for (int j = 0; j < dataGridViewRow.Cells.Count - 2; j++)
                            {
                                int excelColumnIndex = MapDataGridViewColumnIndexToExcel(j + 2);
                                worksheet.Cells[rowIndex + 1, excelColumnIndex].Value = dataGridViewRow.Cells[j + 2].Value?.ToString();
                            }
                        }
                        else
                        {
                            // 如果没有找到对应的行，则在 Excel 中新增一行并填入相应的信息
                            int rowCount = worksheet.Dimension.Rows;
                            worksheet.Cells[rowCount + 1, 1].Value = name;
                            worksheet.Cells[rowCount + 1, 2].Value = number;
                            for (int j = 0; j < dataGridViewRow.Cells.Count - 2; j++)
                            {
                                int excelColumnIndex = MapDataGridViewColumnIndexToExcel(j + 2);
                                worksheet.Cells[rowCount + 1, excelColumnIndex].Value = dataGridViewRow.Cells[j + 2].Value?.ToString();
                            }
                        }
                    }
                    excelPackage.Save();
                    MessageBox.Show("数据已成功保存到Excel文件。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误: " + ex.Message);
            }
        }


        private int FindRowIndexByNameAndNumber(ExcelWorksheet worksheet, string name, string number)
        {
            // 遍历 Excel 中的数据，查找匹配的行
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                if (worksheet.Cells[row, 1].Value?.ToString() == name && worksheet.Cells[row, 2].Value?.ToString() == number)
                {
                    return row - 1; // 返回行索引（从0开始）
                }
            }
            return -1; // 如果未找到匹配的行，则返回-1
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
