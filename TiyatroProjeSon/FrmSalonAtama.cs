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
    public partial class FrmSalonAtama : Form
    {
        public FrmSalonAtama()
        {
            InitializeComponent();

        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void OyunAdiGetir()
        {
            baglanti.Open();
            string sorgu = "SELECT *FROM tbl_Oyunlar ORDER BY ADI ASC";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbOyunAdi.Items.Add(oku["ADI"].ToString());
            }
            baglanti.Close();
        }
        void salonAdiGetir()
        {
            baglanti.Open();
            string sorgu = "SELECT *FROM tbl_Salonlar ORDER BY SALONADI ASC";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbSalonAdi.Items.Add(oku["SALONADI"].ToString());
            }
            baglanti.Close();
        }

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            string tarih = $"{(int)nGun.Value}-{(int)nAy.Value:D2}-{(int)nYil.Value:D2}";
            string sorgu = "SELECT DISTINCT *FROM tbl_Seanslar WHERE SALON=@salonadi AND TARIH=@tarih";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@salonadi", cmbSalonAdi.Text.ToString());
            komut.Parameters.AddWithValue("@tarih", tarih);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbDoluSaatler.Items.Add(oku["SAAT"].ToString());
            }
            baglanti.Close();
            SeansKontrol();
        }
        private void SeansSaatleri(object sender, EventArgs e)
        {
            foreach(RadioButton item in panelSeans.Controls)
            {
                if(item.Checked) //item seçili olma durumu 1 ise
                {
                    lblSeans.Text = item.Text.ToString(); // Radiobuttonda seçilen saati lblseans'a attık.
                }
            }
        }
        void SeansKontrol()
        {
            panelSeans.Controls.Clear();
            for (int i = 10; i <= 22; i++)
            {
                for (int j = 0; j <= 30; j += 30) //Sadece tam saat ve buçuk geçe film ekleyeceğimizi düşünüp 30'a kadar yazdık
                {
                    RadioButton rnd = new RadioButton();
                    rnd.ForeColor = Color.DarkOrange;
                    rnd.FlatStyle = FlatStyle.Flat;
                    rnd.CheckedChanged += new EventHandler(SeansSaatleri);
                    if (j == 0)
                    {
                        rnd.Text = i.ToString() + ":" + j.ToString() + "0";
                    }
                    else
                    {
                        rnd.Text = i.ToString() + ":" + j.ToString();
                    }
                    if (cmbDoluSaatler.Items.Contains(rnd.Text))
                    {
                        rnd.Enabled = false;
                    }
                    panelSeans.Controls.Add(rnd);
                }

            }
        }
        void Kaydet()
        {
            string sorgu = "INSERT INTO tbl_Seanslar (OYUNADI,TARIH,SAAT,SALON) VALUES (@oyunadi,@tarih,@saat,@salon)";
            string tarih = $"{(int)nGun.Value}-{(int)nAy.Value:D2}-{(int)nYil.Value:D2}";
            baglanti.Open();
            SqlCommand ekle = new SqlCommand(sorgu, baglanti);
            ekle.Parameters.AddWithValue("@oyunadi",cmbOyunAdi.Text);
            ekle.Parameters.AddWithValue("@tarih", tarih);
            ekle.Parameters.AddWithValue("@saat", lblSeans.Text);
            ekle.Parameters.AddWithValue("@salon", cmbSalonAdi.Text);
            ekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("SEANS BAŞARIYLA EKLENMİŞTİR");

        }

        private void FrmSalonAtama_Load(object sender, EventArgs e)
        {
            OyunAdiGetir();
            salonAdiGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kaydet();
            cmbDoluSaatler.Items.Clear();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            cmbSalonAdi.Text = "";
            cmbOyunAdi.Text = "";
            cmbOyunAdi.Items.Clear();
            cmbSalonAdi.Items.Clear();
            cmbDoluSaatler.Items.Clear();
            lblSeans.Text = "";
            OyunAdiGetir();
            salonAdiGetir();
            panelSeans.Controls.Clear();
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu = "DELETE FROM tbl_Seanslar WHERE CONVERT(date, TARIH, 103) < DATEADD(day, -2, CONVERT(date, GETDATE()))";
                baglanti.Open();
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                baglanti.Close();
                MessageBox.Show("Geçmiş tarihli seanslar başarıyla temizlendi");
            }
        }
    }
}
