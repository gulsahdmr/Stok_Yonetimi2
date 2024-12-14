using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Yonetimi
{
    internal class Urunler
    {
        sqlConnection connection=new sqlConnection();

        public Boolean urunEkle(string ad, int stok, float fiyat)
        {
            string query = "INSERT INTO Tbl_Products (ProductName, Stock, Price) VALUES (@ProductName, @Stock, @Price)";

            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProductName", ad);
                        command.Parameters.AddWithValue("@Stock", stok);
                        command.Parameters.AddWithValue("@Price", fiyat);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public Boolean urunSil(int productid)
        {
            string query = "DELETE FROM Tbl_Products WHERE ProductID = @ProductID";

            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProductID", productid);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public Boolean urunGuncelle(int id, string yeniAd, int yeniStok, float yeniFiyat)
        {
            string query = @"UPDATE Tbl_Products SET ProductName = @ProductName, Price = @Price, Stock = @Stock WHERE ProductID = @ProductID";

            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProductID", id);
                        command.Parameters.AddWithValue("@ProductName", yeniAd);
                        command.Parameters.AddWithValue("@Price", yeniFiyat);
                        command.Parameters.AddWithValue("@Stock", yeniStok);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


    }
}
