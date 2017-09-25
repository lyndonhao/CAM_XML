using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using DataType;

namespace DataProcessTools
{
    public  class ParseData
    {
        /// <summary>
        /// 查找在x_ListInt是否有值等于x_nInputInt
        /// </summary>
        /// <param name="x_nInputInt"></param>
        /// <param name="x_ListInt"></param>
        /// <returns></returns>
        public bool FindInt(int x_nInputInt, List<int> x_ListInt)
        {
            bool l_bResult = false;
            for (int i = 0; i <= x_ListInt.Count-1; i++)
            {
                if (x_nInputInt == x_ListInt[i])
                {
                    l_bResult = true;
                    break;
                }
            }
            return l_bResult;
        }
        /// <summary>
        /// 查找标识符在单个字符串中起始位置后首次出现的位置 test success
        /// </summary>
        /// <param name="x_sIdentification"></param>
        /// <param name="x_sTargetString"></param>
        /// <param name="x_nPosition"></param>字符串所在位置
        /// <returns>true:包含字符串
        /// </returns>false：不包含字符串
        public  bool FindString(string x_sIdentification,string x_sTargetString,ref List<int> x_nPosition,int x_nStartIndex)
        {
            bool l_bResult = false;
            int i = x_sTargetString.IndexOf(x_sIdentification,x_nStartIndex);
            if (i < 0) { l_bResult = false; } else { l_bResult = true; x_nPosition.Add(i); }
            return l_bResult;
        }
        /// <summary>
        /// 查找标识符在字符串数组中的所有位置。
        /// </summary>
        /// <param name="x_sIdentification"></param>
        /// <param name="x_sTargetString"></param>
        /// <param name="x_nPosition"></param>
        /// <param name="x_nStartIndex"></param>
        /// <returns></returns>
        public  bool FindString(string x_sIdentification, string[] x_sTargetString, ref List<int> x_nIndex)
        {
            bool l_bResult = false;
            //int l_nPosition = new int();
            List<int> l_nPosition=new List<int>();
            List<int> l_ListIndex = new List<int>();
            for (int i=0;i<=x_sTargetString.Length-1;i++)
            {
                l_bResult = FindString(x_sIdentification, x_sTargetString[i], ref l_nPosition, 0);
                if (l_bResult == true) { l_ListIndex.Add(i); }
            }
            if (l_ListIndex.Count == 0) { l_bResult = false; } else { x_nIndex=l_ListIndex; l_bResult = true; }
            return l_bResult;   
        }
        /// <summary>
        /// 查找标识符在字符串中的所有位置 test success
        /// </summary>
        /// <param name="x_sIdentification"></param>
        /// <param name="x_sTargetString"></param>
        /// <param name="x_nPosition"></param>
        /// <returns></returns>
        public  bool FindString(string x_sIdentification, string x_sTargetString, ref List<int> x_nPosition)
        {
            bool l_bResult = true;
            int i = x_sTargetString.IndexOf(x_sIdentification, 0);
            if (i < 0)
            {
                l_bResult = false;
                return l_bResult;
            }
            else
            {
                int j = 0;
                //int k = new int();
                x_nPosition.Add(i);
                do
                {
                   // k = 0;
                    l_bResult = FindString(x_sIdentification, x_sTargetString, ref x_nPosition, x_nPosition[j]+1);
                    if (l_bResult ==true) {j=j+1;}
                }
                while (l_bResult == true);
                l_bResult = true;
                return l_bResult;
            }
        }
        /// <summary>
        /// 字符串数组转换为点类型 test success
        /// </summary>
        /// <param name="x_sPoint"></param>
        /// <param name="x_pPoint"></param>
        /// <returns></returns>
        public  bool String2Point(string[] x_sPoint,ref DataType.StaubliRobotData.St_PointRx x_pPoint)
        {
            bool l_bOk = false;
            if (x_sPoint.Length != 6)
            {
                l_bOk = false;
            }
            else
            {
                x_pPoint.x= double.Parse(x_sPoint[0]);
                x_pPoint.y = double.Parse(x_sPoint[1]);
                x_pPoint.z = double.Parse(x_sPoint[2]);
                x_pPoint.Rx = double.Parse(x_sPoint[3]);
                x_pPoint.Ry = double.Parse(x_sPoint[4]);
                x_pPoint.Rz = double.Parse(x_sPoint[5]);
                l_bOk = true;
            }
            return l_bOk;
        }

