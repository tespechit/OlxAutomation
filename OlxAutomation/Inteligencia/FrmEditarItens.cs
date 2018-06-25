using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlxAutomation.Inteligencia
{
    public partial class FrmEditarItens : Form
    {
        public FrmEditarItens()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModeloCelular modelo = new ModeloCelular();

            modelo.Fabricante = txtFabricante.Text;
            modelo.Categoria = txtCategoria.Text;
            modelo.Modelo = txtModelo.Text;
            modelo.Versao = txtVersao.Text;


            GenerateTag(modelo);

        }


        private string GenerateTag(ModeloCelular modelo)
        {
            string teste = modelo.Versao;

            string[] arr = teste.Split(' ');

            foreach(string t in arr)
            {
                teste = teste + t;
            }


            return "";
        }


    }
}
