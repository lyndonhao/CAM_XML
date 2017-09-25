using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
//using FileTools;
//using DataTypes;


namespace CAMToVal
{
    public class CAMPro
    {
        #region  实例化

        DataType.CAMDataType.stProcessPara ProcessPara = new DataType.CAMDataType.stProcessPara();
        DataType.CAMDataType.stTrajPara TrajPara = new DataType.CAMDataType.stTrajPara();
        DataType.CAMDataType.stTrajStatus TrajStatus = new DataType.CAMDataType.stTrajStatus();
        DataType.CAMDataType.fTrack Track = new DataType.CAMDataType.fTrack();
        DataType.CAMDataType.nom_Speed Speed = new DataType.CAMDataType.nom_Speed();
        DataType.CAMDataType.nFrameIndex FrameIndex = new DataType.CAMDataType.nFrameIndex();
        DataType.CAMDataType.nIndexGun IndexGun = new DataType.CAMDataType.nIndexGun();
        DataType.CAMDataType.nLaserDelay LaserDelay = new DataType.CAMDataType.nLaserDelay();
        DataType.CAMDataType.nPenetrate Penetrate = new DataType.CAMDataType.nPenetrate();
        DataType.CAMDataType.nDistance Distance = new DataType.CAMDataType.nDistance();
        DataType.CAMDataType.pCutting Cutting = new DataType.CAMDataType.pCutting();

       

        public List<DataType.CAMDataType.stProcessPara> ProcessParaLs = new List<DataType.CAMDataType.stProcessPara>();
        public List<DataType.CAMDataType.stTrajPara> TrajParaLs = new List<DataType.CAMDataType.stTrajPara>();
        public List<DataType.CAMDataType.stTrajStatus> TrajStatusLs = new List<DataType.CAMDataType.stTrajStatus>();
        public List<DataType.CAMDataType.fTrack> TrackLs = new List<DataType.CAMDataType.fTrack>();
        public List<DataType.CAMDataType.nom_Speed> SpeedLs = new List<DataType.CAMDataType.nom_Speed>();
        public List<DataType.CAMDataType.nFrameIndex> FrameIndexLs = new List<DataType.CAMDataType.nFrameIndex>();
        public List<DataType.CAMDataType.nIndexGun> IndexGunLs = new List<DataType.CAMDataType.nIndexGun>();
        public List<DataType.CAMDataType.nLaserDelay> LaserDelayLs = new List<DataType.CAMDataType.nLaserDelay>();
        public List<DataType.CAMDataType.nPenetrate> PenetrateLs = new List<DataType.CAMDataType.nPenetrate>();
        public List<DataType.CAMDataType.nDistance> DistanceLs = new List<DataType.CAMDataType.nDistance>();
        public List<DataType.CAMDataType.pCutting> CuttingLs = new List<DataType.CAMDataType.pCutting>();

        //public DataType.CAMDataType.GraphType graphType = new DataType.CAMDataType.GraphType();

        #endregion

        #region  变量定义
        public string _frameIndexOut;   //frame索引号
        public string _toolIndexOut;   //tool索引号

        public int nGraphCounts;  //图形数量
        public string _Tool;
        public string _Frame;

        string strCir = "Circle";
        string strCircle = "Circle(";
        string strObl = "Oblong";
        string strOblong = "Oblong(";
        string strRect = "Rect";
        string strRects = "Rect(";
        string strPol = "Polygon";
        string strPolygon = "Polygon(";

        string strMovec = "MOVEC/";
        string strMovej = "MOVEJ/";
        string strMovejs = "MOVEJ";
        string strMovejj = "MOVEJj/";
        string strMovejjs = "MOVEJj";
        string strFrame = "FRAME/";
        string strTool = "TOOL/";

        string strToolon = "TOOLON";
        string strTooloff = "TOOLOFF";

        string strMoveC = "moveC";
        string strMoveJ = "moveJ";
        string strMoveL = "moveL";
        #endregion

