using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ValDataProcess;
using XmlTools;

using FileTools;
//using CAMProcess;



namespace ValToXml
{
    public class ValPro
    {

        #region  实例化
        FileTools.fileDialog ftfd = new FileTools.fileDialog();
        FileTools.fileDirectory ftfdy = new FileTools.fileDirectory();

        CAMToVal.CAMPro camData = new CAMToVal.CAMPro();
        ValDataPro valDC = new ValDataPro();

        //dtx
        List<DataType.ValDataType.Database> databaseTableLs = new List<DataType.ValDataType.Database>();
        List<DataType.ValDataType.Datas> datasTableLs = new List<DataType.ValDataType.Datas>();
        List<DataType.ValDataType.Data> dataTableLs = new List<DataType.ValDataType.Data>();
        List<DataType.ValDataType.Field> fieldTableProcessParaLs = new List<DataType.ValDataType.Field>();
        List<DataType.ValDataType.Field> fieldTableTrajParaLs = new List<DataType.ValDataType.Field>();
        List<DataType.ValDataType.Field> fieldTableTrajStatusLs = new List<DataType.ValDataType.Field>();
        DataType.ValDataType.DataValue dataValueTable = new DataType.ValDataType.DataValue();
        List<DataType.ValDataType.Field> dataFieldLs = new List<DataType.ValDataType.Field>();
        DataType.ValDataType.FieldValue fieldValueTable = new DataType.ValDataType.FieldValue();
        DataType.ValDataType.DataFrame DataFrame = new DataType.ValDataType.DataFrame();
        DataType.ValDataType.DataMdesc DataMdesc = new DataType.ValDataType.DataMdesc();
        DataType.ValDataType.DataJoint DataJoint = new DataType.ValDataType.DataJoint();

        //pjx
        DataType.ValDataType.Database projectTablePjx = new DataType.ValDataType.Database();
        DataType.ValDataType.Data ParametersTablePjx = new DataType.ValDataType.Data();
        DataType.ValDataType.Datas programsTablePjx = new DataType.ValDataType.Datas();
        DataType.ValDataType.Datas databaseTablePjx = new DataType.ValDataType.Datas();
        DataType.ValDataType.Data dataPjxTablePjx = new DataType.ValDataType.Data();
        DataType.ValDataType.Datas librariesTablePjx = new DataType.ValDataType.Datas();
        DataType.ValDataType.Data libraryTablePjx = new DataType.ValDataType.Data();
        DataType.ValDataType.Datas typesTablePjx = new DataType.ValDataType.Datas();
        List<DataType.ValDataType.Data> typeTablePjxLs = new List<DataType.ValDataType.Data>();
        #endregion

        string _strXmlDtx = null;

        public void initTable()
        {
            //dtx
            string[] strDaTableSizes = { camData.nGraphCounts.ToString(), camData.nGraphCounts.ToString(), camData.nGraphCounts.ToString(), "1", 
                                           camData.nGraphCounts.ToString(), "1", "1", "1", "1", "1", "11" };

            databaseTableLs.Clear();
            datasTableLs.Clear();
            dataTableLs.Clear();
            fieldTableProcessParaLs.Clear();
            fieldTableTrajParaLs.Clear();
            fieldTableTrajStatusLs.Clear();

            typeTablePjxLs.Clear();

            

            valDC.DatabasePar(ref databaseTableLs);
            valDC.DatasPar(ref datasTableLs);
            valDC.DataPar(strDaTableSizes, ref dataTableLs);
            valDC.FieldPar(ref fieldTableProcessParaLs, ref fieldTableTrajParaLs, ref fieldTableTrajStatusLs);
            valDC.DataValuePar(ref dataValueTable);
            valDC.FieldValuePar(ref fieldValueTable);
            valDC.DataFramePar(ref DataFrame);
            valDC.DataMdescPar(ref DataMdesc);
            valDC.DataJointPar(ref DataJoint);

            //pjx
            valDC.ProjectParPjx(ref projectTablePjx);
            valDC.ParametersParPjx(ref ParametersTablePjx);
            valDC.ProgramsParPjx(ref programsTablePjx);
            valDC.DatabaseParPjx(ref databaseTablePjx);
            valDC.DataParPjx(_strXmlDtx, ref dataPjxTablePjx);
            valDC.LibrariesParPjx(ref librariesTablePjx);
            valDC.LibraryParPjx(ref libraryTablePjx);
            valDC.TypesParPjx(ref typesTablePjx);
            valDC.TypeParPjx(ref typeTablePjxLs);
        }

