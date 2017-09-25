using System;

namespace DataType
{
    /// <summary>
    /// Description of ValDataType1.
    /// </summary>
    public struct ValDataType : IEquatable<ValDataType>
    {
        int member; // this is just an example member, replace it with your own struct members!

        #region Equals and GetHashCode implementation
        // The code in this region is useful if you want to use this structure in collections.
        // If you don't need it, you can just remove the region and the ": IEquatable<ValDataType1>" declaration.

        public override bool Equals(object obj)
        {
            if (obj is ValDataType)
                return Equals((ValDataType)obj); // use Equals method below
            else
                return false;
        }

        public bool Equals(ValDataType other)
        {
            // add comparisions for all members here
            return this.member == other.member;
        }

        public override int GetHashCode()
        {
            // combine the hash codes of all members here (e.g. with XOR operator ^)
            return member.GetHashCode();
        }

        public static bool operator ==(ValDataType left, ValDataType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValDataType left, ValDataType right)
        {
            return !left.Equals(right);
        }
        #endregion



        #region
        public struct Database
        {
            public string tableName;

            public string xsi;
            public string ns;

            public string xsiValue;
            public string nsValue;
        }

        public struct Datas
        {
            public string tableName;
        }

        public struct Data
        {
            public string tableName;

            public string attName;
            public string attAccess;
            public string attXsi;
            public string attType;
            public string attSize;

            public string attNameValue;
            public string attAccessValue;
            public string attXsiValue;
            public string attTypeValue;
            public string attSizeValue;
        }



        public struct DataValue
        {
            public string tableName;
            public string key;
        }

        public struct Field
        {
            public string tableName;

            public string attName;
            public string attXsi;
            public string attType;
            public string attSize;

            public string attNameValue;
            public string attXsiValue;
            public string attTypeValue;
            public string attSizeValue;
        }

        public struct FieldValue
        {
            public string tableName;
            public string key;
            public string value;
        }

        public struct DataFrame
        {
            public string tableName;
            public string key;
            public string x;
            public string y;
            public string z;
            public string rx;
            public string ry;
            public string rz;
            public string fatherId;
        }

        public struct DataMdesc
        {
            public string tableName;
            public string key;
            public string accel;
            public string vel;
            public string decel;
            public string rmax;
            public string tmax;
            public string leave;
            public string reach;
        }

        public struct DataJoint
        {
            public string tableName;
            public string key;
            public string j1;
            public string j2;
            public string j3;
            public string j4;
            public string j5;
            public string j6;
        }

        #endregion 
    }
}
