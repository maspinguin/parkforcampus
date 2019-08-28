﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    public partial class MainForm : Form
    {
        int pegawaiPerPage = 2;
        int pegawaiTotalPage = 0;
        int pegawaiActivePage = 1;
        int pegawaiStart = 0;
        int pegawaiTotal = 0;
        string pegawaiSearch = "";
        List<Model.ResponsePegawaiDetail> listPegawai;

        public MainForm()
        {
            InitializeComponent();

            this.setFormProperty();
            this.getListPegawai();
        }

        private void setFormProperty()
        {
            labelServerAddress.Text = Configuration.ENDPOINT;
        }

        public void getListPegawai()
        {
            string path = "Apimobile/list_pegawai";
            RestRequest request = Configuration.getHttpConfig(path);
            request.AddJsonBody(
                new {
                    start = pegawaiStart,
                    limit = pegawaiPerPage,
                    orderBy = "asc",
                    search = pegawaiSearch
                }
            );
            IRestResponse<Model.ResponsePegawai> response2 = Configuration.CLIENT.Execute<Model.ResponsePegawai>(request);
            if (response2 != null)
            {

                
                if (response2.Data.data==null)
                {
                  
                    MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);
                    pegawaiTotal = 0;


                    if (response2.Data.status == 401)
                    {
                        this.Hide();
                        LoginForm a = new LoginForm();
                        a.ShowDialog();
                    }
                }
                else
                {
                    Debug.WriteLine(response2.Data.data[0].data.Count);
                    listPegawai = response2.Data.data[0].data;
                    
                    pegawaiTotal = response2.Data.data[0].total;
                    this.setPagination();
                    this.setTabelPegawai();
                }
            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Application.Exit();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pegawaiSearch = textBoxPegawai_search.Text;
        }

        private void buttonPegawai_search_Click(object sender, EventArgs e)
        {
            pegawaiTotal = 0;
            pegawaiTotalPage = 0;
            pegawaiActivePage = 1;
            
            this.getListPegawai();
            
        }

        private void setPagination()
        {
            pegawaiTotalPage = (int)Math.Ceiling((decimal)pegawaiTotal / (decimal)pegawaiPerPage);

            if(pegawaiTotalPage <= pegawaiActivePage)
            {
                buttonPegawai_next.Enabled = false;
            }
            else
            {
                buttonPegawai_next.Enabled = true;
            }

            if (pegawaiActivePage <= 1)
            {
                buttonPegawai_previous.Enabled = false;
            } else
            {
                buttonPegawai_previous.Enabled = true;
            }

            labelPegawai_totalPage.Text = pegawaiTotalPage.ToString();
            labelPegawai_activePage.Text = pegawaiActivePage.ToString();
        }

        private void setTabelPegawai()
        {
            
            dataGridViewPegawai.Rows.Clear();
            dataGridViewPegawai.Refresh();
            dataGridViewPegawai.DataSource = null;
            dataGridViewPegawai.ColumnCount = 5;
            dataGridViewPegawai.Columns[0].HeaderText = "ID";
            dataGridViewPegawai.Columns[1].HeaderText = "NIP";
            dataGridViewPegawai.Columns[2].HeaderText = "Nama";
            dataGridViewPegawai.Columns[3].HeaderText = "Alamat";
            dataGridViewPegawai.Columns[4].HeaderText = "Email";
            for (int i = 0; i < listPegawai.Count; i++)
            {
                dataGridViewPegawai.Rows.Add(
                    listPegawai[i].id,
                    listPegawai[i].nip,
                    listPegawai[i].nama,
                    listPegawai[i].alamat,
                    listPegawai[i].email
                    );
            }
        }

        private void buttonPegawai_next_Click(object sender, EventArgs e)
        {
            pegawaiStart = pegawaiActivePage * pegawaiPerPage ;
            pegawaiActivePage++;
           
            getListPegawai();
        }

        private void buttonPegawai_previous_Click(object sender, EventArgs e)
        {
            pegawaiStart = pegawaiStart - pegawaiPerPage;
            pegawaiActivePage--;

            getListPegawai();
        }
    }
}