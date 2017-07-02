using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace TestTask
{
    class Program
    {
        public class Node<T>
        {
            public Node(T data)
            {
                Data = data;
            }

            public T Data { get; set; }
            public Node<T> Next { get; set; }
        }

        public class Queue<T> : IEnumerable<T>
        {
            Node<T> head;
            Node<T> tail;
            int count;

            public void Enqueue(T data)
            {
                Node<T> node = new Node<T>(data);
                Node<T> tempNode = tail;
                tail = node;
                if (count == 0)
                    head = tail;
                else
                    tempNode.Next = tail;
                count++;
            }

            public T Dequeue()
            {
                if (count == 0)
                    throw new InvalidOperationException();
                T output = head.Data;
                head = head.Next;
                count--;
                return output;
            }

            public T First
            {
                get
                {
                    if (IsEmpty)
                        throw new InvalidOperationException();
                    return head.Data;
                }
            }

            public T Last
            {
                get
                {
                    if (IsEmpty)
                        throw new InvalidOperationException();
                    return tail.Data;
                }
            }

            public int Count { get { return count; } }
            public bool IsEmpty { get { return count == 0; } }

            public void Clear()
            {
                head = null;
                tail = null;
                count = 0;
            }

            public bool Contains(T data)
            {
                Node<T> current = head;
                while (current != null)
                {
                    if (current.Data.Equals(data))
                        return true;
                    current = current.Next;
                }
                return false;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this).GetEnumerator();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                Node<T> current = head;
                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
        }

        public class Use
        {
            Queue<string> queue = new Queue<string>();

            long dlya;
            static string g = null;
            ArrayList roll = new ArrayList();
            

            public void InstanceMethod()
            {
                string[] dirs = Directory.GetFiles(@"E:\Exampl", "*");
                
                foreach (string dir in dirs)
                {
                    queue.Enqueue(dir);
                }
                dlya = queue.LongCount();
            }

            public void HashMethod()
            {
                Thread.Sleep(20);
                while (!(queue.Count == 0))
                {
                    string obj = queue.Dequeue();
                    g = ComputeMD5Checksum(obj);
                    roll.Add(g);
                }
            }

            public void Conclusion()
            {
                Thread.Sleep(1500);
                Console.WriteLine(dlya - queue.Count + "/" + dlya);
            }

            public ArrayList Roll { get { return roll; } }
            public int GetI { get { return i; } }

            private string ComputeMD5Checksum(string path)
            {
                using (FileStream source = System.IO.File.OpenRead(path))
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] fileData = new byte[source.Length];
                    source.Read(fileData, 0, (int)source.Length);
                    byte[] hashSumm = md5.ComputeHash(fileData);
                    string result = BitConverter.ToString(hashSumm).Replace("-", String.Empty);
                    return result;
                }
            }
        }

        static void Main(string[] args)
        {
            Use use = new Use();
            Thread first = new Thread(new ThreadStart(use.InstanceMethod));
            Thread other = new Thread(new ThreadStart(use.HashMethod));
            Thread other1 = new Thread(new ThreadStart(use.HashMethod));
            Thread other2 = new Thread(new ThreadStart(use.HashMethod));
            first.Start();
            other.Start();
            other1.Start();
            other2.Start();
            use.Conclusion();

            Thread.Sleep(2000);

            ArrayList rollNew = new ArrayList();
            rollNew = use.Roll;
            string strConn = "Data Source=localhost;Initial Catalog=NewDataBase;Integrated Security=True;Pooling=False";
            SqlConnection Conn = new SqlConnection(@strConn);
            string sInsSql = (@"INSERT INTO Banan (ID, Result) " +  "VALUES (@number, @result)");
            Conn.Open();
            for (int i = 0; i < rollNew.Count; i++)
            {
                using (var command = new SqlCommand(sInsSql, Conn))
                {
                    command.Parameters.AddWithValue("@number", i+1);
                    command.Parameters.AddWithValue("@result", rollNew[i]);
                    command.ExecuteNonQuery();
                    //Console.WriteLine("Add was successful" + (i + 1));
                }
            }
            
            Console.Read();
        }

    }
}
