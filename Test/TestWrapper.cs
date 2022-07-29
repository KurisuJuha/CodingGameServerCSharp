using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingGameBase;

namespace Test
{
    public abstract class TestWrapper
    {
        private static CodingGameServer codingGameServer;
        private static TestWrapper tw;

        public int value
        {
            get
            {
                byte[] d1 = codingGameServer.sharedata["number"].data;
                int d2 = BitConverter.ToInt32(d1);
                return d2;
            }
            set
            {
                codingGameServer.sharedata["number"].data = BitConverter.GetBytes(value);
            }
        }

        public static void Main(string[] args)
        {
            codingGameServer = new CodingGameServer();

            var t = System.Reflection.Assembly.GetAssembly(typeof(TestWrapper))
    .GetTypes()
    .Where(x => x.IsSubclassOf(typeof(TestWrapper)) && !x.IsAbstract)
    .ToArray();
            var instance = System.Activator.CreateInstance(t[0]) as TestWrapper;
            tw = instance;

            codingGameServer.Tick = tw.Tick;

            codingGameServer.StartServer();

            while (true)
            {
                Console.ReadLine();
            }
        }

        public abstract void Tick();
    }
}
