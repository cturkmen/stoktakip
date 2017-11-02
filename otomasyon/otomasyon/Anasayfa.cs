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
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void cıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void stokGirişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokGiris sg = new StokGiris();
            sg.ShowDialog();

            //show(): 2.formu kapatmadan 1.formda değişiklik yapabiliriz.
            //showDialog(): 2.formu kapatmadan 1. forma gelmez.
        }

        private void stokGüncellemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokGuncelleme stg = new StokGuncelleme();
            stg.ShowDialog();
        }

        private void stokTemizlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StokTemizleme st = new StokTemizleme();
            st.ShowDialog();
        }

        private void canlıSatışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanliSatis cs = new CanliSatis();
            cs.ShowDialog();
        }

        private void günlükRaporlamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunlukRaporlama gr = new GunlukRaporlama();
            gr.ShowDialog();
        }

        private void aylıkRaporlamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AylikRaporlama ar = new AylikRaporlama();
            ar.ShowDialog();
        }

        private void senelikRaporlamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SenelikRaporlama sr = new SenelikRaporlama();
            sr.ShowDialog();
        }

        private void günlükStokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunlukMail gm = new GunlukMail();
            gm.ShowDialog();
        }

        private void aylıkStokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AylikMail aym = new AylikMail();
            aym.ShowDialog();
        }

        private void senelikStokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SenelikMail sm = new SenelikMail();
            sm.ShowDialog();
        }

        private void günlükSatışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GunlukSatisMail gsm = new GunlukSatisMail();
            gsm.ShowDialog();
        }

        private void aylıkSatışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AylikSatisMail asm = new AylikSatisMail();
            asm.ShowDialog();

        }

        private void senelikSatışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SenelikSatisMail ssm = new SenelikSatisMail();
            ssm.ShowDialog();
        }

        private void şifreİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullaniciSifreIslemleri ksi = new KullaniciSifreIslemleri();
            ksi.ShowDialog();
        }

        private void adminŞifreAyarlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminSifreIslemleri asi = new AdminSifreIslemleri();
            asi.ShowDialog();
        }

        private void mailAyarlarıEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailAyarlariEkle mae = new MailAyarlariEkle();
            mae.ShowDialog();
        }

        private void mailAyarlarıGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailAyarlariGuncelle mag = new MailAyarlariGuncelle();
            mag.ShowDialog();

        }

       
    }
}
