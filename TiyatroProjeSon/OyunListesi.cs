using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiyatroProjeSon
{
    public partial class OyunListesi : UserControl
    {
        public OyunListesi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmOyunDetay frm = new FrmOyunDetay();
            frm.idNo = lblIDNo.Text;
            frm.oyunAdi = lblFilmAdi.Text;
            frm.ShowDialog();
        }

        private void OyunListesi_Load(object sender, EventArgs e)
        {

        }
    }
}
