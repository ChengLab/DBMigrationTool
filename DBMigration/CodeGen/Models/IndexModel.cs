namespace CodeGen.Models
{
    /// <summary>
    /// ����ģ�͡�
    /// </summary>
    internal sealed class IndexModel
    {
        /// <summary>
        /// �������ơ�
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// �����ơ�
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// ��������0��������1��������
        /// </summary>
        public int IndexType { get; set; }
    }
}