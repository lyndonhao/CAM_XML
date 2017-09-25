using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataType;

namespace MathMatics
{
    public static class BasicMathTool
    {
        /// <summary>
        /// 向量差
        /// </summary>
        /// <param name="x_vVector1"></param>
        /// <param name="x_vVector2"></param>
        /// <returns></returns>
        public static DataType.BasicDataType.vector SubVector(DataType.BasicDataType.vector x_vVector1, DataType.BasicDataType.vector x_vVector2)
        {
            DataType.BasicDataType.vector l_vV1;
            l_vV1.x = x_vVector2.x - x_vVector1.x;
            l_vV1.y = x_vVector2.y - x_vVector1.y;
            l_vV1.z = x_vVector2.z - x_vVector1.z;
            return l_vV1;
        }
        /// <summary>
        /// 判断向量是否相等
        /// </summary>
        /// <param name="x_vVector1"></param>
        /// <param name="x_vVector2"></param>
        /// <param name="x_nPrecision"></param>
        /// <returns></returns>
        public static bool IsEqualVector(DataType.BasicDataType.vector x_vVector1, DataType.BasicDataType.vector x_vVector2, double x_nPrecision)
        {
            bool l_bResult = false;
            if (x_vVector1.x - x_vVector2.x <= x_nPrecision & x_vVector1.y - x_vVector2.y <= x_nPrecision & x_vVector1.z - x_vVector2.z <= x_nPrecision)
            {
                l_bResult = true;
            }
            return l_bResult;
        }
        /// <summary>
        /// 两点间距离
        /// </summary>
        /// <param name="x_vVector1"></param>
        /// <param name="x_vVector2"></param>
        /// <returns></returns>
        public static double VectorDistance(DataType.BasicDataType.vector x_vVector1, DataType.BasicDataType.vector x_vVector2)
        {
            double l_nValue = Math.Sqrt(Math.Pow(x_vVector2.x - x_vVector1.x, 2) + Math.Pow(x_vVector2.y - x_vVector1.y, 2) + Math.Pow(x_vVector2.z - x_vVector1.z, 2));
            return l_nValue;
        }
        /// <summary>
        /// 向量叉乘
        /// </summary>
        /// <param name="x_X">向量</param>
        /// <param name="x_Y">向量</param>
        /// <returns>返回值(法向量)</returns>
        public static double[] Vect3CrossPord(double[] x_X, double[] x_Y)
        {
            double[] _value = new double[3];
            //a * b=  (AyBz-AzBy)*i+(AzBx-AxBz)*j+(AxBy-AyBx)k)
            _value[0] = x_X[1] * x_Y[2] - x_X[2] * x_Y[1];
            _value[1] = x_X[2] * x_Y[0] - x_X[0] * x_Y[2];
            _value[2] = x_X[0] * x_Y[1] - x_X[1] * x_Y[0];
            return _value;
        }
        /// <summary>
        /// 向量叉乘
        /// </summary>
        /// <param name="x_vVector1"></param>
        /// <param name="x_vVector2"></param>
        /// <returns></returns>
        public static DataType.BasicDataType.vector Vect3CrossPord(DataType.BasicDataType.vector x_vVector1, DataType.BasicDataType.vector x_vVector2)
        {
            double[] x = new double[3];
            double[] y = new double[3];
            double[] z = new double[3];
            DataType.BasicDataType.vector l_vResule;
            x[0] = x_vVector1.x;
            x[1] = x_vVector1.y;
            x[2] = x_vVector1.z;
            y[0] = x_vVector2.x;
            y[1] = x_vVector2.y;
            y[2] = x_vVector2.z;
            z = Vect3CrossPord(x, y);
            l_vResule.x = z[0];
            l_vResule.y = z[1];
            l_vResule.z = z[2];
            return l_vResule;
        }
        /// <summary>
        /// 向量的模
        /// </summary>
        /// <param name="x_vVector"></param>
        /// <returns></returns>
        public static double VectorNorm(DataType.BasicDataType.vector x_vVector)
        {
            return Math.Sqrt(Math.Pow(x_vVector.x, 2) + Math.Pow(x_vVector.y, 2) + Math.Pow(x_vVector.z, 2));
        }

        /// <summary>
        /// 向量点积
        /// </summary>
        /// <param name="x_X"></param>
        /// <param name="x_Y"></param>
        /// <returns></returns>
        public static double Vect3ScalorProd(double[] x_X, double[] x_Y)
        {

            return x_X[0] * x_Y[0] + x_X[1] * x_Y[1] + x_X[2] * x_Y[2];
        }
        /// <summary>
        /// 向量点积
        /// </summary>
        /// <param name="x_vVector1"></param>
        /// <param name="x_vVector2"></param>
        /// <returns></returns>
        public static double Vect3ScalorProd(DataType.BasicDataType.vector x_vVector1, DataType.BasicDataType.vector x_vVector2)
        {
            return x_vVector1.x * x_vVector2.x + x_vVector1.y * x_vVector2.y + x_vVector1.z * x_vVector2.z;
        }
        public static bool VectAngle(DataType.BasicDataType.vector x_vVector1, DataType.BasicDataType.vector x_vVector2, ref double x_nAngle)
        {
            double l_CosAngle = Vect3ScalorProd(x_vVector1, x_vVector2) / (VectorNorm(x_vVector1) * VectorNorm(x_vVector2));
            //double l_SinAngle =VectorNorm(Vect3CrossPord(x_vVector1, x_vVector2)) / (VectorNorm(x_vVector1) * VectorNorm(x_vVector2));
            //double l_tanAngle = l_SinAngle / l_CosAngle;
            if (l_CosAngle <= 1 && l_CosAngle >= -1)
            {
                x_nAngle = Radian2Angle(Math.Acos(l_CosAngle));
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public static bool VectAngle(DataType.BasicDataType.vector x_vVector1, DataType.BasicDataType.vector x_vVector2,DataType.BasicDataType.vector x_vVector3, ref double x_nAngle)
        {
            DataType.BasicDataType.vector l_vCenter=new BasicDataType.vector();
            double l_nRadius=new double();
            bool l_bResult=PointTool.CreatCircle(x_vVector1, x_vVector2, x_vVector3, out l_vCenter, out l_nRadius);
            if (l_bResult == true)
            {
                double l_nDistance1 = VectorDistance(x_vVector1, x_vVector2);
                double l_nDistance2 = VectorDistance(x_vVector2, x_vVector3);
                double l_ntheta1 = 2 * Math.Asin(l_nDistance1 / (2 * l_nRadius));
                double l_ntheta2 = 2 * Math.Asin(l_nDistance2 / (2 * l_nRadius));
                double l_ntheta = l_ntheta1 + l_ntheta2;
                x_nAngle = Radian2Angle(l_ntheta);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool VectAngle(DataType.StaubliRobotData.St_PointRx x_pPoint1, DataType.StaubliRobotData.St_PointRx x_pPoint2, DataType.StaubliRobotData.St_PointRx x_pPoint3, ref double x_nAngle)
        {
            BasicDataType.vector l_v1, l_v2, l_v3;
            l_v1 = PointTool.Point2Vector(x_pPoint1);
            l_v2 = PointTool.Point2Vector(x_pPoint2);
            l_v3 = PointTool.Point2Vector(x_pPoint3);           
            bool l_bResult = VectAngle(l_v1, l_v2, l_v3, ref x_nAngle);
            return l_bResult;
        }
        public static double Radian2Angle(double x_nRadian)
        {
            double l_nAngle = 180 * x_nRadian / Math.PI;
            return l_nAngle;
        }
    }
}
