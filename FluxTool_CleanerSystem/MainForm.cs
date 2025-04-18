using Ajin_IO_driver;
using MsSqlManagerLibrary;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FluxTool_CleanerSystem
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        LoginForm m_loginForm;
        OperationForm m_operationForm;
        MaintnanceForm m_maintnanceForm;
        RecipeForm m_recipeForm;
        ConfigureForm m_configureForm;
        IOForm m_ioForm;
        AlarmForm m_alarmForm;
        EventLogForm m_eventLogForm;
        UserRegistForm m_userRegistForm;
        
        Squence.PM1Process pM1Process;
        Squence.PM1Cylinder pM1Cylinder;

        Squence.PM2Process pM2Process;
        Squence.PM2Cylinder pM2Cylinder;

        Squence.PM3Process pM3Process;
        Squence.PM3Cylinder pM3Cylinder;
        
        Squence.WaterTankFill waterTankFill;        

        private Label[] m_PrcsModeBox;
        private Label[] m_PrcsCtrlBox;
        private Label[] m_PrcsStsBox;
        private Label[] m_PrcsStepBox;

        private Label[] m_CylinderModeBox;
        private Label[] m_CylinderCtrlBox;
        private Label[] m_CylinderStsBox;
        private Label[] m_CylinderStepBox;

        bool bLogCnt;

        public MainForm()
        {           
            InitializeComponent();

            SubFormCreate();

            CreateThread();                        
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Width = 1280;
            Height = 1024;
            Top = 0;
            Left = 0;            

            Define.bLogin = false;
            Define.bOpActivate = false;

            bLogCnt = false;

            MyNativeWindows myNativeWindows = new MyNativeWindows();

            for (int i = 0; i < this.Controls.Count; i++)
            {
                MdiClient mdiClient = this.Controls[i] as MdiClient;
                if (mdiClient != null)
                {
                    myNativeWindows.ReleaseHandle();
                    myNativeWindows.AssignHandle(mdiClient.Handle);
                }
            }

            m_PrcsModeBox = new Label[Define.MODULE_MAX] { labelCH1_ProcessMode, labelCH2_ProcessMode, labelCH3_ProcessMode, labelWT_ProcessMode };
            m_PrcsCtrlBox = new Label[Define.MODULE_MAX] { labelCH1_ProcessCtrl, labelCH2_ProcessCtrl, labelCH3_ProcessCtrl, labelWT_ProcessCtrl };
            m_PrcsStsBox = new Label[Define.MODULE_MAX] { labelCH1_ProcessSts, labelCH2_ProcessSts, labelCH3_ProcessSts, labelWT_ProcessSts };
            m_PrcsStepBox = new Label[Define.MODULE_MAX] { labelCH1_ProcessStep, labelCH2_ProcessStep, labelCH3_ProcessStep, labelWT_ProcessStep };

            m_CylinderModeBox = new Label[Define.MODULE_MAX-1] { labelCH1_CylinderMode, labelCH2_CylinderMode, labelCH3_CylinderMode };
            m_CylinderCtrlBox = new Label[Define.MODULE_MAX-1] { labelCH1_CylinderCtrl, labelCH2_CylinderCtrl, labelCH3_CylinderCtrl };
            m_CylinderStsBox = new Label[Define.MODULE_MAX-1] { labelCH1_CylinderSts, labelCH2_CylinderSts, labelCH3_CylinderSts };
            m_CylinderStepBox = new Label[Define.MODULE_MAX-1] { labelCH1_CylinderStep, labelCH2_CylinderStep, labelCH3_CylinderStep };
            
            
            // IO보드 초기화
            if (DIOClass.OpenDevice())
            {
                m_ioForm.DI_Parsing_timer.Start();
            }

            Global.Init();

            // 장비 정보 불러오기
            EQ_INFO_LOAD();

            // 가동 시간 불러오기
            RUNTIME_LOAD();

            timerDisplay.Enabled = true;
            simulationTimer.Enabled = true;

            SubFormShow((byte)Page.LogInPage);

            F_ButtonVisible(false, false, false, false, false, false, false, false);            
        }

        public class MyNativeWindows : NativeWindow
        {
            public MyNativeWindows()
            {
            }

            private const int WM_NCCALCSIZE = 0x0083;
            private const int SB_BOTH = 3;

            [DllImport("user32.dll")]
            private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case WM_NCCALCSIZE:
                        ShowScrollBar(m.HWnd, SB_BOTH, 0);
                        break;
                }
                base.WndProc(ref m);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDisplay.Enabled = false;
            simulationTimer.Enabled = false;

            FreeThread();

            Dispose();
        }

        private void SubFormCreate()
        {
            m_operationForm = new OperationForm();
            m_operationForm.MdiParent = this;
            m_operationForm.Show();

            m_loginForm = new LoginForm();
            m_loginForm.MdiParent = this;
            m_loginForm.Show();

            m_userRegistForm = new UserRegistForm();
            m_userRegistForm.MdiParent = this;
            m_userRegistForm.Show();

            m_maintnanceForm = new MaintnanceForm();
            m_maintnanceForm.MdiParent = this;
            m_maintnanceForm.Show();

            m_recipeForm = new RecipeForm();
            m_recipeForm.MdiParent = this;
            m_recipeForm.Show();

            m_configureForm = new ConfigureForm();
            m_configureForm.MdiParent = this;
            m_configureForm.Show();

            m_ioForm = new IOForm();
            m_ioForm.MdiParent = this;
            m_ioForm.Show();

            m_alarmForm = new AlarmForm();
            m_alarmForm.MdiParent = this;
            m_alarmForm.Show();

            m_eventLogForm = new EventLogForm();
            m_eventLogForm.MdiParent = this;
            m_eventLogForm.Show();            
        }

        private void CreateThread()
        {            
            pM1Process = new Squence.PM1Process();
            pM1Cylinder = new Squence.PM1Cylinder();

            pM2Process = new Squence.PM2Process();
            pM2Cylinder = new Squence.PM2Cylinder();

            pM3Process = new Squence.PM3Process();
            pM3Cylinder = new Squence.PM3Cylinder();
            
            waterTankFill = new Squence.WaterTankFill();
        }

        private void FreeThread()
        {            
            pM1Process.Dispose();
            pM1Cylinder.Dispose();

            pM2Process.Dispose();
            pM2Cylinder.Dispose();

            pM3Process.Dispose();
            pM3Cylinder.Dispose();
            
            waterTankFill.Dispose();

            DIOClass.CloseDevice();
        }

        public void SubFormShow(byte PageNum)
        {
            try
            {
                Define.currentPage = PageNum;
                byte iPage = PageNum;

                switch (iPage)
                {
                    case (byte)Page.LogInPage:
                        {
                            m_loginForm.Activate();
                            m_loginForm.BringToFront();

                            F_ModuleButtonVisible(false, false, false, false);
                        }
                        break;

                    case (byte)Page.OperationPage:
                        {
                            m_operationForm.Activate();
                            m_operationForm.BringToFront();

                            F_ModuleButtonVisible(false, false, false, false);
                        }
                        break;

                    case (byte)Page.MaintnancePage:
                        {
                            m_maintnanceForm.Activate();
                            m_maintnanceForm.BringToFront();

                            F_ModuleButtonVisible(true, true, true, true);

                            // 프로그램을 처음 켜고 Maintnance버튼을 눌렀을 때 초기화
                            if (Define.MaintCurrentPage == (byte)MODULE._PM1)
                                btnLeftModule.BackColor = Color.Lime;
                        }
                        break;

                    case (byte)Page.RecipePage:
                        {
                            m_recipeForm.Activate();
                            m_recipeForm.BringToFront();

                            F_ModuleButtonVisible(false, false, false, false);
                        }
                        break;

                    case (byte)Page.ConfigurePage:
                        {
                            m_configureForm.Activate();
                            m_configureForm.BringToFront();

                            F_ModuleButtonVisible(false, false, false, false);
                        }
                        break;

                    case (byte)Page.IOPage:
                        {
                            m_ioForm.Activate();
                            m_ioForm.BringToFront();

                            F_ModuleButtonVisible(false, false, false, false);
                        }
                        break;

                    case (byte)Page.AlarmPage:
                        {
                            m_alarmForm.Activate();
                            m_alarmForm.BringToFront();

                            F_ModuleButtonVisible(false, false, false, false);
                        }
                        break;

                    case (byte)Page.EventLogPage:
                        {
                            m_eventLogForm.Activate();
                            m_eventLogForm.BringToFront();

                            F_ModuleButtonVisible(false, false, false, false);
                        }
                        break;

                    case (byte)Page.UserRegist:
                        {
                            m_userRegistForm.Activate();
                            m_userRegistForm.BringToFront();

                            F_ModuleButtonVisible(false, false, false, false);
                        }
                        break;
                }
            }
            catch
            {
                MessageBox.Show("폼 양식을 가져오는 도중 오류가 발생했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void F_ButtonVisible(bool bOpBtn, bool bMaintBtn, bool bRecipeBtn, bool bConfigureBtn, bool bIOBtn, bool bAlarmBtn, bool bEventLogBtn, bool bUserRegistBtn)
        {
            pictureBoxOperation.Enabled = bOpBtn;
            btnOperation.Enabled = bOpBtn;

            pictureBoxMain.Enabled = bMaintBtn;
            btnMaintnance.Enabled = bMaintBtn;

            pictureBoxRecipe.Enabled = bRecipeBtn;
            btnRecipe.Enabled = bRecipeBtn;

            pictureBoxConfigure.Enabled = bConfigureBtn;
            btnConfigure.Enabled = bConfigureBtn;

            pictureBoxIO.Enabled = bIOBtn;
            btnIO.Enabled = bIOBtn;

            pictureBoxAlarm.Enabled = bAlarmBtn;
            pictureBoxAlarm2.Enabled = bAlarmBtn;
            btnAlarm.Enabled = bAlarmBtn;

            pictureBoxEventLog.Enabled = bEventLogBtn;
            btnEventLog.Enabled = bEventLogBtn;

            pictureBoxUserRegist.Enabled = bUserRegistBtn;
            btnUserRegist.Enabled = bUserRegistBtn;
        }

        private void F_ModuleButtonVisible(bool bLMBtn, bool bCMBtn, bool bRMBtn, bool bWTBtn)
        {
            btnLeftModule.Visible = bLMBtn;
            btnCenterModule.Visible = bCMBtn;
            btnRightModule.Visible = bRMBtn;
            btnWaterTank.Visible = bWTBtn;
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            SubFormShow((byte)Page.OperationPage);
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            SubFormShow((byte)Page.MaintnancePage);
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            SubFormShow((byte)Page.RecipePage);
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            SubFormShow((byte)Page.ConfigurePage);
        }

        private void btnIO_Click(object sender, EventArgs e)
        {
            SubFormShow((byte)Page.IOPage);
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            SubFormShow((byte)Page.AlarmPage);
        }

        private void pictureBoxAlarm_Click(object sender, EventArgs e)
        {
            Global.SetDigValue((int)DigOutputList.Buzzer_o, (uint)DigitalOffOn.Off, "PM1");
        }

        private void btnEventLog_Click(object sender, EventArgs e)
        {
            SubFormShow((byte)Page.EventLogPage);
        }

        private void btnUserRegist_Click(object sender, EventArgs e)
        {
            SubFormShow((byte)Page.UserRegist);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("프로그램을 종료 하겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Dispose();
                //Application.Exit();
                Application.ExitThread();
                Environment.Exit(0);
            }
        }

        private void btnModule_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string strTmp = btn.Text.ToString();
            switch (strTmp)
            {
                case "CH1":
                    {
                        if (!m_maintnanceForm.m_PM1Form.Visible)
                            m_maintnanceForm.m_PM1Form.Visible = true;

                        if (m_maintnanceForm.m_PM2Form.Visible != false)
                            m_maintnanceForm.m_PM2Form.Visible = false;

                        if (m_maintnanceForm.m_PM3Form.Visible != false)
                            m_maintnanceForm.m_PM3Form.Visible = false;

                        if (m_maintnanceForm.m_waterTankForm.Visible != false)
                            m_maintnanceForm.m_waterTankForm.Visible = false;

                        btnLeftModule.BackColor = Color.Lime;
                        btnCenterModule.BackColor = Color.Transparent;
                        btnRightModule.BackColor = Color.Transparent;
                        btnWaterTank.BackColor = Color.Transparent;

                        Define.MaintCurrentPage = (byte)MODULE._PM1;
                    }
                    break;

                case "CH2":
                    {
                        if (!m_maintnanceForm.m_PM2Form.Visible)
                            m_maintnanceForm.m_PM2Form.Visible = true;

                        if (m_maintnanceForm.m_PM1Form.Visible != false)
                            m_maintnanceForm.m_PM1Form.Visible = false;

                        if (m_maintnanceForm.m_PM3Form.Visible != false)
                            m_maintnanceForm.m_PM3Form.Visible = false;

                        if (m_maintnanceForm.m_waterTankForm.Visible != false)
                            m_maintnanceForm.m_waterTankForm.Visible = false;

                        btnLeftModule.BackColor = Color.Transparent;
                        btnCenterModule.BackColor = Color.Lime;
                        btnRightModule.BackColor = Color.Transparent;
                        btnWaterTank.BackColor = Color.Transparent;

                        Define.MaintCurrentPage = (byte)MODULE._PM2;
                    }
                    break;

                case "CH3":
                    {
                        if (!m_maintnanceForm.m_PM3Form.Visible)
                            m_maintnanceForm.m_PM3Form.Visible = true;

                        if (m_maintnanceForm.m_PM1Form.Visible != false)
                            m_maintnanceForm.m_PM1Form.Visible = false;

                        if (m_maintnanceForm.m_PM2Form.Visible != false)
                            m_maintnanceForm.m_PM2Form.Visible = false;

                        if (m_maintnanceForm.m_waterTankForm.Visible != false)
                            m_maintnanceForm.m_waterTankForm.Visible = false;

                        btnLeftModule.BackColor = Color.Transparent;
                        btnCenterModule.BackColor = Color.Transparent;
                        btnRightModule.BackColor = Color.Lime;
                        btnWaterTank.BackColor = Color.Transparent;

                        Define.MaintCurrentPage = (byte)MODULE._PM3;
                    }
                    break;

                case "WaterTank":
                    {
                        if (!m_maintnanceForm.m_waterTankForm.Visible)
                            m_maintnanceForm.m_waterTankForm.Visible = true;

                        if (m_maintnanceForm.m_PM1Form.Visible != false)
                            m_maintnanceForm.m_PM1Form.Visible = false;

                        if (m_maintnanceForm.m_PM2Form.Visible != false)
                            m_maintnanceForm.m_PM2Form.Visible = false;

                        if (m_maintnanceForm.m_PM3Form.Visible != false)
                            m_maintnanceForm.m_PM3Form.Visible = false;

                        btnLeftModule.BackColor = Color.Transparent;
                        btnCenterModule.BackColor = Color.Transparent;
                        btnRightModule.BackColor = Color.Transparent;
                        btnWaterTank.BackColor = Color.Lime;

                        Define.MaintCurrentPage = (byte)MODULE._WATERTANK;
                    }
                    break;
            }
        }

        private void panelLogo_Click(object sender, EventArgs e)
        {
            if (panelOption.Visible == false)
                panelOption.Visible = true;
            else
                panelOption.Visible = false;
        }

        private void boxBtnSeqStatus_Click(object sender, EventArgs e)
        {
            if (panelSeqStatus.Visible == false)
                panelSeqStatus.Visible = true;
            else
                panelSeqStatus.Visible = false;
        }

        private void checkBoxInterlockRelease_Click(object sender, EventArgs e)
        {
            if (checkBoxInterlockRelease.Checked)
            {
                checkBoxInterlockRelease.Checked = true;
                Define.bInterlockRelease = true;
            }
            else
            {
                checkBoxInterlockRelease.Checked = false;
                Define.bInterlockRelease = false;
            }
        }
        
        private void checkBoxSimulation_Click(object sender, EventArgs e)
        {
            if (checkBoxSimulation.Checked)
            {
                checkBoxSimulation.Checked = true;
                Define.bSimulation = true;

                Global.EventLog("Simulation mode is on.", "PM1", "Event");
            }
            else
            {
                checkBoxSimulation.Checked = false;
                Define.bSimulation = false;

                Global.EventLog("Simulation mode is off.", "PM1", "Event");
            }
        }

        private void timerDisplay_Tick(object sender, EventArgs e)
        {
            Display();

            // 시퀀스 Mode, Ctrl, Sts, Step의 상태를 모니터링 하기 위함
            //SeqDisplay();
        }

        private void Display()
        {
            laDate.Text = System.DateTime.Today.ToShortDateString();
            laTime.Text = System.DateTime.Now.ToLongTimeString();
            laUserLevel.Text = "Level : " + Define.UserLevel;

            if (Define.currentPage == (byte)Page.OperationPage)
            {
                labelPageName.Text = "Operation";
                if (Define.bOpActivate)
                {
                    m_operationForm.Activate();
                    m_operationForm.BringToFront();

                    Define.bOpActivate = false;
                }
            }
            else if (Define.currentPage == (byte)Page.MaintnancePage)
            {
                labelPageName.Text = "Maintnance";
            }
            else if (Define.currentPage == (byte)Page.RecipePage)
            {
                labelPageName.Text = "Recipe";
            }
            else if (Define.currentPage == (byte)Page.ConfigurePage)
            {
                labelPageName.Text = "Configure";
            }
            else if (Define.currentPage == (byte)Page.IOPage)
            {
                labelPageName.Text = "Input/Output";
            }
            else if (Define.currentPage == (byte)Page.AlarmPage)
            {
                labelPageName.Text = "Alarm";
            }
            else if (Define.currentPage == (byte)Page.EventLogPage)
            {
                labelPageName.Text = "Event Log";
            }
            else if (Define.currentPage == (byte)Page.UserRegist)
            {
                labelPageName.Text = "User regist";
                m_userRegistForm.BringToFront();
            }
            else if (Define.currentPage == (byte)Page.LogInPage)
            {
                labelPageName.Text = "Log-In";
            }

            // User level에 따른 버튼 활성화
            if (Define.UserLevel == "Master")
            {
                // op, maint, recipe, configure, io, alarm, userRegist
                F_ButtonVisible(true, true, true, true, true, true, true, true);
            }
            else if (Define.UserLevel == "Maintnance")
            {
                F_ButtonVisible(true, true, true, true, true, true, true, false);
            }
            else if (Define.UserLevel == "User")
            {
                F_ButtonVisible(true, false, true, true, false, true, true, false);
            }


            // Tower lamp
            if ((Define.seqCtrl[(byte)MODULE._PM1] == Define.CTRL_ALARM) ||
                (Define.seqCtrl[(byte)MODULE._PM2] == Define.CTRL_ALARM) ||
                (Define.seqCtrl[(byte)MODULE._PM3] == Define.CTRL_ALARM) ||
                
                (Define.seqCtrl[(byte)MODULE._WATERTANK] == Define.CTRL_ALARM) ||

                (Global.GetDigValue((int)DigInputList.EMO_Switch_i) == "Off"))
            {
                if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Red_o] != null)
                {
                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Red_o] != "On")
                    {
                        Global.SetDigValue((int)DigOutputList.Tower_Lamp_Red_o, (uint)DigitalOffOn.On, "PM1");

                        HostConnection.Host_Set_Signal(Define.EqType, Define.EqLineCode, Define.EqAsset, 
                            (int)DigitalOffOn.On, (int)DigitalOffOn.Off, (int)DigitalOffOn.Off, (int)DigitalOffOn.Off, Define.EqRemarks1, Define.EqRemarks2, DateTime.Now);
                    }                        
                    else
                    {
                        Global.SetDigValue((int)DigOutputList.Tower_Lamp_Red_o, (uint)DigitalOffOn.Off, "PM1");
                    }                        
                }

                if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Yellow_o] != null)
                {
                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Yellow_o] != "Off")                        
                        Global.SetDigValue((int)DigOutputList.Tower_Lamp_Yellow_o, (uint)DigitalOffOn.Off, "PM1");
                }

                if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Green_o] != null)
                {
                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Green_o] != "Off")
                        Global.SetDigValue((int)DigOutputList.Tower_Lamp_Green_o, (uint)DigitalOffOn.Off, "PM1");
                }

                if (pictureBoxAlarm.Visible)
                    pictureBoxAlarm.Visible = false;
                else
                    pictureBoxAlarm.Visible = true;
            }
            else
            {
                if ((Define.seqCtrl[(byte)MODULE._PM1] == Define.CTRL_IDLE) &&
                    (Define.seqCtrl[(byte)MODULE._PM2] == Define.CTRL_IDLE) &&
                    (Define.seqCtrl[(byte)MODULE._PM3] == Define.CTRL_IDLE) &&
                    
                    (Define.seqCtrl[(byte)MODULE._WATERTANK] == Define.CTRL_IDLE))
                {
                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Red_o] != null)
                    {
                        if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Red_o] != "Off")
                            Global.SetDigValue((int)DigOutputList.Tower_Lamp_Red_o, (uint)DigitalOffOn.Off, "PM1");
                    }

                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Yellow_o] != null)
                    {
                        if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Yellow_o] != "On")
                        {
                            Global.SetDigValue((int)DigOutputList.Tower_Lamp_Yellow_o, (uint)DigitalOffOn.On, "PM1");

                            HostConnection.Host_Set_Signal(Define.EqType, Define.EqLineCode, Define.EqAsset, 
                                (int)DigitalOffOn.Off, (int)DigitalOffOn.On, (int)DigitalOffOn.Off, (int)DigitalOffOn.Off, Define.EqRemarks1, Define.EqRemarks2, DateTime.Now);
                        }                            
                    }

                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Green_o] != null)
                    {
                        if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Green_o] != "Off")
                            Global.SetDigValue((int)DigOutputList.Tower_Lamp_Green_o, (uint)DigitalOffOn.Off, "PM1");
                    }

                    if (pictureBoxAlarm.Visible != false)
                        pictureBoxAlarm.Visible = false;
                }
                else
                {
                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Red_o] != null)
                    {
                        if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Red_o] != "Off")
                            Global.SetDigValue((int)DigOutputList.Tower_Lamp_Red_o, (uint)DigitalOffOn.Off, "PM1");
                    }

                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Yellow_o] != null)
                    {
                        if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Yellow_o] != "Off")
                            Global.SetDigValue((int)DigOutputList.Tower_Lamp_Yellow_o, (uint)DigitalOffOn.Off, "PM1");
                    }

                    if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Green_o] != null)
                    {
                        if (Global.digSet.curDigSet[(int)DigOutputList.Tower_Lamp_Green_o] != "On")
                        {
                            Global.SetDigValue((int)DigOutputList.Tower_Lamp_Green_o, (uint)DigitalOffOn.On, "PM1");

                            HostConnection.Host_Set_Signal(Define.EqType, Define.EqLineCode, Define.EqAsset,
                                (int)DigitalOffOn.Off, (int)DigitalOffOn.Off, (int)DigitalOffOn.On, (int)DigitalOffOn.Off, Define.EqRemarks1, Define.EqRemarks2, DateTime.Now);
                        }                            
                    }

                    // 가동 시간
                    RUNTIME_CALC();
                }

                if (pictureBoxAlarm.Visible != false)
                    pictureBoxAlarm.Visible = false;
            }


            if (Define.bInterlockRelease)
            {
                if (labelInterlockEnaDis.Visible)
                    labelInterlockEnaDis.Visible = false;
                else
                    labelInterlockEnaDis.Visible = true;                
            }
            else
            {
                if (labelInterlockEnaDis.Visible != false)
                    labelInterlockEnaDis.Visible = false;
            }

            // Fluorescent Lamp set
            if (Global.GetDigValue((int)DigInputList.Front_Door_Sensor_i) == "Off")
            {
                if (Global.digSet.curDigSet[(int)DigOutputList.FluorescentLamp_o] != null)
                {
                    if (Global.digSet.curDigSet[(int)DigOutputList.FluorescentLamp_o] != "On")
                    {
                        Global.SetDigValue((int)DigOutputList.FluorescentLamp_o, (uint)DigitalOffOn.On, "PM3");
                    }
                }
            }
            else
            {
                if (Global.digSet.curDigSet[(int)DigOutputList.FluorescentLamp_o] != null)
                {
                    if (Global.digSet.curDigSet[(int)DigOutputList.FluorescentLamp_o] != "Off")
                    {
                        Global.SetDigValue((int)DigOutputList.FluorescentLamp_o, (uint)DigitalOffOn.Off, "PM3");
                    }
                }
            }

            // Process end - buzzer auto off
            if (Global.GetDigValue((int)DigInputList.Front_Door_Sensor_i) == "Off")
            {
                if (Global.digSet.curDigSet[(int)DigOutputList.Buzzer_o] != null)
                {
                    if (Global.digSet.curDigSet[(int)DigOutputList.Buzzer_o] != "Off")
                        Global.SetDigValue((int)DigOutputList.Buzzer_o, (uint)DigitalOffOn.Off, "PM1");
                }
            }

            // Daily count init
            string sTime = DateTime.Now.ToString("HH:mm:ss");
            if (sTime == "00:00:00")
            {
                if (!bLogCnt)
                {                    
                    // 가동 시간 및 가동률 서버 업데이트
                    RUNTIME_UPDATE();

                    // 가동 시간 초기화
                    RUNTIME_INIT();

                    if (Define.iPM1DailyCnt != 0)
                        Define.iPM1DailyCnt = 0;

                    if (Define.iPM2DailyCnt != 0)
                        Define.iPM2DailyCnt = 0;

                    if (Define.iPM3DailyCnt != 0)
                        Define.iPM3DailyCnt = 0;

                    bLogCnt = true;
                }
            }
            else
            {
                if (bLogCnt != false)
                {
                    bLogCnt = false;
                }
            }
        }

        private void EQ_INFO_LOAD()
        {
            StringBuilder sbEqInfo = new StringBuilder();
            GetPrivateProfileString("EqInfo", "Type", "", sbEqInfo, sbEqInfo.Capacity, string.Format("{0}{1}", Global.ConfigurePath, "EqInfo.ini"));
            Define.EqType = sbEqInfo.ToString();

            GetPrivateProfileString("EqInfo", "LineCode", "", sbEqInfo, sbEqInfo.Capacity, string.Format("{0}{1}", Global.ConfigurePath, "EqInfo.ini"));
            Define.EqLineCode = sbEqInfo.ToString();

            GetPrivateProfileString("EqInfo", "Asset", "", sbEqInfo, sbEqInfo.Capacity, string.Format("{0}{1}", Global.ConfigurePath, "EqInfo.ini"));
            Define.EqAsset = sbEqInfo.ToString();

            GetPrivateProfileString("EqInfo", "Remarks1", "", sbEqInfo, sbEqInfo.Capacity, string.Format("{0}{1}", Global.ConfigurePath, "EqInfo.ini"));
            Define.EqRemarks1 = sbEqInfo.ToString();

            GetPrivateProfileString("EqInfo", "Remarks2", "", sbEqInfo, sbEqInfo.Capacity, string.Format("{0}{1}", Global.ConfigurePath, "EqInfo.ini"));
            Define.EqRemarks2 = sbEqInfo.ToString();
        }

        private void RUNTIME_LOAD()
        {
            StringBuilder sbTodayRunTime = new StringBuilder();
            GetPrivateProfileString("TodayRuntime", "Time", "", sbTodayRunTime, sbTodayRunTime.Capacity, string.Format("{0}{1}", Global.dailyCntfilePath, "TodayRuntime.ini"));
            Define.dTodayRunTime = Convert.ToDouble(sbTodayRunTime.ToString());
        }

        private void RUNTIME_CALC()
        {
            Define.dTodayRunTime += 0.5;
            WritePrivateProfileString("TodayRuntime", "Time", Define.dTodayRunTime.ToString(), string.Format("{0}{1}", Global.dailyCntfilePath, "TodayRuntime.ini"));
        }

        private void RUNTIME_UPDATE()
        {
            // Only daily count performance
            int iDailyAllCnt = Define.iPM1DailyCnt + Define.iPM2DailyCnt + Define.iPM3DailyCnt;
            double dPerformance = ((double)(iDailyAllCnt) / Define.iCapa) * 100;
            string strPerformance = dPerformance.ToString("0.000");

            // 실제 가동 시간 기준 performance
            double dTimePerformance = (Define.dTodayRunTime / (double)Define.iSemiAutoTime) * 100;
            string strTimePerformance = dTimePerformance.ToString("0.000");

            HostConnection.Host_Set_Log(Global.hostEquipmentInfo_Log,
                DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                Define.iPM1DailyCnt.ToString("00"),
                Define.iPM2DailyCnt.ToString("00"),
                Define.iPM3DailyCnt.ToString("00"),                
                strPerformance,
                Define.dTodayRunTime.ToString(),
                strTimePerformance);
        }

        private void RUNTIME_INIT()
        {
            WritePrivateProfileString("TodayRuntime", "Time", "0", string.Format("{0}{1}", Global.dailyCntfilePath, "TodayRuntime.ini"));
            Define.dTodayRunTime = 0;
        }        

        private void SeqDisplay()
        {
            for (int i=0; i<Define.MODULE_MAX; ++i)
            {
                if (Define.seqMode[i] == Define.MODE_IDLE)
                {
                    m_PrcsModeBox[i].Text = "Idle";
                }
                else if (Define.seqMode[i] == Define.MODE_PROCESS)
                {
                    m_PrcsModeBox[i].Text = "Process";
                }
                else if (Define.seqMode[i] == Define.MODE_INIT)
                {
                    m_PrcsModeBox[i].Text = "Init";
                }

                if (Define.seqCtrl[i] == Define.CTRL_IDLE)
                {
                    m_PrcsCtrlBox[i].Text = "Idle";
                }
                else if (Define.seqCtrl[i] == Define.CTRL_RUN)
                {
                    m_PrcsCtrlBox[i].Text = "Run";
                }
                else if (Define.seqCtrl[i] == Define.CTRL_RUNNING)
                {
                    m_PrcsCtrlBox[i].Text = "Runnig";
                }
                else if (Define.seqCtrl[i] == Define.CTRL_ALARM)
                {
                    m_PrcsCtrlBox[i].Text = "Alarm";
                }
                else if (Define.seqCtrl[i] == Define.CTRL_RETRY)
                {
                    m_PrcsCtrlBox[i].Text = "Retry";
                }
                else if (Define.seqCtrl[i] == Define.CTRL_HOLD)
                {
                    m_PrcsCtrlBox[i].Text = "Hold";
                }
                else if (Define.seqCtrl[i] == Define.CTRL_ABORT)
                {
                    m_PrcsCtrlBox[i].Text = "Abort";
                }                 

                if (Define.seqSts[i] == Define.STS_IDLE)
                {
                    m_PrcsStsBox[i].Text = "Idle";
                }
                else if (Define.seqSts[i] == Define.STS_PROCESS_ING)
                {
                    m_PrcsStsBox[i].Text = "ProcessIng";
                }
                else if (Define.seqSts[i] == Define.STS_PROCESS_END)
                {
                    m_PrcsStsBox[i].Text = "ProcessEnd";
                }
                else if (Define.seqSts[i] == Define.STS_INIT_ING)
                {
                    m_PrcsStsBox[i].Text = "InitIng";
                }
                else if (Define.seqSts[i] == Define.STS_INIT_END)
                {
                    m_PrcsStsBox[i].Text = "InitEnd";
                }
                else if (Define.seqSts[i] == Define.STS_ABORTOK)
                {
                    m_PrcsStsBox[i].Text = "AbortOK";
                }                
            }

            m_PrcsStepBox[(byte)MODULE._PM1].Text = (pM1Process.step.Layer).ToString();
            m_PrcsStepBox[(byte)MODULE._PM2].Text = (pM2Process.step.Layer).ToString();
            m_PrcsStepBox[(byte)MODULE._PM3].Text = (pM3Process.step.Layer).ToString();
            m_PrcsStepBox[(byte)MODULE._WATERTANK].Text = (waterTankFill.step.Layer).ToString();

            for (int i = 0; i < Define.MODULE_MAX - 1; ++i)
            {
                if (Define.seqCylinderMode[i] == Define.MODE_CYLINDER_IDLE)
                {
                    m_CylinderModeBox[i].Text = "Idle";
                }
                else if (Define.seqCylinderMode[i] == Define.MODE_CYLINDER_RUN)
                {
                    m_CylinderModeBox[i].Text = "Run";
                }
                else if (Define.seqCylinderMode[i] == Define.MODE_CYLINDER_HOME)
                {
                    m_CylinderModeBox[i].Text = "Home";
                }

                if (Define.seqCylinderCtrl[i] == Define.CTRL_IDLE)
                {
                    m_CylinderCtrlBox[i].Text = "Idle";
                }
                else if (Define.seqCylinderCtrl[i] == Define.CTRL_RUN)
                {
                    m_CylinderCtrlBox[i].Text = "Run";
                }
                else if (Define.seqCylinderCtrl[i] == Define.CTRL_RUNNING)
                {
                    m_CylinderCtrlBox[i].Text = "Runnig";
                }
                else if (Define.seqCylinderCtrl[i] == Define.CTRL_ALARM)
                {
                    m_CylinderCtrlBox[i].Text = "Alarm";
                }
                else if (Define.seqCylinderCtrl[i] == Define.CTRL_RETRY)
                {
                    m_CylinderCtrlBox[i].Text = "Retry";
                }
                else if (Define.seqCylinderCtrl[i] == Define.CTRL_HOLD)
                {
                    m_CylinderCtrlBox[i].Text = "Hold";
                }
                else if (Define.seqCylinderCtrl[i] == Define.CTRL_ABORT)
                {
                    m_CylinderCtrlBox[i].Text = "Abort";
                }

                if (Define.seqCylinderSts[i] == Define.STS_CYLINDER_IDLE)
                {
                    m_CylinderStsBox[i].Text = "Idle";
                }
                else if (Define.seqCylinderSts[i] == Define.STS_CYLINDER_RUNING)
                {
                    m_CylinderStsBox[i].Text = "RunIng";
                }
                else if (Define.seqCylinderSts[i] == Define.STS_CYLINDER_RUNEND)
                {
                    m_CylinderStsBox[i].Text = "RunEnd";
                }
                else if (Define.seqCylinderSts[i] == Define.STS_CYLINDER_HOMEING)
                {
                    m_CylinderStsBox[i].Text = "HomeIng";
                }
                else if (Define.seqCylinderSts[i] == Define.STS_CYLINDER_HOMEEND)
                {
                    m_CylinderStsBox[i].Text = "HomeEnd";
                }
                else if (Define.seqCylinderSts[i] == Define.STS_CYLINDER_ABORTOK)
                {
                    m_CylinderStsBox[i].Text = "AbortOK";
                }
            }

            m_CylinderStepBox[(byte)MODULE._PM1].Text = (pM1Cylinder.step.Layer).ToString();
            m_CylinderStepBox[(byte)MODULE._PM2].Text = (pM2Cylinder.step.Layer).ToString();
            m_CylinderStepBox[(byte)MODULE._PM3].Text = (pM3Cylinder.step.Layer).ToString();            
        }

        private void simulationTimer_Tick(object sender, EventArgs e)
        {
            if (Define.bSimulation)
            {
                DIOClass.diVal.checkLow[(int)DigInputList.EMO_Switch_i - 16] = "On";
                DIOClass.diVal.checkLow[(int)DigInputList.Water_Level_High_i - 16] = "On";
                DIOClass.diVal.checkLow[(int)DigInputList.Water_Level_Low_i - 16] = "On";

                DIOClass.diVal.checkLow[(int)DigInputList.Front_Door_Sensor_i - 16] = "On";
                DIOClass.diVal.checkLow[(int)DigInputList.Left_Door_Sensor_i - 16] = "On";
                DIOClass.diVal.checkLow[(int)DigInputList.Right_Door_Sensor_i - 16] = "On";
                DIOClass.diVal.checkLow[(int)DigInputList.Back_Door_Sensor_i - 16] = "On";

                // CH1
                if ((Global.digSet.curDigSet[(int)DigOutputList.CH1_Door_Open_o] == "On") && (Global.digSet.curDigSet[(int)DigOutputList.CH1_Door_Close_o] == "Off"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH1_Door_Op_i - 16] = "On";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH1_Door_Cl_i - 16] = "Off";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH1_Door_Open_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH1_Door_Close_o] == "On"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH1_Door_Op_i - 16] = "Off";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH1_Door_Cl_i - 16] = "On";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH1_Door_Open_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH1_Door_Close_o] == "Off"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH1_Door_Op_i - 16] = "Off";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH1_Door_Cl_i - 16] = "Off";
                }


                if ((Global.digSet.curDigSet[(int)DigOutputList.CH1_Cylinder_Fwd_o] == "On") && (Global.digSet.curDigSet[(int)DigOutputList.CH1_Cylinder_Bwd_o] == "Off"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Fwd_i] = "On";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Bwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Home_i] = "Off";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH1_Cylinder_Fwd_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH1_Cylinder_Bwd_o] == "On"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Fwd_i] = "Off";

                    if (Define.seqCylinderMode[(byte)MODULE._PM1] == Define.MODE_CYLINDER_HOME)                        
                    {
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Bwd_i] = "Off";
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Home_i] = "On";
                    }
                    else
                    {
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Bwd_i] = "On";
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Home_i] = "Off";
                    }
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH1_Cylinder_Fwd_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH1_Cylinder_Bwd_o] == "Off"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Fwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Bwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Cylinder_Home_i] = "Off";
                }

                DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Tool_Detect1_i] = "On";
                DIOClass.diVal.checkHigh[(int)DigInputList.CH1_Tool_Detect2_i] = "On";
                

                // CH2
                if ((Global.digSet.curDigSet[(int)DigOutputList.CH2_Door_Open_o] == "On") && (Global.digSet.curDigSet[(int)DigOutputList.CH2_Door_Close_o] == "Off"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH2_Door_Op_i - 16] = "On";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH2_Door_Cl_i - 16] = "Off";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH2_Door_Open_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH2_Door_Close_o] == "On"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH2_Door_Op_i - 16] = "Off";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH2_Door_Cl_i - 16] = "On";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH2_Door_Open_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH2_Door_Close_o] == "Off"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH2_Door_Op_i - 16] = "Off";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH2_Door_Cl_i - 16] = "Off";
                }


                if ((Global.digSet.curDigSet[(int)DigOutputList.CH2_Cylinder_Fwd_o] == "On") && (Global.digSet.curDigSet[(int)DigOutputList.CH2_Cylinder_Bwd_o] == "Off"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Fwd_i] = "On";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Bwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Home_i] = "Off";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH2_Cylinder_Fwd_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH2_Cylinder_Bwd_o] == "On"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Fwd_i] = "Off";

                    if (Define.seqCylinderMode[(byte)MODULE._PM2] == Define.MODE_CYLINDER_HOME)
                    {
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Bwd_i] = "Off";
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Home_i] = "On";
                    }
                    else
                    {
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Bwd_i] = "On";
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Home_i] = "Off";
                    }
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH2_Cylinder_Fwd_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH2_Cylinder_Bwd_o] == "Off"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Fwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Bwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Cylinder_Home_i] = "Off";
                }

                DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Tool_Detect1_i] = "On";
                DIOClass.diVal.checkHigh[(int)DigInputList.CH2_Tool_Detect2_i] = "On";

                
                // CH3
                if ((Global.digSet.curDigSet[(int)DigOutputList.CH3_Door_Open_o] == "On") && (Global.digSet.curDigSet[(int)DigOutputList.CH3_Door_Close_o] == "Off"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH3_Door_Op_i - 16] = "On";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH3_Door_Cl_i - 16] = "Off";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH3_Door_Open_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH3_Door_Close_o] == "On"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH3_Door_Op_i - 16] = "Off";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH3_Door_Cl_i - 16] = "On";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH3_Door_Open_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH3_Door_Close_o] == "Off"))
                {
                    DIOClass.diVal.checkLow[(int)DigInputList.CH3_Door_Op_i - 16] = "Off";
                    DIOClass.diVal.checkLow[(int)DigInputList.CH3_Door_Cl_i - 16] = "Off";
                }


                if ((Global.digSet.curDigSet[(int)DigOutputList.CH3_Cylinder_Fwd_o] == "On") && (Global.digSet.curDigSet[(int)DigOutputList.CH3_Cylinder_Bwd_o] == "Off"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Fwd_i] = "On";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Bwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Home_i] = "Off";
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH3_Cylinder_Fwd_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH3_Cylinder_Bwd_o] == "On"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Fwd_i] = "Off";

                    if (Define.seqCylinderMode[(byte)MODULE._PM3] == Define.MODE_CYLINDER_HOME)                        
                    {
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Bwd_i] = "Off";
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Home_i] = "On";
                    }
                    else
                    {
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Bwd_i] = "On";
                        DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Home_i] = "Off";
                    }
                }

                if ((Global.digSet.curDigSet[(int)DigOutputList.CH3_Cylinder_Fwd_o] == "Off") && (Global.digSet.curDigSet[(int)DigOutputList.CH3_Cylinder_Bwd_o] == "Off"))
                {
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Fwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Bwd_i] = "Off";
                    DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Cylinder_Home_i] = "Off";
                }

                DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Tool_Detect1_i] = "On";
                DIOClass.diVal.checkHigh[(int)DigInputList.CH3_Tool_Detect2_i] = "On";
                
            }
        }
    }
}
