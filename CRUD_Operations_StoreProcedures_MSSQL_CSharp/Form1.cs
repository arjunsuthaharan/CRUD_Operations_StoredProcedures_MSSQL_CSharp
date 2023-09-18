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

namespace CRUD_Operations_StoreProcedures_MSSQL_CSharp
{
    public partial class CRUD_Form : Form
    {

        SqlConnection con = new SqlConnection("*****");
        public CRUD_Form()
        {
            InitializeComponent();
        }

        private void titleLabel_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            // for adding new entry to database table
            con.Open();
            int gender = 0;

            if (rbMale.Checked == true)
            {
                gender = 1;
            }
            else if (rbFemale.Checked == true)
            {
                gender = 2;
            }
            else if (rbOther.Checked == true)
            {
                gender = 3;
            }
            SqlCommand com = new SqlCommand("exec dbo.spPersonInsert '" +
                int.Parse(idTextBox.Text) + "','" +
                nameTextBox.Text + "','" +
                emailTextBox.Text + "','" +
                gender + "','" +
                int.Parse(ageTextBox.Text) + "'",
                con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully saved to Database");
            LoadAllPersonRecords();
        }

        void LoadAllPersonRecords()
        {
            // for loading all records from database table to grid view
            SqlCommand com = new SqlCommand("exec dbo.spViewPersonTable", con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            recordsGridView.DataSource = dataTable;

        }

        private void updateBtn_Click(object sender, EventArgs e)
        {

            // for updating existing entry in database table
            con.Open();
            int gender = 0;

            if (rbMale.Checked == true)
            {
                gender = 1;
            }
            else if (rbFemale.Checked == true)
            {
                gender = 2;
            }
            else if (rbOther.Checked == true)
            {
                gender = 3;
            }
            SqlCommand com = new SqlCommand("exec dbo.spPersonInsert '" +
                int.Parse(idTextBox.Text) + "','" +
                nameTextBox.Text + "','" +
                emailTextBox.Text + "','" +
                gender + "','" +
                int.Parse(ageTextBox.Text) + "'",
                con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully updated to Database");
            LoadAllPersonRecords();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // for deleting entry from database table
            con.Open();

            if (MessageBox.Show("Are you sure you want to delete the entry?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SqlCommand com = new SqlCommand("exec dbo.spDeletePersonEntry '" +
                int.Parse(idTextBox.Text) + "'",
                con);
                com.ExecuteNonQuery();
                MessageBox.Show("Successfully deleted entry from Database");
                LoadAllPersonRecords();
            }
            con.Close();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // for searching and outputting entry from database table to grid view
            SqlCommand com = new SqlCommand("exec dbo.spSelectPersonEntry '" + int.Parse(idTextBox.Text) + "'", con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            recordsGridView.DataSource = dataTable;
        }

        private void retrieveBtn_Click(object sender, EventArgs e)
        {
            // for searching and retrieving entry from database table into textbox inputs
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT name, email, genderID, age FROM tblPerson WHERE ID = '" + int.Parse(idTextBox.Text) + "'", con);
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {
                nameTextBox.Text = srd.GetValue(0).ToString();
                emailTextBox.Text = srd.GetValue(1).ToString();
                //genderIDTextBox.Text = srd.GetValue(2).ToString();
                ageTextBox.Text = srd.GetValue(3).ToString();

                if(int.Parse(srd.GetValue(2).ToString()) == 1)
                {
                    rbMale.Checked = true;
                }
                else if (int.Parse(srd.GetValue(2).ToString()) == 2)
                {
                    rbFemale.Checked = true;
                }
                else if (int.Parse(srd.GetValue(2).ToString()) == 3)
                {
                    rbOther.Checked = true;
                }
            }
            con.Close();
        }

        private void genderIDLabel_Click(object sender, EventArgs e)
        {

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CRUD_Form_Load(object sender, EventArgs e)
        {
            LoadAllPersonRecords();
        }
    }
}
