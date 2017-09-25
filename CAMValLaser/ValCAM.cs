using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using ValToXml;
using DataProcessTools;


//test position
namespace CAMValLaser
{
    public partial class ValCAM : Form
    {
        //add git test code
        ValPro Vc = new ValPro();
        public ValCAM()
        {
            InitializeComponent();
        }

        private void init()
        {
            txB_LinePrecison.Text = "0.1";
            txBCirclePrecision.Text = "0.1";
            tbFrameIndex.Text = "5";
            tbToolIndex.Text = "1";
            labFrame.Text = "Frame " + tbFrameIndex.Text + ":";
            labTool.Text = "Tool " + tbToolIndex.Text + ":";
            tbFrame.Text = "";
            tbTool.Text = "";
        }

        private void ValCAM_Load(object sender, EventArgs e)
        {
            init();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            string[] strArray = null;
            init();
            string _sPath = PtReduce(ref strArray);

            createValData(_sPath,strArray);
            
        }

        

        private string PtReduce(ref string[] strArray)
        {
            bool b_lRe = false;
            bool l_bResult = false;
            bool bls=false;
            string sPath = "";
            ProcessData l_ProcessData = new ProcessData();
            string l_sPath = string.Empty;
            FileTools.fileRead fr = new FileTools.fileRead();

            OpenFileDialog ofd = new OpenFileDialog();
            double l_nlinePrecision = new double();
            double l_nCirclePrecision = new double();

            if (txB_LinePrecison.Text != "" & txBCirclePrecision.Text != "")
            {
                l_nlinePrecision = double.Parse(txB_LinePrecison.Text);
                l_nCirclePrecision = double.Parse(txBCirclePrecision.Text);

                //获取点    
                if (ofd.ShowDialog() == DialogResult.OK) { l_sPath = ofd.FileName; }
                if (l_sPath != string.Empty)
                {
                    //读取文件
                    string[] l_s = fr.ReadTxt(l_sPath);
                    List<string> _l_s = new List<string>();
                    
                    string[] l_sResult = new string[l_s.Length];
                    for (int i = 0; i < l_s.Length;i++ )
                    {
                        if (l_s[i].Contains("DELAYON"))
                        {

                        }
                        else if (l_s[i] == "TOOLON") 
                        {
                            _l_s.Add(l_s[i]);
                            b_lRe = true;
                        }
                        else if (l_s[i] == "TOOLOFF" && !b_lRe)
                        {
                            //_l_s.Add(l_s[i].Replace(l_s[i], ""));
                        }
                        else if (l_s[i] == "TOOLOFF" && b_lRe)
                        {
                            _l_s.Add(l_s[i]);
                            b_lRe = false;
                        }
                        else 
                        {
                            _l_s.Add(l_s[i]);
                        }
                    }
                    l_sResult = _l_s.ToArray();

                    foreach (string str in l_sResult)
                    {
                        if (str.Contains("TOOLON"))
                        {
                            bls = true;
                            break;
                        }
                        else
                        {
                            bls = false;
                            continue;
                        }
                    }

                    if (bls)
                    {
                        //GlobalData.sOldStringLength = l_s.Length;
                        int l_nRemainLength = new int();
                        int l_nOldLength = new int();
                        bool l_bok = l_ProcessData.PtReduce(l_sResult, out GlobalData.sOutString, l_nlinePrecision, l_nCirclePrecision, "TOOLON", "TOOLOFF", ref l_nOldLength, ref l_nRemainLength);

                        FileTools.fileWrite fw = new FileTools.fileWrite();
                        sPath = Application.StartupPath + @"\Val_Temp.txt";
                        l_bResult = fw.WriteTxt(GlobalData.sOutString, sPath);

                        if (l_bResult == false)
                        {
                            MessageBox.Show(GlobalData.sError[1]);
                        }

                        label_OldPoint.Text = Convert.ToString(l_nOldLength);
                        label_NewPoint.Text = Convert.ToString(l_nRemainLength);
                        label_ReducePoint.Text = Convert.ToString(l_nOldLength - l_nRemainLength);
                        // .Text = Convert.ToString(l_s.Length);
                        string l_filename = Path.GetFileName(l_sPath);
                        string l_sHistory = DateTime.Now.ToString() + ":" + l_filename + " 直线精度：" + txB_LinePrecison.Text + "圆弧精度：" + txBCirclePrecision.Text + "/" + label_OldPoint.Text + "/" + label_NewPoint.Text + "/" + label_ReducePoint.Text;
                        listBox_OperationInfor.Items.Add(l_sHistory);
                        GlobalData.ListHistory.Add(l_sHistory);

                        strArray = GlobalData.sOutString;
                    }
                    else
                    {
                        FileTools.fileWrite fw = new FileTools.fileWrite();
                        sPath = Application.StartupPath + @"\Val_Temp.txt";
                        l_bResult = fw.WriteTxt(l_sResult, sPath);

                        strArray = l_sResult;
                    }
                }
            }
            else
            {
                MessageBox.Show(GlobalData.sError[0]);
            }

            //strArray = GlobalData.sOutString;
            return sPath;
        }


        private void createValData(string sPaths,string[] strArr)
        {
            string frame = (int.Parse(tbFrameIndex.Text)-1).ToString();
            string tool = tbToolIndex.Text;

            labFrame.Text = "Frame " + tbFrameIndex.Text + ":";
            labTool.Text = "Tool " + tbToolIndex.Text + ":";


            bool bl = Vc.createValXmlFiles(strArr, ref frame, ref tool);
            
            if (bl == true)
            {
                tbFrame.Text = frame;
                tbTool.Text = tool;

                MessageBox.Show("数据转换成功！");
            }
            else
            {
                MessageBox.Show("数据转换失败！");
            }
            File.Delete(sPaths);
        }

        




    }
}
