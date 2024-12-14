using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Yonetimi
{
    public class Kullanicilar
    {
        sqlConnection connection=new sqlConnection();
        public int kullaniciid { get; set;}
        Random random=new Random();

        public Boolean UyeOl(string ad, string sifre)
        {
            int butce = random.Next(500, 3001);
            string customerType = butce > 2000 ? "Premium" : "Standard"; // Bütçeye göre müşteri tipi belirle
            string query = "INSERT INTO Tbl_Customers (CustomerName, Budget, Password, CustomerType) VALUES (@CustomerName, @Budget, @Password, @CustomerType)";
            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@CustomerName", ad);
                        command.Parameters.AddWithValue("@Budget", butce);
                        command.Parameters.AddWithValue("@Password", sifre);
                        command.Parameters.AddWithValue("@CustomerType", customerType);


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

        public Boolean kullaniciSil(int customerId)
        {
            string query = "DELETE FROM Tbl_Customers WHERE CustomerID = @CustomerID";

            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerId);


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
        public Boolean kullaniciguncelle(int id, string yeniAd, string yeniSifre)
        {
            string query = @" UPDATE Tbl_Customers SET CustomerName = @CustomerName, Password = @Password WHERE CustomerID = @CustomerID";

            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@CustomerID", id);
                        command.Parameters.AddWithValue("@CustomerName", yeniAd);
                        command.Parameters.AddWithValue("@Password", yeniSifre);

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