        #region 结构体初始化
        private void InitStructList()
        {
            ProcessParaLs.Clear();
            TrajParaLs.Clear();
            TrajStatusLs.Clear();
            SpeedLs.Clear();
            LaserDelayLs.Clear();
            PenetrateLs.Clear();
            DistanceLs.Clear();
            FrameIndexLs.Clear();
            IndexGunLs.Clear();
            TrackLs.Clear();
            CuttingLs.Clear();

            nGraphCounts = 0;

            _Tool = "";
            _Frame = "";
        }
        #endregion

        #region   cam数据处理

        //提取数据信息
        public bool CamDataProcess(string[] strFileArr)
        {
            bool bResult = true;
            List<int> _toolOnCount=new List<int>();
            List<int> _toolOffCount=new List<int>();

            //数据预处理，去除无用信息
            List<string> strLs = CamArrayProcess(strFileArr, out _toolOnCount, out _toolOffCount);

            InitStructList();
            

            //流程
            for (int i = 0; i < strLs.Count;i++ )
            {
                if (strLs[i].Contains(strCir))       //circle
                {
                    CircleProcess(strLs[i], strLs[i + 1], strLs[i + 2]);
                    nGraphCounts++;
                    i = i + 2;  //跳过下两点
                }
                else if (strLs[i].Contains(strObl) | strLs[i].Contains(strRect) | strLs[i].Contains(strPol)) //polygon
                {
                    PolygonProcess(strLs[i], strLs[i + 1], strLs[i + 2], strLs[i + 3]);
                    nGraphCounts++;
                    i = i + 3;  //跳过下三点
                }
                else if (strLs[i].Contains(strMovec)) //movec
                {
                    ToolProcess(_toolOnCount, _toolOffCount, i+1);

                    CircularArc(strLs[i], strLs[i + 1]);
                    nGraphCounts++;
                    i = i + 1;  //跳过下一点
                }
                else if (strLs[i].Contains(strMovej) | strLs[i].Contains(strMovejj))  //movej、movel
                {
                    ToolProcess(_toolOnCount, _toolOffCount, i);

                    TrajProcess(strLs[i]);
                    nGraphCounts++;
                }
                else if (strLs[i].Contains(strToolon) || strLs[i].Contains(strTooloff))  //toolon、tooloff
                {
                    //
                }
                else if (strLs[i].Contains(strTool))  //tool
                {
                    ToolValueProcess(strLs[i]);
                }
                else if (strLs[i].Contains(strFrame))  //frame
                {
                    FrameProcess(strLs[i]);
                }                
            }

           //xml表外层部分信息
            DefaultOtherProcess();

            return bResult;
        }

        //去处无用信息
        private List<string> CamArrayProcess(string[] strFileArr, out  List<int> toolOnCount, out List<int> toolOffCount)
        {
            List<string> strLs = new List<string>();
            int iCount = 0;
            toolOnCount = new List<int>();
            toolOffCount = new List<int>();

            foreach (string str in strFileArr)
            {
                if (str.Contains(strTool) | str.Contains(strFrame) | str.Contains(strMovejs) | str.Contains(strMovej) |
                    str.Contains(strMovec) | str.Contains(strToolon) | str.Contains(strTooloff) | str.Contains(strCircle) |
                    str.Contains(strOblong) | str.Contains(strPolygon) | str.Contains(strRects))
                {
                    strLs.Add(str);

                    if (str.Contains(strToolon)) toolOnCount.Add(iCount);
                    else if (str.Contains(strTooloff)) toolOffCount.Add(iCount);

                    iCount++;
                }
            }

            return strLs;
        }

 
        #region  圆形处理

