﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LoginProcess(textBoxUsername.Text, textBoxPassword.Text);
        }

        private void LoginProcess(string username = "", string password = "")
        {
            string path = "Auth/login";
            RestRequest request = Configuration.getHttpConfig(path);
            request.AddJsonBody(
                new { username = username, password = password}
            );
            IRestResponse<Model.ResponseModel> response2 = Configuration.CLIENT.Execute<Model.ResponseModel>(request);
            if (response2 != null)
            {
                int status = response2.Data.status;

                if (status == 200)
                {
                    Configuration.configClass.SaveLinebyWord("ENDPOINT", "token", response2.Data.token);
                    Configuration.TOKEN = response2.Data.token;
                    Configuration.LOGINNAMA = response2.Data.nama;
                    Configuration.LOGINNIP = response2.Data.nomor_induk;
                    Configuration.LOGINIDTYPE = response2.Data.id_type;
                    
                    if (Configuration.LOGINIDTYPE != 3 && Configuration.LOGINIDTYPE != 4)
                    {
                        MessageBox.Show("User tidak diizinkan!");
                        Configuration.configClass.SaveLinebyWord("ENDPOINT", "token","null");
                    }
                    else
                    {
                        this.Hide();
                        MainForm a = new MainForm();
                        a.ShowDialog();
                    }
                   
                }
                else
                {
                    MessageBox.Show(response2.Data.message, "Error Status "+ status );
                }

                //client.ExecuteAsync(request, response => {
                //    Debug.WriteLine(response.Content);
                //});
            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Application.Exit();
            }

        }

       

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
