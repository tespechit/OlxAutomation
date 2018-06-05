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
            List<string> listmessages = new List<string>();

            again:
            try
            {

            
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://sp.olx.com.br/sao-paulo-e-regiao/zona-norte/santana");

            lst = driver.FindElements(By.ClassName("OLXad-list-link")).ToList();

            foreach(IWebElement elem in lst)
            {
                listmessages.Add(elem.Text);
            }

            string message = listmessages.Last();

            if(lastmessage == message)
            {
                //MessageBox.Show("Não há novos anúncios");
            }
            else
            {
                SendMail("Novo Item no OLX em Santana", message);
                lastmessage = listmessages.Last();
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
            objeto_mail.Subject = subject;
            objeto_mail.Body = message;
            client.Send(objeto_mail);

        }
    }
}
