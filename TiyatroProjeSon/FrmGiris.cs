using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TiyatroProjeSon
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sorgula = new SqlCommand("Select *From tbl_kullanicilar WHERE kullaniciAdi=@p1 AND kullaniciSifre=@p2", baglanti);
            sorgula.Parameters.AddWithValue("@p1",txtKullaniciadi.Text);
            sorgula.Parameters.AddWithValue("@p2", txtKullaniciSifre.Text);
            SqlDataReader dr = sorgula.ExecuteReader();
            if(dr.Read())
            {
                FrmAnaForm fr = new FrmAnaForm();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatali Kullanıcı adı & Şifre!");
            }
            baglanti.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmKayit fr = new FrmKayit();
            fr.ShowDialog();
        }
    }
}
