using System.Linq;
using System.Text;

namespace CodeGen.Models
{
    /// <summary>
    /// ��ģ�͡�
    /// </summary>
    internal sealed class TableModel
    {
        /// <summary>
        /// �����ơ�
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// �С�
        /// </summary>
        public ColumnModel[] Columns { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public ForeignKeyModel[] ForeignKeys { get; set; }

        /// <summary>
        /// ת���ɴ�������롣
        /// </summary>
        /// <returns>�����ַ�����</returns>
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
        /// ת����ɾ������롣
        /// </summary>
        /// <param name="isEntity">�Ƿ�ʹ��ʵ�塣</param>
        /// <returns>�����ַ�����</returns>
        public string ToDeleteCode(bool isEntity)
        {
            return string.Format("Delete.Table(\"{0}\");", Name);
        }
    }
}