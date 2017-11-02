using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otomasyon
{
    public partial class GunlukRaporlama : Form
    {
        public GunlukRaporlama()
        {
            InitializeComponent();
        }
        baglanti bgl = new baglanti();
        private void btnRapor_Click(object sender, EventArgs e)
        {
            bgl.Tablo("select * from satis where urunCinsi='" + cmbUrunCinsi.Text + "'and urunAdi='" + txtMetin.Text + "'", dataGridView1);

            if (dataGridView1.DataSource!=null)
            {
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    bgl.toplam += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString());
                }
                lblRapor.Text = bgl.toplam.ToString();
            }
        }

        private void GunlukRaporlama_Load(object sender, EventArgs e)
        {
            bgl.Reader1("select * from urun_Cinsi",cmbUrunCinsi);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bgl.Reader(dataGridView1.CurrentRow.Cells[1].Value.ToString(),pictureBox1);
        }
    }
}
