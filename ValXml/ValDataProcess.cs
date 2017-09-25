using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValToXml
{
    public class ValDataPro
    {
        

        #region   dtx文件参数
        //Database表属性
        //public string strRootTableName = "Database";
        //public string[] strDbeAttributeName = { "xmlns:xsi", "xmlns" };
        //public string[] strDbeAttributeValue = { "http://www.w3.org/2001/XMLSchema-instance", "http://www.staubli.com/robotics/VAL3/Data/2" };

        //Datas表属性
        //public string strDasFileName = "Datas";
        //public string[] strDasAttributeName = { };
        //public string[] strDasAttributeValue = { };
        //public string[] strDasTableNames = { "" };

        //Data表属性
        //public string strDaFileName = "Data";
        //public string[] strDaAttributeName = { "name", "access", "xsi:type", "type", "size" };
        public string[] strDaTableNames = { "stProcessPara", "stTrajPara", "stTrajStatus", "fTrack", "nom_speed", "nFrameIndex", "nIndexGun", "nLaserDelay", 
                                                     "nPenetrate", "ndistance", "pCutting" };
        public string[] strDaTableTypes = { "ProcessPara", "TrajPara", "TrajStatus", "frame", "mdesc", "num", "num", "num", "num", "num", "pointRx" };    
        //public string[] strDaTableSizes = { "", "", "", "", "", "", "", "", "", "", ""};

        //Field表属性ProcessPara
        //public string strFdFileName = "Field";
        //public string[] strFdAttributeName = { "name", "xsi:type", "type", "size" };
        public string[] strFdPpaTableNames = { "bToStop", "nFollowDistance", "nFreqDistance", "nGasDistance", "nGasOn", "nGasPressure", "nJerk", 
                                                        "nLaserFrequency", "nLaserIndex", "nLaserOn", "nLaserPower", "nLaserProgNum", "nPWMDistance", "nPWMPercent", 
                                                        "nPenetrate", "nStop", "nTraceCalNum", "nTraceOn", "nTracePosition", "nTraceZNNum", "sAction" };
        public string[] strFdPpaTableTypes = { "bool", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", 
                                                        "num", "num", "num", "num", "num", "string" };
        public string[] strFdPpaTableSizes = { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "8" };        
        //Field表属性TrajPara
        public string[] strFdTpaTableNames = { "bInorOutside", "bQuick_Move", "nCompensation", "nCutInAngle", "nCutInRadius", "nCutOutAngle", "nCutOutRadius", 
                                                        "nCutType", "nEdge", "nInclinedAngle", "nLeadInLength", "nLeadOutLength", "nLength", "nMultiple", "nOverloop", 
                                                        "nRadius", "nRotation", "nWidth" };
        public string[] strFdTpaTableTypes = { "bool", "bool", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", 
                                                        "num", "num", "num" };
        public string[] strFdTpaTableSizes = { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };        
        //Field表属性TrajStatus
        public string[] strFdTssTableNames = { "bAutoSet", "bBypass", "jJoints", "nActionIndex", "nAdvanceIndex", "nIndex", "nJointMode", "nMoveType", 
                                                        "nParameterIndex", "nProcessIndex", "nSpeedIndex", "nTcpAction", "ndistance", "pCircle", "pPoints", 
                                                        "pPolygon2", "pPolygon3", "sComments", "sInstruction" };
        public string[] strFdTssTableTypes = { "bool", "bool", "jointRx", "num", "num", "num", "num", "num", "num", "num", "num", "num", "num", "pointRx", 
                                                        "pointRx", "pointRx", "pointRx", "string", "string" };
        public string[] strFdTssTableSizes = { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };

        //属性值（通用）
        //public string strArr = "array";
        //public string strPub = "public";

        //value表头（通用）
        //public string strValFileNames = "Value";
        //键值（通用）
        //public string strKeyAttributeNames = "key";
        //public string strKeyAttributeValues = "";

        
        //数据类型
        //键值num/string/bool
        //public string[] strAttributeNamesBSN = { "key", "value" };
        //public string[] strAttributeValuesBSN = { "", "" };
        //键值frame/pointRx
        //public string[] strAttributeNamesFP = { "key", "fatherId" };
        //public string[] strAttributeNamesFPVal = { "key", "x", "y", "z", "rx", "ry", "rz", "fatherId" };
        //public string[] strAttributeValuesFP = { "", "" };
        //键值jointRx
        //public string[] strAttributeNamesJR = { "key", "j1", "j2", "j3", "j4", "j5", "j6" };
        //public string[] strAttributeValuesJR = { "", "","","","","","" };
        //键值mdesc
        //public string[] strAttributeNamesMC = { "key", "accel", "vel", "decel", "rmax", "tmax", "leave", "reach" };
        //public string[] strAttributeValuesMC = { "", "", "", "", "", "", "", "" };
        //键值pointRx
        //public string[] strAttributeNamesPR = { "key", "x", "y", "z", "rx", "ry", "rz", "fatherId" };
        //public string[] strAttributeValuesPR = { "", "", "", "", "", "", "", "" };

        //新类型（字符串版）
        //public string _TrajInfor = "";

        #endregion

        
        #region   pjx文件参数
        //Project表
        //public string strProject = "Project";
        //public string[] strXmlnsName = { "xmlns" };
        //public string[] strXmlnsValue = { "http://www.staubli.com/robotics/VAL3/Project/3" };

        //Parameters表
        //public string strParameters = "Parameters";
        //public string[] strParametersName = { "version", "stackSize", "millimeterUnit" };
        //public string[] strParametersValue = { "s7.9", "5000", "true" };

        //Programs表
        //public string strPrograms = "Programs";
        //public string[] strProgramsName = { };
        //public string[] strProgramsValue = { };

        //Database表
        //public string strDatabase = "Database";
        //public string[] strDatabaseName = { };
        //public string[] strDatabaseValue = { };

        //Data表
        //public string strData = "Data";
        //public string[] strFile = { "file" };

        //Libraries表
        //public string strLibraries = "Libraries";
        //public string[] strLibrariesName = { };
        //public string[] strLibrariesValue = { };

        //Library表
        //public string strLibrary = "Library";
        //public string[] strLibraryName = { "alias", "path", "autoload", "password" };
        //public string[] strLibraryValue = { "io", "Disk://io/io.pjx", "true", "" };

        //Types表
        //public string strTypes = "Types";
        //public string[] strTypesName = { };
        //public string[] strTypesValue = { };

        //Type表
        //public string strType = "Type";
        //public string[] strTypeTitle = { "name", "path" };
        public string[] strTypeName = { "ProcessPara", "TrajPara", "TrajStatus" };
        public string[] strTypeValue = { "Disk://Structure/ProcessPara/ProcessPara.pjx", "Disk://Structure/TrajPara/TrajPara.pjx", 
                                                  "Disk://Structure/TrajStatus/TrajStatus.pjx" };

        #endregion

        


        #region  dtx赋值

        public void DatabasePar(ref List<DataType.ValDataType.Database> databaseTableLs)
        {
            DataType.ValDataType.Database databaseTable = new DataType.ValDataType.Database();

            databaseTable.tableName = "Database";

            databaseTable.xsi = "xmlns:xsi";
            databaseTable.ns = "xmlns";

            databaseTable.xsiValue = "http://www.w3.org/2001/XMLSchema-instance";
            databaseTable.nsValue = "http://www.staubli.com/robotics/VAL3/Data/2";


            databaseTableLs.Add(databaseTable);
            
        }

        public void DatasPar(ref List<DataType.ValDataType.Datas> datasTableLs)
        {
            DataType.ValDataType.Datas datasTable = new DataType.ValDataType.Datas();

            datasTable.tableName = "Datas";

            datasTableLs.Add(datasTable);
        }

        public void DataPar(string[] strDaTableSizes, ref List<DataType.ValDataType.Data> dataTableLs)
        {
            
            for (int i = 0; i < strDaTableNames.Length;i++ )
            {
                DataType.ValDataType.Data dataTable = new DataType.ValDataType.Data();

                dataTable.tableName = "Data";

                dataTable.attName = "name";
                dataTable.attAccess = "access";
                dataTable.attXsi = "xsi:type";
                dataTable.attType = "type";
                dataTable.attSize = "size";

                dataTable.attNameValue = strDaTableNames[i];
                dataTable.attAccessValue = "public";
                dataTable.attXsiValue = "array";
                dataTable.attTypeValue = strDaTableTypes[i];
                dataTable.attSizeValue = strDaTableSizes[i];


                dataTableLs.Add(dataTable);
            }
        }

        public void FieldPar(ref List<DataType.ValDataType.Field> fieldTableProcessParaLs, ref List<DataType.ValDataType.Field> fieldTableTrajParaLs, ref List<DataType.ValDataType.Field> fieldTableTrajStatusLs)
        {
            FieldPar1(strFdPpaTableNames, strFdPpaTableTypes, strFdPpaTableSizes, ref fieldTableProcessParaLs);

            FieldPar1(strFdTpaTableNames, strFdTpaTableTypes, strFdTpaTableSizes, ref fieldTableTrajParaLs);

            FieldPar1(strFdTssTableNames, strFdTssTableTypes, strFdTssTableSizes, ref fieldTableTrajStatusLs);
        }

        public void FieldPar1(string[] str1, string[] str2, string[] str3, ref List<DataType.ValDataType.Field> fieldTableLs)
        {
            int _iCount = str1.Length;
            for (int i = 0; i < _iCount ; i++)
            {
                DataType.ValDataType.Field fieldTable = new DataType.ValDataType.Field();

                fieldTable.tableName = "Field";

                fieldTable.attName = "name";
                fieldTable.attXsi = "xsi:type";
                fieldTable.attType = "type";
                fieldTable.attSize = "size";

                fieldTable.attNameValue = str1[i];
                fieldTable.attXsiValue = "array";
                fieldTable.attTypeValue = str2[i];
                fieldTable.attSizeValue = str3[i];

                fieldTableLs.Add(fieldTable);
            }
        }

        public void DataValuePar( ref DataType.ValDataType.DataValue dataValueTable)
        {
            dataValueTable.tableName = "Value";
            dataValueTable.key = "key";
        }


        public void FieldValuePar(ref DataType.ValDataType.FieldValue fieldValueTable)
        {
            fieldValueTable.tableName = "Value";
            fieldValueTable.key = "key";
            fieldValueTable.value = "value";
        }

        public void DataFramePar(ref DataType.ValDataType.DataFrame DataFrame)
        {
            DataFrame.tableName = "Value";
            DataFrame.key = "key";
            DataFrame.x = "x";
            DataFrame.y = "y";
            DataFrame.z = "z";
            DataFrame.rx = "rx";
            DataFrame.ry = "ry";
            DataFrame.rz = "rz";
            DataFrame.fatherId = "fatherId";
        }

        public void DataMdescPar(ref DataType.ValDataType.DataMdesc DataMdesc)
        {
            DataMdesc.tableName = "Value";
            DataMdesc.key = "key";
            DataMdesc.accel = "accel";
            DataMdesc.vel = "vel";
            DataMdesc.decel = "decel";
            DataMdesc.rmax = "rmax";
            DataMdesc.tmax = "tmax";
            DataMdesc.leave = "leave";
            DataMdesc.reach = "reach";
        }

        public void DataJointPar(ref DataType.ValDataType.DataJoint DataJoint)
        {
            DataJoint.tableName = "Value";
            DataJoint.key = "key";
            DataJoint.j1 = "j1";
            DataJoint.j2 = "j2";
            DataJoint.j3 = "j3";
            DataJoint.j4 = "j4";
            DataJoint.j5 = "j5";
            DataJoint.j6 = "j6";
        }

        #endregion




        #region pjx赋值

        public void ProjectParPjx(ref DataType.ValDataType.Database projectTablePjx)
        {
            projectTablePjx.tableName = "Project";

            projectTablePjx.ns = "xmlns";

            projectTablePjx.nsValue = "http://www.staubli.com/robotics/VAL3/Project/3";
        }

        public void ParametersParPjx(ref DataType.ValDataType.Data ParametersTablePjx)
        {
            ParametersTablePjx.tableName = "Parameters";

            ParametersTablePjx.attName = "version";
            ParametersTablePjx.attXsi = "stackSize";
            ParametersTablePjx.attType = "millimeterUnit";


            ParametersTablePjx.attNameValue = "s7.9";
            ParametersTablePjx.attXsiValue = "5000";
            ParametersTablePjx.attTypeValue = "true";
        }

        public void ProgramsParPjx(ref DataType.ValDataType.Datas programsTablePjx)
        {
            programsTablePjx.tableName = "Programs";
        }

        public void DatabaseParPjx(ref DataType.ValDataType.Datas databaseTablePjx)
        {
            databaseTablePjx.tableName = "Database";
        }



        public void DataParPjx(string fileName, ref DataType.ValDataType.Data dataPjxTablePjx)
        {
            dataPjxTablePjx.tableName = "Data";

            dataPjxTablePjx.attName = "file";

            dataPjxTablePjx.attNameValue = fileName;
        }

        public void LibrariesParPjx(ref DataType.ValDataType.Datas librariesTablePjx)
        {
            librariesTablePjx.tableName = "Libraries";
        }

        public void LibraryParPjx(ref DataType.ValDataType.Data libraryTablePjx)
        {
            libraryTablePjx.tableName = "Library";

            libraryTablePjx.attName = "alias";
            libraryTablePjx.attAccess = "path";
            libraryTablePjx.attXsi = "autoload";
            libraryTablePjx.attType = "password";

            libraryTablePjx.attNameValue = "io";
            libraryTablePjx.attAccessValue = "Disk://io/io.pjx";
            libraryTablePjx.attXsiValue = "true";
            libraryTablePjx.attTypeValue = "";
        }

        public void TypesParPjx(ref DataType.ValDataType.Datas typesTablePjx)
        {
            typesTablePjx.tableName = "Types";
        }

        public void TypeParPjx(ref List<DataType.ValDataType.Data> typeTablePjxLs)
        {
            DataType.ValDataType.Data typeTablePjx = new DataType.ValDataType.Data();

            for (int i = 0; i < strTypeName.Length; i++)
            {
                typeTablePjx.tableName = "Type";

                typeTablePjx.attName = "name";
                typeTablePjx.attAccess = "path";


                typeTablePjx.attNameValue = strTypeName[i];
                typeTablePjx.attAccessValue = strTypeValue[i];


                typeTablePjxLs.Add(typeTablePjx);
            }
        }

        #endregion

    }
}
