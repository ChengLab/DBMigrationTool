using System.Linq;
using System.Text;

namespace CodeGen.Models
{
    /// <summary>
    /// 表模型。
    /// </summary>
    internal sealed class TableModel
    {
        /// <summary>
        /// 表名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列。
        /// </summary>
        public ColumnModel[] Columns { get; set; }

        /// <summary>
        /// 外键。
        /// </summary>
        public ForeignKeyModel[] ForeignKeys { get; set; }

        /// <summary>
        /// 转换成创建表代码。
        /// </summary>
        /// <returns>代码字符串。</returns>
        public string ToCreateCode()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("            Create.Table(\"{0}\")", Name));
            foreach (var columnModel in Columns.AsParallel().Take(Columns.Length - 1))
            {
                builder.AppendLine("                " + columnModel.ToCode());
            }
            return builder.Append("                " + Columns.Last().ToCode()).Append(";").ToString();
        }

        /// <summary>
        /// 转换成删除表代码。
        /// </summary>
        /// <param name="isEntity">是否使用实体。</param>
        /// <returns>代码字符串。</returns>
        public string ToDeleteCode(bool isEntity)
        {
            return string.Format("Delete.Table(\"{0}\");", Name);
        }
    }
}