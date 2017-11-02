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
using System.IO;

namespace otomasyon
{
    public partial class StokGiris : Form
    {
        public StokGiris()
        {
            InitializeComponent();
        }

        string resimPath = string.Empty;
        baglanti bgl = new baglanti();
        string yetki;
        public void ComboGetir()
        {
            bgl.Reader1("select * from urun_Cinsi",cmbUrunCinsi);
        }

        Giris gr = (Giris)Application.OpenForms["Giris"];
        

        private void pictureUrunResmi_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Resim Seçme Kutusu";
            openFileDialog1.FileName = "Resim seç";
            openFileDialog1.Filter = "jpeg Dosyası|*.jpeg|jpg dosyası|*.jpg|gif dosyası|*.gif|png Dosyası|*.png";
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureUrunResmi.Image = Image.FromFile(openFileDialog1.FileName);
                resimPath = openFileDialog1.FileName.ToString();
                pictureUrunResmi.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] resim = null;
                FileInfo fInfo = new FileInfo(resimPath);
                long sayac = fInfo.Length;
                FileStream fs = new FileStream(resimPath,FileMode.Open,FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                resim = br.ReadBytes((int)sayac);



                SqlConnection con = bgl.baglantim();
                SqlCommand cmd = new SqlCommand("sp_urun_ekle",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@urunKodu",txtUrunKodu.Text.Trim());
                cmd.Parameters.AddWithValue("@urunAdi",txtUrunAdi.Text.Trim());
                cmd.Parameters.AddWithValue("@urunFiyati",decimal.Parse(txtUrunFiyati.Text.Trim()));
                cmd.Parameters.AddWithValue("@urunCinsi",cmbUrunCinsi.Text.Trim());
                cmd.Parameters.AddWithValue("@urunAdedi",txtUrunAdedi.Text.Trim());
                cmd.Parameters.AddWithValue("@kritikStok",txtKritikStok.Text.Trim());
                cmd.Parameters.AddWithValue("@urunResmi",resim);
                cmd.Parameters.AddWithValue("@urunEkleyen",gr.k_Yetki.ToString());

                int eSatirlar = cmd.ExecuteNonQuery();
                if (eSatirlar>0)
                {
                    MessageBox.Show("ürün başarıyla eklendi");
                    
                }
                else
                {
                    MessageBox.Show("bu ürün kodu daha önce eklenmiştir.");
                    
                }
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = string.Empty;
                    }
                    else if (item is PictureBox)
                        pictureUrunResmi.Image = null;
                    else if (item is ComboBox)
                        item.Text = string.Empty;

                }

                DialogResult dr = MessageBox.Show("başka ürün eklemek istiyormusunuz", "bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    this.Hide();
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            

            
            

        }

        private void StokGiris_Load(object sender, EventArgs e)
        {
            yetki = gr.k_Yetki.ToString();//giris.cs form kapanacağı için
            ComboGetir();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UrunCinsi uc = new UrunCinsi();
            uc.ShowDialog();
        }
    }
}
