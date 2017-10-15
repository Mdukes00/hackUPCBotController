using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackUPCBotController
{
    class Program
    {
        static SerialPort sp = new SerialPort("COM5", 9600);


        private static bool _continue = true;

        static Thread writeThread = new Thread(Write);

        static void Main(string[] args)
        {

            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            sp.Open();

            writeThread.Start();
            while (_continue)
            {


                if (Console.ReadLine().ToString() == "0")
                {
                    _continue = false;

                }

            }
            writeThread.Join();
            sp.Close();
        }

        private static void Write()
        {
            while (_continue)
            {
                try
                {

                    sp.Write(Console.Read().ToString());
                    Read();

                }
                catch (TimeoutException) { }
            }
        }

        public static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = sp.ReadLine();
                    Console.WriteLine(message);
                }
                catch (TimeoutException) { }
            }
        }
    }

}

