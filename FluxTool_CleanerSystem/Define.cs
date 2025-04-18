using System.Runtime.CompilerServices;

namespace FluxTool_CleanerSystem
{    
    public enum MODULE
    {
        _PM1 = 0,
        _PM2 = 1,
        _PM3 = 2,
        _WATERTANK = 3
    }

    public enum Page
    {
        LogInPage = 0,
        OperationPage = 1,
        MaintnancePage = 2,
        RecipePage = 3,
        ConfigurePage = 4,
        IOPage = 5,
        AlarmPage = 6,
        EventLogPage = 7,
        UserRegist = 8
    }

    public enum DigitalOffOn
    {
        Off = 0,
        On = 1
    }

    public enum DigitalOnOff
    {
        On = 0,
        Off = 1
    }

    public struct TDigSet
    {
        public string[] curDigSet;
    }

    // 공정 진행 시 화면에 표시해줄 내용
    public struct TPrcsInfo
    {
        public string[] prcsRecipeName;
        public int[] prcsCurrentStep;
        public int[] prcsTotalStep;
        public string[] prcsStepName;
        public double[] prcsStepCurrentTime;
        public double[] prcsStepTotalTime;
        public string[] prcsEndTime;
    }

    // IO LIST /////////////////////////////////////////////
    public enum DigInputList
    {
        CH1_Cylinder_Home_i = 0,
        CH1_Cylinder_Fwd_i = 1,
        CH1_Cylinder_Bwd_i = 2,
        CH1_Tool_Detect1_i = 3,
        CH1_Tool_Detect2_i = 4,

        CH2_Cylinder_Home_i = 5,
        CH2_Cylinder_Fwd_i = 6,
        CH2_Cylinder_Bwd_i = 7,
        CH2_Tool_Detect1_i = 8,
        CH2_Tool_Detect2_i = 9,

        CH3_Cylinder_Home_i = 10,
        CH3_Cylinder_Fwd_i = 11,
        CH3_Cylinder_Bwd_i = 12,
        CH3_Tool_Detect1_i = 13,
        CH3_Tool_Detect2_i = 14,

        Spare1_i = 15,

        Front_Door_Sensor_i = 16,
        Left_Door_Sensor_i = 17,
        Right_Door_Sensor_i = 18,
        Back_Door_Sensor_i = 19,
        EMO_Switch_i = 20,
        Water_Level_High_i = 21,
        Water_Level_Low_i = 22,

        Spare2_i = 23,

        CH1_Door_Op_i = 24,
        CH1_Door_Cl_i = 25,
        CH2_Door_Op_i = 26,
        CH2_Door_Cl_i = 27,
        CH3_Door_Op_i = 28,
        CH3_Door_Cl_i = 29,

        Spare3_i = 30,
        Spare4_i = 31
    }

    public enum DigOutputList
    {                
        CH1_Side_AirValve_o = 0,
        
        Spare1_o = 1,
        Spare2_o = 2,

        CH1_Door_Open_o = 3,
        CH1_Door_Close_o = 4,
        
        CH1_WaterValve_o = 5,
        CH2_WaterValve_o = 6,
        CH3_WaterValve_o = 7,
        
        CH1_Cylinder_Fwd_o = 8,
        CH1_Cylinder_Pwr_o = 9,
        CH1_Cylinder_Bwd_o = 10,
        
        Spare3_o = 11,
        Spare4_o = 12,
        Spare5_o = 13,

        CH1_Pin_UpDn_o = 14,
        
        Spare6_o = 15,        
        
        CH2_Side_AirValve_o = 16,
        
        Spare7_o = 17,
        Spare8_o = 18,

        CH2_Door_Open_o = 19,
        CH2_Door_Close_o = 20,

        CH1_Booster_AirValve_o = 21,
        CH2_Booster_AirValve_o = 22,
        CH3_Booster_AirValve_o = 23,

        CH2_Cylinder_Fwd_o = 24,
        CH2_Cylinder_Pwr_o = 25,
        CH2_Cylinder_Bwd_o = 26,
        
        Spare9_o = 27,
        Spare10_o = 28,
        Spare11_o = 29,
        
