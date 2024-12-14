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
    public partial class UyeOl : Form
    {
        Kullanicilar yeniuye=new Kullanicilar();
        public UyeOl()
        {
            InitializeComponent();
        }

        private void btnUyeOl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAd.Text) && !string.IsNullOrEmpty(txtSifre.Text))
            {
                if (yeniuye.UyeOl(txtAd.Text, txtSifre.Text))
                {
                    MessageBox.Show("Üye olma işlemi tamamlandı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Text = "";
                    txtSifre.Text = "";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Geçersiz kullanıcı adı veya şifre.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
