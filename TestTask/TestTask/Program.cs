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

        static void Main(string[] args)
        {
            Console.Write("Imput the sourse: ");
            string sourse = Console.ReadLine();

            ThreadFileTree fileThread = new ThreadFileTree(sourse);
            ThreadFileHash hashThread = new ThreadFileHash(fileThread.Roll);

            BDInfo infoBD = new BDInfo();

            infoBD.AddInfoToBD(fileThread.Roll, hashThread.Roll);

            Console.Read();
        }

    }
}