        CH2_Pin_UpDn_o = 30,

        Spare12_o = 31,        
        
        CH3_Side_AirValve_o = 32,
        
        Spare13_o = 33,
        Spare14_o = 34,

        CH3_Door_Open_o = 35,
        CH3_Door_Close_o = 36,

        CH1_Air_Knife_o = 37,
        CH2_Air_Knife_o = 38,
        CH3_Air_Knife_o = 39,
        
        CH3_Cylinder_Fwd_o = 40,
        CH3_Cylinder_Pwr_o = 41,
        CH3_Cylinder_Bwd_o = 42,

        FluorescentLamp_o = 43,

        Spare16_o = 44,
        Spare17_o = 45,
        
        CH3_Pin_UpDn_o = 46,

        Spare18_o = 47,
        Spare19_o = 48,
        Spare20_o = 49,

        Hot_Water_Pump_o = 50,
        Hot_WaterHeater_o = 51,
        Main_Water_Supply = 52,
                        
        Tower_Lamp_Red_o = 53,
        Tower_Lamp_Yellow_o = 54,
        Tower_Lamp_Green_o = 55,
        Buzzer_o = 56,

        Spare21_o = 57,
        Spare22_o = 58,
        Spare23_o = 59,
        Spare24_o = 60,
        Spare25_o = 61,
        Spare26_o = 62,
        Spare27_o = 63
    }
    ////////////////////////////////////////////////////////

    // IO (String to int)///////////////////////////////////
    public static class IO_StrToInt
    {
        private static string _io_Name = "";

        public static string io_code
        {
            get
            {
                if (string.IsNullOrEmpty(_io_Name))
                {
                    _io_Name = "IO Name is null";
                }

                return _io_Name;
            }
            set
            {
                if (value == "0")           _io_Name = "CH1_Side_AirValve_o";              
                else if (value == "1")      _io_Name = "Spare1_o";
                else if (value == "2")      _io_Name = "Spare2_o";
                else if (value == "3")      _io_Name = "CH1_Door_Open_o";
                else if (value == "4")      _io_Name = "CH1_Door_Close_o";
                else if (value == "5")      _io_Name = "CH1_WaterValve_o";
                else if (value == "6")      _io_Name = "CH2_WaterValve_o";
                else if (value == "7")      _io_Name = "CH3_WaterValve_o";
                else if (value == "8")      _io_Name = "CH1_Cylinder_Fwd_o";
                else if (value == "9")      _io_Name = "CH1_Cylinder_Pwr_o";
                else if (value == "10")     _io_Name = "CH1_Cylinder_Bwd_o";
                else if (value == "11")     _io_Name = "Spare3_o";
                else if (value == "12")     _io_Name = "Spare4_o";
                else if (value == "13")     _io_Name = "Spare5_o";
                else if (value == "14")     _io_Name = "CH1_Pin_UpDn_o";
                else if (value == "15")     _io_Name = "Spare6_o";
                else if (value == "16")     _io_Name = "CH2_Side_AirValve_o";
                else if (value == "17")     _io_Name = "Spare7_o";
                else if (value == "18")     _io_Name = "Spare8_o";
                else if (value == "19")     _io_Name = "CH2_Door_Open_o";
                else if (value == "20")     _io_Name = "CH2_Door_Close_o";
                else if (value == "21")     _io_Name = "CH1_Booster_AirValve_o";
                else if (value == "22")     _io_Name = "CH2_Booster_AirValve_o";
                else if (value == "23")     _io_Name = "CH3_Booster_AirValve_o";
                else if (value == "24")     _io_Name = "CH2_Cylinder_Fwd_o";
                else if (value == "25")     _io_Name = "CH2_Cylinder_Pwr_o";
                else if (value == "26")     _io_Name = "CH2_Cylinder_Bwd_o";
                else if (value == "27")     _io_Name = "Spare9_o";
                else if (value == "28")     _io_Name = "Spare10_o";
                else if (value == "29")     _io_Name = "Spare11_o";
                else if (value == "30")     _io_Name = "CH2_Pin_UpDn_o";
                else if (value == "31")     _io_Name = "Spare12_o";
                else if (value == "32")     _io_Name = "CH3_Side_AirValve_o";
                else if (value == "33")     _io_Name = "Spare13_o";
                else if (value == "34")     _io_Name = "Spare14_o";
                else if (value == "35")     _io_Name = "CH3_Door_Open_o";
                else if (value == "36")     _io_Name = "CH3_Door_Close_o";
                else if (value == "37")     _io_Name = "CH1_Air_Knife_o";
                else if (value == "38")     _io_Name = "CH2_Air_Knife_o";
                else if (value == "39")     _io_Name = "CH3_Air_Knife_o";
                else if (value == "40")     _io_Name = "CH3_Cylinder_Fwd_o";
                else if (value == "41")     _io_Name = "CH3_Cylinder_Pwr_o";
                else if (value == "42")     _io_Name = "CH3_Cylinder_Bwd_o";
                else if (value == "43")     _io_Name = "FluorescentLamp_o";
                else if (value == "44")     _io_Name = "Spare16_o";
                else if (value == "45")     _io_Name = "Spare17_o";
                else if (value == "46")     _io_Name = "CH3_Pin_UpDn_o";
                else if (value == "47")     _io_Name = "Spare18_o";
                else if (value == "48")     _io_Name = "Spare19_o";
                else if (value == "49")     _io_Name = "Spare20_o";
                else if (value == "50")     _io_Name = "Hot_Water_Pump_o";
                else if (value == "51")     _io_Name = "Hot_WaterHeater_o";
                else if (value == "52")     _io_Name = "Main_Water_Supply";
                else if (value == "53")     _io_Name = "Tower_Lamp_Red_o";
                else if (value == "54")     _io_Name = "Tower_Lamp_Yellow_o";
                else if (value == "55")     _io_Name = "Tower_Lamp_Green_o";
                else if (value == "56")     _io_Name = "Buzzer_o";
                else if (value == "57")     _io_Name = "Spare21_o";
                else if (value == "58")     _io_Name = "Spare22_o";
                else if (value == "59")     _io_Name = "Spare23_o";
                else if (value == "60")     _io_Name = "Spare24_o";
                else if (value == "61")     _io_Name = "Spare25_o";
                else if (value == "62")     _io_Name = "Spare26_o";
                else if (value == "63")     _io_Name = "Spare27_o";
            }
        }
    }
    ////////////////////////////////////////////////////////

