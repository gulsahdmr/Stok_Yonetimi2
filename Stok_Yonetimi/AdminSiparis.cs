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
    public partial class AdminSiparis : Form
    {
        sqlConnection connection = new sqlConnection(); // Veritabanı bağlantı sınıfı
        private int kullaniciid;
        public AdminSiparis(int kullaniciid)
        {
            InitializeComponent();
            this.kullaniciid = kullaniciid;
        }

        public void tamamlanmisSiparisListele()
        {
            string query = @"SELECT o.[OrderID], c.[CustomerName], p.[ProductName], o.[Quantity], o.[TotalPrice], o.[OrderDate]
                             FROM [Tbl_Orders] o
                             INNER JOIN [Tbl_Customers] c ON o.[CustomerID] = c.[CustomerID]
                             INNER JOIN [Tbl_Products] p ON o.[ProductID] = p.[ProductID]
                             WHERE o.[OrderStatus] = 'Completed'";

            using (SqlConnection conn = connection.Baglanti())
            {
                 
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();

                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.DefaultCellStyle.Font = new Font("Arial", 8);
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Otomatik genişlik kapalı
                dataGridView1.Columns[0].Width = 55;
                dataGridView1.Columns[1].Width = 95;
                dataGridView1.Columns[2].Width = 75;
                dataGridView1.Columns[3].Width = 60;
                dataGridView1.Columns[4].Width = 60;
                dataGridView1.Columns[5].Width = 95;



            }
        }
        public void onayBekleyenSiparisListele()
        {
            string query = @"SELECT o.[OrderID], c.[CustomerName], p.[ProductName], o.[Quantity], o.[TotalPrice], o.[OrderDate]
                             FROM [Tbl_Orders] o
                             INNER JOIN [Tbl_Customers] c ON o.[CustomerID] = c.[CustomerID]
                             INNER JOIN [Tbl_Products] p ON o.[ProductID] = p.[ProductID]
                             WHERE o.[OrderStatus] = 'Waiting'";

            using (SqlConnection conn = connection.Baglanti())
            {

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                dataGridView2.DefaultCellStyle.Font = new Font("Arial", 8);
                dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Otomatik genişlik kapalı
                dataGridView2.Columns[0].Width = 55;
                dataGridView2.Columns[1].Width = 100;
                dataGridView2.Columns[2].Width = 80;
                dataGridView2.Columns[3].Width = 60;
                dataGridView2.Columns[4].Width = 70;
                dataGridView2.Columns[5].Width = 95;



            }
        }

        private void AdminSiparis_Load_1(object sender, EventArgs e)
        {
            tamamlanmisSiparisListele();
            onayBekleyenSiparisListele();
                
        }
    }
}
