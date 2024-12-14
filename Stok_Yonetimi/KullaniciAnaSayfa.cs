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
    public partial class KullaniciAnaSayfa : Form
    {
        private int kullaniciid;
        public KullaniciAnaSayfa(int kullaniciid)
        {
            this.kullaniciid = kullaniciid;
            InitializeComponent();
        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            KullanıcıUrunler kullanıcıUrunler=new KullanıcıUrunler(kullaniciid);
            kullanıcıUrunler.Show();
            
        }

        private void btnSepet_Click(object sender, EventArgs e)
        {
            Sepet sepet = new Sepet(kullaniciid);  
            sepet.Show();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            Log log = new Log(kullaniciid);    
            log.Show();
        }
    }
}
