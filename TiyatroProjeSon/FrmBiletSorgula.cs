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
    public partial class FrmBiletSorgula : Form
    {
        public FrmBiletSorgula()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtBiletNo.Text != "")
            {
                string sorgu = "Select *FROM tbl_Biletler WHERE BKOD=@a1";
                baglanti.Open();
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@a1", txtBiletNo.Text);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    FrmBiletDetay frm = new FrmBiletDetay();
                    frm.biletNo = txtBiletNo.Text.ToString();
                    txtBiletNo.Text = "";
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("KAYITLI BİLET BULUNAMADI");
                    baglanti.Close();
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("LÜTFEN BİLET NUMARASI GİRİNİZ");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
