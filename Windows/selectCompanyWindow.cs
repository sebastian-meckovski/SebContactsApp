﻿using SebContactsApp.Model;
using SebContactsApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SebContactsApp
{
    public partial class selectCompanyWindow : Form
    {
        public static object selectedId; // not sure if it's the right approach

        public selectCompanyWindow()
        {
            InitializeComponent();
        }

        private void selectContactWindow_Load(object sender, EventArgs e)
        {
            
            
            SqlConnection connection;
            connection = new SqlConnection(DatabaseCredentials.connectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var tableQuery = "SELECT cd_id, cd_statement_name FROM customer_detail";

            SqlCommand cmd = new SqlCommand(tableQuery, connection);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();
            companyListDropdown.DataSource = dt;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            selectedId = ((DataRowView)companyListDropdown.SelectedItem).Row["cd_id"];
            Close();
        }
    }
}
