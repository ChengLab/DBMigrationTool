namespace CodeGen.Models
{
    /// <summary>
    /// 索引模型。
    /// </summary>
    internal sealed class IndexModel
    {
        /// <summary>
        /// 索引名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列名称。
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 索引类型0：主键，1：索引。
        /// </summary>
        public int IndexType { get; set; }
    }
}