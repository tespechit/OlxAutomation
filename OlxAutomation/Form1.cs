using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlxAutomation
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        string lastmessage;
        ChromeOptions options = new ChromeOptions();

        List<Anuncio> lstanounces = new List<Anuncio>();


        public Form1()
        {
            options.AddArguments("--headless");
            InitializeComponent();
        }

        private void BtoMonitor_Click(object sender, EventArgs e)
        {
            //SendMail("Teste", "Teste message");
            //OpenOlx();

            while (true)
            {
                GetAnounces();
                Thread.Sleep(2000);
            }

        }

        private void OpenOlx()
        {
            //driver = new ChromeDriver(options);

        }

        private void GetAnounces()
        {
            List<IWebElement> lst = new List<IWebElement>();
            List<Anuncio> listmessages = new List<Anuncio>();

            again:
            try
            {
                driver = new ChromeDriver(options);
                driver.Navigate().GoToUrl("https://sp.olx.com.br/sao-paulo-e-regiao/zona-norte/santana");

                lst = driver.FindElements(By.ClassName("OLXad-list-link")).ToList();

                foreach (IWebElement elem in lst)
                {
                    listmessages.Add(new Anuncio { Title = elem.GetAttribute("href"), Texto = elem.Text });
                }

                foreach (Anuncio msg in listmessages)
                {
                    bool alreadyExists = lstanounces.Any(x => x.Title == msg.Title);

                    if (!alreadyExists)
                    {
                        lstanounces.Add(msg);
                        SendMail(msg.Texto, msg.Title);
                    }


                }


                string message = listmessages.Last().Title;

                if (lastmessage == message)
                {
                    //MessageBox.Show("Não há novos anúncios");
                }
                else
                {

                    lastmessage = listmessages.Last().Title;
                }

                driver.Close();
                driver.Dispose();
            }
            catch
            {
                //driver.Close();
                driver.Dispose();
                goto again;
            }
        }


        private static void SendMail(string subject, string message)
        {
            //string mensagemtratada = subject;
            //int posmsg = mensagemtratada.
            string valor = subject;
            try
            {

                string mensagemtratada = subject;
                int hasValue = subject.IndexOf("R$");
                int teste2 = subject.IndexOf("\r\n");
                string infoanuncio = "";

                if (hasValue > 0)
                {
                    infoanuncio = subject.Substring(hasValue, subject.Length - hasValue);
                    infoanuncio = infoanuncio.Replace("\r\n", "-");
                    //mensagemtratada = subject.Replace("\r\n", string.Empty);
                }
                else
                {

                }

                mensagemtratada = subject.Replace("\r\n", " - ");
                //mensagemtratada = mensagemtratada.Substring(1, mensagemtratada.IndexOf("São Paulo, Santana") - 1);
                //mensagemtratada.Substring();
                MailMessage objeto_mail = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("ribeiro.rs@gmail.com", "c299792458MS");
                objeto_mail.From = new MailAddress("ribeiro.rs@gmail.com");
                objeto_mail.To.Add(new MailAddress("ribeiro.rs@gmail.com"));
                objeto_mail.Subject = mensagemtratada;
                objeto_mail.Body = message;
                client.Send(objeto_mail);

            }
            catch (Exception ex)
            {

            }

        }
    }
}
