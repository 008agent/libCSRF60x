using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace CA_libWA
{
    class ComSearch
    {
        static UInt32[] baudrates = { 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };

        public static void searchRF60x(out String strPortName, out UInt32 dwBaudrate)
        {
            IntPtr hCom = new IntPtr();
            CSLib_RF60x._RF60x_HELLO_ANSWER_ ha = new CSLib_RF60x._RF60x_HELLO_ANSWER_();


            strPortName = "";
            dwBaudrate = 0;

            foreach (String S in SerialPort.GetPortNames())
            {
                foreach (UInt32 rate in baudrates)
                {
                    Console.WriteLine("searching for {0}:{1}",S,rate);
                    if (CSLib_RF60x.RF60x_OpenPort(S, rate, ref hCom))
                    {
                        if (CSLib_RF60x.RF60x_HelloCmd(hCom, 1, ref ha))
                        {
                            strPortName = S;
                            dwBaudrate = rate;
                            Console.WriteLine("Found device {0} on port={1} with baudrate={2}", ha.bDeviceType, S, rate);
                            CSLib_RF60x.RF60x_ClosePort(hCom);
                            return;
                        }
                    }
                    try
                    {
                        CSLib_RF60x.RF60x_ClosePort(hCom);
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine("RF60x_ClosePort() Exception handled");
                    }
                }

            }
        }
    }
}
