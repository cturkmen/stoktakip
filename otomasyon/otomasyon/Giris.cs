using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Data.SqlClient;

namespace otomasyon
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();
        string pc_Modeli;
        string pc_ısmi;
        public string k_Yetki;
        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = bgl.baglantim();
                SqlCommand cmd = new SqlCommand("sp_KullaniciGirisi",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kAdi",txtKAdi.Text);
                cmd.Parameters.AddWithValue("@kSifre",txtKSifre.Text);
                cmd.Parameters.AddWithValue("@pcIsmi",pc_ısmi.ToString());
                cmd.Parameters.AddWithValue("@pcModeli",pc_Modeli.ToString());
                cmd.Parameters.AddWithValue("@girisKullaniciBilgisi",txtKAdi.Text);

                int Satirlar = cmd.ExecuteNonQuery();
                if (Satirlar>0)
                {
                    k_Yetki = txtKAdi.Text;
                    Anasayfa a = new Anasayfa();
                    a.Show();
                }
                else
                {
                    MessageBox.Show("giriş hatalı. bilgileri kontrol ediniz.");

                    foreach (Control item in this.Controls)
                    {
                        if (item is TextBox)
                        {
                            item.Text = null;
                        }
                       
                    }
                }

                    
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            ManagementObjectSearcher query = new ManagementObjectSearcher("select * from Win32_ComputerSystem");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (var item in queryCollection)
            {
                pc_Modeli = item["model"].ToString();
                pc_ısmi = item["name"].ToString();
            }
        }
    }
}