        public bool String2Joint(string[] x_sJoint, ref DataType.StaubliRobotData.St_JointRx x_jJoint)
        {
            bool l_bOk = false;
            if (x_sJoint.Length != 6)
            {
                l_bOk = false;
            }
            else
            {
                x_jJoint.J1 = double.Parse(x_sJoint[0]);
                x_jJoint.J2 = double.Parse(x_sJoint[1]);
                x_jJoint.J3 = double.Parse(x_sJoint[2]);
                x_jJoint.J4 = double.Parse(x_sJoint[3]);
                x_jJoint.J5 = double.Parse(x_sJoint[4]);
                x_jJoint.J6 = double.Parse(x_sJoint[5]);
                l_bOk = true;
            }
            return l_bOk;
        }
        public bool Joint2Double(ref double[] x_nNumber, DataType.StaubliRobotData.St_JointRx x_jJoint)
        {
            bool l_bOk = false;
            if (x_nNumber.Length != 6)
            {
                l_bOk = false;
            }
            else
            {
                x_nNumber[0] = x_jJoint.J1;
                x_nNumber[1] = x_jJoint.J2;
                x_nNumber[2] = x_jJoint.J3;
                x_nNumber[3] = x_jJoint.J4;
                x_nNumber[4] = x_jJoint.J5;
                x_nNumber[5] = x_jJoint.J6;
                l_bOk = true;
            }
            return l_bOk;
        }
        public bool Point2Double(ref double[] x_nNumber, DataType.StaubliRobotData.St_PointRx x_pPoint)
        {
            bool l_bOk = false;
            if (x_nNumber.Length != 6)
            {
                l_bOk = false;
            }
            else
            {
                x_nNumber[0] = x_pPoint.x;
                x_nNumber[1] = x_pPoint.y;
                x_nNumber[2] = x_pPoint.z;
                x_nNumber[3]=x_pPoint.Rx;
                x_nNumber[4] = x_pPoint.Ry;
                x_nNumber[5] = x_pPoint.Rz;
                l_bOk = true;
            }
            return l_bOk;
        }

        public string Double2String(double[] x_nNumber,string x_sSeparator)
        {
            string l_sString = string.Empty;
            for (int i = 0; i <= x_nNumber.Length-1; i++)
            {
                if (i != x_nNumber.Length - 1)
                {
                    l_sString = l_sString + x_nNumber[i].ToString("f3") + x_sSeparator;
                }
                else
                {
                    l_sString = l_sString + x_nNumber[i].ToString("f3");
                }
                
            }
            return l_sString;
        }
        public string Joint2String(DataType.StaubliRobotData.St_JointRx x_jJoint, string x_sSeparator)
        {
            string l_sString = string.Empty;
            double[] l_nNum = new double[6];
            bool l_bResult = Joint2Double(ref l_nNum, x_jJoint);
            if (l_bResult == true)
            {
                l_sString = Double2String(l_nNum, x_sSeparator);
            }
            return l_sString;
        }
        public string Point2String(DataType.StaubliRobotData.St_PointRx x_pPoint, string x_sSeparator)
        {
            string l_sString = string.Empty;
            double[] l_nNum = new double[6];
            bool l_bResult = Point2Double(ref l_nNum, x_pPoint);
            if (l_bResult == true)
            {
                l_sString = Double2String(l_nNum, x_sSeparator);
            }
            return l_sString;

        }
        public bool getJoint(string x_sIdentifier1, char x_sIdentifier2, string x_sTargetString, ref DataType.StaubliRobotData.St_JointRx x_jJoint)
        {
            bool l_bOk = false;
            List<int> l_nPosition = new List<int>();
            l_bOk = FindString(x_sIdentifier1, x_sTargetString, ref l_nPosition);
            if (l_bOk == true)
            {
                try
                {
                    string l_sString = x_sTargetString.Substring(l_nPosition[0] + 1, l_nPosition[1] - l_nPosition[0] - 1);
                    string[] l_sJoint = l_sString.Split(x_sIdentifier2);
                    l_bOk = String2Joint(l_sJoint, ref x_jJoint);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error:getPoint exception: " + ex.Message);
                }
            }
            return l_bOk;
        }

