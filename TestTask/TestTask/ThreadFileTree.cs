using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TestTask
{
    class ThreadFileTree
    {
        Thread thread;
        Queue<string> queue = new Queue<string>();

        public ThreadFileTree(string s)
        {
            thread = new Thread(FileTreeMethod);
            thread.Start(s);
        }

        public void FileTreeMethod(object s)
        {
            string sourse = s.ToString();
            string[] soursesFile = Directory.GetFiles(sourse, "*");
            string[] soursesCatalog = Directory.GetDirectories(sourse);

            foreach (string dir in soursesFile)
            {
                if (dir == null)
                {
                    queue.Enqueue("This element is NULL");
                }
                else queue.Enqueue(dir);
                //Console.WriteLine(dir);
            }

            foreach (string cat in soursesCatalog)
            {
                FileTreeMethod(cat);
            }
        }
        
        public Queue<string> Roll { get { return queue; } }
    }
}
