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
    public partial class FrmSalonKayit : Form
    {
        public FrmSalonKayit()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void KoltukOlustur()
        {
            for(int i = 0;i<=100;i++)
            {
                cmbKoltukSayis.Items.Add(i);
            }
        }
        private void FrmSalonKayit_Load(object sender, EventArgs e)
        {
            KoltukOlustur();
            ListeGetir();
            
            
            
        }
        public string salonID = "";
        void ListeGetir()
        {
            //Oluşturulan user controlü forma ekleme
            panelSalon.Controls.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT *FROM tbl_Salonlar ORDER BY SALONADI ASC", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read()) 
            {
                salonListesi arac = new salonListesi();
                arac.lblSalonAdi.Text = dr["SALONADI"].ToString();
                arac.lblKoltukSayisi.Text = dr["KOLTUKSAYISI"].ToString();
                arac.idNo = dr["ID"].ToString();
                panelSalon.Controls.Add(arac);
                
            }
            baglanti.Close();
        }
        void Temizle()
        {
            txtSalonAdi.Text = "";
            cmbKoltukSayis.Text = "";
            txtSalonAdi.Focus();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(txtSalonAdi.Text != "" && cmbKoltukSayis.Text != "")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO tbl_Salonlar (SALONADI,KOLTUKSAYISI) values (@p1,@p2)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtSalonAdi.Text);
                komut.Parameters.AddWithValue("@p2", cmbKoltukSayis.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                Temizle();
                ListeGetir();
                lbltamamlandi.Visible = true;
                timer1.Start();
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbltamamlandi.Visible = false;
            timer1.Stop();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
