using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType
{
   public class BasicDataType
    {
       /// <summary>
       /// 向量
       /// </summary>
        public struct vector
        {
            public double x;
            public double y;
            public double z;
        }
       /// <summary>
       /// 三点确定片面的系数
       /// </summary>
        public struct PlaneCoefficient
        {
            public double a;
            public double b;
            public double c;
            public double d;
        }
    }
}
