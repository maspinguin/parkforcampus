using ParkirClientWindows.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    public partial class MainForm : Form
    {
        public static SerialPort serial1;
        int pegawaiPerPage = 10, mahasiswaPerPage = 10, penggunaPerPage = 10, parkirPerPage =10;
        int pegawaiTotalPage = 0, mahasiswaTotalPage = 0, penggunaTotalPage = 0, parkirTotalPage = 0;
        int pegawaiActivePage = 1, mahasiswaActivePage = 1, penggunaActivePage = 1, parkirActivePage =1;
        int pegawaiStart = 0, mahasiswaStart= 0, penggunaStart = 0, parkirStart = 0;
        int pegawaiTotal = 0, mahasiswaTotal = 0, penggunaTotal = 0, parkirTotal = 0, rekapParkirTotal = 0;
        string pegawaiSearch = "", mahasiswaSearch ="", penggunaSearch = "", parkirSearch = "";
        List<Model.ResponsePegawaiDetail> listPegawai;
        List<Model.ResponseMahasiswaDetail> listMahasiswa;
        List<Model.ResponsePenggunaDetail> listPengguna;
        List<Model.ResponseParkirDetail> listParkir;
        List<Model.ResponseParkirDetail> listRekapParkir;
        List<Model.ResponseRekapParkirDetail> listCountRekapParkir;
        private ArduinoHandler arduinoHandler1;

        public MainForm()
        {
            InitializeComponent();
            arduinoHandler1 = new ArduinoHandler(this, textBoxLogApplication);
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
            comboBox1.SelectedIndex = 0;
            labelNama.Text = "Selamat datang " + Configuration.LOGINNAMA + " ( NIP / ID: " + Configuration.LOGINNIP + ")";

            Configuration.setSerialPortSetting();
            labelArduino1.Text = "PORT: " + Configuration.SERIALPORT1_NAME + " BAUDRATE: " + Configuration.SERIALPORT1_BAUDRATE;
            labelArduino2.Text = "PORT: " + Configuration.SERIALPORT2_NAME + " BAUDRATE: " + Configuration.SERIALPORT2_BAUDRATE;
            //ArduinoHandler.OpenPort1(textBoxArduino1, this);
            arduinoHandler1.OpenPort1(textBoxArduino1);
            arduinoHandler1.OpenPort2(textBoxArduino2);
        }

        private void getListCountByHour()
        {
            string path = "Apimobile/list_count_parkir_by_date_hour";
            RestRequest request = Configuration.getHttpConfig(path);
            string tipe = "";
            if (comboBox1.SelectedItem.ToString() != "semua")
            {
                tipe = comboBox1.SelectedItem.ToString();
            }
            request.AddJsonBody(
                new
                {
                    date = dateTimePicker3.Value.ToString("yyyy-MM-dd"),
                    jenis = tipe
                    
                }
            );
            IRestResponse<Model.ResponseRekapParkir> response2 = Configuration.CLIENT.Execute<Model.ResponseRekapParkir>(request);
            if (response2 != null)
            {
                if (response2.Data.data == null)
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
                    listCountRekapParkir = response2.Data.data[0].data;
                    if(response2.Data.jam_ramai != null)
                    {

                        var dt = Convert.ToDateTime(response2.Data.jam_ramai);
                        label24.Text = dt.Hour + ":00";
                    }

                    this.setTableCountRekapParkir();
                }
            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Application.Exit();
            }
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

        public void getListRekapParkir()
        {
            if (this.comboBox1.InvokeRequired)
            {
                string path = "Apimobile/list_count_parkir_by_date_hour";
                RestRequest request = Configuration.getHttpConfig(path);
                string jenis = "";
                comboBox1.Invoke((MethodInvoker)delegate
                {
                    if (comboBox1.SelectedItem.ToString() != "semua")
                    {
                        jenis = comboBox1.SelectedItem.ToString();
                    }
                });

                request.AddJsonBody(
                    new
                    {
                        orderBy = "desc",
                        jenis = jenis,
                        dateStart = dateTimePicker3.Value.ToString("yyyy-MM-dd 00:00"),
                        dateEnd = dateTimePicker3.Value.ToString("yyyy-MM-dd 23:59")
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
                        listRekapParkir = response2.Data.data[0].data;

                        rekapParkirTotal = response2.Data.data[0].total;

                        setTableRekapParkir();
                    }
                }
                else
                {
                    MessageBox.Show("Network/ Server Error!");
                    Application.Exit();
                }


            }
            else
            {
                string path = "Apimobile/list_parkir";
                RestRequest request = Configuration.getHttpConfig(path);
                string jenis = "";
                if (comboBox1.SelectedItem.ToString() != "semua")
                {
                    jenis = comboBox1.SelectedItem.ToString();
                }
                request.AddJsonBody(
                    new
                    {
                        orderBy = "desc",
                        jenis = jenis,
                        dateStart = dateTimePicker3.Value.ToString("yyyy-MM-dd 00:00"),
                        dateEnd = dateTimePicker3.Value.ToString("yyyy-MM-dd 23:59")


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
                        listRekapParkir = response2.Data.data[0].data;

                        rekapParkirTotal = response2.Data.data[0].total;

                        setTableRekapParkir();
                        // this.setPaginationParkir();
                        //this.setTableParkir();
                    }
                }
                else
                {
                    MessageBox.Show("Network/ Server Error!");
                    Application.Exit();
                }
            }
        }

        public void getListParkir()
        {
            if(this.comboBoxParkir_filter.InvokeRequired)
            {
                string path = "Apimobile/list_parkir";
                RestRequest request = Configuration.getHttpConfig(path);
                string jenis = "";
                comboBoxParkir_filter.Invoke((MethodInvoker)delegate
                {
                    if (comboBoxParkir_filter.SelectedItem.ToString() != "semua")
                    {
                        jenis = comboBoxParkir_filter.SelectedItem.ToString();
                    }
                });
                
                request.AddJsonBody(
                    new
                    {
                        start = parkirStart,
                        limit = parkirPerPage,
                        orderBy = "desc",
                        search = parkirSearch,
                        jenis = jenis,
                        dateStart = dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        dateEnd = dateTimePicker2.Value.ToString("yyyy-MM-dd")


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


            } else
            {
                string path = "Apimobile/list_parkir";
                RestRequest request = Configuration.getHttpConfig(path);
                string jenis = "";
                if (comboBoxParkir_filter.SelectedItem.ToString() != "semua")
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
                        jenis = jenis,
                        dateStart = dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        dateEnd = dateTimePicker2.Value.ToString("yyyy-MM-dd")


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
            pegawaiStart = 0;
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

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            parkirTotal = 0;
            parkirTotalPage = 0;
            parkirActivePage = 1;

            this.getListParkir();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            parkirTotal = 0;
            parkirTotalPage = 0;
            parkirActivePage = 1;

            this.getListParkir();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "Auth/logout";
            RestRequest request = Configuration.getHttpConfig(path);
            
           
            IRestResponse<Model.ResponseModel> response2 = Configuration.CLIENT.Execute<Model.ResponseModel>(request);
            if (response2 != null)
            {
                
                if (response2.Data.status != 200)
                {
                    MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);
                }

                Configuration.configClass.SaveLinebyWord("ENDPOINT", "token", "null");
                Application.Exit();

            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Configuration.configClass.SaveLinebyWord("ENDPOINT", "token", "null");
                Application.Exit();
            }
        }

        private void connectReconnectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            arduinoHandler1.OpenPort1(textBoxArduino1);
        }

        private void connectReconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arduinoHandler1.OpenPort2(textBoxArduino2);
        }

        private void pingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArduinoHandler.write(Configuration.SERIALPORT1.serial, "ping", textBoxArduino1,1);
        }

        private void pingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ArduinoHandler.write(Configuration.SERIALPORT2.serial, "ping", textBoxArduino2,2);
        }
     
        private void doReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArduinoHandler.write(Configuration.SERIALPORT1.serial, "doRead;"+Configuration.KeyA+";"+Configuration.KeyB+"", textBoxArduino1,1);
            
            
        }

        private void settingToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void doWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string card = "32303030333935000000000000000000";
            ArduinoHandler.write(Configuration.SERIALPORT1.serial, "writeCard;" + Configuration.KeyA + ";" + Configuration.KeyB + ";"+card, textBoxArduino1,1);
        }

        private void masukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArduinoHandler.write(Configuration.SERIALPORT1.serial, "portalModeMasuk;",textBoxArduino1,1);
        }

        private void keluarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArduinoHandler.write(Configuration.SERIALPORT1.serial, "portalModeKeluar;", textBoxArduino1, 1);
        }

        private void textBoxParkir_search_TextChanged(object sender, EventArgs e)
        {
            parkirSearch = textBoxParkir_search.Text;
        }

        private void dataGridViewPengguna_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 6)
            {
                string no_kartu = dataGridViewPengguna.Rows[e.RowIndex].Cells[4].Value.ToString();
                //MessageBox.Show(Helper.ASCIItoHex(no_kartu));
                ArduinoHandler.write(Configuration.SERIALPORT1.serial, "writeCard;" + Configuration.KeyA + ";" + Configuration.KeyB + ";"+Helper.ASCIItoHex(no_kartu), textBoxArduino1, 1);

            }

            if(e.ColumnIndex == 7)
            {
                string nim = dataGridViewPengguna.Rows[e.RowIndex].Cells[1].Value.ToString();
                string tipe = dataGridViewPengguna.Rows[e.RowIndex].Cells[5].Value.ToString();
                DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus data?", "Hapus", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //MessageBox.Show(nim);
                    deletePengguna(nim, tipe);
                }
            }

            if (e.ColumnIndex == 8)
            {
                string nim = dataGridViewPengguna.Rows[e.RowIndex].Cells[1].Value.ToString();
                string tipe = dataGridViewPengguna.Rows[e.RowIndex].Cells[5].Value.ToString();
                string no_kartu = dataGridViewPengguna.Rows[e.RowIndex].Cells[4].Value.ToString();
                DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan mengubah data?", "Ubah", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //MessageBox.Show(nim);
                    updatePengguna(nim, tipe, no_kartu);
                }
            }
        }

        private void refreshDataPengguna()
        {
            penggunaStart = 0;
            penggunaTotal = 0;
            penggunaTotalPage = 0;
            penggunaActivePage = 1;

            this.getListPengguna();
        }

        private void refreshDataPegawai()
        {
            pegawaiStart = 0;
            pegawaiTotal = 0;
            pegawaiTotalPage = 0;
            pegawaiActivePage = 1;

            this.getListPegawai();
        }

        private void refreshDataMahasiswa()
        {
            mahasiswaStart = 0;
            mahasiswaTotal = 0;
            mahasiswaTotalPage = 0;
            mahasiswaActivePage = 1;

            this.getListMahasiswa();
        }
        private void deletePengguna(string ni, string tipe)
        {
            try
            {
                string path = "Apimobile/delete_pengguna";
                RestRequest request = Configuration.getHttpConfig(path, false, "DELETE");
                request.AddJsonBody(
                    new { tipe = tipe, nomor_induk = ni }
                    );
                IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
                if (response2 != null)
                {
                    responseData data = response2.Data.data;
                    //MessageBox.Show("res"+ data.message.ToString());
                    int status = data.status;
                    if (status == 200)
                    {
                        MessageBox.Show("Data pengguna Telah Dihapus.");
                    }
                    else
                    {
                        MessageBox.Show(data.message, "Error Status " + status);
                    }

                    refreshDataPengguna();


                    //client.ExecuteAsync(request, response => {
                    //    Debug.WriteLine(response.Content);
                    //});
                }
                else
                {
                    MessageBox.Show("Network/ Server Error!");
                    Application.Exit();
                }

            } catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
           
        }

        private void deleteMahasiswa(string ni)
        {
            try
            {
                string path = "Apimobile/delete_mahasiswa";
                RestRequest request = Configuration.getHttpConfig(path, false, "DELETE");
                request.AddJsonBody(
                    new { nim = ni }
                    );
                IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
                if (response2 != null)
                {
                    responseData data = response2.Data.data;
                    //MessageBox.Show("res"+ data.message.ToString());
                    int status = data.status;
                    if (status == 200)
                    {
                        MessageBox.Show("Data mahasiswa Telah Dihapus.");
                    }
                    else
                    {
                        MessageBox.Show(data.message, "Error Status " + status);
                    }

                    refreshDataMahasiswa();


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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        private void deletePegawai(string ni)
        {
            try
            {
                string path = "Apimobile/delete_pegawai";
                RestRequest request = Configuration.getHttpConfig(path, false, "DELETE");
                request.AddJsonBody(
                    new { nip = ni }
                    );
                IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
                if (response2 != null)
                {
                    responseData data = response2.Data.data;
                    //MessageBox.Show("res"+ data.message.ToString());
                    int status = data.status;
                    if (status == 200)
                    {
                        MessageBox.Show("Data Pegawai Telah Dihapus.");
                    }
                    else
                    {
                        MessageBox.Show(data.message, "Error Status " + status);
                    }

                    refreshDataPegawai();


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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        private void updatePengguna(string ni, string tipe, string no_kartu)
        {
            try
            {
                string path = "Apimobile/update_pengguna";
                RestRequest request = Configuration.getHttpConfig(path, false, "PUT");
                request.AddJsonBody(
                    new { tipe = tipe, nomor_induk = ni, no_kartu = no_kartu }
                    );
                IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
                if (response2 != null)
                {

                    //MessageBox.Show("res"+ data.message.ToString());
                    responseData data = null;
                    if (response2.Data.data != null)
                    {
                        data = response2.Data.data;
                        int status = data.status;
                        if (status == 200)
                        {
                            MessageBox.Show("Data pengguna Telah Diganti.");
                        }
                        else
                        {
                            MessageBox.Show(data.message, "Error Status " + status);
                        }
                        
                    }
                    else
                    {
                        if(response2.Data.status == 500)
                        {
                            MessageBox.Show("Internal Server Error. Error duplicate card! ", "Error Status " + response2.Data.status);
                        }
                    }

                    refreshDataPengguna();


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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void updateMahasiswa(string nim, string nama, string email, string alamat)
        {
            try
            {
                string path = "Apimobile/update_mahasiswa";
                RestRequest request = Configuration.getHttpConfig(path, false, "PUT");
                request.AddJsonBody(
                    new { nim = nim, nama = nama, email = email, alamat = alamat }
                    );
                IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
                if (response2 != null)
                {
                    responseData data = response2.Data.data;
                    //MessageBox.Show("res"+ data.message.ToString());
                    int status = data.status;
                    if (status == 200)
                    {
                        MessageBox.Show("Data Mahasiswa Telah Diganti.");
                    }
                    else
                    {
                        MessageBox.Show(data.message, "Error Status " + status);
                    }

                    refreshDataMahasiswa();


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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void updatePegawai(string nip, string nama, string email, string alamat)
        {
            try
            {
                string path = "Apimobile/update_pegawai";
                RestRequest request = Configuration.getHttpConfig(path, false, "PUT");
                request.AddJsonBody(
                    new { nip = nip, nama = nama, email = email, alamat = alamat }
                    );
                IRestResponse<Model.ResponseModelData> response2 = Configuration.CLIENT.Execute<Model.ResponseModelData>(request);
                if (response2 != null)
                {
                    responseData data = response2.Data.data;
                    //MessageBox.Show("res"+ data.message.ToString());
                    int status = data.status;
                    if (status == 200)
                    {
                        MessageBox.Show("Data Pegawai Telah Diganti.");
                    }
                    else
                    {
                        MessageBox.Show(data.message, "Error Status " + status);
                    }

                    refreshDataMahasiswa();


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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        private void portalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArduinoHandler.write(Configuration.SERIALPORT1.serial, "startModePortal;", textBoxArduino1, 1);
        }

        private void openPortalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArduinoHandler.write(Configuration.SERIALPORT1.serial, "openPortal;", textBoxArduino1, 1);
        }

        private void buttonPengguna_add_Click(object sender, EventArgs e)
        {
            FormPengguna a = new FormPengguna();
          //  a.FormClosed += FormPenggunaClose;
            a.FormClosing += FormPenggunaClosing;
            a.ShowDialog();
           
        }

        private void FormPenggunaClosing(Object sender, FormClosingEventArgs e)
        {
            refreshDataPengguna();
        }

        private void FormPenggunaClose(Object sender, FormClosedEventArgs e)
        {
            refreshDataPengguna();
        }


        private void buttonPegawai_tambah_Click(object sender, EventArgs e)
        {
            FormAddPegawai a = new FormAddPegawai();
           // a.FormClosed += FormPegawaiClose;
            a.FormClosing += FormPegawaiClosing;
            a.ShowDialog();
        }

        private void FormPegawaiClose(Object sender, FormClosedEventArgs e)
        {
            refreshDataPegawai();
        }

        private void FormPegawaiClosing(Object sender, FormClosingEventArgs e)
        {
            refreshDataPegawai();
        }


        private void buttonMahasiswa_tambah_Click(object sender, EventArgs e)
        {
            FormAddMahasiswa a = new FormAddMahasiswa();
           // a.FormClosed += FormMahasiswaClose;
            a.FormClosing += FormMahasiswaClosing;
            a.ShowDialog();
        }

        private void FormMahasiswaClose(Object sender, FormClosedEventArgs e)
        {
            refreshDataMahasiswa();
        }

        private void FormMahasiswaClosing(Object sender, FormClosingEventArgs e)
        {
            refreshDataMahasiswa();
        }


        private void dataGridViewPengguna_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewParkir_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewMahasiswa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                string nim = dataGridViewMahasiswa.Rows[e.RowIndex].Cells[1].Value.ToString();
               
                DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus data " + nim + "?", "Hapus", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //MessageBox.Show(nim);
                    deleteMahasiswa(nim);
                }
            }

            if (e.ColumnIndex == 5)
            {
                string nim = dataGridViewMahasiswa.Rows[e.RowIndex].Cells[1].Value.ToString();
                string nama = dataGridViewMahasiswa.Rows[e.RowIndex].Cells[2].Value.ToString();
                string alamat = dataGridViewMahasiswa.Rows[e.RowIndex].Cells[3].Value.ToString();
                string email = dataGridViewMahasiswa.Rows[e.RowIndex].Cells[4].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan mengganti data "+nim+"?", "Hapus", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //MessageBox.Show(nim);
                    updateMahasiswa(nim, nama, email, alamat);
                }
            }
        }

        private void dataGridViewPegawai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                string nip = dataGridViewPegawai.Rows[e.RowIndex].Cells[1].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan menghapus data " + nip + "?", "Hapus", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //MessageBox.Show(nim);
                    deletePegawai(nip);
                }
            }

            if (e.ColumnIndex == 5)
            {
                string nip = dataGridViewPegawai.Rows[e.RowIndex].Cells[1].Value.ToString();
                string nama = dataGridViewPegawai.Rows[e.RowIndex].Cells[2].Value.ToString();
                string alamat = dataGridViewPegawai.Rows[e.RowIndex].Cells[3].Value.ToString();
                string email = dataGridViewPegawai.Rows[e.RowIndex].Cells[4].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Apakah anda yakin akan mengubah data " + nip + "?", "Ubah", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //MessageBox.Show(nim);
                    updatePegawai(nip, nama, email, alamat);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            getListRekapParkir();
            getListCountByHour();
            label23.Text = rekapParkirTotal.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonPengguna_search_Click(object sender, EventArgs e)
        {
            penggunaStart = 0;
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
            mahasiswaStart = 0;
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

            if (buttonParkir_next.InvokeRequired)
            {
                buttonParkir_next.Invoke((MethodInvoker)delegate
                {
                    if (parkirTotalPage <= parkirActivePage)
                    {
                        buttonParkir_next.Enabled = false;

                    }
                    else
                    {
                        buttonParkir_next.Enabled = true;
                    }

                });
            }
            else
            {
                if (parkirTotalPage <= parkirActivePage)
                {
                    buttonParkir_next.Enabled = false;

                }
                else
                {
                    buttonParkir_next.Enabled = true;
                }

            }

            if(buttonParkir_previous.InvokeRequired)
            {
                buttonParkir_previous.Invoke((MethodInvoker)delegate
               {
                   if (parkirActivePage <= 1)
                   {
                       buttonParkir_previous.Enabled = false;
                   }
                   else
                   {
                       buttonParkir_previous.Enabled = true;
                   }

               });
            } else
            {
                if (parkirActivePage <= 1)
                {
                    buttonParkir_previous.Enabled = false;
                }
                else
                {
                    buttonParkir_previous.Enabled = true;
                }

            }

            if(labelParkir_totalPage.InvokeRequired)
            {
                labelParkir_totalPage.Invoke((MethodInvoker)delegate
                {
                    labelParkir_totalPage.Text = parkirTotalPage.ToString();

                });
            } else
            {
                labelParkir_totalPage.Text = parkirTotalPage.ToString();

            }

            if(labelParkir_activePage.InvokeRequired)
            {
                labelParkir_activePage.Invoke((MethodInvoker)delegate
                {
                    labelParkir_activePage.Text = parkirActivePage.ToString();

                });
            }else
            {
                labelParkir_activePage.Text = parkirActivePage.ToString();

            }
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
            dataGridViewPegawai.Columns[0].ReadOnly = true;
            dataGridViewPegawai.Columns[1].ReadOnly = true;
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

            

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.HeaderText = "Aksi";
                button.Text = "Ubah";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridViewPegawai.Columns.Add(button);
            }
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            {

                delete.Text = "Hapus";
                delete.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridViewPegawai.Columns.Add(delete);
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

            dataGridViewMahasiswa.Columns[0].ReadOnly = true;
            dataGridViewMahasiswa.Columns[1].ReadOnly = true;
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

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.HeaderText = "Aksi";
                button.Text = "Ubah";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridViewMahasiswa.Columns.Add(button);
            }
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            {

                delete.Text = "Hapus";
                delete.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridViewMahasiswa.Columns.Add(delete);
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

            dataGridViewPengguna.Columns[0].ReadOnly = true;
            dataGridViewPengguna.Columns[1].ReadOnly = true;
            dataGridViewPengguna.Columns[2].ReadOnly = true;
            dataGridViewPengguna.Columns[3].ReadOnly = true;
            dataGridViewPengguna.Columns[5].ReadOnly = true;

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

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = "writeCard";
                button.HeaderText = "Aksi";
                button.Text = "Write Card";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridViewPengguna.Columns.Add(button);
            }

            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            {
                deleteButton.Text = "Hapus";
                deleteButton.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridViewPengguna.Columns.Add(deleteButton);
            }

           
            DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
            {
                editButton.Text = "Ubah";
                editButton.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridViewPengguna.Columns.Add(editButton);
            }
        }

        private void setTableParkir()
        {
            if(dataGridViewParkir.InvokeRequired)
            {
                dataGridViewParkir.Invoke((MethodInvoker)delegate
               {
                   dataGridViewParkir.Rows.Clear();
                   dataGridViewParkir.Refresh();
                   dataGridViewParkir.DataSource = null;
                   dataGridViewParkir.ColumnCount = 5;
                   dataGridViewParkir.Columns[2].Width = 200;
                   dataGridViewParkir.Columns[0].HeaderText = "ID";
                   dataGridViewParkir.Columns[1].HeaderText = "Nomor Induk";
                   dataGridViewParkir.Columns[2].HeaderText = "Waktu";
                   dataGridViewParkir.Columns[3].HeaderText = "Jenis Parkir";
                   dataGridViewParkir.Columns[4].HeaderText = "No Kartu";
                   for (int i = 0; i < listParkir.Count; i++)
                   {

                       dataGridViewParkir.Rows.Add(
                           listParkir[i].id,
                           listParkir[i].nomor_induk,
                           listParkir[i].waktu,
                           listParkir[i].jenis_parkir,
                           listParkir[i].no_kartu
                           );
                   }
               });
            } else
            {
                dataGridViewParkir.Rows.Clear();
                dataGridViewParkir.Refresh();
                dataGridViewParkir.DataSource = null;
                dataGridViewParkir.ColumnCount = 5;
                dataGridViewParkir.Columns[2].Width = 200;
                dataGridViewParkir.Columns[0].HeaderText = "ID";
                dataGridViewParkir.Columns[1].HeaderText = "Nomor Induk";
                dataGridViewParkir.Columns[2].HeaderText = "Waktu";
                dataGridViewParkir.Columns[3].HeaderText = "Jenis Parkir";
                dataGridViewParkir.Columns[4].HeaderText = "No Kartu";
                for (int i = 0; i < listParkir.Count; i++)
                {

                    dataGridViewParkir.Rows.Add(
                        listParkir[i].id,
                        listParkir[i].nomor_induk,
                        listParkir[i].waktu,
                        listParkir[i].jenis_parkir,
                        listParkir[i].no_kartu
                        );
                }
            }
            
        }

        private void setTableCountRekapParkir()
        {
            if (dataGridView2.InvokeRequired)
            {
                dataGridView2.Invoke((MethodInvoker)delegate
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Refresh();
                    dataGridView2.DataSource = null;
                    dataGridView2.ColumnCount = 2;
                    dataGridView2.Columns[1].Width = 200;
                    dataGridView2.Columns[0].HeaderText = "Jam";
                    dataGridView2.Columns[1].HeaderText = "Jumlah";
                    for (int i = 0; i < listCountRekapParkir.Count; i++)
                    {

                        var dt = Convert.ToDateTime(listCountRekapParkir[i].hour);
                        dataGridView2.Rows.Add(
                            dt.Hour + ":00",
                            listCountRekapParkir[i].jumlah
                            );
                    }
                });
            }
            else
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();
                dataGridView2.DataSource = null;
                dataGridView2.ColumnCount = 2;
                dataGridView2.Columns[1].Width = 200;
                dataGridView2.Columns[0].HeaderText = "Jam";
                dataGridView2.Columns[1].HeaderText = "Jumlah";
                for (int i = 0; i < listCountRekapParkir.Count; i++)
                {
                    var dt = Convert.ToDateTime(listCountRekapParkir[i].hour);
                    dataGridView2.Rows.Add(
                         dt.Hour + ":00",
                        listCountRekapParkir[i].jumlah
                        );
                }
            }

        }

        private void setTableRekapParkir()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    dataGridView1.DataSource = null;
                    dataGridView1.ColumnCount = 4;
                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Nomor Induk";
                    dataGridView1.Columns[2].HeaderText = "Waktu";
                    dataGridView1.Columns[3].HeaderText = "Jenis Parkir";
                    for (int i = 0; i < listRekapParkir.Count; i++)
                    {

                        dataGridView1.Rows.Add(
                            listRekapParkir[i].id,
                            listRekapParkir[i].nomor_induk,
                            listRekapParkir[i].waktu,
                            listRekapParkir[i].jenis_parkir
                            );
                    }
                });
            }
            else
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView1.DataSource = null;
                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Nomor Induk";
                dataGridView1.Columns[2].HeaderText = "Waktu";
                dataGridView1.Columns[3].HeaderText = "Jenis Parkir";
                for (int i = 0; i < listRekapParkir.Count; i++)
                {

                    dataGridView1.Rows.Add(
                        listRekapParkir[i].id,
                        listRekapParkir[i].nomor_induk,
                        listRekapParkir[i].waktu,
                        listRekapParkir[i].jenis_parkir
                        );
                }
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
