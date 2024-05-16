using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace РаспределениеРесурсов
{
    public partial class Добавление : Form
    {
        database db = new database();
        public Добавление()
        {
            InitializeComponent();

            using (SqlCommand command = new SqlCommand("SELECT FactoryID FROM Shipment", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(int.Parse(reader["FactoryID"].ToString()));
                    }
                }
                db.con.Close();
            }

            using (SqlCommand command = new SqlCommand("SELECT WarehouseID FROM Warehouse", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["WarehouseID"].ToString());
                    }
                }
                db.con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
            }
            else
            {
            DobavSklad();
            }

        }

        public void DobavSklad()
        {
            string query = $"insert into Warehouse(Name,Inventory) values ('{textBox1.Text}','{int.Parse(textBox2.Text)}')";

            SqlCommand command = new SqlCommand(query, db.con);

            command.Parameters.AddWithValue("Name", textBox1.Text);
            command.Parameters.AddWithValue("Inventory", int.Parse(textBox2.Text));

            try
            {
                db.con.Open();

                int rowsAffected = command.ExecuteNonQuery();

                MessageBox.Show("Данные занесены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        public void DobavFactory()
        {
            string query = $"insert into Factory(Name,Demand) values ('{textBox4.Text}','{int.Parse(textBox3.Text)}')";

            SqlCommand command = new SqlCommand(query, db.con);

            command.Parameters.AddWithValue("Name", textBox4.Text);
            command.Parameters.AddWithValue("Demand", int.Parse(textBox3.Text));

            try
            {
                db.con.Open();

                int rowsAffected = command.ExecuteNonQuery();

                MessageBox.Show("Данные занесены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
            }
            else
            {
                DobavFactory();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8) //Если символ, введенный с клавы - не цифра (IsDigit),
            {
                e.Handled = true;// то событие не обрабатывается. ch!=8 (8 - это Backspace)
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
            }
            else
            {
                DobavCost();
            }
        }

        public void DobavCost()
        {
            string query = $"insert into Cost(WarehouseID,FactoryID,CostPerUnit) values ('{int.Parse(comboBox1.Text)}','{int.Parse(comboBox2.Text)}','{int.Parse(textBox5.Text)}')";

            SqlCommand command = new SqlCommand(query, db.con);

            command.Parameters.AddWithValue("WarehouseID", int.Parse(comboBox1.Text));
            command.Parameters.AddWithValue("FactoryID", int.Parse(comboBox2.Text));
            command.Parameters.AddWithValue("CostPerUnit", int.Parse(textBox5.Text));

            try
            {
                db.con.Open();

                int rowsAffected = command.ExecuteNonQuery();

                MessageBox.Show("Данные занесены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox4.Text) || string.IsNullOrEmpty(comboBox3.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
            }
            else
            {
            DobavShipment();
            }
        }

        public void DobavShipment()
        {
            string query = $"insert into Shipment(WarehouseID,FactoryID,Quantity) values ('{int.Parse(comboBox4.Text)}','{int.Parse(comboBox3.Text)}','{int.Parse(textBox6.Text)}')";

            SqlCommand command = new SqlCommand(query, db.con);

            command.Parameters.AddWithValue("WarehouseID", int.Parse(comboBox4.Text));
            command.Parameters.AddWithValue("FactoryID", int.Parse(comboBox3.Text));
            command.Parameters.AddWithValue("Quantity", int.Parse(textBox6.Text));

            try
            {
                db.con.Open();

                int rowsAffected = command.ExecuteNonQuery();

                MessageBox.Show("Данные занесены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void Добавление_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            Obnov2();
            Obnov1();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            Obnov2();
            Obnov1();
        }

        public void Obnov1()
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Factory", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(int.Parse(reader["FactoryID"].ToString()));
                    }
                }
                db.con.Close();
            }
            using (SqlCommand command = new SqlCommand("SELECT * FROM Warehouse", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox4.Items.Add(reader["WarehouseID"].ToString());
                    }
                }
                db.con.Close();
            }
        }

        public void Obnov2()
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Factory", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(int.Parse(reader["FactoryID"].ToString()));
                    }
                }
                db.con.Close();
            }
            using (SqlCommand command = new SqlCommand("SELECT * FROM Warehouse", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["WarehouseID"].ToString());
                    }
                }
                db.con.Close();
            }
        }
    }
}
