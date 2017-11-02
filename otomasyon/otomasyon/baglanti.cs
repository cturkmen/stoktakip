using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace otomasyon
{
    class baglanti
    {
        public SqlConnection baglantim()
        {
            SqlConnection con = new SqlConnection("Data Source=202-00;Initial Catalog=stok_takip;User ID=sa;Password=1234");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public SqlConnection baglantim_kapat()
        {
            SqlConnection con = new SqlConnection("Data Source=202-00;Initial Catalog=stok_takip;User ID=sa;Password=1234");
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }

        public DataTable Tablo(string sqlCumlem,DataGridView rapordt)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sqlCumlem, baglantim());
            DataTable dt = new DataTable();
            try
            {
                baglantim();
                adp.Fill(dt);
                rapordt.DataSource = dt;
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            adp.Dispose();// adp yi yok eder.
            baglantim_kapat();
            return dt;
        }


        public SqlDataReader Reader(string urunkodu, PictureBox p)
        {
            baglantim();
            SqlCommand cmd = new SqlCommand("select urunResmi from Stok where urunKodu='" + urunkodu + "'", baglantim());
            SqlDataReader rdr = cmd.ExecuteReader();

            try
            {
                while (rdr.Read())
                {
                    System.Drawing.Image uResim = null;
                    byte[] resim = (byte[])rdr[0];//rdr değişkenini byte tipine çevirdik.bilinçli ve bilinçsiz tür dönüşümü
                    MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                    //bir verinin kullanılmak üzere, geçici olarak RAM bellekte saklanmasını sağlayan sınıftır.
                    ms.Write(resim, 0, resim.Length);
                    uResim = System.Drawing.Image.FromStream(ms, true);
                    p.Image = uResim;


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return rdr;
        }

        public SqlDataReader Reader1(string sqlCumle, ComboBox c)
        {
            baglantim();
            SqlCommand cmd = new SqlCommand("" + sqlCumle + "", baglantim());
            SqlDataReader rdr = cmd.ExecuteReader();
            try
            {
                while (rdr.Read())
                {
                    c.Items.Add(rdr["urunCinsi"].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            rdr.Dispose();
            baglantim_kapat();
            return rdr;
        }

        public void GridSet(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public int sayac = 0;
        public double toplam = 0.0;
        public SqlDataReader Reader2(string sqlCumle, DataGridView datagrid, string dgr1, string dgr2, string dgr3, string dgr4, string dgr5, string dgr6, Label lbl)
        {
            baglantim();
            SqlCommand cmd = new SqlCommand(""+sqlCumle+"",baglantim());
            SqlDataReader rdr = cmd.ExecuteReader();
            try
            {
                while (rdr.Read())
                {
                    datagrid.Rows.Add();
                    datagrid.Rows[sayac].Cells[0].Value = rdr[dgr1].ToString();
                    datagrid.Rows[sayac].Cells[1].Value = rdr[dgr2].ToString();
                    datagrid.Rows[sayac].Cells[2].Value = rdr[dgr3].ToString();
                    datagrid.Rows[sayac].Cells[3].Value = rdr[dgr4].ToString();
                    datagrid.Rows[sayac].Cells[4].Value = dgr5.ToString();
                    datagrid.Rows[sayac].Cells[5].Value = dgr6.ToString();
                    toplam += Convert.ToDouble(rdr[dgr3].ToString());
                    sayac++;
                }
                lbl.Text = String.Format("{0:C}", toplam.ToString());
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            rdr.Dispose(); 
            baglantim_kapat();
            return rdr;
        }




    }
}
