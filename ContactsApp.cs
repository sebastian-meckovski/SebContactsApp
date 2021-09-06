﻿using SebContactsApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SebContactsApp
{

    public partial class ContactsApp : Form
    {
        static string specialFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Contact Data";
        static string databaseName = "Contacts.db";
        public static string databasePath = Path.Combine(specialFolder, databaseName);

        public List<Contact> contacts;

        public ContactsApp()
        {
            InitializeComponent();
            Directory.CreateDirectory($"{specialFolder}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().ToList();
                listboxContacts.DataSource = contacts;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            addNew addNew = new addNew();
            addNew.contact = new Contact();

            addNew.btnSave.Click -= addNew.btnSave_Click_update; //changed functionality of the save button
            addNew.btnSave.Click += addNew.btnSave_Click;

            addNew.ShowDialog();

            UpdateData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            addNew addNew = new addNew();
            addNew.contact = contacts[listboxContacts.SelectedIndex];

            addNew.btnSave.Click += addNew.btnSave_Click_update; //changed functionality of the save button
            addNew.btnSave.Click -= addNew.btnSave_Click; 

            addNew.ShowDialog();

            UpdateData();
        }
    }
}