        public bool getJoint(string x_sIdentifier1, char x_sIdentifier2, string[] x_sTargetString, int x_nStartIndex, int x_nEndIndex, out List<DataType.StaubliRobotData.St_JointRx> x_ListJoint)
        {
            bool l_bResult = false;
            x_ListJoint = null;
            List<DataType.StaubliRobotData.St_JointRx> l_arrayList = new List<DataType.StaubliRobotData.St_JointRx>();
            DataType.StaubliRobotData.St_JointRx l_jJoint = new DataType.StaubliRobotData.St_JointRx();
            for (int i = x_nStartIndex + 1; i <= x_nEndIndex - 1; i++)
            {
                l_bResult = getJoint(x_sIdentifier1, x_sIdentifier2, x_sTargetString[i], ref l_jJoint);
                if (l_bResult == true) { l_arrayList.Add(l_jJoint); }
            }
            if (l_arrayList.Count == 0) { l_bResult = false; } else { x_ListJoint = l_arrayList; l_bResult = true; }
            return l_bResult;
        }

        public bool getJoint(string x_sIdentifier1, char x_sIdentifier2, string[] x_sTargetString, string x_sStartString, string x_sEndString, out List<DataType.StaubliRobotData.St_JointRx> x_ListJoint)
        {
            x_ListJoint = null;
            //ParseData ParseTool = new ParseData();
            //List<DataType.StaubliRobotData.St_PointRx> l_ListPoint = new List<DataType.StaubliRobotData.St_PointRx>();
            List<int> l_ListIndex1 = new List<int>();
            List<int> l_ListIndex2 = new List<int>();
            bool l_bResult = false;
            bool l_bResult1 = FindString(x_sStartString, x_sTargetString, ref l_ListIndex1);
            bool l_bResult2 = FindString(x_sEndString, x_sTargetString, ref l_ListIndex2);
            if (l_bResult1 == true & l_bResult2 == true)
            {
                int l_nStartIndex = l_ListIndex1[0];
                int l_nEndIndex = l_ListIndex2[0];
                l_bResult = getJoint(x_sIdentifier1, x_sIdentifier2, x_sTargetString, l_nStartIndex, l_nEndIndex, out x_ListJoint);
            }
            return l_bResult;
        }
        /// <summary>
        /// 获取点信息 test success
        /// </summary>
        /// <param name="x_sIdentifier1"></param>
        /// <param name="x_sIdentifier2"></param>
        /// <param name="x_sTargetString"></param>
        /// <param name="x_pPoint"></param>
        /// <returns></returns>
        public  bool getPoint(string x_sIdentifier1,char x_sIdentifier2,string x_sTargetString,ref DataType.StaubliRobotData.St_PointRx x_pPoint)
        {       
            bool l_bOk = false;
            List<int> l_nPosition=new List<int>();
            l_bOk=FindString(x_sIdentifier1,x_sTargetString,ref l_nPosition);
            if (l_bOk == true)
            {
                try
                {
                    string l_sString = x_sTargetString.Substring(l_nPosition[1] + 1, l_nPosition[2] - l_nPosition[1]-1);
                    string[] l_sPoint = l_sString.Split(x_sIdentifier2);
                    l_bOk = String2Point(l_sPoint,ref x_pPoint);

                }
                catch (Exception ex)
                {
                    throw new Exception("Error:getPoint exception: " + ex.Message);
                }
            }
            return l_bOk;
        }
        /// <summary>
        /// 给定起始和结束数组索引,获取字符串数组中从开始索引到结束索引下的所有点。test success
        /// </summary>
        /// <param name="x_sIdentifier1"></param>
        /// <param name="x_sIdentifier2"></param>
        /// <param name="x_sTargetString"></param>
        /// <param name="x_nStartIndex"></param>
        /// <param name="x_nEndIndex"></param>
        /// <param name="x_ListPoint"></param>
        /// <returns></returns>
        public  bool getPoint(string x_sIdentifier1,char x_sIdentifier2,string[] x_sTargetString,int x_nStartIndex,int x_nEndIndex,out List<DataType.StaubliRobotData.St_PointRx> x_ListPoint)
        {
            bool l_bResult = false;
            x_ListPoint=null;
            List<DataType.StaubliRobotData.St_PointRx> l_arrayList = new List<DataType.StaubliRobotData.St_PointRx>();
            DataType.StaubliRobotData.St_PointRx l_pPoint=new DataType.StaubliRobotData.St_PointRx();
            for (int i = x_nStartIndex + 1; i <= x_nEndIndex - 1; i++)
            {
                l_bResult = getPoint(x_sIdentifier1, x_sIdentifier2, x_sTargetString[i],ref l_pPoint);
                if (l_bResult == true) { l_arrayList.Add(l_pPoint); }
            }
            if (l_arrayList.Count == 0) { l_bResult = false; } else { x_ListPoint = l_arrayList; l_bResult = true; }
            return l_bResult;
        }
        /// <summary>
        /// 获取从开始字符串到结束字符串下所有的点。
        /// </summary>
        /// <param name="x_sIdentifier1"></param>标识符
        /// <param name="x_sIdentifier2"></param>
        /// <param name="x_sTargetString"></param>目标字符串
        /// <param name="x_sStartString"></param>开始字符串
        /// <param name="x_sEndString"></param>结束字符串
        /// <param name="x_ListPoint"></param>
        /// <returns></returns>
        public bool getPoint(string x_sIdentifier1, char x_sIdentifier2, string[] x_sTargetString, string x_sStartString, string x_sEndString,int x_nStartIndex, out List<DataType.StaubliRobotData.St_PointRx> x_ListPoint)
        {
            x_ListPoint = null;
            //ParseData ParseTool = new ParseData();
            //List<DataType.StaubliRobotData.St_PointRx> l_ListPoint = new List<DataType.StaubliRobotData.St_PointRx>();
            List<int> l_ListIndex1 = new List<int>();
            List<int> l_ListIndex2 = new List<int>();
            bool l_bResult = false;
            bool l_bResult1 = FindString(x_sStartString, x_sTargetString, ref l_ListIndex1);
            bool l_bResult2 = FindString(x_sEndString, x_sTargetString, ref l_ListIndex2);
            if (l_bResult1 == true & l_bResult2 == true)
            {
                int l_nStartIndex = l_ListIndex1[x_nStartIndex];
                int l_nEndIndex = l_ListIndex2[x_nStartIndex];
                l_bResult = getPoint(x_sIdentifier1, x_sIdentifier2, x_sTargetString, l_nStartIndex, l_nEndIndex, out x_ListPoint);      
            }

            return l_bResult;
        }