        #region  创建文件流程
        /// <summary>
        /// 创建文件（包含数据处理、pjx、dtx文件）
        /// </summary>
        /// <param name="_sPaths">需要处理的文件路径</param>
        /// <param name="_frame">frame</param>
        /// <param name="_tool">tool</param>
        public bool createValXmlFiles(string[] _strArr, ref string _frame, ref string _tool)
        {
            bool bFlag = false;

            

            try
            {
                //传进的frame和tool索引值
                camData._frameIndexOut = _frame;
                camData._toolIndexOut = _tool;

                //CAM数据处理
                bool blResult = camData.CamDataProcess(_strArr);
                if (!blResult) MessageBox.Show("数据中存在空字符！");

                //获取保存文件路径并创建存放文件的文件夹
                string strXmlPath = ftfd.GetSaveFilePath();
                string _strXmlPath = ftfdy.createDirectory(strXmlPath);

                string _strXmlPathdtx = _strXmlPath + ".dtx";

                string _strXmlPathpjx = _strXmlPath + ".pjx";
                _strXmlDtx = _strXmlPath.Substring(_strXmlPath.LastIndexOf("\\") + 1) + ".dtx";

                //创建Dtx文件
                initTable();
                dataDtx(_strXmlPathdtx);
                
                

                //创建Pjx文件              
                dataPjx(_strXmlPathpjx);

                //frame和tool值
                _frame = camData._Frame;
                _tool = camData._Tool;

                bFlag = true;
            }
            catch (Exception ex)
            {
                bFlag = false;
                MessageBox.Show(ex.Message);
            }

            return bFlag;
        }
        #endregion

        #region  创建dtx、pjx xml表文件
        /// <summary>
        /// data表及下一级内容,获取Datas表下Data表的数量，循环打印出Data表内容
        /// </summary>
        //private void dataDtx(string strXmlPath)
        //{
        //    //获取保存文件路径
        //    string _strXmlPath = strXmlPath + ".dtx";

        //    //创建XML文件
        //    XmlClass.createXmlFile(_strXmlPath);

        //    //创建声明及根表
        //    XmlClass.createTable(valDC.strRootTableName, valDC.strDbeAttributeName, valDC.strDbeAttributeValue);

        //    //Datas表（现只有一个）
        //    for (int _intCount1 = 0; _intCount1 < valDC.strDasTableNames.Length; _intCount1++)
        //    {
        //        //创建Datas表
        //        XmlClass.createTable(valDC.strDasFileName);
        //    }

        //    //strDaTableSizes：获取Data表Size值
        //    string[] strDaTableSizes = { camData.nGraphCounts.ToString(), camData.nGraphCounts.ToString(), camData.nGraphCounts.ToString(), "1", 
        //                                   camData.nGraphCounts.ToString(), "1", "1", "1", "1", "1", "11" };

        //    for (int _intDaCount = 0; _intDaCount < dataTableLs.Count; _intDaCount++)
        //    {
        //        //strDaAttributeValue：获取Data表name，access，xsi:type，type，size
        //        string[] strDaAttributeValue = { valDC.strDaTableNames[_intDaCount], valDC.strPub, valDC.strArr, 
        //                                           valDC.strDaTableTypes[_intDaCount], strDaTableSizes[_intDaCount] };
        //        //创建表
        //        XmlClass.createTable(valDC.strDaFileName, valDC.strDaAttributeName, strDaAttributeValue);
        //        //不同数据类型的下一级
        //        //tableTypeOuter(valDC.strDaTableNames[_intDaCount], strDaTableSizes[_intDaCount]);   //按表名判断
        //        //创建表尾符
        //        XmlClass.endTable();
        //    }

        //    XmlClass.endTable();
        //    XmlClass.closeTable();

        //}


        //改写

