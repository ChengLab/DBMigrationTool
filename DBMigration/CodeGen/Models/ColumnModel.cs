using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CodeGen.Models
{
    /// <summary>
    /// 列模型。
    /// </summary>
    internal sealed class ColumnModel
    {
        public ColumnModel()
        {
        }

        public ColumnModel(IDataRecord reader)
        {
            Name = reader["Name"].ToString();
            IsNull = bool.Parse(reader["Nullable"].ToString());
            var type = reader["DataType"].ToString();
            Type = (SqlDbType)Enum.Parse(typeof(SqlDbType), Enum.GetNames(typeof(SqlDbType)).First(i => i.Equals(type, StringComparison.OrdinalIgnoreCase)));
            Length = int.Parse(reader["Length"].ToString());
            IsIdentity = bool.Parse(reader["Identity"].ToString());
            IsForeignKey = bool.Parse(reader["IsForeignKey"].ToString());
        }

        /// <summary>
        /// 表模型。
        /// </summary>
        public TableModel Table { get; set; }

        /// <summary>
        /// 表名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否允许NULL。
        /// </summary>
        public bool IsNull { get; set; }

        /// <summary>
        /// 是否是标识列。
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否是外键。
        /// </summary>
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// 是否是主键。
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// 是否是索引。
        /// </summary>
        public bool IsIndex { get; set; }

        /// <summary>
        /// 数据类型。
        /// </summary>
        public SqlDbType Type { get; set; }

        /// <summary>
        /// 数据长度。
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 装换成代码。
        /// </summary>
        /// <returns>代码字符串。</returns>
        public string ToCode()
        {
            string asName;

            switch (Type)
            {
                case SqlDbType.BigInt:
                    asName = "AsInt64()";
                    break;

                case SqlDbType.NVarChar:
                    asName = string.Format("AsString({0})", Length <= 0 ? "int.MaxValue" : Length.ToString(CultureInfo.InvariantCulture));
                    break;

                case SqlDbType.DateTime:
                    asName = "AsDateTime()";
                    break;

                case SqlDbType.Int:
                    asName = "AsInt32()";
                    break;

                case SqlDbType.Bit:
                    asName = "AsBoolean()";
                    break;

                case SqlDbType.Float:
                    asName = "AsFloat()";
                    break;

                case SqlDbType.Text:
                    asName = "AsCustom(\"text\")";
                    break;

                case SqlDbType.SmallInt:
                    asName = "AsInt16()";
                    break;

                case SqlDbType.Decimal:
                    asName = "AsDecimal()";
                    break;

                default:
                    throw new Exception("不支持的类型" + Type);
            }

            var builder = new StringBuilder();
            builder.AppendFormat(".WithColumn(\"{0}\").{1}.{2}()", Name, asName, IsNull ? "Nullable" : "NotNullable");
            if (IsPrimary)
                builder.Append(".PrimaryKey()");
            if (IsIndex)
                builder.Append(".Indexed()");
            if (IsIdentity)
                builder.Append(".Identity()");

            var foreignKey = Table.ForeignKeys.FirstOrDefault(i => i.SourceColumn == Name);
            if (foreignKey != null)
            {
                builder.AppendFormat(".ForeignKey(\"{0}\", \"{1}\")", foreignKey.ReferencedTable, foreignKey.ReferencedColumn);
                switch (foreignKey.DeleteRule)
                {
                    case Rule.Cascade:
                        builder.Append(".OnDelete(Rule.Cascade)");
                        break;

                    case Rule.SetNull:
                        builder.Append(".OnDelete(Rule.SetNull)");
                        break;

                    case Rule.SetDefault:
                        builder.Append(".OnDelete(Rule.SetDefault)");
                        break;
                }
                switch (foreignKey.UpdateRule)
                {
                    case Rule.Cascade:
                        builder.Append(".OnUpdate(Rule.Cascade)");
                        break;

                    case Rule.SetNull:
                        builder.Append(".OnUpdate(Rule.SetNull)");
                        break;

                    case Rule.SetDefault:
                        builder.Append(".OnUpdate(Rule.SetDefault)");
                        break;
                }
            }

            return builder.ToString();
        }
    }
}