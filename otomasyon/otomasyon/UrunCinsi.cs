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

namespace otomasyon
{
    public partial class UrunCinsi : Form
    {
        public UrunCinsi()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();
        StokGiris sg=(StokGiris)Application.OpenForms["StokGiris"];
        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlConnection con = bgl.baglantim();
            SqlCommand cmd = new SqlCommand("urun_cinsi_ekle",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@urunCinsi", txtUrunCinsi.Text.Trim());
            int Satirlar = cmd.ExecuteNonQuery();
            if (Satirlar>0)
            {
                MessageBox.Show("ürün kaydedildi");
                sg.ComboGetir();
                
            }
            else
            {
                MessageBox.Show("ürün kayıt olmadı");
            }
            txtUrunCinsi.Text = string.Empty;
            this.Hide();
        }

        private void UrunCinsi_Load(object sender, EventArgs e)
        {

        }
    }
}
