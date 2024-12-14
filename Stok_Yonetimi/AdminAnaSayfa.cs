using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Yonetimi
{
    public partial class AdminAnaSayfa : Form
    {
        private int kullaniciid;
        public AdminAnaSayfa(int kullaniciid)
        {
            InitializeComponent();
            this.kullaniciid = kullaniciid;
        }
        private void btnUrunIslemleri_Click(object sender, EventArgs e)
        {
            AdminUrunIslemleri urun=new AdminUrunIslemleri(kullaniciid);
            urun.Show(); 
            this.Close();
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            UrunGrafigi urunGrafigi = new UrunGrafigi(kullaniciid);
            urunGrafigi.Show();
        }

        private void btnKullanicilar_Click(object sender, EventArgs e)
        {
            AdminKullanicilar kullanicilar = new AdminKullanicilar(kullaniciid);   
            kullanicilar.Show();
            this.Close();
        }

        private void btnSiparisler_Click(object sender, EventArgs e)
        {
            AdminSiparis urunSiparis = new AdminSiparis(kullaniciid);
            urunSiparis.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log log = new Log(kullaniciid);
            log.Show();
        }
    }
}
