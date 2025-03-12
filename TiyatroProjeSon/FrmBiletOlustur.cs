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
    public partial class FrmBiletOlustur : Form
    {
        public FrmBiletOlustur()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        private void FrmBiletOlustur_Load(object sender, EventArgs e)
        {
            OyunAdiGetir();
            BiletNoOlustur();
            
        }
        void OyunAdiGetir()
        {
            string sorgu = "SELECT *FROM tbl_Oyunlar ORDER BY ADI ASC";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while(oku.Read())
            {
                cmbOyunAdi.Items.Add(oku["ADI"].ToString());
            }
            baglanti.Close();
        }
        void BiletNoOlustur()
        {
            //Random ile Kod oluşturma işlemi gerçekleştirilecek.
            Random rnd = new Random();
            string karakterler = "123456789987654321123456789987654321";//Şifreyi oluşturacak karakterler
            string kod = "";

            for (int i = 0; i < 5; i++)
            {
                kod += karakterler[rnd.Next(karakterler.Length)];

            }
            txtBiletNo.Text = kod.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbOyunAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //distinct --> Veritabanımızda kayıtlı olan aynı türden veriler arasında sadece 1 tanesini getirir. Dikkat diğer verilerin hiçbirini silmez, sadece bir tanesini gösterir.
            cmbTarih.Items.Clear();
            string sorgu = "SELECT DISTINCT TARIH FROM tbl_Seanslar WHERE OYUNADI=@oyunadi";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@oyunadi", cmbOyunAdi.Text.ToString());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbTarih.Items.Add(oku["TARIH"].ToString());
            }
            baglanti.Close();
        }

        private void cmbTarih_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelSeans.Controls.Clear();
            string saatler = "";
            string sorgu = "SELECT DISTINCT SAAT FROM tbl_Seanslar WHERE OYUNADI=@oyunadi AND TARIH=@tarih";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@oyunadi", cmbOyunAdi.Text.ToString());
            komut.Parameters.AddWithValue("@tarih",cmbTarih.Text.ToString());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                saatler = oku["SAAT"].ToString();
                RadioButton rnd = new RadioButton();
                rnd.Text = saatler;
                rnd.ForeColor = Color.Red;
                rnd.FlatStyle = FlatStyle.Flat;
                rnd.CheckedChanged += new EventHandler(SeansSaatleri);
                panelSeans.Controls.Add(rnd);
            }
            baglanti.Close();
        }
        private void SeansSaatleri(object sender, EventArgs e)
        {
            //Burada radioButton'un seçili olma durumunu kontrol ediyoruz
            foreach(RadioButton item in panelSeans.Controls)
            {
                if(item.Checked) //RadioButton checked ise
                {
                    lblsecilenSeans.Text = item.Text;
                    cmbSalonAdi.Items.Clear();

                    string sorgu = "SELECT DISTINCT SALON FROM tbl_Seanslar WHERE OYUNADI=@oyunadi AND TARIH=@tarih AND SAAT=@saat";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@oyunadi", cmbOyunAdi.Text.ToString());
                    komut.Parameters.AddWithValue("@tarih", cmbTarih.Text.ToString());
                    komut.Parameters.AddWithValue("@saat", lblsecilenSeans.Text);
                    SqlDataReader oku = komut.ExecuteReader();
                    while(oku.Read())
                    {
                        cmbSalonAdi.Items.Add(oku["SALON"].ToString());
                    }
                    baglanti.Close();
                }
            }
        }

        private void cmbSalonAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            KoltukSayisiGetir();
            pictureBox1.Visible = true;
            lblPerde.Visible = true;
            lblPerde.BringToFront();
        }
        void KoltukSayisiGetir()
        {
            string sorgu = "SELECT * FROM tbl_Salonlar WHERE SALONADI=@salonadi";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@salonadi", cmbSalonAdi.Text.ToString());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                lblKoltukSayisi.Text = oku["KOLTUKSAYISI"].ToString();
            }
            baglanti.Close();
            DoluKoltukGetir();
            KoltukOlustur();
        }
        void KoltukOlustur()
        {
            koltukPaneli.Controls.Clear();
            int sayi = Convert.ToInt16(lblKoltukSayisi.Text);
            for (int i = 1; i <= sayi; i++)
            {
                Button btn = new Button();
                if (i <= 10)
                {
                    btn.Text = "A" + i.ToString();
                    btn.Name = "A" + i.ToString();
                }
                else if (i <= 20)
                {
                    btn.Text = "B" + (i - 10).ToString();
                    btn.Name = "B" + (i - 10).ToString();
                }
                else if (i <= 30)
                {
                    btn.Text = "C" + (i - 10 * 2).ToString();
                    btn.Name = "C" + (i - 10 * 2).ToString();
                }
                else if (i <= 40)
                {
                    btn.Text = "D" + (i - 10 * 3).ToString();
                    btn.Name = "D" + (i - 10 * 3).ToString();
                }
                else if (i <= 50)
                {
                    btn.Text = "E" + (i - 10 * 4).ToString();
                    btn.Name = "E" + (i - 10 * 4).ToString();
                }
                else if (i <= 60)
                {
                    btn.Text = "F" + (i - 10 * 5).ToString();
                    btn.Name = "F" + (i - 10 * 5).ToString();
                }
                else if (i <= 70)
                {
                    btn.Text = "G" + (i - 10 * 6).ToString();
                    btn.Name = "G" + (i - 10 * 6).ToString();
                }
                else if (i <= 80)
                {
                    btn.Text = "H" + (i - 10 * 7).ToString();
                    btn.Name = "H" + (i - 10 * 7).ToString();
                }
                btn.Width = 50;
                btn.Height = 50;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0; //Çerçeveleri kaldırır.
                btn.Font = new System.Drawing.Font("Segoe UI Semibold", 12);
                btn.Click += Btn_Click;
                if (listeSeansKoltuklari.Items.Contains(btn.Text)) //buton numarası dolu koltuklar listbox'ında var mı kontrolü
                {
                    btn.BackgroundImage = Properties.Resources.kirmizi1;
                    btn.BackgroundImageLayout = ImageLayout.Zoom;
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.BackgroundImage = Properties.Resources.mavi1;
                    btn.BackgroundImageLayout = ImageLayout.Zoom;
                    btn.ForeColor = Color.Yellow;
                }
                koltukPaneli.Controls.Add(btn);
            }
        }
        void DoluKoltukGetir()
        {
            //Bu kod seanslar tablosundaki dolu koltukları getirir.
            lblSeanstakiKoltuk.Text = "";
            string sorgu = "SELECT *FROM tbl_Seanslar WHERE OYUNADI=@oyunadi AND TARIH=@tarih AND SAAT=@saat AND SALON=@salonadi";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@oyunadi", cmbOyunAdi.Text);
            komut.Parameters.AddWithValue("@tarih", cmbTarih.Text);
            komut.Parameters.AddWithValue("@SAAT", lblsecilenSeans.Text);
            komut.Parameters.AddWithValue("@salonadi", cmbSalonAdi.Text);
            SqlDataReader oku = komut.ExecuteReader();
            while(oku.Read())
            {
                
                lblSeanstakiKoltuk.Text += " ," + oku["KOLTUKLAR"].ToString();
                if(lblSeanstakiKoltuk.Text.Length > 2)
                {
                   lblSeanstakiKoltuk.Text =  lblSeanstakiKoltuk.Text.Substring(2);
                }
                else
                {
                    lblSeanstakiKoltuk.Text = "";
                }
            }
            baglanti.Close();
            KoltukAyirma();
        }
        void KoltukAyirma()
        {
            //Bu metod lblSeanstakiKoltuk üzerinde gelen koltuk numaralarını ayırmamıza yarar.
            listeSeansKoltuklari.Items.Clear();
            string no = "";
            string[] sec;
            no = lblSeanstakiKoltuk.Text.ToString();
            sec = no.Split(','); //Bu kod hangi karakteri belirttiysek o karakterde ayırma işlemi yapar. Biz burda arasına , giren kelimeleri ayırıyoruz.
            foreach(string bulunan in sec)
            {
                listeSeansKoltuklari.Items.Add(bulunan);
            }
        }
        void BiletNoSorgula()
        {
            //Bu alanda "programın kendi oluşturduğu bilet numarası başka bir bilette var mı" sorgulaması yapılacaktır.
            string sorgu = "SELECT *FROM tbl_Biletler WHERE BKOD=@bkod";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@bkod", txtBiletNo.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if(!oku.Read()) //Bu barkoddan bir tane daha yoksa
            {
                BiletOlustur();
            }
            else // Bu barkoddan bir tane daha varsa
            {
                BiletNoOlustur();
                baglanti.Close();
                BiletNoSorgula();
            }
            baglanti.Close() ;
        }
        void BiletOlustur()
        {
            string sorgu = "INSERT INTO tbl_Biletler (ADSOYAD,TELNO,BKOD,KOLTUKNO,OYUNADI,TARIH,SAAT,SALON,TUR,ISLEMSAATI,GUN) VALUES (@adsoyad1,@telno1,@bkod1,@koltukno1,@oyunadi1,@tarih1,@saat1,@salon1,@tur1,@islemsaati1,@gun1)";
            baglanti.Close();
            baglanti.Open();
            SqlCommand kayit = new SqlCommand(sorgu, baglanti);
            kayit.Parameters.AddWithValue("@adsoyad1",txtAdSoyad.Text);
            kayit.Parameters.AddWithValue("@telno1",txtTelNo.Text);
            kayit.Parameters.AddWithValue("@bkod1",txtBiletNo.Text);
            kayit.Parameters.AddWithValue("@koltukno1",txtSecilenKoltuklar.Text);
            kayit.Parameters.AddWithValue("@oyunadi1",cmbOyunAdi.Text);
            kayit.Parameters.AddWithValue("@tarih1",cmbTarih.Text);
            kayit.Parameters.AddWithValue("@saat1",lblsecilenSeans.Text);
            kayit.Parameters.AddWithValue("@salon1",cmbSalonAdi.Text);
            kayit.Parameters.AddWithValue("@tur1",cmbBiletTur.Text);
            DateTime suankiZaman = DateTime.Now;
            TimeSpan suankiSaat = new TimeSpan(suankiZaman.Hour, suankiZaman.Minute, 0);
            kayit.Parameters.AddWithValue("@islemsaati1", string.Format("{0:D2}:{1:D2}", suankiSaat.Hours, suankiSaat.Minutes));
            kayit.Parameters.AddWithValue("@gun1", suankiZaman.Date.ToString("dd.MM.yyyy"));
            kayit.ExecuteNonQuery();
            baglanti.Close();
            

            //UPDATE KISMI burdaki kodlarımız biletler tablosunda satılan biletleri seanslar tablosundakiyle birleştirir ve satılan koltukların renklerinin kırmızıya dönmesini sağlar.
            baglanti.Open();
            string sorgu2 = @"
            UPDATE tbl_Seanslar 
            SET KOLTUKLAR = CASE 
                WHEN KOLTUKLAR IS NULL THEN @yeniKoltuklar 
                WHEN KOLTUKLAR = '' THEN @yeniKoltuklar 
                ELSE KOLTUKLAR + ',' + @yeniKoltuklar 
            END 
            WHERE OYUNADI = @oyunadi AND TARIH = @tarih AND SAAT = @saat AND SALON = @salon";
            SqlCommand guncelle = new SqlCommand(sorgu2, baglanti);


            guncelle.Parameters.AddWithValue("@yeniKoltuklar", txtSecilenKoltuklar.Text); // Yeni seçilen koltuklar
            guncelle.Parameters.AddWithValue("@oyunadi", cmbOyunAdi.Text);
            guncelle.Parameters.AddWithValue("@tarih", cmbTarih.Text);
            guncelle.Parameters.AddWithValue("@saat", lblsecilenSeans.Text);
            guncelle.Parameters.AddWithValue("@salon", cmbSalonAdi.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            lbluyari.Text = "Bilet satışı başarılı bir şekilde kayıt edildi";
            lbluyari.Visible = true;
            lbluyari.ForeColor = Color.DarkGreen;
            timer1.Start();
            KoltukSayisiGetir();
        }
        private void btnOlustur_Click(object sender, EventArgs e)
        {
            if(txtAdSoyad.Text != "" && txtBiletNo.Text !="" &&txtSecilenKoltuklar.Text !="" && txtTelNo.Text != "" && cmbOyunAdi.Text != "" && cmbBiletTur.Text != "" && cmbSalonAdi.Text != "" && cmbTarih.Text != "" )
            {
                //Bilet kayıt işlemleri
                BiletNoSorgula();  // Sorgulama sonucunda var olan barkod no veritabanında kayıtlı değil ise kaydetme işlemi gerçekleştirilecektir. Bu metodun içinde işlemler yapılacaktır.


            }
            else
            {
                MessageBox.Show("LÜTFEN TÜM ALANLARI EKSİKSİZ BİR ŞEKİLDE DOLDURUNUZ");
            }
        }
        void SecilenKoltuklar()
        {
            //Seçilen koltuklar listbox'ındaki koltukları textbox Seçilen koltuklar kısmına atma
            txtSecilenKoltuklar.Text = "";
            foreach (string item in listSecilenKoltuk.Items)
            {
                txtSecilenKoltuklar.Text += "," + item;
            }
            if (txtSecilenKoltuklar.Text.Length > 1)
            {
                txtSecilenKoltuklar.Text = txtSecilenKoltuklar.Text.Substring(1); //Eğer koltuk sayısı 1'den azsa virgül atma konusunda sıkıntı yaşamamak için kullanılıyor.

            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            //tıklama olayı
            Button btn = (Button)sender;
            if(btn.ForeColor == Color.White) //Arka plan kırmızı yani dolu koltuk
            {
                lbluyari.Visible = true;
                lbluyari.ForeColor = Color.Red;
                lbluyari.Text = "Seçilen koltuk doludur! Lütfen başka koltuk seçiniz.";
                timer1.Start();
            }
            else // Koltuk boş ise
            {
                if(btn.ForeColor == Color.Yellow) //Koltuğun text'i sarı ise yani koltuk rengi mavi ise
                {
                    btn.BackgroundImage = Properties.Resources.sari1;
                    btn.BackgroundImageLayout = ImageLayout.Zoom;
                    btn.ForeColor = Color.Blue;
                    listSecilenKoltuk.Items.Add(btn.Text); //Seçilen koltuk listBox'ına bu koltukların text'ini at.
                    SecilenKoltuklar();

                }
                else //Koltuğun rengi sarı ise
                { 
                    btn.BackgroundImage = Properties.Resources.mavi1;
                    btn.BackgroundImageLayout = ImageLayout.Zoom;
                    btn.ForeColor = Color.Yellow;
                    listSecilenKoltuk.Items.Remove(btn.Text);//Seçilen koltuk listBox'ından bu koltukların text'ini çıkar.
                    SecilenKoltuklar();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbluyari.Visible = false;
            timer1.Stop();
            
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAdSoyad.Text = "";
            txtTelNo.Text = "";
            txtSecilenKoltuklar.Text = "";
            txtTelNo.Text = "";
            txtBiletNo.Text = "";
            cmbBiletTur.Text = "";
            lblSeanstakiKoltuk.Text = "";
            lblKoltukSayisi.Text = "";
            lblsecilenSeans.Text = "";
            cmbBiletTur.Items.Clear();
            cmbBiletTur.Items.Add("YETİŞKİN");
            cmbBiletTur.Items.Add("ÖĞRENCİ");
            cmbSalonAdi.Items.Clear();
            cmbSalonAdi.Text = "";
            cmbTarih.Text = "";
            cmbTarih.Items.Clear();
            BiletNoOlustur();
            panelSeans.Controls.Clear();
            koltukPaneli.Controls.Clear();
            listSecilenKoltuk.Items.Clear();
            cmbOyunAdi.Text = "";
            cmbOyunAdi.Items.Clear();
            OyunAdiGetir();
            txtAdSoyad.Focus();
            listeSeansKoltuklari.Items.Clear();
        }
    }
}
