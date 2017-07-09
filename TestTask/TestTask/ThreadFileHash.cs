using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestTask
{
    public class ThreadFileHash
    {
        Thread thread1;
        Thread thread2;
        Thread thread3;
        Thread thread4;
        Thread thread5;

        List<string> result = new List<string>();
        string g = null;
        private Object thisLock = new Object();
        Queue<string> queueNew = new Queue<string>();
        int kol;
        
        public ThreadFileHash(Queue<string> queue)
        {
            Thread.Sleep(800);
            queueNew = queue;
            kol = queueNew.Count;
            thread1 = new Thread(HashMethod);
            thread2 = new Thread(HashMethod);
            thread3 = new Thread(HashMethod);
            thread4 = new Thread(HashMethod);
            thread5 = new Thread(HashMethod);
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();
        }

        public void HashMethod()
        {
            lock (thisLock)
            {
                while (!(queueNew.Count == 0))
                {
                    string obj = queueNew.Dequeue();
                    g = ComputeMD5Checksum(obj);
                    result.Add(g);
                    Console.WriteLine(kol-queueNew.Count + "/" + kol);
                    //Console.WriteLine(g);
                }
            }
        }

        private string ComputeMD5Checksum(string path)
        {
            if (path.Equals("This element is NULL"))
            {
                return "This element is NULL";
            }
            else
            {
                using (FileStream source = File.OpenRead(path))
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

        public List<string> Roll { get { return result; } }
    }
}
