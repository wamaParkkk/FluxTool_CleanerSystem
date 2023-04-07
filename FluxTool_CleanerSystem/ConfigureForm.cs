using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FluxTool_CleanerSystem
{
    public partial class ConfigureForm : Form
    {
        AnalogDlg AnaDlg;

        public ConfigureForm()
        {            
            InitializeComponent();
        }

        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            Width = 1172;
            Height = 824;
            Top = 0;
            Left = 0;

            PARAMETER_LOAD();
            TEMP_PARAMETER_LOAD();
        }

        private void ConfigureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }

        private void PARAMETER_LOAD()
        {
            string sTmpData;
            string FileName = "Configure.txt";

            if (File.Exists(Global.ConfigurePath + FileName))
            {
                byte[] bytes;
                using (var fs = File.Open(Global.ConfigurePath + FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    sTmpData = Encoding.Default.GetString(bytes);

                    char sp = ',';
                    string[] spString = sTmpData.Split(sp);
                    for (int i = 0; i < spString.Length; i++)
                    {
                        Configure_List.Door_OpCl_Timeout = int.Parse(spString[0]);
                        Configure_List.Cylinder_FwdBwd_Timeout = int.Parse(spString[1]);                        
                        Configure_List.Water_Heating_Timeout = int.Parse(spString[2]);
                        Configure_List.Water_Fill_Timeout = int.Parse(spString[3]);

                        txtBoxDoorOpenCloseTimeout.Text = (Configure_List.Door_OpCl_Timeout).ToString();
                        txtBoxCylinderFwdBwdTimeout.Text = (Configure_List.Cylinder_FwdBwd_Timeout).ToString();                        
                        txtBoxWaterHeatingTimeout.Text = (Configure_List.Water_Heating_Timeout).ToString();
                        txtBoxWaterFillTimeout.Text = (Configure_List.Water_Fill_Timeout).ToString();
                    }
                }
            }
        }

        private void TEMP_PARAMETER_LOAD()
        {
            string sTmpData;
            string FileName = "HeatingTemp.txt";

            if (File.Exists(Global.ConfigurePath + FileName))
            {
                byte[] bytes;
                using (var fs = File.Open(Global.ConfigurePath + FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    sTmpData = Encoding.Default.GetString(bytes);

                    char sp = ',';
                    string[] spString = sTmpData.Split(sp);
                    for (int i = 0; i < spString.Length; i++)
                    {
                        Configure_List.Water_Heating_Temp = double.Parse(spString[0]);

                        txtBoxHeatingTemp.Text = (Configure_List.Water_Heating_Temp).ToString();                        
                    }
                }
            }
        }

        private void txtBoxDoorOpenCloseTimeout_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            AnaDlg = new AnalogDlg();
            AnaDlg.Init();
            if (AnaDlg.ShowDialog() == DialogResult.OK)
            {
                textBox.Text = AnaDlg.m_strResult;

                string[] sVal = new string[1];
                string sTemp = textBox.Text.ToString().Trim();
                sVal[0] = sTemp;
                if (!Global.Value_Check(sVal))
                {
                    MessageBox.Show("잘못 된 값이 입력되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Text = "0";
                }
            }
        }

        private void btnParameterSave_Click(object sender, EventArgs e)
        {
            string sDoorOpClTimeout = txtBoxDoorOpenCloseTimeout.Text.ToString().Trim();
            string sCylinderFwdBwdTimeout = txtBoxCylinderFwdBwdTimeout.Text.ToString().Trim();            
            string sWaterHeatingTimeout = txtBoxWaterHeatingTimeout.Text.ToString().Trim();
            string sWaterFillTimeout = txtBoxWaterFillTimeout.Text.ToString().Trim();

            if (Parameter_WriteFile(sDoorOpClTimeout, sCylinderFwdBwdTimeout, sWaterHeatingTimeout, sWaterFillTimeout))
            {
                Configure_List.Door_OpCl_Timeout = int.Parse(sDoorOpClTimeout);
                Configure_List.Cylinder_FwdBwd_Timeout = int.Parse(sCylinderFwdBwdTimeout);                
                Configure_List.Water_Heating_Timeout = int.Parse(sWaterHeatingTimeout);
                Configure_List.Water_Fill_Timeout = int.Parse(sWaterFillTimeout);

                MessageBox.Show("Configure 값이 저장 되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Configure 값이 저장 되지 않았습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool Parameter_WriteFile(string param1, string param2, string param3, string param4)
        {
            string FileName = "Configure.txt";

            try
            {                
                File.WriteAllText(Global.ConfigurePath + FileName, param1 + "," + param2 + "," + param3 + "," + param4, Encoding.Default);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림");
                return false;
            }
        }

        private void btnHeatingTempSave_Click(object sender, EventArgs e)
        {
            string sHeatingTemp = txtBoxHeatingTemp.Text.ToString().Trim();            

            if (Temp_Parameter_WriteFile(sHeatingTemp))
            {
                Configure_List.Water_Heating_Temp = double.Parse(sHeatingTemp);                

                MessageBox.Show("Configure 값이 저장 되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Configure 값이 저장 되지 않았습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool Temp_Parameter_WriteFile(string param1)
        {
            string FileName = "HeatingTemp.txt";

            try
            {
                File.WriteAllText(Global.ConfigurePath + FileName, param1, Encoding.Default);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림");
                return false;
            }
        }
    }
}
