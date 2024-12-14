using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Stok_Yonetimi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection connection = new sqlConnection();
            using(SqlConnection conn=connection.Baglanti())
            {
                try
                {
                    string query = "SELECT * FROM Tbl_Products"; // Örnek sorgu
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Ürün ID: {reader["UrunID"]}, Ürün Adı: {reader["UrunAdi"]}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            GirisSayfasi giris=new GirisSayfasi();
            giris.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UyeOl uye=new UyeOl();  
            uye.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GirisSayfasi giris = new GirisSayfasi();
            giris.Show();
        }
    }
}