        public string[] StringEmpty(string[] x_sString,int x_nStartIndex,int x_nEndIndex)
        {
            string[] l_sString = new string[x_sString.Length];
            for (int i = 0; i < x_sString.Length; i++){ l_sString[i] = x_sString[i]; }  
            for (int i = x_nStartIndex; i <= x_nEndIndex ; i++)
            {
                l_sString[i] = string.Empty;
            }  
            return l_sString;
        }
        /// <summary>
        /// 转换为离线文件标准字符串形式
        /// </summary>
        /// <returns></returns>
        public string Trans2Standard(DataType.StaubliRobotData.St_JointRx x_jJoint,DataType.StaubliRobotData.St_PointRx x_pPoint,string x_sPropertySeparator,string x_sPointSeparator,string x_sMoveType)
        {
            string l_sString = string.Empty;
            string l_sJoint = Joint2String(x_jJoint, x_sPointSeparator);
            string l_sPoint = Point2String(x_pPoint, x_sPointSeparator);
            l_sString = x_sMoveType + x_sPropertySeparator + l_sJoint + x_sPropertySeparator + l_sPoint + x_sPropertySeparator;
            return l_sString;
        }

        public string[] Trans2Standard(List<DataType.StaubliRobotData.St_JointRx> x_jJoint, List<DataType.StaubliRobotData.St_PointRx> x_pPoint, string x_sPropertySeparator, string x_sPointSeparator, string[] x_sString, int x_nStartIndex, int x_nEndIndex, string[] x_sMoveType)
        {
            string[] l_sString = EvaluateString(x_sString);
            l_sString = StringEmpty(x_sString, x_nStartIndex, x_nEndIndex);
            int j = 0;
            for (int i = x_nStartIndex ; i <= x_jJoint.Count+x_nStartIndex-1; i++)
            {
                l_sString[i] = Trans2Standard(x_jJoint[j], x_pPoint[j], x_sPropertySeparator, x_sPointSeparator,x_sMoveType[j]);
                j = j + 1;
            }
            return l_sString;
        }
        public string[] Trans2Standard(List<DataType.StaubliRobotData.St_JointRx> x_jJoint, List<DataType.StaubliRobotData.St_PointRx> x_pPoint, string x_sPropertySeparator, string x_sPointSeparator, string[] x_sString, int x_nStartIndex, int x_nEndIndex, List<string> x_ListMoveType)
        {
            string[] l_sMoveType = x_ListMoveType.ToArray();
            string[] l_sString = Trans2Standard(x_jJoint, x_pPoint, x_sPropertySeparator, x_sPointSeparator, x_sString, x_nStartIndex, x_nEndIndex, l_sMoveType);  
            return l_sString;
        }
        //public List<string[]> Trans2Standard(List<DataType.StaubliRobotData.St_JointRx> x_jJoint, List<DataType.StaubliRobotData.St_PointRx> x_pPoint, string x_sPropertySeparator, string x_sPointSeparator, List<string[]> x_sListTargetString, int x_nStartIndex, int x_nEndIndex, string[] x_sMoveType)
        //{
        //    List<string[]> l_ListString = new List<string[]>();
        //    string[] l_sTargetString=x_sListTargetString[0];
        //    string[] l_sOutString = Trans2Standard(x_jJoint, x_pPoint, x_sPropertySeparator, x_sPointSeparator,l_sTargetString,x_nStartIndex,x_nEndIndex,x_sMoveType);
        //    l_ListString.Add(l_sOutString);
        //    return l_ListString;
        //}
        //public List<string[]> Trans2Standard(List<DataType.StaubliRobotData.St_JointRx> x_jJoint, List<DataType.StaubliRobotData.St_PointRx> x_pPoint, string x_sPropertySeparator, string x_sPointSeparator, List<string[]> x_sListTargetString, int x_nStartIndex, int x_nEndIndex, List<string> x_ListMoveType)
        //{
        //    List<string[]> l_ListString = new List<string[]>();
        //    string[] l_sTargetString = x_sListTargetString[0];
        //    string[] l_sMoveType = x_ListMoveType.ToArray();
        //    string[] l_sOutString = Trans2Standard(x_jJoint, x_pPoint, x_sPropertySeparator, x_sPointSeparator, l_sTargetString, x_nStartIndex, x_nEndIndex, l_sMoveType);
        //    l_ListString.Add(l_sOutString);
        //    return l_ListString;
        //}
        public string[] EvaluateString(string[] x_sString)
        {
            string[] l_sString = new string[x_sString.Length];
            for (int i = 0; i < x_sString.Length; i++) { l_sString[i] = x_sString[i]; }
            return l_sString;
        }
    }
}
