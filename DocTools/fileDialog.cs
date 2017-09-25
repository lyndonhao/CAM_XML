using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileTools
{
    public class fileDialog
    {
        /// <summary>
        /// 获得选择文件打开对话框
        /// </summary>
        /// <returns></returns>
        public string GetOpenFilePath()
        {
            string sFilePath = string.Empty;

            OpenFileDialog openFile = new OpenFileDialog();
            //openFile.Filter = "All files (*.*)|*.*";
            openFile.Filter = "|*.txt*";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                sFilePath = openFile.FileName;
            }

            return sFilePath;
        }

        /// <summary>
        /// 返回保存文件对话框
        /// </summary>
        /// <returns></returns>
        public string GetSaveFilePath()
        {
            string sFilePath = string.Empty;

            SaveFileDialog saveFile = new SaveFileDialog();
            //saveFile.Filter = "All files (*.*)|*.*";
            //saveFile.Filter = "|*.mdb*";
            saveFile.Filter = "|*.dtx*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                sFilePath = saveFile.FileName;
            }

            return sFilePath;
        }


    }
}
