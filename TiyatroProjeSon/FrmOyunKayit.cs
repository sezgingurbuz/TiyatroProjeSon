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
    public partial class FrmOyunKayit : Form
    {
        public FrmOyunKayit()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtOyunDetay_TextChanged(object sender, EventArgs e)
        {
            int karakterSayisi = txtOyunDetay.Text.Length;
            int kalanKarakterSayisi = 300 - karakterSayisi;
            lblkarakter.Text = kalanKarakterSayisi.ToString();
            if(kalanKarakterSayisi == 0)
            {
                lblkarakter.ForeColor = Color.Red;
            }
            else
            {
                lblkarakter.ForeColor=Color.Peru;
            }
        }

        private void lblTrajedi_Click(object sender, EventArgs e)
        {
            if(lblTrajedi.ForeColor == Color.SlateGray)
            {
                lblTrajedi.ForeColor = Color.DarkOrange;
            }
            else
            {
                lblTrajedi.ForeColor = Color.SlateGray;
            }
        }

        private void lblDrama_Click(object sender, EventArgs e)
        {
            if (lblDrama.ForeColor == Color.SlateGray)
            {
                lblDrama.ForeColor = Color.DarkOrange;
            }
            else
            {
                lblDrama.ForeColor = Color.SlateGray;
            }
        }

        private void lblKomedi_Click(object sender, EventArgs e)
        {
            if (lblKomedi.ForeColor == Color.SlateGray)
            {
                lblKomedi.ForeColor = Color.DarkOrange;
            }
            else
            {
                lblKomedi.ForeColor = Color.SlateGray;
            }
        }

        private void lblMuzikal_Click(object sender, EventArgs e)
        {
            if (lblMuzikal.ForeColor == Color.SlateGray)
            {
                lblMuzikal.ForeColor = Color.DarkOrange;
            }
            else
            {
                lblMuzikal.ForeColor = Color.SlateGray;
            }
        }

        private void lblKorku_Click(object sender, EventArgs e)
        {
            if (lblKorku.ForeColor == Color.SlateGray)
            {
                lblKorku.ForeColor = Color.DarkGreen;
                pbKorku.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\unlocked.png";
            }
            else
            {
                lblKorku.ForeColor = Color.SlateGray;
                pbKorku.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\lock.png";
            }
        }

        private void lblGenel_Click(object sender, EventArgs e)
        {
            if (lblGenel.ForeColor == Color.SlateGray)
            {
                lblGenel.ForeColor = Color.DarkGreen;
                pbGenel.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\unlocked.png";
            }
            else
            {
                lblGenel.ForeColor = Color.SlateGray;
                pbGenel.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\lock.png";
            }
        }

        private void lblCocuk_Click(object sender, EventArgs e)
        {
            if (lblCocuk.ForeColor == Color.SlateGray)
            {
                lblCocuk.ForeColor = Color.DarkGreen;
                pbCocuk.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\unlocked.png";
            }
            else
            {
                lblCocuk.ForeColor = Color.SlateGray;
                pbCocuk.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\lock.png";
            }
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            if (lbl7.ForeColor == Color.SlateGray)
            {
                lbl7.ForeColor = Color.DarkGreen;
                pb7.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\unlocked.png";
            }
            else
            {
                lbl7.ForeColor = Color.SlateGray;
                pb7.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\lock.png";
            }
        }

        private void lbl13_Click(object sender, EventArgs e)
        {
            if (lbl13.ForeColor == Color.SlateGray)
            {
                lbl13.ForeColor = Color.DarkGreen;
                pb13.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\unlocked.png";
            }
            else
            {
                lbl13.ForeColor = Color.SlateGray;
                pb13.ImageLocation = @"D:\Yazilim\C#\Git koyulabilecek projeler\TiyatroProjeSon\img\lock.png";
            }
        }
        
        void TemizlemeMetodu()
        {
            this.Controls.Clear();
            this.InitializeComponent();
            txtOyunAdi.Focus();

        }
        string secilentur = "";
        string secilenOzellik = "";
        void Tur()
        {
            foreach(Control arac in grbTur.Controls)
            {
                if(arac is Label)
                {
                    if(arac.ForeColor == Color.SlateGray)
                    {

                    }
                    else
                    {
                        //Seçilmiş ise
                        secilentur += " ," + arac.Text.ToString();

                    }
                }
                
            }
        }
        void Ozellik()
        {
            foreach(Control arac in grbOzellik.Controls)
            {
                if(arac is Label)
                {
                    if(arac.ForeColor == Color.SlateGray)
                    {

                    }
                    else
                    {
                        //seçilmiş ise
                        secilenOzellik += " ," + arac.Text.ToString();
                    }
                }
            }
        }
        string resimyolu = "";
        
        private void btnResimYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "AFİŞ SEÇME EKRANI";
            openFileDialog.Filter = "PNG |*.png |JPG | *.jpg | JPEG| *.jpeg | All Files | *.*";
            openFileDialog.FilterIndex = 4;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //resim alma işlemi bu alanda gerçekleştirilecektir.
                pbAfis.Image = new Bitmap(openFileDialog.FileName);
                resimyolu = openFileDialog.FileName.ToString();
            }
        }

        private void btnKayitTamamla_Click(object sender, EventArgs e)
        {
            Tur();
            Ozellik();
            //input kontrolü
            if(txtOyunAdi.Text != "" && txtOyuncular.Text != "" && txtOyunDetay.Text != "" && resimyolu != "" && secilenOzellik != "" && secilentur != "")
            {
                string sorgu = "INSERT INTO tbl_Oyunlar (ADI,TURU,OZELLIKLERI,OYUNCULAR,AFIS,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6)";
                baglanti.Open();
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", txtOyunAdi.Text.ToUpper());
                if(secilentur.Length> 2 ) // Yapılan seçim ve seçimin uzunluğu
                {
                    komut.Parameters.AddWithValue("@p2",secilentur.Substring(2));
                }
                else
                {
                    komut.Parameters.AddWithValue("@p2", secilentur);
                }
                if (secilenOzellik.Length > 2) // Yapılan seçim ve seçimin uzunluğu
                {
                    komut.Parameters.AddWithValue("@p3", secilenOzellik.Substring(2));
                }
                else
                {
                    komut.Parameters.AddWithValue("@p3", secilenOzellik);
                }
                komut.Parameters.AddWithValue("@p4",txtOyuncular.Text.ToUpper());
                komut.Parameters.AddWithValue("@p5", resimyolu);
                komut.Parameters.AddWithValue("@p6", txtOyunDetay.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("OYUN KAYDI BAŞARIYLA TAMAMLANMIŞTIR");
                TemizlemeMetodu();
            }
            else
            {
                MessageBox.Show("BOŞ ALAN BIRAKILMAMALIDIR!");
            }
        }

        private void FrmOyunKayit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
