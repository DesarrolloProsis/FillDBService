using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaFillDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_Probar_Click(object sender, EventArgs e)
        {
            FillDBService.Service1 FillDBService = new FillDBService.Service1();
            FillDBService.Prueba();
            textBox1.Text = FillDBService.Notification;
        }
    }
}
