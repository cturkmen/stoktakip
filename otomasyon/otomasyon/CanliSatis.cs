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
    public partial class CanliSatis : Form
    {
        public CanliSatis()
        {
            InitializeComponent();
        }
        baglanti bgl = new baglanti();
        Giris gr = (Giris)Application.OpenForms["Giris"];
        string adet = "1";


        private void txtUrunKodu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                try
                {
          bgl.Reader2("select * from stok where urunKodu='" +txtUrunKodu.Text+"'" ,dataGridView1, "urunKodu","urunAdi","urunFiyati","urunCinsi",adet,gr.k_Yetki.ToString(),lblToplam);

          bgl.Reader(txtUrunKodu.Text, pictureBox1);
          txtUrunKodu.Text = string.Empty;
          bgl.GridSet(dataGridView1);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        //double sonToplam;
        public void para()
        {
            if (lblToplam.Text!="0")
            {
                lblToplam.Text += Convert.ToDouble(bgl.toplam.ToString());
            }
            else
            {
                lblToplam.Text = bgl.toplam.ToString() + "₺";
            }
        }

        public void para_ustu()
        {
            double miktar = Convert.ToDouble(txtMiktar.Text);
            double kalan = miktar - Convert.ToDouble(bgl.toplam.ToString());
            lblParaustu.Text = kalan.ToString() + "₺";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value!=null)
                    {
                        SqlConnection con = bgl.baglantim();
                        SqlCommand cmd = new SqlCommand("sp_satis",con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@urunKodu",dataGridView1.Rows[i].Cells[0].Value.ToString());
                        cmd.Parameters.AddWithValue("@urunAdi",dataGridView1.Rows[i].Cells[1].Value.ToString());
                        cmd.Parameters.AddWithValue("@urunFiyati", decimal.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                        cmd.Parameters.AddWithValue("@urunCinsi", dataGridView1.Rows[i].Cells[3].Value.ToString());
                        cmd.Parameters.AddWithValue("@urunAdedi", dataGridView1.Rows[i].Cells[4].Value.ToString());
                        cmd.Parameters.AddWithValue("@urunEkleyen", dataGridView1.Rows[i].Cells[5].Value.ToString());

                        cmd.ExecuteNonQuery();
                        con.Close();


                    }
                }

                //SATIRLAR TEMİZLENİYOR.......
                for (int i = 0; i <= dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value!=null)
                    {
                        dataGridView1.Rows.Clear();
                        bgl.sayac = 0;
                        bgl.toplam = 0.0;
                        lblToplam.Text = "0.00" + "₺";
                        lblParaustu.Text = "0.00" + "₺";
                        txtMiktar.Text = null;
                        
                    }
                }
                pictureBox1.Image = null;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            para_ustu();
        }

        

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[2].Value != null)
                {
                    if (lblToplam.Text != "0")
                    {
                        bgl.sayac--;
                        bgl.toplam -= Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                        lblToplam.Text = bgl.toplam.ToString();

                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            //MessageBox.Show(cell.RowIndex.ToString());
                            dataGridView1.Rows.RemoveAt(cell.RowIndex);
                            pictureBox1.Image = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            }
        }
    }

