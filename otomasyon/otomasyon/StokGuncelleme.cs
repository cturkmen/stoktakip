using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otomasyon
{
    public partial class StokGuncelleme : Form
    {
        public StokGuncelleme()
        {
            InitializeComponent();
        }
        baglanti bgl = new baglanti();
        Giris gr = (Giris)Application.OpenForms["Giris"];
        string resimPath = string.Empty;

        public void VeriGetir()
        {
            DataTable dt = bgl.Tablo("select * from stok",dataGridView1);
          
        }

        public void resimli_proc()
        {
            try
            {
                byte[] resim = null;
                FileInfo fInfo = new FileInfo(resimPath);
                long sayac = fInfo.Length;
                FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                resim = br.ReadBytes((int)sayac);



                SqlConnection con = bgl.baglantim();
                SqlCommand cmd = new SqlCommand("sp_urun_guncelle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@urunKodu", txtUrunKodu.Text.Trim());
                cmd.Parameters.AddWithValue("@urunAdi", txtUrunAdi.Text.Trim());
                cmd.Parameters.AddWithValue("@urunFiyati", decimal.Parse(txtUrunFiyati.Text.Trim()));
                cmd.Parameters.AddWithValue("@urunCinsi", cmbUrunCinsi.Text.Trim());
                cmd.Parameters.AddWithValue("@urunAdedi", txtUrunAdedi.Text.Trim());
                cmd.Parameters.AddWithValue("@kritikStok", txtKritikStok.Text.Trim());
                cmd.Parameters.AddWithValue("@urunResmi", resim);
                cmd.Parameters.AddWithValue("@urunEkleyen", gr.k_Yetki.ToString());

                int eSatirlar = cmd.ExecuteNonQuery();
                if (eSatirlar > 0)
                {
                    MessageBox.Show("ürün başarıyla güncellendi");

                }
                else
                {
                    MessageBox.Show("bu ürün güncellenemedi");
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void resimsiz_proc()
        {
            try
            {
                SqlConnection con = bgl.baglantim();
                SqlCommand cmd = new SqlCommand("sp_urun_guncelle_resimsiz", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@urunKodu", txtUrunKodu.Text.Trim());
                cmd.Parameters.AddWithValue("@urunAdi", txtUrunAdi.Text.Trim());
                cmd.Parameters.AddWithValue("@urunFiyati", decimal.Parse(txtUrunFiyati.Text.Trim()));
                cmd.Parameters.AddWithValue("@urunCinsi", cmbUrunCinsi.Text.Trim());
                cmd.Parameters.AddWithValue("@urunAdedi", txtUrunAdedi.Text.Trim());
                cmd.Parameters.AddWithValue("@kritikStok", txtKritikStok.Text.Trim());
              
                cmd.Parameters.AddWithValue("@urunEkleyen", gr.k_Yetki.ToString());

                int eSatirlar = cmd.ExecuteNonQuery();
                if (eSatirlar > 0)
                {
                    MessageBox.Show("ürün başarıyla güncellendi");

                }
                else
                {
                    MessageBox.Show("bu ürün güncellenemedi");
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (resimPath=="")
                {
                    resimsiz_proc();
                    VeriGetir();
                }
                else
                {
                    resimli_proc();
                    VeriGetir();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void StokGuncelleme_Load(object sender, EventArgs e)
        {
            VeriGetir();
            bgl.GridSet(dataGridView1);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            txtUrunKodu.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtUrunAdi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtUrunFiyati.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cmbUrunCinsi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtUrunAdedi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtKritikStok.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            bgl.Reader(txtUrunKodu.Text,pictureUrunResmi);
        }
    }
}
