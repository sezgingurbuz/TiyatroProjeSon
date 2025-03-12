using System;
using System.Collections;
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
    public partial class FrmRaporEkrani : Form
    {
        public FrmRaporEkrani()
        {
            InitializeComponent();
            
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A0SS61H\\SQLEXPRESS01;Initial Catalog=TiyatroSonProje;Integrated Security=True;");
        DataTable dt = new DataTable();
        void VerileriGoster()
        {

            SqlDataAdapter adtr = new SqlDataAdapter("SELECT *FROM tbl_Biletler", baglanti);
            adtr.Fill(dt);
            advancedDataGridView1.DataSource = dt;
        }

        private void FrmRaporEkrani_Load(object sender, EventArgs e)
        {
            VerileriGoster();
            
        }

        private void advancedDataGridView1_FilterStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = advancedDataGridView1.FilterString;
        }

        private void advancedDataGridView1_SortStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = advancedDataGridView1.SortString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
        
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            
            
        }

        
    }
}
