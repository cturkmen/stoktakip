using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace otomasyon
{
    public partial class StokTemizleme : Form
    {
        public StokTemizleme()
        {
            InitializeComponent();
        }
        baglanti bgl = new baglanti();
        

        private void VeriGetir()
        {
            DataTable dt = bgl.Tablo("select * from stok order by id",dataGridView1);
          
        }

        private void StokTemizleme_Load(object sender, EventArgs e)
        {
            VeriGetir();
            bgl.GridSet(dataGridView1);
           
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            txtUrunKodu.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtUrunAdi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString(); ;
            txtUrunFiyati.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtUrunCinsi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtUrunAdedi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtKritikStok.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            bgl.Reader(txtUrunKodu.Text,pictureUrunResmi);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUrunKodu.Text==null)
                {
                    MessageBox.Show("lütfen ürün seçiniz");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("ürünü silmek istiyormusunuz","uyarı",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
                    if (dr==DialogResult.OK)
                    {
                        SqlConnection con = bgl.baglantim();
                        SqlCommand cmd = new SqlCommand("sp_urun_sil",con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@urunKodu",txtUrunKodu.Text.Trim());
                        cmd.ExecuteNonQuery();
                        con.Close();
                        VeriGetir();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = string.Empty;
                }
                else if (item is PictureBox)
                    pictureUrunResmi.Image = null;
            }

        }
        
    }
}
