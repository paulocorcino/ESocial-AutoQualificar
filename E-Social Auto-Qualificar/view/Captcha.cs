using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace E_Social_Auto_Qualificar.view
{
    public partial class Captcha : Form
    {

        public string ImagemUrl { get; set; }
        public string CaptchaTxt { get; set; }
        public CookieCollection Cookie { get; set; }

        public Captcha()
        {
            InitializeComponent();          
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCaptha.Text))
                MessageBox.Show("Favor digitar o texto exibido na imagem","Capcha",MessageBoxButtons.OK, MessageBoxIcon.Error);

            CaptchaTxt = txtCaptha.Text;
            txtCaptha.Focus();

            if (!String.IsNullOrEmpty(CaptchaTxt))
                this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //ImagemUrl = "http://www9.dataprev.gov.br/Esocial/faces/resource/1394728545142/CAPTCHA.xhtml?rsrc=j_idt68";

            //imgCap.Load(ImagemUrl);
            //imgCap.LoadAsync(ImagemUrl);
            GetImageCaptcha();
            txtCaptha.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtCaptha.Text = "";
            this.Close();
        }

        private void Captcha_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (String.IsNullOrEmpty(CaptchaTxt))
                CaptchaTxt = "NULO";
        }

        private void GetImageCaptcha()
        {
            try
            {
                CookieContainer c = new CookieContainer();
                c.Add(Cookie);

                var request = (HttpWebRequest)WebRequest.Create(ImagemUrl);
                request.Method = "GET";
                request.Accept = "image/webp,image/png,image/*";
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
                request.CookieContainer = c;

                var resp = request.GetResponse();
                var respStream = resp.GetResponseStream();
                var contentLen = resp.ContentLength;
                byte[] outData;
                using (var tempMemStream = new MemoryStream())
                {
                    byte[] buffer = new byte[128];
                    while (true)
                    {
                        int read = respStream.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                        {
                            outData = tempMemStream.ToArray();
                            break;
                        }
                        tempMemStream.Write(buffer, 0, read);
                    }
                }
                imgCap.Image = new Bitmap(new MemoryStream(outData));

                request = null;
                resp.Close();
                resp = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível obter a imagem, caso desetentar novamente clique em Atualizar, caso deseje parar clique em cancelar.", "Capcha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                imgCap.Image = null;
            }


        }

        private void Captcha_Shown(object sender, EventArgs e)
        {
            btnRefresh_Click(null, EventArgs.Empty);
        }

        private void txtCaptha_Enter(object sender, EventArgs e)
        {
            try
            {
                ActiveForm.AcceptButton = button1;
            }
            catch (Exception ex)
            {
            }

        }

        private void Captcha_Load(object sender, EventArgs e)
        {
            model.dtoConfiguracao dto = new model.dtoConfiguracao();
            var c = dto.GetConfiguracao();
            dto = null;

            if(c.SOMCAPTCHA)
                Console.Beep(5000, 2000);
        }
    }
}
