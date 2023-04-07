using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FluxTool_CleanerSystem.SerialComm
{
    public class ST541Class
    {        
        static SerialPort _serialPort;
        static bool _continue = true;
        static bool bSet_flag = false;

        static bool bThread_start;
        private static Thread readThread;
        static string readData = string.Empty;

        public static void ST541_Init()
        {
            bool bRtn;                        
            
            bRtn = DRV_INIT();
            if (bRtn)
            {
                bThread_start = true;

                readThread = new Thread(Read);
                readThread.Start();
            }
            else
            {
                bThread_start = false;

                Global.EventLog("Heater controller initialization failed", "ST541", "Event");
                DRV_CLOSE();
            }
        }

        private static bool DRV_INIT()
        {
            if (InitPortInfo())
            {
                Global.EventLog("Successfully read communication port information", "ST541", "Event");
            }                
            else
            {
                Global.EventLog("Failed to read communication port information", "ST541", "Event");
                return false;
            }
            
            if (PortOpen())
            {
                Global.EventLog("Successfully opened port", "ST541", "Event");
            }
            else
            {
                Global.EventLog("Failed to opened port", "ST541", "Event");
                return false;
            }

            return true;
        }

        private static bool InitPortInfo()
        {
            _serialPort = new SerialPort();

            string sTmpData;
            string FileName = "ST541PortInfo.txt";

            try
            {
                if (File.Exists(Global.serialPortInfoPath + FileName))
                {
                    byte[] bytes;
                    using (var fs = File.Open(Global.serialPortInfoPath + FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        bytes = new byte[fs.Length];
                        fs.Read(bytes, 0, (int)fs.Length);
                        sTmpData = Encoding.Default.GetString(bytes);

                        char sp = ',';
                        string[] spString = sTmpData.Split(sp);
                        for (int i = 0; i < spString.Length; i++)
                        {
                            string sPortName = spString[0];
                            int iBaudRate = int.Parse(spString[1]);
                            int iDataBits = int.Parse(spString[2]);
                            int iStopBits = int.Parse(spString[3]);
                            int iParity = int.Parse(spString[4]);

                            _serialPort.PortName = sPortName;
                            _serialPort.BaudRate = iBaudRate;
                            _serialPort.DataBits = iDataBits;
                            _serialPort.StopBits = (StopBits)iStopBits;
                            _serialPort.Parity = (Parity)iParity;

                            _serialPort.ReadTimeout = 500;
                            _serialPort.WriteTimeout = 500;
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "알림");
                return false;
            }            
        }

        private static bool PortOpen()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();                               
                foreach (string port in ports)
                {
                    if (port != "")
                    {
                        _serialPort.Open();
                        if (_serialPort.IsOpen)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }                    
                }

                return false;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "알림");
                return false;
            }            
        }

        private static void DRV_CLOSE()
        {
            if (bThread_start)
            {
                readThread.Abort();
            }
            
            Global.EventLog("ST541 communication driver has been terminated", "ST541", "Event");
        }

        // NOVA ST541 Thread //////////////////////////////////////////////////////////////////////////
        private static void Read()
        {            
            while (_continue)
            {
                try
                {
                    if (!bSet_flag)
                    {
                        Parameter_read();
                    }
                    
                    Thread.Sleep(10);
                }
                catch (TimeoutException)
                {

                }                
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        
        private static void Parameter_read()
        {
            // PV
            string send_Command = string.Format("{0}{1:D2}RSD,01,0001{2}{3}", Convert.ToChar(0x02), 1, Convert.ToChar(0x0D), Convert.ToChar(0x0A));
            _serialPort.Write(send_Command);

            Thread.Sleep(10);

            readData = _serialPort.ReadLine();
            bool bFind = readData.Contains("OK");
            if (bFind)
            {
                string strTmp = readData.Substring(10, 4);
                // 16진수 string값을 10진수로 변환
                int iDecimal = Int32.Parse(strTmp, System.Globalization.NumberStyles.HexNumber);
                Define.temp_PV = iDecimal * 0.1;
            }
        }

        public static void set_Temp(double dVal)
        {
            bSet_flag = true;

            int iVal = Convert.ToInt32(dVal * 10.0);

            string send_Command = string.Format("{0}{1:D2}WSD,01,0201,{2:D4}{3}{4}", Convert.ToChar(0x02), 1, iVal, Convert.ToChar(0x0D), Convert.ToChar(0x0A));
            _serialPort.Write(send_Command);

            bSet_flag = false;

            Thread.Sleep(10);
        }

        public static void set_Run(int iVal)
        {
            bSet_flag = true;
            
            string send_Command = string.Format("{0}{1:D2}WSD,01,0101,{2:X4}{3}{4}", Convert.ToChar(0x02), 1, iVal, Convert.ToChar(0x0D), Convert.ToChar(0x0A));
            _serialPort.Write(send_Command);

            bSet_flag = false;

            Thread.Sleep(10);
        }
    }
}
