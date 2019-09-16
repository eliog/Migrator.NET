namespace Migrator.Framework
{
	public interface IViewField
	{
		string TableName { get; set; }
		string ColumnName { get; set; }

		string KeyColumnName { get; set; }
		string ParentTableName { get; set; }
		string ParentKeyColumnName { get; set; }
	}
}
