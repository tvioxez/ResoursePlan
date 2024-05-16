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
    public partial class ФабрикиИСклад : Form
    {
        database db = new database();
        public ФабрикиИСклад()
        {
            InitializeComponent();

            using (SqlCommand command = new SqlCommand("SELECT FactoryID FROM Shipment", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(int.Parse(reader["FactoryID"].ToString()));
                    }
                }
                db.con.Close();
            }

            using (SqlCommand command = new SqlCommand("SELECT Name FROM Warehouse", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader["Name"].ToString());
                    }
                }
                db.con.Close();
            }

            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }



        private void CreateColumns()
        {

            dataGridView1.Columns.Add("f.Name", "Название фабрики");
            dataGridView1.Columns.Add("c.CostPerUnit", "Стоимость за единицу продукции");
        }

        private void CreateRows(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetString(0),record.GetInt32(1));
        }

        private void RefreshDataGrid(DataGridView dgw)
        {

            dgw.Rows.Clear();

            string query = $"select f.Name, c.CostPerUnit from Cost c, Factory f where c.FactoryID = f.FactoryID and c.FactoryID = '{comboBox1.Text}'";

            SqlCommand cmd = new SqlCommand(query, db.con);

            db.con.Open();

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CreateRows(dgw, sqlDataReader);
            }
            sqlDataReader.Close();
            db.con.Close();
        }

        private void RefreshDataGrids(DataGridView dgw)
        {

            dgw.Rows.Clear();

            string query = $"select f.Name, c.CostPerUnit from Cost c, Factory f where c.FactoryID = f.FactoryID";

            SqlCommand cmd = new SqlCommand(query, db.con);

            db.con.Open();

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                CreateRows(dgw, sqlDataReader);
            }
            sqlDataReader.Close();
            db.con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Выберите Склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Rasch();
                Zap();
            }
        }

        public void Rasch()
        {
            using (SqlCommand command = new SqlCommand($"SELECT SUM(s.Quantity * f.CostPerUnit) as Сумма FROM Shipment s, Cost f where f.FactoryID = s.FactoryID and s.WarehouseID = {int.Parse(textBox1.Text)}", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        label7.Text = reader["Сумма"].ToString();
                    }
                }
                db.con.Close();
            }
        }

        public void Zap()
        {
            using (SqlCommand command = new SqlCommand($"SELECT SUM(Quantity) as Сумма FROM Shipment where WarehouseID = {int.Parse(textBox1.Text)}", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        label9.Text = reader["Сумма"].ToString();
                    }
                }
                db.con.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand($"SELECT Quantity FROM Shipment where {comboBox1.Text} = FactoryID", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       textBox3.Text = reader["Quantity"].ToString();
                    }
                }
                db.con.Close();
            }
            RefreshDataGrid(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrids(dataGridView1);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Авт авт = new Авт();
            авт.ShowDialog();
        }

        private void ФабрикиИСклад_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand($"SELECT WarehouseID,Inventory FROM Warehouse where '{comboBox2.Text}' = Name", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textBox2.Text = reader["Inventory"].ToString();
                        textBox1.Text = reader["WarehouseID"].ToString();
                    }
                }
                db.con.Close();
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Авт а = new Авт();
            а.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            Obnov();
        }

        public void Obnov()
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Shipment", db.con))
            {
                db.con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(int.Parse(reader["FactoryID"].ToString()));
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
                        comboBox2.Items.Add(reader["Name"].ToString());
                    }
                }
                db.con.Close();
            }
        }
    }
}
