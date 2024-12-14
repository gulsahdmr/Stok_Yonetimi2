using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace Stok_Yonetimi
{
    public partial class GirisSayfasi : Form
    {
        sqlConnection connection = new sqlConnection();
        Kullanicilar kullanicilar=new Kullanicilar();
        public GirisSayfasi()
        {
            InitializeComponent();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            string query = "SELECT CustomerID, Rol FROM Tbl_Customers WHERE CustomerName = @CustomerName AND Password = @Password";

            using (SqlConnection conn = connection.Baglanti())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@CustomerName", textBox1.Text);
                        command.Parameters.AddWithValue("@Password", textBox2.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                kullanicilar.kullaniciid= reader.GetInt32(0); ;
                                string rol = reader.GetString(1);  
                                if (rol == "Admin")
                                {
                                    AdminAnaSayfa adminAnaSayfa = new AdminAnaSayfa(kullanicilar.kullaniciid);
                                    adminAnaSayfa.Show();
                                }
                                else if (rol == "Kullanici")
                                {
                                    KullaniciAnaSayfa kullaniciAnaSayfa = new KullaniciAnaSayfa(kullanicilar.kullaniciid);
                                    kullaniciAnaSayfa.Show();
                                }

                                Debug.WriteLine(kullanicilar.kullaniciid);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Geçersiz kullanıcı adı veya şifre.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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
}

