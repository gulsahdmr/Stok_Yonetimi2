using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Yonetimi
{
    internal class Siparisler
    {
        sqlConnection connection = new sqlConnection();
        public Boolean SepeteEkle(int customerId, int productId, int quantity, float totalPrice)
        {
            string stockQuery = "SELECT Stock FROM Tbl_Products WHERE ProductID = @ProductID";
            string checkQuery = "SELECT SUM(Quantity) FROM Tbl_Orders WHERE CustomerID = @CustomerID AND ProductID = @ProductID";
            string insertQuery = "INSERT INTO Tbl_Orders (CustomerID, ProductID, Quantity, TotalPrice, OrderDate, OrderStatus) " +
                                 "VALUES (@CustomerID, @ProductID, @Quantity, @TotalPrice, @OrderDate, @OrderStatus)";

            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    // Stok kontrolü yap
                    using (SqlCommand stockCommand = new SqlCommand(stockQuery, conn))
                    {
                        stockCommand.Parameters.AddWithValue("@ProductID", productId);

                        object result = stockCommand.ExecuteScalar();
                        if (result == null || Convert.ToInt32(result) < quantity)
                        {
                            MessageBox.Show("Yeterli stok bulunmuyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    // Mevcut sipariş miktarını kontrol et
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                    {
                        checkCommand.Parameters.AddWithValue("@CustomerID", customerId);
                        checkCommand.Parameters.AddWithValue("@ProductID", productId);

                        object result = checkCommand.ExecuteScalar();
                        int existingQuantity = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                        if (existingQuantity + quantity > 5)
                        {
                            MessageBox.Show("Hata: Aynı üründen en fazla 5 adet alınabilir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    // Yeni siparişi ekle
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, conn))
                    {
                        insertCommand.Parameters.AddWithValue("@CustomerID", customerId);
                        insertCommand.Parameters.AddWithValue("@ProductID", productId);
                        insertCommand.Parameters.AddWithValue("@Quantity", quantity);
                        insertCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        insertCommand.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        insertCommand.Parameters.AddWithValue("@OrderStatus", "Waiting");

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                    return false;
                }
            }
        }
        public Boolean sepettenSil(int orderId)
        {
            string query = "DELETE FROM Tbl_Orders WHERE OrderID = @OrderID";

            try
            {
                using (SqlConnection conn = connection.Baglanti())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
        }
    }
}
