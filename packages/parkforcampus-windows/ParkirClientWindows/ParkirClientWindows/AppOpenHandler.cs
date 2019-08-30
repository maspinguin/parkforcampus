using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Diagnostics;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    class AppOpenHandler
    {
        public static void CheckToken()
        {
            try
            {
                bool checkToken = true;
                string path = "Auth/checkToken";
                RestRequest request = Configuration.getHttpConfig(path, checkToken);

                /*Configuration.CLIENT.ExecuteAsync(request, response => {
                    Debug.WriteLine(response.Content);
                });
                */
                //IRestResponse response = client.Execute(request);
                //var content = response.Content;
                
                IRestResponse<Model.ResponseModel> response2 = Configuration.CLIENT.Execute<Model.ResponseModel>(request);
                if(response2 != null)
                {
                    if(response2.Data.status == 200)
                    {
                        bool isTokenValid = response2.Data.valid;

                        if (isTokenValid)
                        {
                            Configuration.LOGINNAMA = response2.Data.nama;
                            Configuration.LOGINNIP = response2.Data.nomor_induk;
                            Application.Run(new MainForm());
                        }
                        else
                        {

                            
                            MessageBox.Show(response2.Data.message, "Error token");
                            Application.Run(new LoginForm());
                       
                        }
                    }
                    else
                    {
                        MessageBox.Show(response2.Data.message, "Error token");
                        LoginForm login = new LoginForm();
                        login.ShowDialog();
                        
                    }


                    //client.ExecuteAsync(request, response => {
                    //    Debug.WriteLine(response.Content);
                    //});
                } else
                {
                    MessageBox.Show("Network/ Server Error!");
                    Application.Exit();
                }
                


                //var asyncHandle = client.ExecuteAsync<Model.ResponseModel>(request, response => {
                //    Debug.WriteLine(response.Data.message);
                //});

                // abort the request on demand
                //asyncHandle.Abort();
            }
            catch (Exception err)
            {

                MessageBox.Show(err.ToString(), "Server Error!");
                Application.Exit();
            }
        }
    }
}
