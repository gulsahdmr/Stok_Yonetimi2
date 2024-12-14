using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stok_Yonetimi
{
    public class sqlConnection
    {
        public SqlConnection Baglanti()
        {
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-D54BORL\\SQLEXPRESS;Initial Catalog=Stok_Yonetimi;Integrated Security=True");
            baglanti.Open();
            Debug.WriteLine("Baglanti sağlandı");
            return baglanti;
        }
    }
}
