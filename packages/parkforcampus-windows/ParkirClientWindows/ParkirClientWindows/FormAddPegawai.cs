using ParkirClientWindows.Model;
using RestSharp;
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
    public partial class FormAddPegawai : Form
    {
        public FormAddPegawai()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ProsesTambah();
        }

        private void ProsesTambah()
        {
            string path = "Apimobile/insert_pegawai";
            RestRequest request = Configuration.getHttpConfig(path);
            request.AddJsonBody(
                new { nip = textBox1.Text, nama = textBox2.Text, email = textBox3.Text, alamat = textBox4.Text }
            );
            IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
            if (response2 != null)
            {
                responseData data = response2.Data.data;
                int status = data.status;

                if (status == 200)
                {
                    MessageBox.Show("Data Pegawai Telah Ditambahkan.");
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
