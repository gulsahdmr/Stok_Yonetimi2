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
    public partial class AdminKullanicilar : Form
    {
        sqlConnection connection = new sqlConnection();
        Kullanicilar kullanici = new Kullanicilar();
        private int kullaniciid;

        public AdminKullanicilar(int kullaniciid)
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
                    string query = @"
                        SELECT TOP (1000) 
                            [CustomerID],
                            [CustomerName],
                            [Budget],
                            [CustomerType],
                            [TotalSpent],
                            [Password]
                        FROM [Tbl_Customers]";

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
                txtSifre.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtAd.Text) && !string.IsNullOrEmpty(txtSifre.Text))
            {
                if (kullanici.UyeOl(txtAd.Text, txtSifre.Text) )
                {
                    MessageBox.Show("Yeni kullanıcı eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Text = "";
                    txtSifre.Text = "";
                    txtId.Text = "";
                    btnListele.PerformClick();
                }
                else
                {
                    MessageBox.Show("Yeni kullanıcı eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Yeni kullanıcı eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtAd.Text) && !string.IsNullOrEmpty(txtSifre.Text) && (!string.IsNullOrWhiteSpace(txtId.Text) && int.TryParse(txtId.Text,out int custumerid)))
            {
                if (kullanici.kullaniciguncelle(custumerid,txtAd.Text, txtSifre.Text))
                {
                    MessageBox.Show("Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Text = "";
                    txtSifre.Text = "";
                    txtId.Text = "";
                    btnListele.PerformClick();
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtId.Text) && int.TryParse(txtId.Text, out int customerId))
            {
                if (kullanici.kullaniciSil(customerId))
                {
                    MessageBox.Show("Kullanıcı başarıyla silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Text = "";
                    txtSifre.Text = "";
                    txtId.Text = "";
                    btnListele.PerformClick();
                }
                else
                {
                    MessageBox.Show("Kullanıcı silinemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