        public void dataDtx(string strXmlPath)
        {
            //string _strXmlPath = strXmlPath + ".dtx";

            //创建XML文件
            XmlTool.createXmlFile(strXmlPath);

            #region  创建database表
            for(int iDatabaseTable = 0; iDatabaseTable < databaseTableLs.Count; iDatabaseTable++ )
            {
                string[] a0 = new string[] { databaseTableLs[iDatabaseTable].xsi, databaseTableLs[iDatabaseTable].ns };
                string[] b0 = new string[] { databaseTableLs[iDatabaseTable].xsiValue, databaseTableLs[iDatabaseTable].nsValue };
                XmlTool.createTable(databaseTableLs[iDatabaseTable].tableName, a0, b0);

                #region 创建datas表
                for (int iDatasTable = 0; iDatasTable < datasTableLs.Count; iDatasTable++) //DataType.ValDataType.Datas s1 in datasTableLs
                {

                    XmlTool.createTable(datasTableLs[iDatasTable].tableName);

                    #region  创建data表
                    for (int iDataTable = 0; iDataTable < dataTableLs.Count; iDataTable++)//DataType.ValDataType.Data s2 in dataTableLs
                    {
                        string[] a2 = new string[] { dataTableLs[iDataTable].attName, dataTableLs[iDataTable].attAccess, dataTableLs[iDataTable].attXsi,
                            dataTableLs[iDataTable].attType, dataTableLs[iDataTable].attSize };
                        string[] b2 = new string[] { dataTableLs[iDataTable].attNameValue, dataTableLs[iDataTable].attAccessValue, dataTableLs[iDataTable].attXsiValue, 
                            dataTableLs[iDataTable].attTypeValue, dataTableLs[iDataTable].attSizeValue };

                        XmlTool.createTable(dataTableLs[iDataTable].tableName, a2, b2);

                        #region  创建Value-field-Value表

                        tableTypeOuter(dataTableLs[iDataTable].attNameValue, iDatasTable, dataTableLs[iDataTable].attSizeValue);

                        #endregion

                        XmlTool.endTable();
                    }
                    #endregion

                    //XmlClass.endTable();
                }
                #endregion

                XmlTool.endTable();
            }
            #endregion

            XmlTool.closeTable();
        }


        /// <summary>
        /// 创建pjx文件
        /// </summary>
        /// <param name="strXmlPath">pjx文件路径</param>
        //private void dataPjx(string strXmlPath)
        //{
        //    //获取保存文件路径
        //    string _strXmlPath = strXmlPath + ".pjx";
        //    string _strXmlDtx = strXmlPath.Substring(strXmlPath.LastIndexOf("\\") + 1) + ".dtx";

        //    //创建XML文件
        //    XmlClass.createXmlFile(_strXmlPath);

        //    //创建声明及根表(Project表)
        //    XmlClass.createTable(valDC.strProject, valDC.strXmlnsName, valDC.strXmlnsValue);

        //    //创建Parameters表
        //    XmlClass.createTable(valDC.strParameters, valDC.strParametersName, valDC.strParametersValue);
        //    XmlClass.endTable();

        //    //创建Programs表
        //    XmlClass.createTable(valDC.strPrograms, valDC.strProgramsName, valDC.strProgramsValue);
        //    XmlClass.endTable();

        //    //创建Database表
        //    XmlClass.createTable(valDC.strDatabase, valDC.strDatabaseName, valDC.strDatabaseValue);

        //    //创建Data表
        //    string[] strFileValue = { _strXmlDtx };
        //    XmlClass.createTable(valDC.strData, valDC.strFile, strFileValue);
        //    XmlClass.endTable();
        //    XmlClass.endTable();

        //    //创建Libraries表
        //    XmlClass.createTable(valDC.strLibraries, valDC.strLibrariesName, valDC.strLibrariesValue);

        //    //创建Library表
        //    XmlClass.createTable(valDC.strLibrary, valDC.strLibraryName, valDC.strLibraryValue);
        //    XmlClass.endTable();
        //    XmlClass.endTable();

        //    //创建Types表
        //    XmlClass.createTable(valDC.strTypes, valDC.strTypesName, valDC.strTypesValue);
        //    for (int _intDaCount = 0; _intDaCount < valDC.strTypeName.Length; _intDaCount++)
        //    {
        //        string[] strDaAttributeValue = { valDC.strTypeName[_intDaCount], valDC.strTypeValue[_intDaCount] };
        //        //创建Type表
        //        XmlClass.createTable(valDC.strType, valDC.strTypeTitle, strDaAttributeValue);
        //        XmlClass.endTable();
        //    }

        //    XmlClass.endTable();
        //    XmlClass.closeTable();
        //}

