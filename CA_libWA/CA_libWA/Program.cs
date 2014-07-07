using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CA_libWA
{
    class Program
    {
        [DllImport("user32.dll")]
        public extern static int MessageBox(IntPtr hWnd,String lpText,String lpCaption,uint uType);
        
        static void Main(string[] args)
        {
            CSLib_RF60x._RF60x_HELLO_ANSWER_ ans = new CSLib_RF60x._RF60x_HELLO_ANSWER_();
            IntPtr hComPort = new IntPtr();
                bool result = CSLib_RF60x.RF60x_OpenPort("COM1", 9600, ref hComPort);
                Console.WriteLine("executing RF60x_OpenPort() result = {0}", result);
                     result = CSLib_RF60x.RF60x_HelloCmd(hComPort, 1, ref ans);
                Console.WriteLine("executing RF60x_HelloCmd() result = {0}", result);
                     result = CSLib_RF60x.RF60x_ClosePort(hComPort);
                Console.WriteLine("executing RF60x_ClosePort() result = {0}", result);
            Console.ReadKey();
        }
    }
}