        //圆形处理
        private void CircleProcess(string strFileLine, string strFileLine1, string strFileLine2)
        {
            string[] strPoint = null;
            string[] strPoint1 = null;
            string[] strPoint2 = null;
            string[] strPoint3 = null;

            //数据分割后到数组
            ArraySplit(strFileLine, strFileLine1, strFileLine2, null, ref strPoint, ref strPoint1, ref strPoint2, ref strPoint3);

            int nInorOutside = int.Parse(strPoint[2]);
            double Radius = double.Parse(strPoint[1]);

            //圆形半径计算
            if (Radius ==0) CircleCalculate(strPoint, strPoint1, strPoint2, out Radius);
            
            //图形参数
            TrajStatus.sComments = strCir;
            TrajStatus.nMoveType = 3;           
            TrajStatus.nJointMode = 1;
            TrajPara.nLength = TrajPara.nWidth = TrajPara.nEdge = TrajPara.nCutType = ProcessPara.nLaserOn = TrajStatus.nTcpAction = 0;
            TrajPara.nRadius = Radius;

            if (nInorOutside == 0) TrajPara.bInorOutside = true;
            else if (nInorOutside == 1) TrajPara.bInorOutside = false;


            //速度参数
            Speed.nom_speed.vel = 20;
            Speed.nom_speed.rmax = 600;
            Speed.nom_speed.leave = Speed.nom_speed.reach= 0;


            double adt = Radius * 10;//int.Parse(Math.Round(strGraph[1])) * 10;

            if (adt > 100)
            {
                Speed.nom_speed.accel = Speed.nom_speed.decel = Speed.nom_speed.tmax = 100;    //"60"  "60"  "75" 
            }
            else
            {
                Speed.nom_speed.accel = Speed.nom_speed.decel = Speed.nom_speed.tmax = Radius * 10;//"60" "60" "75"
            }


            //图形内、外切
            if (nInorOutside==0)
            {
                //TrajPara.nCutInRadius = TrajPara.nCutOutRadius = Radius - 1;  //氧气
                TrajPara.nCutInRadius = TrajPara.nCutOutRadius = Radius;  //空气
            }
            else
            {
                //TrajPara.nCutInRadius = TrajPara.nCutOutRadius = Radius + 1;  //氧气
                TrajPara.nCutInRadius = TrajPara.nCutOutRadius = Radius;  //空气
            }

                
            ////图形joint、point点
            string[] strPolygon2 = null;
            string[] strPolygon3 =null;
            string[] strArc = null;
            strPolygon2 = strPolygon3=strArc=new string[14] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            PositionParameter(strPoint1, strPolygon2, strPolygon3, strArc, strCir);

            //默认数据赋值
            DefaultDataInit();
        }

        //圆形半径计算
        private void CircleCalculate(string[] strGraph, string[] strPoint, string[] strPoint2, out double nLength)
        {
            DataType.CAMDataType.CoordValue[] CoordVal = new DataType.CAMDataType.CoordValue[3];

            CoordVal[0].x = double.Parse(strPoint[7]);
            CoordVal[0].y = double.Parse(strPoint[8]);
            CoordVal[0].z = double.Parse(strPoint[9]);

            CoordVal[1].x = double.Parse(strPoint2[7]);
            CoordVal[1].y = double.Parse(strPoint2[8]);
            CoordVal[1].z = double.Parse(strPoint2[9]);


            //新增自动计算圆半径
            double DistanceR = Math.Sqrt(Math.Pow(CoordVal[1].x - CoordVal[0].x, 2) + Math.Pow(CoordVal[1].y - CoordVal[0].y, 2) + Math.Pow(CoordVal[1].z - CoordVal[0].z, 2));  //半径
            nLength = DistanceR;//圆形半径保留小数点后两位               

        }

        #endregion

        #region  多边形处理

