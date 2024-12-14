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
    public partial class KullanıcıUrunler : Form
    {
        sqlConnection connection=new sqlConnection();
        Siparisler siparis=new Siparisler();
        private int kullaniciid;
        public KullanıcıUrunler(int kullaniciid)
        {
            InitializeComponent();
            this.kullaniciid = kullaniciid;
        }

        private void KullanıcıUrunler_Load(object sender, EventArgs e)
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
                object cellValue = dataGridView1.Rows[e.RowIndex].Cells[3].Value;

                if (string.IsNullOrWhiteSpace(txtAdet.Text) || !int.TryParse(txtAdet.Text, out int adet) || adet <= 0)
                {
                    MessageBox.Show("Ürün seçmeden önce geçerli bir adet giriniz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cellValue != null && float.TryParse(cellValue.ToString(), out float birimfiyat))
                {
                    txtFiyat.Text = (birimfiyat*Convert.ToInt32(txtAdet.Text)).ToString();
                }
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            }
        }

        private void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAd.Text) && (!string.IsNullOrWhiteSpace(txtId.Text) && int.TryParse(txtId.Text, out int productid)) && (int.TryParse(txtAdet.Text, out int stok) && !string.IsNullOrWhiteSpace(txtAdet.Text)) && (!string.IsNullOrWhiteSpace(txtFiyat.Text) && float.TryParse(txtFiyat.Text, out float fiyat)))
            {
                if (siparis.SepeteEkle(kullaniciid,Convert.ToInt32(txtId.Text), stok, fiyat))
                {
                    MessageBox.Show("Ürün sepete eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Text = "";
                    txtAdet.Text = "";
                    txtId.Text = "";
                    txtFiyat.Text = "";
                }
                else
                {
                    MessageBox.Show("Sepete ürün eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAd.Text = "";
                    txtAdet.Text = "";
                    txtId.Text = "";
                    txtFiyat.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Sepete ürün eklenemedi. Eksik bilgi girişi var", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
