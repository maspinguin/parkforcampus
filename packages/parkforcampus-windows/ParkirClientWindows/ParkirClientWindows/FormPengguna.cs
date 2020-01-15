using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using System.Diagnostics;
using ParkirClientWindows.Model;

namespace ParkirClientWindows
{
    public partial class FormPengguna : Form
    {
        public FormPengguna()
        {
            InitializeComponent();
            getTypePengguna();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getListNama(comboBox1.Text);


        }

        private void getListNama(string type= "Mahasiswa")
        {
            if (type == "Mahasiswa")
            {
                string path = "Apimobile/list_mahasiswa_non_pengguna";
                RestRequest request = Configuration.getHttpConfig(path);
                request.AddJsonBody(
                    new
                    {
                        orderBy = "asc",
                        search = ""
                    }
                    );
                IRestResponse<Model.ResponseMahasiswa> response2 = Configuration.CLIENT.Execute<Model.ResponseMahasiswa>(request);
                if (response2 != null)
                {
                    if (response2.Data.data == null)
                    {
                        MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);



                        if (response2.Data.status == 401)
                        {
                            this.Hide();
                            LoginForm a = new LoginForm();
                            a.ShowDialog();
                        }
                    }
                    else
                    {
                        comboBox2.Items.Clear();
                        for (int i = 0; i < response2.Data.data[0].data.Count; i++)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            ResponseMahasiswaDetail dataDetailMahasiswa = response2.Data.data[0].data[i];
                            item.Text = dataDetailMahasiswa.nama;
                            item.Value = dataDetailMahasiswa.nim;

                            comboBox2.Items.Add(item);
                        }


                    }
                }
                else
                {
                    MessageBox.Show("Network/ Server Error!");
                    Application.Exit();
                }
            }
            else if(type == "Pegawai" || type == "Satpam")
            {
                string path = "Apimobile/list_pegawai_non_pengguna";
                RestRequest request = Configuration.getHttpConfig(path);
                request.AddJsonBody(
                    new
                    {
                        orderBy = "asc",
                        search = ""
                    }
                );
                IRestResponse<Model.ResponsePegawai> response2 = Configuration.CLIENT.Execute<Model.ResponsePegawai>(request);
                if (response2 != null)
                {
                    if (response2.Data.data == null)
                    {
                        MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);



                        if (response2.Data.status == 401)
                        {
                            this.Hide();
                            LoginForm a = new LoginForm();
                            a.ShowDialog();
                        }
                    }
                    else
                    {
                        comboBox2.Items.Clear();
                        for (int i = 0; i < response2.Data.data[0].data.Count; i++)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            ResponsePegawaiDetail dataDetailPegawai = response2.Data.data[0].data[i];
                            item.Text = dataDetailPegawai.nama;
                            item.Value = dataDetailPegawai.nip;

                            comboBox2.Items.Add(item);
                        } 
                       
                        
                    }
                }
                else
                {
                    MessageBox.Show("Network/ Server Error!");
                    Application.Exit();
                }
            }


        }

        private void ProsesTambah()
        {
            string path = "Apimobile/insert_pengguna";
            RestRequest request = Configuration.getHttpConfig(path);
            request.AddJsonBody(
                new { tipe = comboBox1.Text, nomor_induk = textBox1.Text, password = "123" }
                );
            IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
            if (response2 != null)
            {
                responseData data = response2.Data.data;
                //MessageBox.Show("res"+ data.message.ToString());
                int status = data.status;
                if (status == 200)
                {
                    MessageBox.Show("Data pengguna Telah Ditambahkan.");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(response2.Data.message, "Error Status " + status);
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
        private void getTypePengguna()
        {
            /*
            string path = "Apimobile/list_type_pengguna";
            RestRequest request = Configuration.getHttpConfig(path,true,"GET");
            IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
            responseData data = response2.Data.data;
            Debug.WriteLine("hasilnyaaa" + data);
            */
            string path = "Apimobile/list_pegawai";
            RestRequest request = Configuration.getHttpConfig(path);
            request.AddJsonBody(
                new
                {
                    start = 0,
                    limit = 10,
                    orderBy = "asc",
                    search = ""
                }
            );
            IRestResponse<Model.ResponsePegawai> response2 = Configuration.CLIENT.Execute<Model.ResponsePegawai>(request);
            if (response2 != null)
            {

                Debug.WriteLine(response2.Data.data[0]);
                /*
                if (response2.Data.data == null)
                {

                    MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);
            
                    if (response2.Data.status == 401)
                    {
                        this.Hide();
                        LoginForm a = new LoginForm();
                        a.ShowDialog();
                    }
                }
                else
                {
                    Debug.WriteLine(response2.Data);
                }*/
            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Application.Exit();
            }
        }
        private void FormPengguna_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBoxItem selected = (ComboBoxItem)comboBox2.SelectedItem;
            textBox1.Text = selected.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ProsesTambah();
        }
    }
}
