using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolContrib
{
    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();
        public Form1()
        {
            InitializeComponent();
        }

        private void AddStudentButton_Click(object sender, EventArgs e)
        {
            Student newStudent = new Student(this.StudentNameBox.Text, this.StudentBirthdayPicker.Value.Date);
            this.students.Add(newStudent);

            this.StudentList.Rows.Add(newStudent.name, newStudent.birthday.Date.ToString("yyyy-MM-dd"));
        }

        private void RemoveStudentButton_Click(object sender, EventArgs e)
        {
            if (this.students.Count > 0)
            {
                int index = this.StudentList.CurrentCell.RowIndex;

                this.students.RemoveAt(index);
                this.StudentList.Rows.RemoveAt(index);
            }
        }

        private void StartCalcButton_Click(object sender, EventArgs e)
        {
            this.InfoGridView.Rows.Clear();

            double contrib = 50;
            this.InfoGridView.Rows.Add("Base contribution", "\u20AC" + string.Format("{0:f2}", Math.Round(contrib, 2)));

            int olderThanTenCount = 0;
            double olderThanTenCost = 0;

            int youngerThanTenCount = 0;
            double youngerThanTenCost = 0;

            foreach (Student student in this.students)
            {
                DateTime now = DateTime.Now;
                TimeSpan tenYears = now.AddYears(10) - now;

                if ((this.RefDatePicker.Value.Date - student.birthday) > tenYears)
                {
                    olderThanTenCount++;
                    if (olderThanTenCount <= 2)
                    {
                        olderThanTenCost += 37;
                    }
                }
                else
                {
                    youngerThanTenCount++;
                    if (youngerThanTenCount <= 3)
                    {
                        youngerThanTenCost += 25;
                    }
                    Console.WriteLine(youngerThanTenCount);
                    Console.WriteLine(youngerThanTenCost);
                }
            }

            if (olderThanTenCost > 0)
            {
                this.InfoGridView.Rows.Add("10+ student contribution", "\u20AC" + string.Format("{0:f2}", Math.Round(olderThanTenCost, 2)));
            }
            if (youngerThanTenCost > 0)
            {
                this.InfoGridView.Rows.Add("10- student contribution", "\u20AC" + string.Format("{0:f2}", Math.Round(youngerThanTenCost, 2)));
            }

            contrib += olderThanTenCost + youngerThanTenCost;
            this.InfoGridView.Rows.Add("Contribution without cap", "\u20AC" + string.Format("{0:f2}", Math.Round(contrib, 2)));
            this.InfoGridView.Rows.Add("Max contribution", "\u20AC" + string.Format("{0:f2}", Math.Round(150.00, 2)));
            if (contrib > 150)
            {
                contrib = 150;
            }

            if (this.SingleParentCheckBox.Checked)
            {
                this.InfoGridView.Rows.Add("Single parent reduction", "25%");
                contrib = contrib * 0.75;
            }

            this.InfoGridView.Rows.Add("Total contribution", "\u20AC" + string.Format("{0:f2}", Math.Round(contrib, 2)));
        }
    }

    public class Student
    {
        public string name;
        public DateTime birthday;

        public Student (string name, DateTime birthday)
        {
            this.name = name;
            this.birthday = birthday;
        }
    }
}
