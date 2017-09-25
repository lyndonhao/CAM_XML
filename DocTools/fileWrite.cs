using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FileTools
{
   public class fileWrite
    {
       /// <summary>
       /// 输出txt文件
       /// </summary>
       /// <param name="x_sOutString"></param>
       /// <param name="x_sPath"></param>
        public bool WriteTxt(string[] x_sOutString,string x_sPath)
        {
            bool l_bResult = false;
            if (x_sOutString != null)   
            {
                StreamWriter sw = new StreamWriter(x_sPath);
                try 
                {
                    foreach (string s in x_sOutString) { sw.WriteLine(s); } 
                    //sw.WriteLine("cc);
                    sw.Close();
                    l_bResult = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error:WriteTxt exception: " + ex.Message);
                }
            }
            else
            {
                l_bResult = false;
            }
            return l_bResult;
            
            
        }  
    }
}
