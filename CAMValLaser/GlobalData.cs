using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAMValLaser
{
   public static class GlobalData
    {
       public static string[] sOutString=null;
       public static int sOldStringLength = new int();
       public static List<string> ListHistory=new List<string>();
       public static string[] sError = new string[3]
       {
           "错误信息：精度设置不能为空。",
           "错误信息：未处理相关文件。",
           "错误信息: 历史记录为空。"
       };
    }
}
