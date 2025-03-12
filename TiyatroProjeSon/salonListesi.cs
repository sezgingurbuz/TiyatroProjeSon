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
    public partial class salonListesi : UserControl
    {
        public salonListesi()
        {
            InitializeComponent();
        }
        public string idNo = "";
        
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("OYUN SİLİNECEKTİR. EMİN MİSİNİZ?", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                //Evete basılırsa yapılacak işlemler

                try
                {
                    baglanti.Open();
                    string sorgu = "DELETE FROM tbl_Salonlar WHERE ID=@p1";
                    SqlCommand sil = new SqlCommand(sorgu, baglanti);
                    sil.Parameters.AddWithValue("@p1", idNo);
                    sil.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata" + ex.Message);
                }
                finally
                {
                    MessageBox.Show("OYUN SİLİNMİŞTİR! Lütfen formu kapatıp yeniden açınız");
                    baglanti.Close();

                }
            }
            
        }

        
    }

    
}