    // ALARM LIST //////////////////////////////////////////
    public class Alarm_List
    {
        private string _alarm_Name = "";

        public string alarm_code
        {
            get
            {
                if (string.IsNullOrEmpty(_alarm_Name))
                {
                    _alarm_Name = "Alarm name is missing";
                }

                return _alarm_Name;
            }
            set
            {
                if (value == "900")         _alarm_Name = "Tool does not exist";
                else if (value == "1000")   _alarm_Name = "Door open time out";
                else if (value == "1001")   _alarm_Name = "Door close time out";
                else if (value == "1002")   _alarm_Name = "Failed to read recipe file";                              
                else if (value == "1005")   _alarm_Name = "Cylinder forward time out";
                else if (value == "1006")   _alarm_Name = "Cylinder backward time out";
                else if (value == "1007")   _alarm_Name = "Cylinder home time out";
                else if (value == "1008")   _alarm_Name = "Water filling has timed out";
                else if (value == "1009")   _alarm_Name = "Water heating has timed out";

            }
        }
    }
    ////////////////////////////////////////////////////////

    // CONFIGURE LIST //////////////////////////////////////
    public class Configure_List
    {
        public static int Door_OpCl_Timeout = 0;
        public static int Cylinder_FwdBwd_Timeout = 0;
        public static int Water_Heating_Timeout = 0;
        public static int Water_Fill_Timeout = 0;

        public static double Water_Heating_Temp = 0.0;
    }
    ////////////////////////////////////////////////////////
    
    class Define
    {
        public const int BUFSIZ = 512;
        public const int MODULE_MAX = 4;        
        public const int CH_MAX = 32;
        public const int RECIPE_MAX_STEP = 50;

