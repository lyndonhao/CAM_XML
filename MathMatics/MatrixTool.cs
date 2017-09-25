using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMatics
{
    public static class MatrixTool
    {

        public static bool MatrixDotMultiply(double[,] x_arrayLeft, double[] x_arrayRight, out double[] x_arrayOut)
        {
            bool _bok = false;
            //x_arrayOut = null;
            ///左矩阵列须等于右矩阵行才可以点乘
            ///
            if (x_arrayLeft.GetLength(1) == x_arrayRight.GetLength(0))
            {
                double[] _resultValue = new double[x_arrayLeft.GetLength(0)];
                for (int i = 0; i < x_arrayLeft.GetLength(0); i++)
                {
                    for (int j = 0; j < x_arrayRight.GetLength(0); j++)
                    {
                        _resultValue[i] += x_arrayLeft[i, j] * x_arrayRight[j];
                    }
                    
                  // PointTool.IsOnCircle() 
                }
                x_arrayOut = _resultValue;
                _bok = true;
            }
            else
            {
                x_arrayOut = null;
                _bok = false;
            }
            return _bok;
        }
        /// <summary>
        /// 矩阵乘
        /// </summary>
        /// <param name="x_arrayLeft">左矩阵</param>
        /// <param name="x_arrayRight">右矩阵</param>
        /// <param name="x_arrayOut">结果</param>
        /// <returns>是否成功计算</returns>
        public static bool MatrixDotMultiply(double[,] x_arrayLeft, double[,] x_arrayRight, out double[,] x_arrayOut)
        {
            bool _bok = false;
            //x_arrayOut = null;
            ///左矩阵列须等于右矩阵行才可以点乘
            ///
            if (x_arrayLeft.GetLength(1) == x_arrayRight.GetLength(0))
            {
                double[,] _resuleValue = new double[x_arrayLeft.GetLength(0)
                    , x_arrayRight.GetLength(1)
                    ];
                for (int i = 0; i < x_arrayLeft.GetLength(0); i++)
                {
                    for (int j = 0; j < x_arrayRight.GetLength(1); j++)
                    {
                        for (int l = 0; l < x_arrayLeft.GetLength(1); l++)
                        {
                            _resuleValue[i, j] += x_arrayLeft[i, l] * x_arrayRight[l, j];
                        }
                    }
                }
                x_arrayOut = _resuleValue;
                _bok = true;
            }
            else
            {
                x_arrayOut = null;
                _bok = false;
            }
            return _bok;
        }

        public static double[,] MatrixDotMultiply(double[,] x_arrayLeft, double[,] x_arrayRight)
        {
            double[,] _arrayOut = new double[4, 4];
            //x_arrayOut = null;
            ///左矩阵列须等于右矩阵行才可以点乘
            ///
            if (x_arrayLeft.GetLength(1) == x_arrayRight.GetLength(0))
            {
                double[,] _resuleValue = new double[x_arrayLeft.GetLength(0)
                    , x_arrayRight.GetLength(1)
                    ];
                for (int i = 0; i < x_arrayLeft.GetLength(0); i++)
                {
                    for (int j = 0; j < x_arrayRight.GetLength(1); j++)
                    {
                        for (int l = 0; l < x_arrayLeft.GetLength(1); l++)
                        {
                            _resuleValue[i, j] += x_arrayLeft[i, l] * x_arrayRight[l, j];
                        }
                    }
                }
                _arrayOut = _resuleValue;


            }
            else
            {
                _arrayOut = null;

            }
            return _arrayOut;
        }
        #region 矩阵转置
        /// <summary>
        /// 转置矩阵
        /// </summary>
        /// <param name="x_inputArray">输入矩阵</param>
        /// <returns>返回矩阵</returns>
        public static double[,] MatrixTranspose(double[,] x_inputArray)
        {
            double[,] _value = new double[x_inputArray.GetLength(1)
                , x_inputArray.GetLength(0)
                ];
            for (int i = 0; i < x_inputArray.GetLength(0); i++)
            {
                for (int j = 0; j < x_inputArray.GetLength(1); j++)
                {
                    _value[i, j] = x_inputArray[j, i];
                }
            }
            return _value;
        }
        #endregion
        /// <summary>
        /// 矩阵行列式的值 test success
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double MatrixSurplus(double[,] a)
        {
            int i, j, k, p, r, m, n;
            m = a.GetLength(0);
            n = a.GetLength(1);
            double X, temp = 1, temp1 = 1, s = 0, s1 = 0;

            if (n == 2)
            {
                for (i = 0; i < m; i++)
                    for (j = 0; j < n; j++)
                        if ((i + j) % 2 > 0) temp1 *= a[i, j];
                        else temp *= a[i, j];
                X = temp - temp1;
            }
            else
            {
                for (k = 0; k < n; k++)
                {
                    for (i = 0, j = k; i < m && j < n; i++, j++)
                        temp *= a[i, j];
                    if (m - i > 0)
                    {
                        for (p = m - i, r = m - 1; p > 0; p--, r--)
                            temp *= a[r, p - 1];
                    }
                    s += temp;
                    temp = 1;
                }

                for (k = n - 1; k >= 0; k--)
                {
                    for (i = 0, j = k; i < m && j >= 0; i++, j--)
                        temp1 *= a[i, j];
                    if (m - i > 0)
                    {
                        for (p = m - 1, r = i; r < m; p--, r++)
                            temp1 *= a[r, p];
                    }
                    s1 += temp1;
                    temp1 = 1;
                }

                X = s - s1;
            }
            return X;
        }

        /// <summary>
        /// 矩阵的逆 test success
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool MatrixInverse(double[,] a, ref double[,] b)
        {
            double X = MatrixSurplus(a);
            if (X == 0) return false;
            X = 1 / X;

            double[,] B = new double[a.GetLength(0), a.GetLength(1)];
            double[,] SP = new double[a.GetLength(0), a.GetLength(1)];
            double[,] AB = new double[a.GetLength(0), a.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    for (int m = 0; m < a.GetLength(0); m++)
                        for (int n = 0; n < a.GetLength(1); n++)
                            B[m, n] = a[m, n];
                    {
                        for (int x = 0; x < a.GetLength(1); x++)
                            B[i, x] = 0;
                        for (int y = 0; y < a.GetLength(0); y++)
                            B[y, j] = 0;
                        B[i, j] = 1;
                        SP[i, j] = MatrixSurplus(B);
                        AB[i, j] = X * SP[i, j];
                    }
                }
            b = MatrixTranspose(AB);

            return true;
        }       
    }
}