        private void dataPjx(string strXmlPath)
        {
            //获取保存文件路径
            //string _strXmlPath = strXmlPath + ".pjx";
            //string _strXmlDtx = strXmlPath.Substring(strXmlPath.LastIndexOf("\\") + 1) + ".dtx";

            //创建XML文件
            XmlTool.createXmlFile(strXmlPath);

            //创建声明及根表(Project表)
            XmlTool.createTable(projectTablePjx.tableName, projectTablePjx.ns, projectTablePjx.nsValue);
            

            //创建Parameters表
            string[] strParameters = { ParametersTablePjx.attName, ParametersTablePjx.attXsi, ParametersTablePjx.attType };
            string[] strParametersValue = { ParametersTablePjx.attNameValue, ParametersTablePjx.attXsiValue, ParametersTablePjx.attTypeValue };
            XmlTool.createTable(ParametersTablePjx.tableName, strParameters, strParametersValue);
            XmlTool.endTable();

            //创建Programs表
            XmlTool.createTable(programsTablePjx.tableName);
            XmlTool.endTable();

            //创建Database表
            XmlTool.createTable(databaseTablePjx.tableName);

            //创建Data表
            //string[] strFileValue = { _strXmlDtx };
            XmlTool.createTable(dataPjxTablePjx.tableName, dataPjxTablePjx.attName, dataPjxTablePjx.attNameValue);//
            XmlTool.endTable();
            XmlTool.endTable();

            //创建Libraries表
            XmlTool.createTable(librariesTablePjx.tableName);

            //创建Library表
            string[] strLibrary = { libraryTablePjx.attName, libraryTablePjx.attAccess, libraryTablePjx.attXsi, libraryTablePjx.attType };
            string[] strLibraryValue = { libraryTablePjx.attNameValue, libraryTablePjx.attAccessValue, libraryTablePjx.attXsiValue, libraryTablePjx.attTypeValue };
            XmlTool.createTable(libraryTablePjx.tableName, strLibrary, strLibraryValue);
            XmlTool.endTable();
            XmlTool.endTable();

            //创建Types表
            XmlTool.createTable(typesTablePjx.tableName);
            for (int _intTypeCount = 0; _intTypeCount < typeTablePjxLs.Count; _intTypeCount++)
            {
                string[] strType = { typeTablePjxLs[_intTypeCount].attName, typeTablePjxLs[_intTypeCount].attAccess };
                string[] strTypeValue = { typeTablePjxLs[_intTypeCount].attNameValue, typeTablePjxLs[_intTypeCount].attAccessValue };
                //创建Type表
                XmlTool.createTable(typeTablePjxLs[_intTypeCount].tableName, strType, strTypeValue);
                XmlTool.endTable();
            }

            XmlTool.endTable();
            XmlTool.closeTable();
        }


        #endregion

