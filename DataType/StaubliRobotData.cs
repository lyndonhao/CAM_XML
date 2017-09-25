using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType
{
   
    public class StaubliRobotData
    {
        #region
        public enum ShoulderConfig
        {
            left,
            right,
            ssame,
            sfree
        }
        public enum ElbowConfig
        {
            eNegative,
            ePositive,
            esame,
            efree
        }
        public enum WristConfig
        {
            wNegative,
            wPositive,
            wsame,
            wfree
        }
        public struct Vector
        {
            public double x, y, z, Rx, Ry, Rz;
        }
        /// <summary>
        /// joint
        /// </summary>
        public struct St_JointRx
        {
            public double J1, J2, J3, J4, J5, J6;
        }
        /// <summary>
        /// point
        /// </summary>
        public struct St_PointRx
        {
            public double x, y, z, Rx, Ry, Rz;
            public ShoulderConfig shoulder;
            public ElbowConfig elbow;
            public WristConfig wrist;
            public Vector frame;
        }
        /// <summary>
        /// tool
        /// </summary>
        public struct St_Tool
        {
            public double x, y, z, Rx, Ry, Rz;
        }
        #endregion
    }
}