        //多边形处理
        private void PolygonProcess(string strFileLine, string strFileLine1, string strFileLine2, string strFileLine3)
        {
            string[] strPoint = null;
            string[] strPoint1 = null;
            string[] strPoint2 = null;
            string[] strPoint3 = null;

            //数据分割后到数组
            ArraySplit(strFileLine, strFileLine1, strFileLine2, strFileLine3, ref strPoint, ref strPoint1, ref strPoint2, ref strPoint3);


            int nEdges = int.Parse(strPoint[1]);
            int nInorOutside = int.Parse(strPoint[4]);
            double dLeaveReach = double.Parse(strPoint[5]);
            double nLength = double.Parse(strPoint[2]);
            double nWidth = double.Parse(strPoint[3]);

            //多边形长、宽计算
            if (nLength == 0 | nWidth==0) PolygonCalculate(strPoint, strPoint1, strPoint2, strPoint3, out nLength, out nWidth);
            
            //图形参数 
            TrajStatus.sComments = strPol;
            TrajPara.nEdge = nEdges;
            TrajStatus.nMoveType = 4;
            TrajPara.nRadius = 0;
            TrajStatus.nJointMode = TrajPara.nCutType = ProcessPara.nLaserOn = TrajStatus.nTcpAction = 0;
            TrajPara.nLength = nLength;
            TrajPara.nWidth = nWidth;

            if (nInorOutside == 0) TrajPara.bInorOutside = true;
            else if (nInorOutside == 1) TrajPara.bInorOutside = false;
            

            //椭圆和四边形内、外切半径
            TrajPara.nCutInRadius = TrajPara.nCutOutRadius = nWidth / 2;

            //速度参数
            Speed.nom_speed.accel = Speed.nom_speed.decel = 10;
            Speed.nom_speed.vel = 20;   
            Speed.nom_speed.rmax = 600;
            Speed.nom_speed.tmax = 100;

            //速度参数中Leave、Reach
            if (dLeaveReach != 0)
            {
                Speed.nom_speed.leave = Speed.nom_speed.reach = dLeaveReach;
            }
            else
            {
                Speed.nom_speed.leave = Speed.nom_speed.reach = 1;//原先为3
            }
           

            ////图形joint、point点
            string[] strArc = new string[14] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", };
            PositionParameter(strPoint1, strPoint2, strPoint3, strArc, strPol);

            //默认数据赋值
            DefaultDataInit();

        }

        //多边形长、宽计算
        private void PolygonCalculate(string[] strGraph, string[] strPoint, string[] strPolygon2, string[] strPolygon3, out double nLength, out double nWidth)
        {
            DataType.CAMDataType.CoordValue[] CoordVal =new DataType.CAMDataType.CoordValue[3];
            

            CoordVal[2].x = double.Parse(strPolygon3[7]);
            CoordVal[2].y = double.Parse(strPolygon3[8]);
            CoordVal[2].z = double.Parse(strPolygon3[9]);

            CoordVal[1].x = double.Parse(strPolygon2[7]);
            CoordVal[1].y = double.Parse(strPolygon2[8]);
            CoordVal[1].z = double.Parse(strPolygon2[9]);

            CoordVal[0].x = double.Parse(strPoint[7]);
            CoordVal[0].y = double.Parse(strPoint[8]);
            CoordVal[0].z = double.Parse(strPoint[9]);

            int nSides = int.Parse(strGraph[1]);
            double distanceL = 0;
            double distanceW = 0;

            //新增自动计算多边形长宽及椭圆长边、短边
            if (nSides == 0)  //oblong
            {
                distanceL = Math.Sqrt(Math.Pow(CoordVal[1].x - CoordVal[0].x, 2) + Math.Pow(CoordVal[1].y - CoordVal[0].y, 2) + Math.Pow(CoordVal[1].z - CoordVal[0].z, 2));  //长

                double[] Axyz = new double[3];
                Axyz[0] = CoordVal[1].x - CoordVal[0].x;
                Axyz[1] = CoordVal[1].y - CoordVal[0].y;
                Axyz[2] = CoordVal[1].z - CoordVal[0].z;

                double[] Bxyz = new double[3];
                Bxyz[0] = CoordVal[2].x - CoordVal[1].x;
                Bxyz[1] = CoordVal[2].y - CoordVal[1].y;
                Bxyz[2] = CoordVal[2].z - CoordVal[1].z;

                double[] Cxyz = new double[3];
                //a * b=  (AyBz-AzBy)*i+(AzBx-AxBz)*j+(AxBy-AyBx)k)
                Cxyz[0] = Axyz[1] * Bxyz[2] - Axyz[2] * Bxyz[1];
                Cxyz[1] = Axyz[2] * Bxyz[0] - Axyz[0] * Bxyz[2];
                Cxyz[2] = Axyz[0] * Bxyz[1] - Axyz[1] * Bxyz[0];

                double l_x = Math.Sqrt(Math.Pow(Cxyz[0], 2) + Math.Pow(Cxyz[1], 2) + Math.Pow(Cxyz[2], 2));
                double l_y = Math.Sqrt(Math.Pow(Axyz[0], 2) + Math.Pow(Axyz[1], 2) + Math.Pow(Axyz[2], 2));
                double l_nDistance = (double)l_x / l_y;  //椭圆短边的一半

                distanceW = l_nDistance * 2;//椭圆宽
            }
            else if (nSides >3) //rect//polygon
            {
                distanceL = Math.Sqrt(Math.Pow(CoordVal[1].x - CoordVal[0].x, 2) + Math.Pow(CoordVal[1].y - CoordVal[0].y, 2) + Math.Pow(CoordVal[1].z - CoordVal[0].z, 2));  //长
                distanceW = Math.Sqrt(Math.Pow(CoordVal[2].x - CoordVal[1].x, 2) + Math.Pow(CoordVal[2].y - CoordVal[1].y, 2) + Math.Pow(CoordVal[2].z - CoordVal[1].z, 2));  //宽
            }

            nLength = distanceL;
            nWidth = distanceW;
        }

