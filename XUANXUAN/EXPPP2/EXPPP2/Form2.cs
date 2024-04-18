using System;
using System.Windows.Forms;



namespace EXPPP2
{
    public partial class Form2 : Form
    {
        private StudentInfoForm studentInfoForm;
        private GradeManagementForm gradeManagementForm;
        private CourseManagementForm courseManagementForm;

        public Form2()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            // 创建对应的子页面
            studentInfoForm = new StudentInfoForm();
            gradeManagementForm = new GradeManagementForm();
            courseManagementForm = new CourseManagementForm();
            // 在splitContainer1_Panel2_Paint中默认显示学生信息管理页面
            ShowStudentInfoForm();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 点击学生信息管理按钮时显示学生信息管理页面
            ShowStudentInfoForm();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            // 点击成绩管理按钮时显示成绩管理页面
            ShowGradeManagementForm();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // 点击课程管理按钮时显示课程管理页面
            ShowCourseManagementForm();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            // 点击退出按钮时退出程序
            Application.Exit();
        }
        private void ShowStudentInfoForm()
        {
            // 在splitContainer1_Panel2_Paint中显示学生信息管理页面
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(studentInfoForm);
            studentInfoForm.Dock = DockStyle.Fill;
            studentInfoForm.Show();
        }
        private void ShowGradeManagementForm()
        {
            Console.WriteLine("ShowGradeManagementForm method called."); // 添加输出语句
            // 在splitContainer1_Panel2_Paint中显示学生信息管理页面
            // 在splitContainer1_Panel2_Paint中显示成绩管理页面
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(gradeManagementForm);
            gradeManagementForm.Dock = DockStyle.Fill;
            gradeManagementForm.Show();
        }

        private void ShowCourseManagementForm()
        {
            // 在splitContainer1_Panel2_Paint中显示课程管理页面
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(courseManagementForm);
            courseManagementForm.Dock = DockStyle.Fill;
            courseManagementForm.Show();
        }
        private void splitContainer1_Panel2_Paint(object sender, EventArgs e)
        {
          
        }


    }
}