        #region   数据类型处理
        /// <summary>
        /// 按数据名判断（外表）
        /// </summary>
        /// <param name="tType">数据类型</param>
        /// <param name="tSize">大小</param>
        private void tableTypeOuter(string tType,int tDatasSize ,string tDataSize)
        {
            if (tDataSize != "")
            {
                for (int tDataSizefs = 0; tDataSizefs < int.Parse(tDataSize); tDataSizefs++)
                {
                    switch (tType) //tType
                    {
                        case "stProcessPara":  //"stProcessPara" 
                            fieldTable("stProcessPara",tDatasSize ,tDataSizefs);
                            break;
                        case "stTrajPara":
                            fieldTable("stTrajPara", tDatasSize,tDataSizefs);
                            break;
                        case "stTrajStatus":
                            fieldTable("stTrajStatus", tDatasSize, tDataSizefs);
                            break;
                        case "fTrack":
                            fraPrxVal(tDataSizefs.ToString(), camData.TrackLs[tDataSizefs].ftrack.x.ToString(), camData.TrackLs[tDataSizefs].ftrack.y.ToString(),
                                camData.TrackLs[tDataSizefs].ftrack.z.ToString(), camData.TrackLs[tDataSizefs].ftrack.rx.ToString(),
                                camData.TrackLs[tDataSizefs].ftrack.ry.ToString(), camData.TrackLs[tDataSizefs].ftrack.rz.ToString(),
                                camData.TrackLs[tDataSizefs].ftrack.fatherId.ToString());
                            break;
                        case "nom_speed":
                            mxdc(tDataSizefs.ToString(), camData.SpeedLs[tDataSizefs].nom_speed.accel.ToString(), camData.SpeedLs[tDataSizefs].nom_speed.vel.ToString(),
                                camData.SpeedLs[tDataSizefs].nom_speed.decel.ToString(), camData.SpeedLs[tDataSizefs].nom_speed.rmax.ToString(),
                                camData.SpeedLs[tDataSizefs].nom_speed.tmax.ToString(), camData.SpeedLs[tDataSizefs].nom_speed.leave.ToString(),
                                camData.SpeedLs[tDataSizefs].nom_speed.reach.ToString());
                            break;
                        case "nFrameIndex":
                            typeBlStrNum(tDataSizefs.ToString(), camData.FrameIndexLs[tDataSizefs].frameIndex.ToString());
                            break;
                        case "nIndexGun":
                            typeBlStrNum(tDataSizefs.ToString(), camData.IndexGunLs[tDataSizefs].indexGun.ToString());
                            break;
                        case "nLaserDelay":
                            typeBlStrNum(tDataSizefs.ToString(), camData.LaserDelayLs[tDataSizefs].laserDelay.ToString());
                            break;
                        case "nPenetrate":
                            typeBlStrNum(tDataSizefs.ToString(), camData.PenetrateLs[tDataSizefs].penetrate.ToString());
                            break;
                        case "ndistance":
                            typeBlStrNum(tDataSizefs.ToString(), camData.DistanceLs[tDataSizefs].distance.ToString());
                            break;
                        case "pCutting":
                            fraPrxVal(tDataSizefs.ToString(), camData.CuttingLs[tDataSizefs].cutting.x.ToString(), camData.CuttingLs[tDataSizefs].cutting.y.ToString(),
                                camData.CuttingLs[tDataSizefs].cutting.z.ToString(), camData.CuttingLs[tDataSizefs].cutting.rx.ToString(),
                                camData.CuttingLs[tDataSizefs].cutting.ry.ToString(), camData.CuttingLs[tDataSizefs].cutting.rz.ToString(),
                                camData.CuttingLs[tDataSizefs].cutting.fatherId.ToString());
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 按数据名判断（内表）
        /// </summary>
        /// <param name="tType">数据名</param>
        /// <param name="tSize">大小</param>
        private void tableTypeInner(string tType, string tSize, int _tSizefs)
        {
            if (tSize != "")
            {
                for (int tSizefs = 0; tSizefs < int.Parse(tSize); tSizefs++)
                {
                    //string tSizefs = i.ToString();
                    //int tSizefs = i;
                    switch (tType)
                    {
                        //内表（按生成数据格式顺序排列的）

                        //stProcessPara
                        case "bToStop":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].bToStop.ToString());
                            break;
                        case "nFollowDistance":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nFollowDistance.ToString());
                            break;
                        case "nFreqDistance":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nFreqDistance.ToString());
                            break;
                        case "nGasDistance":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nGasDistance.ToString());
                            break;
                        case "nGasOn":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nGasOn.ToString());
                            break;
                        case "nGasPressure":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nGasPressure.ToString());
                            break;
                        case "nJerk":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nJerk.ToString());
                            break;
                        case "nLaserFrequency":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nLaserFrequency.ToString());
                            break;
                        case "nLaserIndex":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nLaserIndex.ToString());
                            break;
                        case "nLaserOn":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nLaserOn.ToString());
                            break;
                        case "nLaserPower":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nLaserPower.ToString());
                            break;
                        case "nLaserProgNum":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nLaserProgNum.ToString());
                            break;
                        case "nPWMDistance":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nPWMDistance.ToString());
                            break;
                        case "nPWMPercent":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nPWMPercent.ToString());
                            break;
                        case "nPenetrate":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nPenetrate.ToString());
                            break;
                        case "nStop":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nStop.ToString());
                            break;
                        case "nTraceCalNum":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nTraceCalNum.ToString());
                            break;
                        case "nTraceOn":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nTraceOn.ToString());
                            break;
                        case "nTracePosition":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nTracePosition.ToString());
                            break;
                        case "nTraceZNNum":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].nTraceZNNum.ToString());
                            break;
                        case "sAction":
                            typeBlStrNum(tSizefs.ToString(), camData.ProcessParaLs[_tSizefs].sAction.ToString());
                            break;

                        //stTrajPara
                        case "bInorOutside":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].bInorOutside.ToString());
                            break;
                        case "bQuick_Move":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].bQuick_Move.ToString());
                            break;
                        case "nCompensation":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nCompensation.ToString());
                            break;
                        case "nCutInAngle":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nCutInAngle.ToString());
                            break;
                        case "nCutInRadius":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nCutInRadius.ToString());
                            break;
                        case "nCutOutAngle":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nCutOutAngle.ToString());
                            break;
                        case "nCutOutRadius":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nCutOutRadius.ToString());
                            break;
                        case "nCutType":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nCutType.ToString());
                            break;
                        case "nEdge":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nEdge.ToString());
                            break;
                        case "nInclinedAngle":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nInclinedAngle.ToString());
                            break;
                        case "nLeadInLength":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nLeadInLength.ToString());
                            break;
                        case "nLeadOutLength":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nLeadOutLength.ToString());
                            break;
                        case "nLength":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nLength.ToString());
                            break;
                        case "nMultiple":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nMultiple.ToString());
                            break;
                        case "nOverloop":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nOverloop.ToString());
                            break;
                        case "nRadius":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nRadius.ToString());
                            break;
                        case "nRotation":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nRotation.ToString());
                            break;
                        case "nWidth":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajParaLs[_tSizefs].nWidth.ToString());
                            break;

                        //stTrajStatus
                        case "bAutoSet":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].bAutoSet.ToString());//或用false或用0，用0时会报错（12）
                            break;
                        case "bBypass":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].bBypass.ToString());//或用false或用0，用0时会报错（12）
                            break;
                        case "jJoints":
                            joRx(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].jJoints.j1.ToString(), camData.TrajStatusLs[_tSizefs].jJoints.j2.ToString(),
                                camData.TrajStatusLs[_tSizefs].jJoints.j3.ToString(), camData.TrajStatusLs[_tSizefs].jJoints.j4.ToString(),
                                camData.TrajStatusLs[_tSizefs].jJoints.j5.ToString(), camData.TrajStatusLs[_tSizefs].jJoints.j6.ToString());
                            break;
                        case "nActionIndex":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nActionIndex.ToString());
                            break;
                        case "nAdvanceIndex":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nAdvanceIndex.ToString());
                            break;
                        case "nIndex":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nIndex.ToString());
                            break;
                        case "nJointMode":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nJointMode.ToString());
                            break;
                        case "nMoveType":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nMoveType.ToString());
                            break;
                        case "nParameterIndex":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nParameterIndex.ToString());
                            break;
                        case "nProcessIndex":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nProcessIndex.ToString());
                            break;
                        case "nSpeedIndex":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nSpeedIndex.ToString());
                            break;
                        case "nTcpAction":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].nTcpAction.ToString());
                            break;
                        case "ndistance":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].ndistance.ToString());
                            break;
                        case "pCircle":
                            fraPrxVal(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].pCircle.x.ToString(), camData.TrajStatusLs[_tSizefs].pCircle.y.ToString(),
                                camData.TrajStatusLs[_tSizefs].pCircle.z.ToString(), camData.TrajStatusLs[_tSizefs].pCircle.rx.ToString(),
                                camData.TrajStatusLs[_tSizefs].pCircle.ry.ToString(), camData.TrajStatusLs[_tSizefs].pCircle.rz.ToString(),
                                camData.TrajStatusLs[_tSizefs].pCircle.fatherId.ToString());
                            break;
                        case "pPoints":
                            fraPrxVal(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].pPoints.x.ToString(), camData.TrajStatusLs[_tSizefs].pPoints.y.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPoints.z.ToString(), camData.TrajStatusLs[_tSizefs].pPoints.rx.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPoints.ry.ToString(), camData.TrajStatusLs[_tSizefs].pPoints.rz.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPoints.fatherId.ToString());
                            break;
                        case "pPolygon2":
                            fraPrxVal(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].pPolygon2.x.ToString(), camData.TrajStatusLs[_tSizefs].pPolygon2.y.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPolygon2.z.ToString(), camData.TrajStatusLs[_tSizefs].pPolygon2.rx.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPolygon2.ry.ToString(), camData.TrajStatusLs[_tSizefs].pPolygon2.rz.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPolygon2.fatherId.ToString());
                            break;
                        case "pPolygon3":
                            fraPrxVal(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].pPolygon3.x.ToString(), camData.TrajStatusLs[_tSizefs].pPolygon3.y.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPolygon3.z.ToString(), camData.TrajStatusLs[_tSizefs].pPolygon3.rx.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPolygon3.ry.ToString(), camData.TrajStatusLs[_tSizefs].pPolygon3.rz.ToString(),
                                camData.TrajStatusLs[_tSizefs].pPolygon3.fatherId.ToString());
                            break;
                        case "sComments":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].sComments.ToString());
                            break;
                        case "sInstruction":
                            typeBlStrNum(tSizefs.ToString(), camData.TrajStatusLs[_tSizefs].sInstruction.ToString());
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Field表选择
        /// </summary>
        /// <param name="tTypesf">结构体类型</param>
        private void fieldTable(string tTypesf, int tDatasSizef, int tDataSizef)
        {
            for (int iValueTable = 0; iValueTable < tDatasSizef + 1; iValueTable++)
            {
                //value表：<Value key="">
                XmlTool.createTable(dataValueTable.tableName, dataValueTable.key, tDataSizef.ToString());
                //ProcessPara/TrajPara/TrajStatus类型表
                switch (tTypesf)
                {
                    case "stProcessPara":
                        proPara(tDataSizef);
                        break;
                    case "stTrajPara":
                        traPara(tDataSizef);
                        break;
                    case "stTrajStatus":
                        traStatus(tDataSizef);
                        break;
                }

                XmlTool.endTable();
            }
        }

        /// <summary>
        /// ProcessPara类型
        /// </summary>
        private void proPara(int _tSizef)
        {
            //Field表：<Field name="" xsi:type="" type="" size="">
            for (int _intFdPpaCount = 0; _intFdPpaCount < fieldTableProcessParaLs.Count; _intFdPpaCount++)
            {
                string[] strFdAttribute = { fieldTableProcessParaLs[_intFdPpaCount].attName, fieldTableProcessParaLs[_intFdPpaCount].attXsi,
                    fieldTableProcessParaLs[_intFdPpaCount].attType, fieldTableProcessParaLs[_intFdPpaCount].attSize };
                string[] strFdAttributeValue = { fieldTableProcessParaLs[_intFdPpaCount].attNameValue, fieldTableProcessParaLs[_intFdPpaCount].attXsiValue, 
                                                   fieldTableProcessParaLs[_intFdPpaCount].attTypeValue, fieldTableProcessParaLs[_intFdPpaCount].attSizeValue };
                XmlTool.createTable(fieldTableProcessParaLs[_intFdPpaCount].tableName, strFdAttribute, strFdAttributeValue);
                //不同数据类型的下一级
                tableTypeInner(fieldTableProcessParaLs[_intFdPpaCount].attNameValue, fieldTableProcessParaLs[_intFdPpaCount].attSizeValue, _tSizef);
                XmlTool.endTable();
            }
            //XmlClass.endTable();
        }

        /// <summary>
        /// TrajPara类型
        /// </summary>
        private void traPara(int _tSizef)
        {
            //Field表：<Field name="" xsi:type="" type="" size="">
            for (int _intFdTpaCount = 0; _intFdTpaCount < fieldTableTrajParaLs.Count; _intFdTpaCount++)
            {
                string[] strFdAttribute = {fieldTableTrajParaLs[_intFdTpaCount].attName, fieldTableTrajParaLs[_intFdTpaCount].attXsi, 
                                                   fieldTableTrajParaLs[_intFdTpaCount].attType, fieldTableTrajParaLs[_intFdTpaCount].attSize };
                string[] strFdAttributeValue = { fieldTableTrajParaLs[_intFdTpaCount].attNameValue, fieldTableTrajParaLs[_intFdTpaCount].attXsiValue, 
                                                   fieldTableTrajParaLs[_intFdTpaCount].attTypeValue, fieldTableTrajParaLs[_intFdTpaCount].attSizeValue };
                XmlTool.createTable(fieldTableTrajParaLs[_intFdTpaCount].tableName, strFdAttribute, strFdAttributeValue);
                //不同数据类型的下一级
                tableTypeInner(fieldTableTrajParaLs[_intFdTpaCount].attNameValue, fieldTableTrajParaLs[_intFdTpaCount].attSizeValue, _tSizef);
                XmlTool.endTable();
            }
            //XmlClass.endTable();
        }

        /// <summary>
        /// TrajStatus类型
        /// </summary>
        private void traStatus(int _tSizef)
        {
            //Field表：<Field name="" xsi:type="" type="" size="">
            for (int _intFdTssCount = 0; _intFdTssCount < fieldTableTrajStatusLs.Count; _intFdTssCount++)
            {
                string[] strFdAttribute = { fieldTableTrajStatusLs[_intFdTssCount].attName, fieldTableTrajStatusLs[_intFdTssCount].attXsi, 
                    fieldTableTrajStatusLs[_intFdTssCount].attType, fieldTableTrajStatusLs[_intFdTssCount].attSize };
                string[] strFdAttributeValue = { fieldTableTrajStatusLs[_intFdTssCount].attNameValue, fieldTableTrajStatusLs[_intFdTssCount].attXsiValue, 
                                                   fieldTableTrajStatusLs[_intFdTssCount].attTypeValue, fieldTableTrajStatusLs[_intFdTssCount].attSizeValue };
                XmlTool.createTable(fieldTableTrajStatusLs[_intFdTssCount].tableName, strFdAttribute, strFdAttributeValue);
                //不同数据类型的下一级
                tableTypeInner(fieldTableTrajStatusLs[_intFdTssCount].attNameValue, fieldTableTrajStatusLs[_intFdTssCount].attSizeValue, _tSizef);
                XmlTool.endTable();
            }
            //XmlClass.endTable();
        }

        #endregion

        #region   数据类型
        /// <summary>
        /// bool/string/num类型
        /// </summary>
        /// <param name="tSizeBSN">key值</param>
        /// <param name="Val"></param>
        private void typeBlStrNum(string tSizeBSN, string Val)
        {
            //value表：<Value key="" value="">
            string[] strAttributeValuesBSN = { tSizeBSN, Val };
            string[] strAttributeBSN = { fieldValueTable.key, fieldValueTable.value };
            XmlTool.createTable(fieldValueTable.tableName, strAttributeBSN, strAttributeValuesBSN);
            XmlTool.endTable();

            //xtc.createWhiteSpace();
        }

        /// <summary>
        /// frame/pointRx类型类型（默认）
        /// </summary>
        /// <param name="tSizeFP">key值</param>
        /// <param name="fId"></param>
        private void fraPrx(string tSizeFP, string fId)
        {
            //value表：<Value key="" fatherId="">
            string[] strAttributeValuesFP = { tSizeFP, fId };
            string[] strAttributeFP = { fieldValueTable.key, fieldValueTable.value };
            XmlTool.createTable(fieldValueTable.tableName, strAttributeFP, strAttributeValuesFP);
            XmlTool.endTable();
        }

        /// <summary>
        /// frame/pointRx类型类型(带值)
        /// </summary>
        /// <param name="tSizeFP">key值</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="Rx"></param>
        /// <param name="Ry"></param>
        /// <param name="Rz"></param>
        /// <param name="fId"></param>
        private void fraPrxVal(string tSizeFP, string X, string Y, string Z, string Rx, string Ry, string Rz, string fId)
        {
            //value表：<Value key="" x="" y="" z="" rx="" ry="" rz="" fatherId="">
            string[] strAttributeValuesFP = { tSizeFP, X, Y, Z, Rx, Ry, Rz, fId };
            string[] strAttributeFP = { DataFrame.key, DataFrame.x, DataFrame.y, DataFrame.z, DataFrame.rx, DataFrame.ry, DataFrame.rz, DataFrame.fatherId };
            XmlTool.createTable(DataFrame.tableName, strAttributeFP, strAttributeValuesFP);
            XmlTool.endTable();
        }

        /// <summary>
        /// jointRx类型
        /// </summary>
        /// <param name="tSizeJR">key值</param>
        /// <param name="J1"></param>
        /// <param name="J2"></param>
        /// <param name="J3"></param>
        /// <param name="J4"></param>
        /// <param name="J5"></param>
        /// <param name="J6"></param>
        private void joRx(string tSizeJR, string J1, string J2, string J3, string J4, string J5, string J6)
        {
            //value表：<Value key="" j1="" j2="" j3="" j4="" j5="" j6="">
            string[] strAttributeValuesJR = { tSizeJR, J1, J2, J3, J4, J5, J6 };
            string[] strAttributeJR = { DataJoint.key,DataJoint.j1, DataJoint.j2, DataJoint.j3, DataJoint.j4, DataJoint.j5, DataJoint.j6};
            XmlTool.createTable(DataJoint.tableName, strAttributeJR, strAttributeValuesJR);
            XmlTool.endTable();
        }

        /// <summary>
        /// mdesc类型
        /// </summary>
        /// <param name="tSizeMC">key值</param>
        /// <param name="Acc">加速度</param>
        /// <param name="Vel">平均速度</param>
        /// <param name="Dec">减速度</param>
        /// <param name="Rm"></param>
        /// <param name="Tm"></param>
        /// <param name="Lea">R角</param>
        /// <param name="Rea">R角</param>
        private void mxdc(string tSizeMC, string Acc, string Vel, string Dec, string Rm, string Tm, string Lea, string Rea)
        {
            //value表：<Value key="" accel="" vel="" decel="" rmax="" tmax="" leave="" reach="">
            string[] strAttributeValuesMC = { tSizeMC, Acc, Vel, Dec, Rm, Tm, Lea, Rea };
            string[] strAttributeJR = { DataMdesc.key, DataMdesc.accel, DataMdesc.vel, DataMdesc.decel, DataMdesc.rmax, DataMdesc.tmax, DataMdesc.leave, DataMdesc.reach };
            XmlTool.createTable(DataMdesc.tableName, strAttributeJR, strAttributeValuesMC);
            XmlTool.endTable();
        }

        #endregion


        










    }
}

