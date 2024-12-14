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

namespace Stok_Yonetimi
{
    public partial class AdminUrunIslemleri : Form
    {
        sqlConnection connection = new sqlConnection();
        Urunler urunler = new Urunler();
        private int kullaniciid;

        public AdminUrunIslemleri(int kullaniciid)
        {
            InitializeComponent();
            this.kullaniciid = kullaniciid;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AdminAnaSayfa adminAnaSayfa = new AdminAnaSayfa(kullaniciid);
            adminAnaSayfa.Show();
            this.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
         
            try
            {
                using (SqlConnection conn = connection.Baglanti())
                {
                    string query = @" SELECT TOP (1000) [ProductID], [ProductName], [Stock], [Price] FROM [Tbl_Products]";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtStok.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAd.Text) && (int.TryParse(txtStok.Text, out int stok) && !string.IsNullOrWhiteSpace(txtStok.Text)) && (!string.IsNullOrWhiteSpace(txtFiyat.Text) && float.TryParse(txtFiyat.Text, out float fiyat)))
            {
                if (urunler.urunEkle(txtAd.Text, stok,fiyat))
                {
                    MessageBox.Show("Yeni ürün eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Text = "";
                    txtStok.Text = "";
                    txtId.Text = "";
                    txtFiyat.Text = "";    
                    btnListele.PerformClick();
                }
                else
                {
                    MessageBox.Show("Yeni ürün eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Yeni ürün eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtId.Text) && int.TryParse(txtId.Text, out int productid))
            {
                if (urunler.urunSil(productid))
                {
                    MessageBox.Show("Ürün başarıyla silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Text = "";
                    txtStok.Text = "";
                    txtId.Text = "";
                    txtFiyat.Text = "";
                    btnListele.PerformClick();
                }
                else
                {
                    MessageBox.Show("Ürün silinemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAd.Text) && (int.TryParse(txtStok.Text, out int stok) && !string.IsNullOrWhiteSpace(txtStok.Text)) && (!string.IsNullOrWhiteSpace(txtFiyat.Text) && float.TryParse(txtFiyat.Text, out float fiyat) && (!string.IsNullOrWhiteSpace(txtId.Text) && int.TryParse(txtId.Text, out int productid))))
            {
                if (urunler.urunGuncelle(productid, txtAd.Text, stok,fiyat))
                {
                    MessageBox.Show("Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Text = "";
                    txtStok.Text = "";
                    txtId.Text = "";
                    txtFiyat.Text = "";
                    btnListele.PerformClick(); ;
                }
                else
                {
                    MessageBox.Show("Güncelleme Başarısız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Güncelleme Başarısız. Eksik bilgi girişi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
