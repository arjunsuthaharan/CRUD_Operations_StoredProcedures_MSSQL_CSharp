﻿using System;
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

        SqlConnection con = new SqlConnection("****");
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
            con.Open();
            SqlCommand com = new SqlCommand("exec dbo.spPersonInsert '" +
                int.Parse(idTextBox.Text) + "','" +
                nameTextBox.Text + "','" +
                emailTextBox.Text + "','" +
                int.Parse(genderIDTextBox.Text) + "','" +
                int.Parse(ageTextBox.Text) + "'",
                con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully saved to Database");
            LoadAllPersonRecords();
        }

        void LoadAllPersonRecords()
        {
            SqlCommand com = new SqlCommand("exec dbo.spViewPersonTable", con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            recordsGridView.DataSource = dataTable;

        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand("exec dbo.spUpdatePerson '" +
                int.Parse(idTextBox.Text) + "','" +
                nameTextBox.Text + "','" +
                emailTextBox.Text + "','" +
                int.Parse(genderIDTextBox.Text) + "','" +
                int.Parse(ageTextBox.Text) + "'",
                con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully updated to Database");
            LoadAllPersonRecords();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
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
    }
}
