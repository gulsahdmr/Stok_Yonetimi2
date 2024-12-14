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
    public partial class Sepet : Form
    {
        sqlConnection connection=new sqlConnection();
        private int kullaniciid;
        Siparisler siparisler = new Siparisler();
        public Sepet(int kullaniciid)
        {
            InitializeComponent();
            this.kullaniciid = kullaniciid;
        }

        public void Sepeti_Listele()
        {
            string query = @" SELECT TOP (1000) o.[OrderID], p.[ProductName], o.[Quantity], o.[TotalPrice], o.[OrderDate], o.[OrderStatus] FROM [Tbl_Orders] o INNER JOIN [Tbl_Products] p ON o.[ProductID] = p.[ProductID] WHERE o.[CustomerID] = @CustomerID";

            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@CustomerID", kullaniciid);

                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);

                    // Verileri DataGridView'e bağla
                    dataGridView1.DataSource = dataTable;

                    // Yazı tipini küçült
                    dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8);
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

                    // Sütun genişliklerini ayarla
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Otomatik genişlik kapalı
                    dataGridView1.Columns[0].Width = 70; 
                    dataGridView1.Columns[1].Width = 90; 
                    dataGridView1.Columns[2].Width = 70;
                    dataGridView1.Columns[3].Width = 70;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 100;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void Sepet_Load(object sender, EventArgs e)
        {
            Sepeti_Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAdet.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Lütfen silinecek bir sipariş seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text, out int orderId))
            {
                MessageBox.Show("Geçerli bir sipariş numarası girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Sepetten silme işlemini çağırıyoruz.
            if (siparisler.sepettenSil(orderId))
            {
                MessageBox.Show("Sipariş başarıyla silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Text = "";
                txtAd.Text = "";
                txtAdet.Text = "";
                txtFiyat.Text = "";
                Sepeti_Listele();
            }
            else
            {
                MessageBox.Show("Silme işlemi başarısız oldu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
