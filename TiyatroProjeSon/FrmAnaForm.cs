using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiyatroProjeSon
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            FrmOyunKayit fr = new FrmOyunKayit();
            fr.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmOyunListe fr = new FrmOyunListe();
            fr.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmSalonKayit fr = new FrmSalonKayit();
            fr.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FrmSalonAtama fr = new FrmSalonAtama();
            fr.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FrmBiletOlustur fr = new FrmBiletOlustur();
            fr.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FrmBiletSorgula fr = new FrmBiletSorgula();
            fr.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            FrmRaporEkrani fr = new FrmRaporEkrani();
            fr.ShowDialog();
        }
       
        private void FrmAnaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
