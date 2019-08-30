using RestSharp;
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
        int pegawaiPerPage = 10, mahasiswaPerPage = 10, penggunaPerPage = 10, parkirPerPage =10;
        int pegawaiTotalPage = 0, mahasiswaTotalPage = 0, penggunaTotalPage = 0, parkirTotalPage = 0;
        int pegawaiActivePage = 1, mahasiswaActivePage = 1, penggunaActivePage = 1, parkirActivePage =1;
        int pegawaiStart = 0, mahasiswaStart= 0, penggunaStart = 0, parkirStart = 0;
        int pegawaiTotal = 0, mahasiswaTotal = 0, penggunaTotal = 0, parkirTotal = 0;
        string pegawaiSearch = "", mahasiswaSearch ="", penggunaSearch = "", parkirSearch = "";
        List<Model.ResponsePegawaiDetail> listPegawai;
        List<Model.ResponseMahasiswaDetail> listMahasiswa;
        List<Model.ResponsePenggunaDetail> listPengguna;
        List<Model.ResponseParkirDetail> listParkir;
        
        public MainForm()
        {
            InitializeComponent();

            this.setFormProperty();
            this.getListPegawai();
            this.getListMahasiswa();
            this.getListPengguna();
            this.getListParkir();
        }

        private void setFormProperty()
        {
            labelServerAddress.Text = Configuration.ENDPOINT;
            comboBoxPengguna_filter.SelectedIndex = 0;
            comboBoxParkir_filter.SelectedIndex = 0;
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
                    this.setPaginationPegawai();
                    this.setTabelPegawai();
                }
            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Application.Exit();
            }
        }

        private void getListMahasiswa()
        {
            string path = "Apimobile/list_mahasiswa";
            RestRequest request = Configuration.getHttpConfig(path);
            request.AddJsonBody(
                new
                {
                    start = mahasiswaStart,
                    limit = mahasiswaPerPage,
                    orderBy = "asc",
                    search = mahasiswaSearch
                }
            );
            IRestResponse<Model.ResponseMahasiswa> response2 = Configuration.CLIENT.Execute<Model.ResponseMahasiswa>(request);
            if (response2 != null)
            {


                if (response2.Data.data == null)
                {

                    MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);
                    mahasiswaTotal = 0;


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
                    listMahasiswa = response2.Data.data[0].data;

                    mahasiswaTotal = response2.Data.data[0].total;
                    this.setPaginationMahasiswa();
                    this.setTableMahasiswa();
                }
            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Application.Exit();
            }
        }

        private void getListPengguna()
        {
            string path = "Apimobile/list_pengguna";
            RestRequest request = Configuration.getHttpConfig(path);
            string tipe = "";
            if(comboBoxPengguna_filter.SelectedItem.ToString() != "Semua")
            {
                tipe = comboBoxPengguna_filter.SelectedItem.ToString();
            }
            request.AddJsonBody(
                new
                {
                    start = penggunaStart,
                    limit = penggunaPerPage,
                    orderBy = "asc",
                    search = penggunaSearch,
                    tipe = tipe
                    
                }
            );
            IRestResponse<Model.ResponsePengguna> response2 = Configuration.CLIENT.Execute<Model.ResponsePengguna>(request);
            if (response2 != null)
            {


                if (response2.Data.data == null)
                {

                    MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);
                    penggunaTotal = 0;


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
                    listPengguna = response2.Data.data[0].data;

                    penggunaTotal = response2.Data.data[0].total;
                    this.setPaginationPengguna();
                    this.setTablePengguna();
                }
            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Application.Exit();
            }
        }

        private void getListParkir()
        {
            string path = "Apimobile/list_parkir";
            RestRequest request = Configuration.getHttpConfig(path);
            string jenis = "";
            if(comboBoxParkir_filter.SelectedItem.ToString() != "semua")
            {
                jenis = comboBoxParkir_filter.SelectedItem.ToString();
            }
            request.AddJsonBody(
                new
                {
                    start = parkirStart,
                    limit = parkirPerPage,
                    orderBy = "desc",
                    search = parkirSearch,
                    jenis = jenis
                    
                }
            );
            IRestResponse<Model.ResponseParkir> response2 = Configuration.CLIENT.Execute<Model.ResponseParkir>(request);
            if (response2 != null)
            {


                if (response2.Data.data == null)
                {

                    MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);
                    parkirTotal = 0;


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
                    listParkir = response2.Data.data[0].data;

                    parkirTotal = response2.Data.data[0].total;
                    this.setPaginationParkir();
                    this.setTableParkir();
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

        private void textBoxPengguna_search_TextChanged(object sender, EventArgs e)
        {
            penggunaSearch = textBoxPengguna_search.Text;
        }

        private void comboBoxParkir_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            parkirTotal = 0;
            parkirTotalPage = 0;
            parkirActivePage = 1;

            this.getListParkir();
        }

        private void buttonParkir_search_Click(object sender, EventArgs e)
        {
            parkirTotal = 0;
            parkirTotalPage = 0;
            parkirActivePage = 1;

            this.getListParkir();
        }

        private void buttonParkir_previous_Click(object sender, EventArgs e)
        {
            parkirStart = parkirStart - parkirPerPage;
            parkirActivePage--;

            getListParkir();
        }

        private void buttonParkir_next_Click(object sender, EventArgs e)
        {
            parkirStart = parkirActivePage * parkirPerPage;
            parkirActivePage++;

            getListParkir();
        }

        private void buttonPengguna_search_Click(object sender, EventArgs e)
        {
            penggunaTotal = 0;
            penggunaTotalPage = 0;
            penggunaActivePage = 1;

            this.getListPengguna();
        }

        private void buttonPengguna_previous_Click(object sender, EventArgs e)
        {
            penggunaStart = penggunaStart - penggunaPerPage;
            penggunaActivePage--;

            getListPengguna();
        }

        private void buttonPengguna_next_Click(object sender, EventArgs e)
        {
            penggunaStart = penggunaActivePage * penggunaPerPage;
            penggunaActivePage++;

            getListPengguna();
        }

        private void comboBoxPengguna_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            penggunaTotal = 0;
            penggunaTotalPage = 0;
            penggunaActivePage = 1;

            this.getListPengguna();
        }

        private void textBoxMahasiswa_search_TextChanged(object sender, EventArgs e)
        {
            mahasiswaSearch = textBoxMahasiswa_search.Text;
        }

        private void buttonMahasiswa_search_Click(object sender, EventArgs e)
        {
            mahasiswaTotal = 0;
            mahasiswaTotalPage = 0;
            mahasiswaActivePage = 1;

            this.getListMahasiswa();
        }

        private void buttonMahasiswa_previous_Click(object sender, EventArgs e)
        {
            mahasiswaStart = mahasiswaStart - mahasiswaPerPage;
            mahasiswaActivePage--;

            getListMahasiswa();
        }

        private void buttonMahasiswa_next_Click(object sender, EventArgs e)
        {
            mahasiswaStart = mahasiswaActivePage * mahasiswaPerPage;
            mahasiswaActivePage++;

            getListMahasiswa();
        }

        private void setPaginationPegawai()
        {
            pegawaiTotalPage = (int)Math.Ceiling((decimal)pegawaiTotal / (decimal)pegawaiPerPage);

            if (pegawaiTotalPage <= pegawaiActivePage)
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
            }
            else
            {
                buttonPegawai_previous.Enabled = true;
            }

            labelPegawai_totalPage.Text = pegawaiTotalPage.ToString();
            labelPegawai_activePage.Text = pegawaiActivePage.ToString();
        }

        private void setPaginationMahasiswa()
        {
            mahasiswaTotalPage = (int)Math.Ceiling((decimal)mahasiswaTotal / (decimal)mahasiswaPerPage);

            if (mahasiswaTotalPage <= mahasiswaActivePage)
            {
                buttonMahasiswa_next.Enabled = false;
            }
            else
            {
                buttonMahasiswa_next.Enabled = true;
            }

            if (mahasiswaActivePage <= 1)
            {
                buttonMahasiswa_previous.Enabled = false;
            }
            else
            {
                buttonMahasiswa_previous.Enabled = true;
            }

            labelMahasiswa_totalPage.Text = mahasiswaTotalPage.ToString();
            labelMahasiswa_activePage.Text = mahasiswaActivePage.ToString();
        }

        private void setPaginationPengguna()
        {
            penggunaTotalPage = (int)Math.Ceiling((decimal)penggunaTotal / (decimal)penggunaPerPage);

            if (penggunaTotalPage <= penggunaActivePage)
            {
                buttonPengguna_next.Enabled = false;
            }
            else
            {
                buttonPengguna_next.Enabled = true;
            }

            if (penggunaActivePage <= 1)
            {
                buttonPengguna_previous.Enabled = false;
            }
            else
            {
                buttonPengguna_previous.Enabled = true;
            }

            labelPengguna_totalPage.Text = penggunaTotalPage.ToString();
            labelPengguna_activePage.Text = penggunaActivePage.ToString();
        }

        private void setPaginationParkir()
        {
            parkirTotalPage = (int)Math.Ceiling((decimal)parkirTotal / (decimal)parkirPerPage);

            if (parkirTotalPage <= parkirActivePage)
            {
                buttonParkir_next.Enabled = false;
            }
            else
            {
                buttonParkir_next.Enabled = true;
            }

            if (parkirActivePage <= 1)
            {
                buttonParkir_previous.Enabled = false;
            }
            else
            {
                buttonParkir_previous.Enabled = true;
            }

            labelParkir_totalPage.Text = parkirTotalPage.ToString();
            labelParkir_activePage.Text = parkirActivePage.ToString();
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

        private void setTableMahasiswa()
        {
            dataGridViewMahasiswa.Rows.Clear();
            dataGridViewMahasiswa.Refresh();
            dataGridViewMahasiswa.DataSource = null;
            dataGridViewMahasiswa.ColumnCount = 5;
            dataGridViewMahasiswa.Columns[0].HeaderText = "ID";
            dataGridViewMahasiswa.Columns[1].HeaderText = "NIM";
            dataGridViewMahasiswa.Columns[2].HeaderText = "Nama";
            dataGridViewMahasiswa.Columns[3].HeaderText = "Alamat";
            dataGridViewMahasiswa.Columns[4].HeaderText = "Email";
            for (int i = 0; i < listMahasiswa.Count; i++)
            {
                dataGridViewMahasiswa.Rows.Add(
                    listMahasiswa[i].id,
                    listMahasiswa[i].nim,
                    listMahasiswa[i].nama,
                    listMahasiswa[i].alamat,
                    listMahasiswa[i].email
                    );
            }
        }

        private void setTablePengguna()
        {
            dataGridViewPengguna.Rows.Clear();
            dataGridViewPengguna.Refresh();
            dataGridViewPengguna.DataSource = null;
            dataGridViewPengguna.ColumnCount = 6;
            dataGridViewPengguna.Columns[0].HeaderText = "ID";
            dataGridViewPengguna.Columns[1].HeaderText = "Nomor Induk";
            dataGridViewPengguna.Columns[2].HeaderText = "Nama";
            dataGridViewPengguna.Columns[3].HeaderText = "Alamat";
            dataGridViewPengguna.Columns[4].HeaderText = "No Kartu";
            dataGridViewPengguna.Columns[5].HeaderText = "Type";
            for (int i = 0; i < listPengguna.Count; i++)
            {
                string tipe = "";
                if (listPengguna[i].id_type == 1)
                {
                    tipe = "Mahasiswa";
                } else if(listPengguna[i].id_type == 2)
                {
                    tipe = "Pegawai";
                } else if(listPengguna[i].id_type == 3)
                {
                    tipe = "Satpam";
                } else
                {
                    tipe = "Administrator";
                }
                dataGridViewPengguna.Rows.Add(
                    listPengguna[i].id,
                    listPengguna[i].nomor_induk,
                    listPengguna[i].nama,
                    listPengguna[i].alamat,
                    listPengguna[i].no_kartu,
                    tipe
                    );
            }
        }

        private void setTableParkir()
        {
            dataGridViewParkir.Rows.Clear();
            dataGridViewParkir.Refresh();
            dataGridViewParkir.DataSource = null;
            dataGridViewParkir.ColumnCount = 4;
            dataGridViewParkir.Columns[0].HeaderText = "ID";
            dataGridViewParkir.Columns[1].HeaderText = "Nomor Induk";
            dataGridViewParkir.Columns[2].HeaderText = "Waktu";
            dataGridViewParkir.Columns[3].HeaderText = "Jenis Parkir";
            for (int i = 0; i < listParkir.Count; i++)
            {
               
                dataGridViewParkir.Rows.Add(
                    listParkir[i].id,
                    listParkir[i].nomor_induk,
                    listParkir[i].waktu,
                    listParkir[i].jenis_parkir
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
