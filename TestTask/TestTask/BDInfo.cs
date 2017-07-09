using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class BDInfo
    {
        public void AddInfoToBD(Queue<string> sourse, List<string> result)
        {
            string strConn = "Data Source=localhost;Initial" +
                " Catalog=NewDataBase;Integrated Security=True;Pooling=False";
            SqlConnection Conn = new SqlConnection(@strConn);
            string sInsSql = (@"INSERT INTO Banan (ID, FileSourse, Result) " + "VALUES (@number, @sourse, @result)");
            Conn.Open();
            for (int i = 0; i < result.Count; i++)
            {
                using (var command = new SqlCommand(sInsSql, Conn))
                {
                    command.Parameters.AddWithValue("@number", i + 1);
                    command.Parameters.AddWithValue("@sourse", sourse.Dequeue());
                    command.Parameters.AddWithValue("@result", result[i]);
                    command.ExecuteNonQuery();
                    //Console.WriteLine("Add was successful" + (i + 1));
                }
            }
            Conn.Close();
        }
    }
}
