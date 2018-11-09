using CodeGen.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeGen
{
    /// <summary>
    /// 数据库服务。
    /// </summary>
    internal sealed class DatabaseServices
    {
        /// <summary>
        /// 获取数据库中的所有表模型。
        /// </summary>
        /// <returns>表模型数组。</returns>
        public TableModel[] GetTables()
        {
            using (var reader = SqlHelper.ExeuReader("select * from INFORMATION_SCHEMA.TABLES"))
            {
                var list = new List<TableModel>();
                while (reader.Read())
                {
                    var tableName = reader["TABLE_NAME"].ToString();
                    var model = new TableModel
                    {
                        Name = tableName,
                        ForeignKeys = GetForeignKeys(tableName)
                    };
                    model.Columns = GetColumns(model);
                    list.Add(model);
                }
                return list.ToArray();
            }
        }

        #region Private Method

        private static ColumnModel[] GetColumns(TableModel table)
        {
            var tableName = table.Name;
            using (var reader = SqlHelper.ExeuReader(string.Format(@"exec sp_executesql N'SELECT
CAST(
        serverproperty(N''Servername'')
       AS sysname) AS [Server_Name],
db_name() AS [Database_Name],
SCHEMA_NAME(tbl.schema_id) AS [Table_Schema],
tbl.name AS [Table_Name],
clmns.column_id AS [ID],
clmns.name AS [Name],
clmns.is_ansi_padded AS [AnsiPaddingStatus],
clmns.is_computed AS [Computed],
ISNULL(cc.definition,N'''') AS [ComputedText],
ISNULL(baset.name, N'''') AS [SystemType],
s1clmns.name AS [DataTypeSchema],
CAST(clmns.is_rowguidcol AS bit) AS [RowGuidCol],
CAST(CASE WHEN baset.name IN (N''nchar'', N''nvarchar'') AND clmns.max_length <> -1 THEN clmns.max_length/2 ELSE clmns.max_length END AS int) AS [Length],
CAST(clmns.precision AS int) AS [NumericPrecision],
clmns.is_identity AS [Identity],
CAST(ISNULL(ic.seed_value,0) AS bigint) AS [IdentitySeed],
CAST(ISNULL(ic.increment_value,0) AS bigint) AS [IdentityIncrement],
ISNULL(clmns.collation_name, N'''') AS [Collation],
CAST(clmns.scale AS int) AS [NumericScale],
clmns.is_nullable AS [Nullable],
CAST(clmns.is_filestream AS bit) AS [IsFileStream],
ISNULL(ic.is_not_for_replication, 0) AS [NotForReplication],
(case when clmns.default_object_id = 0 then N'''' when d.parent_object_id > 0 then N'''' else d.name end) AS [Default],
(case when clmns.default_object_id = 0 then N'''' when d.parent_object_id > 0 then N'''' else schema_name(d.schema_id) end) AS [DefaultSchema],
(case when clmns.rule_object_id = 0 then N'''' else r.name end) AS [Rule],
(case when clmns.rule_object_id = 0 then N'''' else schema_name(r.schema_id) end) AS [RuleSchema],
ISNULL(xscclmns.name, N'''') AS [XmlSchemaNamespace],
ISNULL(s2clmns.name, N'''') AS [XmlSchemaNamespaceSchema],
ISNULL( (case clmns.is_xml_document when 1 then 2 else 1 end), 0) AS [XmlDocumentConstraint],
CAST(ISNULL(cc.is_persisted, 0) AS bit) AS [IsPersisted],
CAST(ISNULL((select TOP 1 1 from sys.foreign_key_columns AS colfk where colfk.parent_column_id = clmns.column_id and colfk.parent_object_id = clmns.object_id), 0) AS bit) AS [IsForeignKey],
CAST(clmns.is_sparse AS bit) AS [IsSparse],
CAST(clmns.is_column_set AS bit) AS [IsColumnSet],
usrt.name AS [DataType]
FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
LEFT OUTER JOIN sys.computed_columns AS cc ON cc.object_id = clmns.object_id and cc.column_id = clmns.column_id
LEFT OUTER JOIN sys.types AS baset ON (baset.user_type_id = clmns.system_type_id and baset.user_type_id = baset.system_type_id) or ((baset.system_type_id = clmns.system_type_id) and (baset.user_type_id = clmns.user_type_id) and (baset.is_user_defined = 0) and (baset.is_assembly_type = 1))
LEFT OUTER JOIN sys.types AS usrt ON usrt.user_type_id = clmns.user_type_id
LEFT OUTER JOIN sys.schemas AS s1clmns ON s1clmns.schema_id = usrt.schema_id
LEFT OUTER JOIN sys.identity_columns AS ic ON ic.object_id = clmns.object_id and ic.column_id = clmns.column_id
LEFT OUTER JOIN sys.objects AS d ON d.object_id = clmns.default_object_id
LEFT OUTER JOIN sys.objects AS r ON r.object_id = clmns.rule_object_id
LEFT OUTER JOIN sys.xml_schema_collections AS xscclmns ON xscclmns.xml_collection_id = clmns.xml_collection_id
LEFT OUTER JOIN sys.schemas AS s2clmns ON s2clmns.schema_id = xscclmns.schema_id
WHERE
(tbl.name=@_msparam_0 and SCHEMA_NAME(tbl.schema_id)=@_msparam_1)
ORDER BY
[Database_Name] ASC,[Table_Schema] ASC,[Table_Name] ASC,[ID] ASC',N'@_msparam_0 nvarchar(4000),@_msparam_1 nvarchar(4000)',@_msparam_0=N'{0}',@_msparam_1=N'dbo'",
                tableName)))
            {
                var list = new List<ColumnModel>();
                var indexs = GetIndexs(tableName);
                while (reader.Read())
                {
                    var model = new ColumnModel(reader)
                    {
                        Table = table
                    };
                    var index = indexs.FirstOrDefault(i => i.ColumnName.Equals(model.Name));
                    if (index != null)
                    {
                        model.IsPrimary = index.IndexType == 0;
                        model.IsIndex = index.IndexType == 1;
                    }
                    list.Add(model);
                }

                return list.ToArray();
            }
        }

        private static ForeignKeyModel[] GetForeignKeys(string tableName)
        {
            var list = new List<ForeignKeyModel>();

            using (var reader = SqlHelper.ExeuReader(string.Format(@"
exec sp_executesql N'SELECT
cstr.name AS [ForeignKey_Name],
cfk.name AS [Name],
crk.name AS [ReferencedColumn]
FROM
sys.tables AS tbl
INNER JOIN sys.foreign_keys AS cstr ON cstr.parent_object_id=tbl.object_id
INNER JOIN sys.foreign_key_columns AS fk ON fk.constraint_object_id=cstr.object_id
INNER JOIN sys.columns AS cfk ON fk.parent_column_id = cfk.column_id and fk.parent_object_id = cfk.object_id
INNER JOIN sys.columns AS crk ON fk.referenced_column_id = crk.column_id and fk.referenced_object_id = crk.object_id
WHERE
(tbl.name=@_msparam_0 and SCHEMA_NAME(tbl.schema_id)=@_msparam_1)',N'@_msparam_0 nvarchar(4000),@_msparam_1 nvarchar(4000)',@_msparam_0=N'{0}',@_msparam_1=N'dbo'", tableName)))
            {
                while (reader.Read())
                {
                    list.Add(new ForeignKeyModel
                    {
                        Name = reader["ForeignKey_Name"].ToString(),
                        SourceColumn = reader["Name"].ToString(),
                        ReferencedColumn = reader["ReferencedColumn"].ToString()
                    });
                }
            }

            using (var reader = SqlHelper.ExeuReader(string.Format(@"exec sp_executesql N'SELECT
cstr.name AS [Name],
cstr.delete_referential_action AS [DeleteAction],
cstr.update_referential_action AS [UpdateAction],
rtbl.name AS [ReferencedTable]
FROM
sys.tables AS tbl
INNER JOIN sys.foreign_keys AS cstr ON cstr.parent_object_id=tbl.object_id
INNER JOIN sys.tables rtbl ON rtbl.object_id = cstr.referenced_object_id
LEFT OUTER JOIN sys.filetable_system_defined_objects AS filetableobj ON filetableobj.object_id = cstr.object_id
WHERE
(tbl.name=@_msparam_0 and SCHEMA_NAME(tbl.schema_id)=@_msparam_1)',N'@_msparam_0 nvarchar(4000),@_msparam_1 nvarchar(4000)',@_msparam_0=N'{0}',@_msparam_1=N'dbo'
", tableName)))
            {
                while (reader.Read())
                {
                    var name = reader["Name"].ToString();
                    var model = list.First(i => i.Name == name);
                    model.DeleteRule = (Rule)int.Parse(reader["DeleteAction"].ToString());
                    model.UpdateRule = (Rule)int.Parse(reader["UpdateAction"].ToString());
                    model.ReferencedTable = reader["ReferencedTable"].ToString();
                }
            }

            return list.ToArray();
        }

        private static IndexModel[] GetIndexs(string tableName)
        {
            var list = new List<IndexModel>();

            using (var reader = SqlHelper.ExeuReader(string.Format(@"exec sp_executesql N'SELECT
i.name AS [Index_Name],
clmns.name AS [Name]
FROM
sys.tables AS tbl
INNER JOIN sys.indexes AS i ON (i.index_id > @_msparam_0 and i.is_hypothetical = @_msparam_1) AND (i.object_id=tbl.object_id)
INNER JOIN sys.index_columns AS ic ON (ic.column_id > 0 and (ic.key_ordinal > 0 or ic.partition_ordinal = 0 or ic.is_included_column != 0)) AND (ic.index_id=CAST(i.index_id AS int) AND ic.object_id=i.object_id)
INNER JOIN sys.columns AS clmns ON clmns.object_id = ic.object_id and clmns.column_id = ic.column_id
WHERE
(tbl.name=@_msparam_2 and SCHEMA_NAME(tbl.schema_id)=@_msparam_3)',N'@_msparam_0 nvarchar(4000),@_msparam_1 nvarchar(4000),@_msparam_2 nvarchar(4000),@_msparam_3 nvarchar(4000)',@_msparam_0=N'0',@_msparam_1=N'0',@_msparam_2=N'{0}',@_msparam_3=N'dbo'",
                tableName)))
            {
                while (reader.Read())
                {
                    list.Add(new IndexModel
                    {
                        Name = reader["Index_Name"].ToString(),
                        ColumnName = reader["Name"].ToString()
                    });
                }
            }

            using (var reader = SqlHelper.ExeuReader(string.Format(@"exec sp_executesql N'SELECT
i.name AS [Name],
CAST(
          CASE i.type WHEN 1 THEN 0 WHEN 4 THEN 4
                      WHEN 3 THEN CASE xi.xml_index_type WHEN 0 THEN 2 WHEN 1 THEN 3 WHEN 2 THEN 7 WHEN 3 THEN 8 END
                      WHEN 4 THEN 4 WHEN 6 THEN 5 WHEN 7 THEN 6 WHEN 5 THEN 9 ELSE 1 END
        AS tinyint) AS [IndexType]
FROM
sys.tables AS tbl
INNER JOIN sys.indexes AS i ON (i.index_id > @_msparam_0 and i.is_hypothetical = @_msparam_1) AND (i.object_id=tbl.object_id)
LEFT OUTER JOIN sys.key_constraints AS k ON k.parent_object_id = i.object_id AND k.unique_index_id = i.index_id
LEFT OUTER JOIN sys.xml_indexes AS xi ON xi.object_id = i.object_id AND xi.index_id = i.index_id
LEFT OUTER JOIN sys.data_spaces AS dsi ON dsi.data_space_id = i.data_space_id
LEFT OUTER JOIN sys.tables AS t ON t.object_id = i.object_id
LEFT OUTER JOIN sys.data_spaces AS dstbl ON dstbl.data_space_id = t.Filestream_data_space_id and (i.index_id < 2 or (i.type = 7 and i.index_id < 3))
LEFT OUTER JOIN sys.stats AS s ON s.stats_id = i.index_id AND s.object_id = i.object_id
LEFT OUTER JOIN sys.spatial_indexes AS spi ON i.object_id = spi.object_id and i.index_id = spi.index_id
LEFT OUTER JOIN sys.spatial_index_tessellations as si ON i.object_id = si.object_id and i.index_id = si.index_id
LEFT OUTER JOIN sys.filetable_system_defined_objects AS filetableobj ON i.object_id = filetableobj.object_id
WHERE
(tbl.name=@_msparam_2 and SCHEMA_NAME(tbl.schema_id)=@_msparam_3)',N'@_msparam_0 nvarchar(4000),@_msparam_1 nvarchar(4000),@_msparam_2 nvarchar(4000),@_msparam_3 nvarchar(4000)',@_msparam_0=N'0',@_msparam_1=N'0',@_msparam_2=N'{0}',@_msparam_3=N'dbo'", tableName)))
            {
                while (reader.Read())
                {
                    var name = reader["Name"].ToString();
                    var indexType = int.Parse(reader["IndexType"].ToString());
                    list.First(i => i.Name == name).IndexType = indexType;
                }
            }

            return list.ToArray();
        }

        #endregion Private Method
    }
}