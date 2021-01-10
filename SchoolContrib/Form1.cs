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
