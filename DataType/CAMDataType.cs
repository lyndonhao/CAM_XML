using System;

namespace DataType
{
    /// <summary>
    /// Description of CAMDataType.
    /// </summary>
    public struct CAMDataType : IEquatable<CAMDataType>
    {
        int member; // this is just an example member, replace it with your own struct members!

        #region Equals and GetHashCode implementation
        // The code in this region is useful if you want to use this structure in collections.
        // If you don't need it, you can just remove the region and the ": IEquatable<CAMDataType>" declaration.

        public override bool Equals(object obj)
        {
            if (obj is CAMDataType)
                return Equals((CAMDataType)obj); // use Equals method below
            else
                return false;
        }

        public bool Equals(CAMDataType other)
        {
            // add comparisions for all members here
            return this.member == other.member;
        }

        public override int GetHashCode()
        {
            // combine the hash codes of all members here (e.g. with XOR operator ^)
            return member.GetHashCode();
        }

        public static bool operator ==(CAMDataType left, CAMDataType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CAMDataType left, CAMDataType right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region  val结构体定义及说明
        //stProcessPara结构体参数，该结构体包含图形的工艺参数。 （所建key值个数与所建程序内部调用此工艺参数是否相同有关）
        public struct stProcessPara
        {
            public bool bToStop;         //是否停止，默认：false；取值：false：未选中，true：选中
            public int nFollowDistance;  //随动提前开启距离，默认：0 
            public int nFreqDistance;    //频率设置提前距离，默认：0 
            public int nGasDistance;     //气压提前开启距离，默认：0
            public int nGasOn;           //    默认：0
            public int nGasPressure;     //气压压力，默认：100；取值：0-100
            public int nJerk;            //
            public int nLaserFrequency;  //激光频率，默认：0
            public int nLaserIndex;      //激光程序号，默认：0
            public int nLaserOn;         //开、关光标志，默认：0 ；1：开光，2：关光
            public int nLaserPower;      //激光功率，默认100；取值：0-100
            public int nLaserProgNum;    //
            public int nPWMDistance;     //占空比提前开启距离，默认：0
            public int nPWMPercent;      //PWM占空比，默认：100；取值：0-100
            public int nPenetrate;       //穿孔程序号，默认：0，取值：0-2
            public int nStop;            //停止时间，默认：0；取值：0-100  与bTostop一起用，开启时bTostop为true
            public int nTraceCalNum;     //默认：0
            public int nTraceOn;         //默认：0
            public int nTracePosition;   //默认：0
            public int nTraceZNNum;      //默认：0
            public string sAction;       //默认：""
        }

        //stTrajPara结构体参数，该结构体包含图形形状的相关属性。  （所建key值个数与所建程序类型有关）  moveJ/moveL/moveC    circle    pPolygon_0  pPolygon_3 pPolygon_4 pPolygon_5 pPolygon_6
        public struct stTrajPara
        {
            public bool bInorOutside;   //内切外切，默认：false；取值：false：内，true：外
            public bool bQuick_Move;    //快速切割，默认：false；取值：false：未选中，true：选中
            public int nCompensation;   //默认：0
            public int nCutInAngle;     //切入角，默认：90；取值：0-180
            public int nCutOutAngle;    //切出角，默认：90；取值：0-180
            public int nCutType;        //切割方式,默认：0
            public int nEdge;           //图形边数，默认：0；取值：0:腰形  3：三角形 4：四边形  5：五边形  6:六边形
            public int nInclinedAngle;  //倾斜角，默认：0
            public int nLeadInLength;   //
            public int nLeadOutLength;  //
            public int nMultiple;       //默认：1
            public int nOverloop;       //过切大小，默认：0；取值：0-3.93  有的无限制
            public int nRotation;       //默认：0
            public double nCutInRadius; //默认：0
            public double nCutOutRadius;//切出半径，默认：圆等于半径，四边形和椭圆形等于宽度一半，多边形等于边长一半；取值：0-oo  没限制
            public double nLength;      //长度，默认：0；取值：0-500
            public double nRadius;      //半径，默认：0；取值：0-500
            public double nWidth;       //宽度，默认：0；取值：0-500
        }

        //stTrajStatus结构体参数，该结构体包含一个图形的点以及基础属性。  （所建key值个数与所建程序个数有关）
        public struct stTrajStatus
        {
            public bool bAutoSet;       //该参数用于实现工艺参数自动匹配功能，默认：false
            public bool bBypass;        //是否略过该点，默认：false
            public int nActionIndex;    //执行动作的索引号，默认：0
            public int nAdvanceIndex;   //提前距离的索引号，默认：0
            public int nIndex;          //索引号，，默认：0
            public int nJointMode;      //改变 blend 的值，默认：根据MoveType来的；取值：moveJ:2,MoveL:1,MoveC:1,circle:0,polygon:0
            public int nMoveType;       //该图形运动类型，默认：0；取值：0:moveJ  1:moveL  2:moveC  3:circle  4:polygon
            public int nParameterIndex; //图形参数索引号，默认：针对polygon图形索引号；取值：1-oo  数量与所建图形个数有关
            public int nProcessIndex;   //工艺参数索引号，默认：0；取值：对应ProcessPara来的
            public int nSpeedIndex;     //速度索引号，默认：根据此索引查找对应速度；
            public int nTcpAction;      //光，气，随动开关，默认：0；取值：1:开光  2:关光
            public int ndistance;       //提前距离，默认：0；取值：0-200
            public string sComments;    //图形名称,默认：根据MoveType来的；取值：moveJ  moveL  moveC  Circle  Polygon
            public string sInstruction; //指令信息，默认：""
            public jointRx jJoints;     //Joint 点，默认：0
            public pointRx pCircle;     //圆弧中间点,默认：0
            public pointRx pPoints;     //图形第一点，默认：0
            public pointRx pPolygon2;   //多边形第二点，默认：0
            public pointRx pPolygon3;   //多边形第三点，默认：0
        }


        public struct jointRx
        {
            public double j1, j2, j3, j4, j5, j6;
        }

        public struct pointRx
        {
            public double x, y, z, rx, ry, rz;
            public string fatherId;
        }

        //fTrack结构体参数
        public struct fTrack
        {
            public frame ftrack;  //frame值
        }

        public struct frame
        {
            public double x, y, z, rx, ry, rz;
            public string fatherId;
        }

        //nom_Speed结构体参数
        public struct nom_Speed
        {
            public mdesc nom_speed;  //速度值
        }

        public struct mdesc
        {
            public double accel;    //加速度，默认：；取值：a0-800  
            public double vel;      //平均速度，默认：；取值：0-400  
            public double decel;    //减速度，默认：；取值：0-800  
            public double rmax;     // 
            public double tmax;     // 
            public double leave;    //R角，默认：；取值：0-400  
            public double reach;    //R角，默认：；取值：0-400
        }



        //nFrameIndex结构体参数
        public struct nFrameIndex
        {
            public int frameIndex;  //frame索引号，默认：4
        }

        //nIndexGun结构体参数
        public struct nIndexGun
        {
            public int indexGun;   //工具值索引号，默认：1
        }

        //nLaserDelay结构体参数
        public struct nLaserDelay
        {
            public int laserDelay;
        }

        //nPenetrate结构体参数
        public struct nPenetrate
        {
            public int penetrate;
        }

        //nDistance结构体参数
        public struct nDistance
        {
            public int distance;
        }

        //pCutting结构体参数
        public struct pCutting
        {
            public pointRx cutting;
        }

        public struct strings
        {
            public string s1, s2, s3, s4, s5, s6, s7, s8;
        }
        #endregion



        #region
        //
        public struct CoordValue
        {
            public double x, y, z;
        }


        public struct GraphParameter
        {
            public string MoveType;
            public double Radius, Length, Width, LeaveReach;
            public int Sides, InoroutCut;
        }

        public enum Graph
        {
            Circle = 0,
            Oblong = 1,
            Rect = 2,
            Polygon = 3,
            Movec = 4,
            Movej = 5,
            Movel = 6
        };

        #endregion





    }
}
