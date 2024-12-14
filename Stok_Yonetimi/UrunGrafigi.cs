using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Stok_Yonetimi
{
    public partial class UrunGrafigi : Form
    {
        sqlConnection connection = new sqlConnection();
        private int kullaniciid;
        public UrunGrafigi(int kullaniciid)
        {
            InitializeComponent();
            this.kullaniciid = kullaniciid;
        }

        private void UrunGrafigi_Load_1(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();

            // Yeni grafik serisi ekle
            Series series = new Series("Stok Durumu")
            {
                ChartType = SeriesChartType.Column 
            };
            chart1.Series.Add(series);

            chart1.Titles.Add("Ürünlerin Stok Durumu");

            try
            {
                using (SqlConnection conn = connection.Baglanti())
                {
                    string query = "SELECT ProductID, ProductName, Stock,Price FROM Tbl_Products";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int criticalLevel = 10; // Kritik stok seviyesi

                            while (reader.Read())
                            {
                                string productName = reader["ProductName"].ToString();
                                int stock = Convert.ToInt32(reader["Stock"]);

                                // Veriyi grafik serisine ekle
                                int pointIndex = series.Points.AddXY(productName, stock);

                                if (stock <= criticalLevel)
                                {
                                    series.Points[pointIndex].Color = Color.Red; // Kritik: Kırmızı
                                }
                                else
                                {
                                    series.Points[pointIndex].Color = Color.Green; // Normal: Yeşil
                                }
                            }
                        }

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
