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
using System.Data.SqlClient;

namespace TiyatroProjeSon
{
    public partial class FrmOyunDetay : Form
    {
        public FrmOyunDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        public string idNo = "";
        public string oyunAdi = "";
        public string resimyolu = ""; // Update öncesi gelen afiş
        public string resimyolu2 = ""; // Update sonrası gelen afiş
        private void verileriGoster()
        {
            string sorgu = "SELECT *FROM tbl_Oyunlar WHERE ID=@p1";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", idNo);
            SqlDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                txtOyunAdi.Text = oku["ADI"].ToString();
                txtOyunTuru.Text = oku["TURU"].ToString();
                txtOyunOzellikleri.Text = oku["OZELLIKLERI"].ToString();
                txtOyuncular.Text = oku["OYUNCULAR"].ToString();
                pbAfis.ImageLocation = oku["AFIS"].ToString();
                resimyolu = oku["AFIS"].ToString();
                txtOyunDetay.Text = oku["DETAY"].ToString();
            }
            baglanti.Close();
        }
        private void FrmOyunDetay_Load(object sender, EventArgs e)
        {
            verileriGoster();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if(btnDuzenle.BackColor == Color.LightSeaGreen) //Butonda Düzenle yazıyorsa
            {
                btnResimYukle.Visible = true;
                txtOyunAdi.ReadOnly = false;
                txtOyuncular.ReadOnly = false;
                txtOyunDetay.ReadOnly = false;
                txtOyunOzellikleri.ReadOnly = false;
                txtOyunDetay.ReadOnly = false;
                txtOyunTuru.ReadOnly = false;
                btnDuzenle.Text = "KAYDET";
                btnDuzenle.BackColor = Color.Lime;
               

            }
            else // Butonda Kaydet yazıyorsa
            {
                baglanti.Open();
                string sorgu = "UPDATE tbl_Oyunlar SET ADI=@k1,TURU=@k2, OZELLIKLERI=@k3, OYUNCULAR=@k4, AFIS=@k5, DETAY=@k6 WHERE ID=@id ";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@id", idNo);
                komut.Parameters.AddWithValue("@k1", txtOyunAdi.Text);
                komut.Parameters.AddWithValue("@k2", txtOyunTuru.Text);
                komut.Parameters.AddWithValue("@k3", txtOyunOzellikleri.Text);
                komut.Parameters.AddWithValue("@k4", txtOyuncular.Text);
                if (resimyolu2 == "")
                {
                    komut.Parameters.AddWithValue("@k5", resimyolu);
                }
                else
                {
                    komut.Parameters.AddWithValue("@k5", resimyolu2);
                }
                komut.Parameters.AddWithValue("@k6", txtOyunDetay.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("OYUN BİLGİLERİ BAŞARIYLA GÜNCELLENMİŞTİR");
                btnDuzenle.Text = "Düzenle";
                btnDuzenle.BackColor = Color.LightSeaGreen;
                verileriGoster();
                btnResimYukle.Visible = false;
                txtOyunAdi.ReadOnly = true;
                txtOyuncular.ReadOnly = true;
                txtOyunDetay.ReadOnly = true;
                txtOyunOzellikleri.ReadOnly = true;
                txtOyunDetay.ReadOnly = true;
                txtOyunTuru.ReadOnly = true;
            }

        }

        private void btnResimYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "AFİŞ SEÇME EKRANI";
            openFileDialog.Filter = "PNG |*.png |JPG | *.jpg | JPEG| *.jpeg | All Files | *.*";
            openFileDialog.FilterIndex = 4;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //resim alma işlemi bu alanda gerçekleştirilecektir.
                pbAfis.Image = new Bitmap(openFileDialog.FileName);
                resimyolu = openFileDialog.FileName.ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("OYUN SİLİNECEKTİR. EMİN MİSİNİZ?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                //Evete basılırsa yapılacak işlemler
                
                try
                {
                    baglanti.Open();
                    string sorgu = "DELETE FROM tbl_Oyunlar WHERE ID=@p1";
                    SqlCommand sil = new SqlCommand(sorgu, baglanti);
                    sil.Parameters.AddWithValue("@p1", idNo);
                    sil.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Hata" + ex.Message);
                }
                finally
                {
                    MessageBox.Show("OYUN SİLİNMİŞTİR!");
                    baglanti.Close();
                    this.Close();
                }
            }
            else
            {
                //Hayıra basılırsa yapılacak işlemler
                MessageBox.Show("İşlem iptal edildi");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
