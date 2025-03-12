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
    public partial class FrmKayit : Form
    {
        public FrmKayit()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        void Temizle()
        {
            txtIsim.Text = "";
            txtKullaniciadi.Text = "";
            txtSifre.Text = "";
            txtSoyisim.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
           try
            {
                string sorgu = "INSERT INTO tbl_Kullanicilar (kullaniciAdi, kullaniciSifre, kullaniciIsim, kullaniciSoyad) VALUES (@p1,@p2,@p3,@p4)";
                SqlCommand kayit = new SqlCommand(sorgu, baglanti);
                kayit.Parameters.AddWithValue("@p1", txtKullaniciadi.Text);
                kayit.Parameters.AddWithValue("@p2", txtSifre.Text);
                kayit.Parameters.AddWithValue("@p3", txtIsim.Text);
                kayit.Parameters.AddWithValue("@p4", txtSoyisim.Text);
                kayit.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Kayıt oluşturulurken hata oluştu." + ex.Message);
            }
            finally
            {
                baglanti.Close();
                MessageBox.Show("Kayıt işlemi başarıyla tamamlandı.");
                Temizle();
            }
            
        }
    }
}
