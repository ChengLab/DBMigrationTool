using System.Data;

namespace CodeGen.Models
{
    /// <summary>
    /// 外键模型。
    /// </summary>
    internal sealed class ForeignKeyModel
    {
        /// <summary>
        /// 外键名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 源列名。
        /// </summary>
        public string SourceColumn { get; set; }

        /// <summary>
        /// 引用列名。
        /// </summary>
        public string ReferencedColumn { get; set; }

        /// <summary>
        /// 引用表名。
        /// </summary>
        public string ReferencedTable { get; set; }

        /// <summary>
        /// 删除规则。
        /// </summary>
        public Rule DeleteRule { get; set; }

        /// <summary>
        /// 更新规则。
        /// </summary>
        public Rule UpdateRule { get; set; }
    }
}