        #endregion

        //圆弧处理
        private void CircularArc(string strFileLine, string strFileLine1)
        {          
            string[] strPoint = null;
            string[] strPoint1 = null;
            string[] strPoint2 = null;
            string[] strPoint3 = null;

            ArraySplit(null, strFileLine, strFileLine1, null, ref strPoint, ref strPoint1, ref  strPoint2, ref strPoint3);

            //图形参数
            TrajStatus.sComments = strMoveC;
            TrajPara.bInorOutside = false;
            TrajStatus.nMoveType = 2;
            TrajStatus.nJointMode = 1;
            TrajPara.nRadius = TrajPara.nLength=TrajPara.nWidth=TrajPara.nEdge= TrajPara.nCutType=0;
            TrajPara.nCutInRadius = TrajPara.nCutOutRadius = 0;
            

            //速度参数
            Speed.nom_speed.accel = Speed.nom_speed.decel = 10;
            Speed.nom_speed.vel = 100;     
            Speed.nom_speed.rmax = 600;
            Speed.nom_speed.tmax = 100;
            Speed.nom_speed.leave = Speed.nom_speed.reach = 1;//原先为3

            ////点位信息
            string[] strPolygon2 = null;
            string[] strPolygon3 = null;
            strPolygon2 = strPolygon3=new string[14] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            PositionParameter(strPoint1, strPolygon2, strPolygon3, strPoint2, strMoveC);

            //默认数据赋值
            DefaultDataInit();

        }

        //轨迹处理
        private void TrajProcess(string strFileLine)
        {
            string[] strPoint = null;
            string[] strPoint1=null;
            string[] strPoint2 = null;
            string[] strPoint3 = null;

            ArraySplit(null, strFileLine, null, null, ref strPoint, ref strPoint1, ref  strPoint2, ref strPoint3);

            string strGraph=strPoint1[0];
            //图形参数
            TrajPara.bInorOutside = false;
            TrajPara.nCutType = 1;
            TrajPara.nRadius = TrajPara.nLength = TrajPara.nWidth = TrajPara.nEdge = 0;
            TrajPara.nCutInRadius = TrajPara.nCutOutRadius = 0;           
            TrajPara.nCutInRadius = TrajPara.nCutOutRadius = 0;
            
            //速度及图形信息
            if (strGraph == strMovejjs)  //moveJ
            {
                //图形参数
                TrajStatus.nMoveType = 0;
                TrajStatus.nJointMode = 2;
                TrajStatus.sComments = strMoveJ;

                //速度参数
                Speed.nom_speed.accel = Speed.nom_speed.vel = Speed.nom_speed.decel = 50;
                Speed.nom_speed.rmax = Speed.nom_speed.tmax = 9999;
                Speed.nom_speed.leave = Speed.nom_speed.reach = 30;

            }
            else if (strGraph == strMovejs) //moveL
            {
                //图形参数                  
                TrajStatus.nMoveType = TrajStatus.nJointMode = 1;
                TrajStatus.sComments = strMoveL;

                //速度参数
                Speed.nom_speed.accel = 10;
                Speed.nom_speed.vel = 100;
                Speed.nom_speed.decel = 10;
                Speed.nom_speed.rmax = 600;
                Speed.nom_speed.tmax = 100;

                //开光点和前后点r角都为0    //但开光前一点为movej，所以前一点不变
                Speed.nom_speed.leave = Speed.nom_speed.reach = 1;
            }

            ////点位信息
            string[] strPolygon2 = null;
            string[] strPolygon3 = null;
            string[] strArc = null;
            strPolygon2= strPolygon3= strArc = new string[14] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            PositionParameter(strPoint1, strPolygon2, strPolygon3, strArc, strMoveJ);

            //默认数据赋值
            DefaultDataInit();
 
        }

