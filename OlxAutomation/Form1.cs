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
        ChromeDriverService service = ChromeDriverService.CreateDefaultService();

        string lastUrl;
        
        List<Anuncio> lstanounces = new List<Anuncio>();


        public Form1()
        {
            service.HideCommandPromptWindow = true;
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
                //driver = new ChromeDriver(service,options);
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://sp.olx.com.br/sao-paulo-e-regiao/zona-norte/santana/celulares");

                lst = driver.FindElements(By.ClassName("OLXad-list-link")).ToList();

                lastUrl = lst[0].GetAttribute("href");

                foreach (IWebElement elem in lst)
                {
                    if(lastUrl == elem.GetAttribute("href"))
                    {
                        break;
                    }
                    Anuncio anuncio = new Anuncio();

                    anuncio.URL = elem.GetAttribute("href");
                    anuncio.Texto = elem.Text;

                    //driver.FindElement(By.CssSelector("body")).SendKeys(System.Windows.Forms.Keys.Control + "t");
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    driver.Navigate().GoToUrl(anuncio.URL);

                    Thread.Sleep(1000);
                    try
                    {
                        string valor = driver.FindElement(By.ClassName("actual-price")).Text;
                        valor = valor.Replace("R$", string.Empty);
                        valor = valor.Replace("R", string.Empty);
                        valor = valor.Replace("$", string.Empty);
                        valor = valor.Replace(" ", string.Empty);

                        anuncio.Valor = float.Parse(valor);
                    }
                    catch(Exception ex)
                    {

                    }


                    anuncio.Texto = driver.FindElement(By.Id("ad_title")).Text.ToUpper();

                    string[] modelist = {
                        "APPLE WATCH",
                        "IPHONE X 256GB",
                        "IPHONE X 128GB",
                        "IPHONE X 64GB",
                        "IPHONE X 32GB",
                        "IPHONE 4",
                        "IPHONE 5 SE 128GB",
                        "IPHONE 5 SE 64GB",
                        "IPHONE 5 SE 32GB",
                        "IPHONE 5S 16GB",
                        "IPHONE 5S 32GB",
                        "IPHONE 5S 64GB",
                        "IPHONE 5S",
                        "IPHONE 6S 128GB",
                        "IPHONE 6S 128GB",
                        "IPHONE 6S 64GB",
                        "IPHONE 6S 32GB",
                        "IPHONE 6S",
                        "IPHONE 6",
                        "IPHONE 7 128GB",
                        "IPHONE 7 64GB",
                        "IPHONE 7 32GB",
                        "IPHONE 7 PLUS 128GB",
                        "IPHONE 7 PLUS 64GB",
                        "IPHONE 7 PLUS 32GB",

                        "MOTOG 4 PLAY",
                        "MOTO G 4 PLAY",
                        "MOTO G4 PLAY",
                        "MOTO G4",
                        "MOTO G5",
                        "MOTO X FORCE 64GB",
                        "MOTO G 5",
                        "GALAXY S8",
                        "GALAXY S7",
                        "GALAXY S6",
                        "GALAXY S5",
                        "GALAXY S4",
                        "GALAXY A5",
                        "A5",
                        "GALAXY J7 PRIME",
                        "GALAXY J7",
                        "J7 NEO",
                        "J7",
                        "SAMSUNG S8",
                        "SAMSUNG S7",
                        "SAMSUNG S6",
                        "SAMSUNG S5",
                        "SONY XPERIA Z2",
                        "XPERIA Z2",
                        "QUANTUM MUV",
                        "XIAOMI MI BAND 1S",
                        "XIAOMI",
                        "Z2",
                        "SONY",
                        "MOTO G3",
                        "J5 DUAL CHIP",
                        "J3",
                        "J5",
                        "J2",
                        "SAMSUNG",
                        "CCE",
                        "MOTO Z2",
                        "MOTO X2",
                        "S7 EDGE",
                        "MOTOROLA",
                        "APPLE",
                        "P20",
                        "MODEM VIVO WI FI",
                        "MODEM",
                        "GEAR FIT2 PRO",
                        "IDENTIFICADOR DE CHAMADAS"
                    };

                    int pos = -1;
                    string chosenmodel = "";
                    bool hasfound = false;
                    foreach(string model in modelist)
                    {
                        int container = anuncio.Texto.IndexOf(model);
                        pos = anuncio.Texto.IndexOf(model);
                        //implementar lógica para caso o anuncio contenha mais de um item na descrição
                        if(pos > -1 && !hasfound)
                        {
                            anuncio.Modelo = model;
                            hasfound = true;
                            break;
                        }
                        
                    }




                    if(pos == -1)
                    {
                        //## try with text ToDo
                        anuncio.Texto = driver.FindElement(By.ClassName("OLXad-description")).Text.ToUpper();

                        foreach (string model in modelist)
                        {
                            int container = anuncio.Texto.IndexOf(model);
                            pos = anuncio.Texto.IndexOf(model);
                            //implementar lógica para caso o anuncio contenha mais de um item na descrição
                            if (pos > -1 && !hasfound)
                            {
                                anuncio.Modelo = model;
                                hasfound = true;
                                break;
                            }

                        }

                    }
                    else
                    {
                        anuncio.Modelo = chosenmodel;
                    }

                    if(anuncio.Modelo == null)
                    {
                        MessageBox.Show("Modelo não identificado!");
                    }

                    ((IJavaScriptExecutor)driver).ExecuteScript("window.close();");
                    driver.SwitchTo().Window(driver.WindowHandles.First());

                    listmessages.Add(anuncio);

                }



                foreach (Anuncio msg in listmessages)
                {
                    //bool alreadyExists = lstanounces.Any(x => x.Title == msg.Title);

                    //if (!alreadyExists)
                    //{
                    //    lstanounces.Add(msg);
                    //    SendMail(msg.Texto, msg.Title);
                    //}


                }


                //string message = listmessages.Last().Title;

                //if (lastmessage == message)
                //{
                //    //MessageBox.Show("Não há novos anúncios");
                //}
                //else
                //{

                //    lastmessage = listmessages.Last().Title;
                //}

                driver.Close();
                driver.Dispose();
            }
            catch(Exception ex)
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
