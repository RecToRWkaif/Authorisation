using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authorisation
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            UserNameField.Text = "Name";
            UserFNameField.Text = "Last Name";
            loginField.Text = "Login";
            passField.Text = "Password";
            

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPoint;
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;

            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void UserNameField_Enter(object sender, EventArgs e)
        {
            if (UserNameField.Text == "Name")
            {
                UserNameField.Text = "";
                UserNameField.ForeColor = Color.Black;
            }
            
        }

        private void UserNameField_Leave(object sender, EventArgs e)
        {
            if (UserNameField.Text == "")
            {

                UserNameField.Text = "Name";
                UserNameField.ForeColor = Color.Gray;

            }


        }

        private void UserFNameField_Enter(object sender, EventArgs e)
        {
            if (UserFNameField.Text == "Last Name")
            {
                UserFNameField.Text = "";
                UserFNameField.ForeColor = Color.Black;
            }

        }

        private void UserFNameField_Leave(object sender, EventArgs e)
        {
            if (UserFNameField.Text == "")
            {

                UserFNameField.Text = "Last Name";
                UserFNameField.ForeColor = Color.Gray;

            }

        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Login")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }

        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {

                loginField.Text = "Login";
                loginField.ForeColor = Color.Gray;

            }

        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Password")
            {
                passField.Text = "";
                passField.ForeColor = Color.Black;
            }

        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {

                passField.Text = "Password";
                passField.ForeColor = Color.Gray;

            }

        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (UserNameField.Text == "Name")
            {

                MessageBox.Show("Put Name!");
                return;

            }

            if (UserFNameField.Text == "Last Name")
            {

                MessageBox.Show("Put the Last Name!");
                return;

            }

            if (loginField.Text == "Login")
            {

                MessageBox.Show("Put a Login");
                return;

            }

            if (passField.Text == "Password")
            {

                MessageBox.Show("Put the pass!");
                return;

            }
            if (isUserExists())
                return;


            DataBase db = new DataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `data_users` (`login`, `password`, `name`, `fname`) VALUES (@login, @password, @name, @fname)", db.GetConnection()); 

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = UserNameField.Text;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = UserFNameField.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Account was been created!");
            else
                MessageBox.Show("Account was not been created!");

            db.closeConnection();
        }

        public Boolean isUserExists()
        {
            DataBase db = new DataBase();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `data_users` WHERE `login` = @uL", db.GetConnection());
            command.Parameters.AddWithValue("@uL", loginField.Text);

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("The value is taken by another user");
                return true;

            }
            else
                return false;
        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginForm = new Login();
            loginForm.Show();
        }
    }
}
