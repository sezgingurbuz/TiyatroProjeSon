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
    public partial class FrmBiletDetay : Form
    {
        public FrmBiletDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        public string biletNo = "";

        private void FrmBiletDetay_Load(object sender, EventArgs e)
        {
            lblBiletNo.Text = biletNo;
            lblBiletNo2.Text = biletNo;
            BarkodNoOlustur();
            bilgiGetir();
        }
        void bilgiGetir()
        {
            string sorgu = "SELECT *FROM tbl_Biletler Where BKOD=@kod";
            baglanti.Open();
            SqlCommand getir = new SqlCommand(sorgu, baglanti);
            getir.Parameters.AddWithValue("@kod", biletNo);
            SqlDataReader dr = getir.ExecuteReader();
            if (dr.Read())
            {
                lblOyunAdi1.Text = dr["OYUNADI"].ToString();
                lblOyunAdi2.Text = dr["OYUNADI"].ToString();
                lblTelefonNo.Text = dr["TELNO"].ToString();
                lblAdSoyad.Text = dr["ADSOYAD"].ToString();
                lblBiletTur.Text = dr["TUR"].ToString();
                lblSalon.Text = dr["SALON"].ToString();
                lblSalonSeans.Text = dr["SALON"].ToString() + " - " + dr["SAAT"].ToString();
                lblTarihSaat.Text = dr["TARIH"].ToString() + " - " + dr["SAAT"].ToString();
                lblOyunTarih.Text = dr["TARIH"].ToString();
                lblIslemTarih.Text = dr["ISLEMSAATI"].ToString();
                lblKoltukNo1.Text = dr["KOLTUKNO"].ToString();
                lblKoltukNo2.Text = dr["KOLTUKNO"].ToString();
            }
            baglanti.Close();
        }
        void BarkodNoOlustur()
        {
            //Random ile Barkod oluşturma işlemi gerçekleştirilecek.
            Random rnd = new Random();
            string karakterler = "123456789987654321123456789987654321";//Şifreyi oluşturacak karakterler
            string kod = "";

            for (int i = 0; i < 5; i++)
            {
                kod += karakterler[rnd.Next(karakterler.Length)];

            }
            lblBarkod1.Text = kod.ToString();
            lblBarkod2.Text = kod.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