        //轨迹激光开关处理
        private void ToolProcess(List<int> toolOnCount, List<int> toolOffCount, int iCount)
        {
            ProcessPara.nLaserOn = TrajStatus.nTcpAction = 0;  //其余点

            foreach (int nCount in toolOnCount)
            {
                if (iCount == nCount + 1)
                {
                    ProcessPara.nLaserOn = TrajStatus.nTcpAction =  1;  //toolon后一点开光
                }
            }

            foreach (int nCount in toolOffCount)
            {
                if (iCount == nCount - 1)
                {
                    ProcessPara.nLaserOn = TrajStatus.nTcpAction = 2; //tooloff前一点关光
                }
            }
        }

        //Tool工具值处理
        private void ToolValueProcess(string strFileLine)
        {
            
            string[] strTool = null;
            string[] strPoint=null;
            string[] strPoint2=null;
            string[] strPoint3=null;

            ArraySplit(null, strFileLine, null, null, ref strPoint, ref strTool, ref strPoint2, ref strPoint3);

            for (int i = 1; i < strTool.Length;i++ )
            {
                if (i == 6) _Tool += strTool[i];
                else _Tool += strTool[i] + ",";
            }
           
        }

        //Frame面处理
        private void FrameProcess(string strFileLine)
        {
            string[] strFrame = null;
            string[] strPoint = null;
            string[] strPoint2 = null;
            string[] strPoint3 = null;

            ArraySplit(null, strFileLine, null, null, ref strPoint, ref strFrame, ref strPoint2, ref strPoint3);

            Track.ftrack.x = double.Parse(strFrame[1]);
            Track.ftrack.y = double.Parse(strFrame[2]);
            Track.ftrack.z = double.Parse(strFrame[3]);
            Track.ftrack.rx = double.Parse(strFrame[4]);
            Track.ftrack.ry = double.Parse(strFrame[5]);
            Track.ftrack.rz = double.Parse(strFrame[6]);
            Track.ftrack.fatherId = "world[0]";

            for (int i = 1; i < strFrame.Length; i++)
            {
                if (i == 6) _Frame += strFrame[i];
                else _Frame += strFrame[i] + ",";
            }

            
            TrackLs.Add(Track);
            
        }

        #region  结构体赋值

