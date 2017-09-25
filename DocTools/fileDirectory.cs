using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileTools
{
    public class fileDirectory
    {
        /// <summary>
        /// 返回创建文件夹的路径
        /// </summary>
        /// <param name="sPath">文件路径</param>
        /// <returns></returns>
        public string createDirectory(string sPath)
        {
            string _sPath = null;
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);

                _sPath = sPath + "\\" + sPath.Substring(sPath.LastIndexOf("\\") + 1);
            }
            return _sPath;
        }


    }
}