        // Login 여부
        public static bool bLogin = false;
        
        // User info
        public static string UserId = "";
        public static string UserName = "";
        public static string UserLevel = "";

        // Eventlog 발생 여부
        public static bool bPM1Event;
        public static bool bPM2Event;
        public static bool bPM3Event;
        public static bool bWaterTankEvent;        

        public static bool bPM1AlmEvent;
        public static bool bPM2AlmEvent;
        public static bool bPM3AlmEvent;
        public static bool bPM1OpAlmEvent;
        public static bool bPM2OpAlmEvent;
        public static bool bPM3OpAlmEvent;
        public static bool bWaterTankAlmEvent;        


        public static bool bOpActivate = false;
        public static byte currentPage = 0;
        public static byte MaintCurrentPage = 0;

        public static bool bInterlockRelease = false;
        public static string sInterlockMsg = "";        
        public static string sInterlockChecklist = "";
        public static bool bDoorAutoRelease = false;
        public static bool bSimulation = false;
        

        // Sequence에서 사용 할 변수
        public static byte[] seqMode = { 0, 0, 0, 0 };
        public static byte[] seqCtrl = { 0, 0, 0, 0 };
        public static byte[] seqSts = { 0, 0, 0, 0 };

        public static byte[] seqCylinderMode = { 0, 0, 0 };
        public static byte[] seqCylinderCtrl = { 0, 0, 0 };
        public static byte[] seqCylinderSts = { 0, 0, 0 };

        // Process seq
        public const byte MODE_IDLE = 0;
        public const byte MODE_PROCESS = 1;
        public const byte MODE_INIT = 2;

        public const byte CTRL_IDLE = 0;
        public const byte CTRL_RUN = 1;
        public const byte CTRL_RUNNING = 2;
        public const byte CTRL_ALARM = 3;
        public const byte CTRL_RETRY = 4;
        public const byte CTRL_HOLD = 5;
        public const byte CTRL_ABORT = 6;

        public const byte STS_IDLE = 0;
        public const byte STS_PROCESS_ING = 1;
        public const byte STS_PROCESS_END = 2;
        public const byte STS_INIT_ING = 3;
        public const byte STS_INIT_END = 4;
        public const byte STS_ABORTOK = 5;

        // Cylinder run seq
        public const byte MODE_CYLINDER_IDLE = 0;
        public const byte MODE_CYLINDER_RUN = 1;
        public const byte MODE_CYLINDER_HOME = 2;

        public const byte STS_CYLINDER_IDLE = 0;
        public const byte STS_CYLINDER_RUNING = 1;
        public const byte STS_CYLINDER_RUNEND = 2;
        public const byte STS_CYLINDER_HOMEING = 3;
        public const byte STS_CYLINDER_HOMEEND = 4;
        public const byte STS_CYLINDER_ABORTOK = 5;        


        // Recipe 선택 관련 변수
        public static int iSelectRecipeModule;                              // 선택 된 Module
        public static string[] sSelectRecipeName = { null, null, null };    // 선택 된 Recipe name
        public static bool[] bChangeRecipe = { false, false, false };       // Recipe change flag

        // 알람 name
        public static string sAlarmName;

        // Daily count
        public static int iPM1DailyCnt;
        public static int iPM2DailyCnt;
        public static int iPM3DailyCnt;

        // SPH : 3대 * 3(1대 기준 시간 당 최대 공정 tool 갯수)
        public const int iCapa = 162;   // SPH * 24hour * 0.75(가동률 75%)

        // 가동 시간
        public static double dTodayRunTime;
        public const int iSemiAutoTime = 86400; // 24h * 60m * 60s

        // Chamber Enable/Disable
        public static bool[] bChamberDisable = { false, false, false };

        // Tool check
        public static bool[] bDontCheckTool = { false, false, false };

        // HanyoungNX Heater 변수      
        public static double temp_PV;
        public static double temp_SV;

        // Sql서버에 장비 상태 업로드 변수
        public static string EqType;
        public static string EqLineCode;
        public static string EqAsset;        
        public static string EqRemarks1;
        public static string EqRemarks2;
    }    
}
