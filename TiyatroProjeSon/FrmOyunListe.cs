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
    public partial class FrmOyunListe : Form
    {
        public FrmOyunListe()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        private void verileriGetir()
        {
            ListePaneli.Controls.Clear();
            string sorgu = "SELECT *FROM tbl_Oyunlar ORDER BY ADI ASC";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                OyunListesi arac = new OyunListesi();
                arac.lblFilmAdi.Text = dr["ADI"].ToString(); // başka bir kodda kullanırsan eğer label'ın adını değiştirmek için user control üstündeki label'ın modifiers özelliğini public yapman gerekli
                arac.lblIDNo.Text = dr["ID"].ToString();
                arac.pbAfis.ImageLocation = dr["AFIS"].ToString();
                ListePaneli.Controls.Add(arac);
            }
            baglanti.Close();
        }
        private void FrmOyunListe_Load(object sender, EventArgs e)
        {
            verileriGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListePaneli.Controls.Clear();
            try
            {
                baglanti.Open();
                SqlCommand arama = new SqlCommand("SELECT * FROM tbl_Oyunlar WHERE ADI LIKE @arama COLLATE Turkish_CI_AI ORDER BY ADI ASC", baglanti);
                arama.Parameters.AddWithValue("@arama", "%" + txtArama.Text + "%");
                SqlDataReader oku = arama.ExecuteReader();
                while(oku.Read())
                {
                    OyunListesi arac = new OyunListesi();
                    arac.lblFilmAdi.Text = oku["ADI"].ToString();
                    arac.lblIDNo.Text = oku["ID"].ToString();
                    arac.pbAfis.ImageLocation = oku["AFIS"].ToString();
                    ListePaneli.Controls.Add(arac);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}