        //部分变量默认值初始化
        private void DefaultDataInit()
        {
            //strProcessPara结构
            ProcessPara.bToStop = false;
            ProcessPara.nFollowDistance = 0;
            ProcessPara.nFreqDistance = 0;
            ProcessPara.nGasDistance = 0;
            ProcessPara.nGasOn = 0;
            ProcessPara.nJerk = 3;
            ProcessPara.nLaserFrequency = 0;
            ProcessPara.nLaserIndex = 0;
            ProcessPara.nLaserProgNum = 0;
            ProcessPara.nPWMDistance = 0;
            ProcessPara.nPenetrate = 0;
            ProcessPara.nStop = 0;
            ProcessPara.nTraceCalNum = 0;
            ProcessPara.nTraceOn = 0;
            ProcessPara.nTracePosition = 0;
            ProcessPara.nTraceZNNum = 0;
            ProcessPara.sAction = "";
            ProcessPara.nLaserPower = 100;
            ProcessPara.nPWMPercent = 100;
            ProcessPara.nGasPressure = 100;

            //strTrajPara结构
            TrajPara.bQuick_Move = false;
            TrajPara.nCompensation = 0;
            TrajPara.nInclinedAngle = 0;
            TrajPara.nLeadInLength = 0;
            TrajPara.nLeadOutLength = 0;
            TrajPara.nMultiple = 1;
            TrajPara.nOverloop = 0;
            TrajPara.nRotation = 0;
            TrajPara.nCutInAngle = 90;
            TrajPara.nCutOutAngle = 90;

            //strTrajStatus结构
            TrajStatus.bAutoSet = false;
            TrajStatus.bBypass = false;
            TrajStatus.nActionIndex = 0;
            TrajStatus.nAdvanceIndex = 0;
            TrajStatus.nIndex = 0;
            TrajStatus.nParameterIndex = nGraphCounts;
            TrajStatus.nProcessIndex = nGraphCounts;
            TrajStatus.nSpeedIndex = nGraphCounts;
            TrajStatus.ndistance = 0;
            TrajStatus.sInstruction = "";

            //结构列表
            ProcessParaLs.Add(ProcessPara);
            TrajParaLs.Add(TrajPara);
            TrajStatusLs.Add(TrajStatus);
            SpeedLs.Add(Speed);
        }

        //点位赋值
        private void PositionParameter(string[] strPoint, string[] strPolygon2, string[] strPolygon3, string[] strArc, string strType)
        {
            //点位信息
            //关节值
            TrajStatus.jJoints.j1 = double.Parse(strPoint[1]);
            TrajStatus.jJoints.j2 = double.Parse(strPoint[2]);
            TrajStatus.jJoints.j3 = double.Parse(strPoint[3]);
            TrajStatus.jJoints.j4 = double.Parse(strPoint[4]);
            TrajStatus.jJoints.j5 = double.Parse(strPoint[5]);
            TrajStatus.jJoints.j6 = double.Parse(strPoint[6]);


            //坐标值
            if (strType == strMoveC)
            {
                TrajStatus.pPoints.x = double.Parse(strArc[7]);
                TrajStatus.pPoints.y = double.Parse(strArc[8]);
                TrajStatus.pPoints.z = double.Parse(strArc[9]);
                TrajStatus.pPoints.rx = double.Parse(strArc[10]);
                TrajStatus.pPoints.ry = double.Parse(strArc[11]);
                TrajStatus.pPoints.rz = double.Parse(strArc[12]);
                TrajStatus.pPoints.fatherId = "fTrack[0]";

                TrajStatus.pCircle.x = double.Parse(strPoint[7]);
                TrajStatus.pCircle.y = double.Parse(strPoint[8]);
                TrajStatus.pCircle.z = double.Parse(strPoint[9]);
                TrajStatus.pCircle.rx = double.Parse(strPoint[10]);
                TrajStatus.pCircle.ry = double.Parse(strPoint[11]);
                TrajStatus.pCircle.rz = double.Parse(strPoint[12]);
                TrajStatus.pCircle.fatherId = "fTrack[0]";
            }
            else
            {
                TrajStatus.pPoints.x = double.Parse(strPoint[7]);
                TrajStatus.pPoints.y = double.Parse(strPoint[8]);
                TrajStatus.pPoints.z = double.Parse(strPoint[9]);
                TrajStatus.pPoints.rx = double.Parse(strPoint[10]);
                TrajStatus.pPoints.ry = double.Parse(strPoint[11]);
                TrajStatus.pPoints.rz = double.Parse(strPoint[12]);
                TrajStatus.pPoints.fatherId = "fTrack[0]";

                TrajStatus.pCircle.x = double.Parse(strArc[7]);
                TrajStatus.pCircle.y = double.Parse(strArc[8]);
                TrajStatus.pCircle.z = double.Parse(strArc[9]);
                TrajStatus.pCircle.rx = double.Parse(strArc[10]);
                TrajStatus.pCircle.ry = double.Parse(strArc[11]);
                TrajStatus.pCircle.rz = double.Parse(strArc[12]);
                TrajStatus.pCircle.fatherId = "fTrack[0]";
            }

            TrajStatus.pPolygon2.x = double.Parse(strPolygon2[7]);
            TrajStatus.pPolygon2.y = double.Parse(strPolygon2[8]);
            TrajStatus.pPolygon2.z = double.Parse(strPolygon2[9]);
            TrajStatus.pPolygon2.rx = double.Parse(strPolygon2[10]);
            TrajStatus.pPolygon2.ry = double.Parse(strPolygon2[11]);
            TrajStatus.pPolygon2.rz = double.Parse(strPolygon2[12]);
            TrajStatus.pPolygon2.fatherId = "fTrack[0]";


            TrajStatus.pPolygon3.x = double.Parse(strPolygon3[7]);
            TrajStatus.pPolygon3.y = double.Parse(strPolygon3[8]);
            TrajStatus.pPolygon3.z = double.Parse(strPolygon3[9]);
            TrajStatus.pPolygon3.rx = double.Parse(strPolygon3[10]);
            TrajStatus.pPolygon3.ry = double.Parse(strPolygon3[11]);
            TrajStatus.pPolygon3.rz = double.Parse(strPolygon3[12]);
            TrajStatus.pPolygon3.fatherId = "fTrack[0]";
        }

