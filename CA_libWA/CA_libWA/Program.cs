﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace CA_libWA
{
    class Program
    {
        [DllImport("user32.dll")]
        public extern static int MessageBox(IntPtr hWnd,String lpText,String lpCaption,uint uType);
        
        /// <summary>
        /// Поток обработки результатов измерения
        /// </summary>
        /// <param name="args">что угодно</param>
        public static void routine_measurement(object args)
        {
            while (true)
            {
                //TODO: place measurement code here;
            }
        }

        static void Main(string[] args)
        {
            // div - разница конечного результата и результата измерения.
            // fresult - вычисленный результат текущего измерения
            // way - направление переполнения
            // wData - полученный с датчика результат
            float div = 0, fresult = 0; string way = "";
            UInt16 wData=0;
            // структура с ответом от устройства
            CSLib_RF60x._RF60x_HELLO_ANSWER_ ans = new CSLib_RF60x._RF60x_HELLO_ANSWER_();
            // дескриптор устройства COM порта
            IntPtr hComPort = new IntPtr();
            string ComName;
            UInt32 ComBaudrate;

            ComSearch.searchRF60x(out ComName, out ComBaudrate);

            bool result = CSLib_RF60x.RF60x_OpenPort(ComName, ComBaudrate, ref hComPort);
                Console.WriteLine("executing RF60x_OpenPort() result = {0}", result);
                     result = CSLib_RF60x.RF60x_HelloCmd(hComPort, 1, ref ans);
                Console.WriteLine("executing RF60x_HelloCmd() result = {0}", result);
                Console.WriteLine("dev_modification={0}\ndev_type={1}\nmax_dist={2}\ndev_range={3}\ndev_serial={4}",ans.bDeviceModification,ans.bDeviceType,ans.wDeviceMaxDistance,ans.wDeviceRange,ans.wDeviceSerial);
                Console.WriteLine("press any key to continue and start measuring...");
                Console.ReadKey();
                try
                {
                    //CSLib_RF60x.RF60x_StartStream(hComPort, 1);
                    while (true)
                    {
                        result = CSLib_RF60x.RF60x_Measure(hComPort, 1, ref wData);
                        CSLib_RF60x.RF60x_GetStreamMeasure(hComPort, ref wData);
                        /*if (wData != 0)
                        {*/
                            fresult = CSLib_RF60x.DToXTransform(wData, ans.wDeviceRange);
                            div = fresult - 2.5f;
                        /*}*/

                        if (div == 0.0f) way = "OK";
                        else if (div < 0.0f) way = "UP";
                        else way = "DOWN";

                        Console.Clear();
                        Console.Write("measure result = {0:f4}mm\ndiv = {1:f4}mm\ndecision = {2}", fresult, div, way);
                        Thread.Sleep(200);//5 measurments per second
                    }
                   
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }
                finally
                {
                    //CSLib_RF60x.RF60x_StopStream(hComPort, 1);
                    result = CSLib_RF60x.RF60x_ClosePort(hComPort);
                    
                    Console.WriteLine("executing RF60x_ClosePort() result = {0}", result);

                    Console.ReadKey();
                }
        }
    }
}