        //外层默认变量赋值
        private void DefaultOtherProcess()
        {
            LaserDelay.laserDelay = 0;
            LaserDelayLs.Add(LaserDelay);

            Penetrate.penetrate = 0;
            PenetrateLs.Add(Penetrate);

            Penetrate.penetrate = 0;
            DistanceLs.Add(Distance);

            FrameIndex.frameIndex = int.Parse(_frameIndexOut);
            FrameIndexLs.Add(FrameIndex);

            IndexGun.indexGun = int.Parse(_toolIndexOut);
            IndexGunLs.Add(IndexGun);

            for (int i = 0; i < 11; i++)
            {
                Cutting.cutting.x=0;
                Cutting.cutting.y=0;
                Cutting.cutting.z=0;
                Cutting.cutting.rx=0;
                Cutting.cutting.ry=0;
                Cutting.cutting.rz=0;
                Cutting.cutting.fatherId = "fTrack[0]";

                CuttingLs.Add(Cutting);
            }
                
        }

        #endregion

        #region 数组处理
        //数组大小
        private int ArrayCountProcess(string _strFileLine)
        {
            Regex rg1 = new Regex(",");
            Regex rg2 = new Regex("/");
            MatchCollection mc1 = rg1.Matches(_strFileLine);
            MatchCollection mc2 = rg2.Matches(_strFileLine);
            int nCount = mc1.Count + mc2.Count + 1;

            return nCount;
        }

        //数组分割
        private void ArraySplit(string strPoint0, string strPoint1, string strPoint2, string strPoint3, ref string[] strGraph, ref string[] strPoint, ref string[] strPolygon2, ref string[] strPolygon3)
        {

            if (strPoint0 != null)
            {
                int nCount = ArrayCountProcess(strPoint0);
                strGraph = new string[nCount];

                strGraph = strPoint0.Split(new char[] { '(', ',', ')' });   //图形信息提取
            }
            if(strPoint1 != null)
            {
                int nCount = ArrayCountProcess(strPoint1);
                strPoint = new string[nCount];

                strPoint = strPoint1.Split(new char[] { ',', '/' });     //点信息提取（多边形第一点）
            }
            if(strPoint2 != null)
            {
                int nCount = ArrayCountProcess(strPoint2);
                strPolygon2 = new string[nCount];

                strPolygon2 = strPoint2.Split(new char[] { ',', '/' });  //点信息提取（多边形第二点）
            }
            if (strPoint3 != null)
            {
                int nCount = ArrayCountProcess(strPoint3);
                strPolygon3 = new string[nCount];

                strPolygon3 = strPoint3.Split(new char[] { ',', '/' });  //点信息提取（多边形第三点）
            }
            
        }

        #endregion


        #endregion













    }
}
