SELECT
	INFORMATION_SCHEMA.COLUMNS.*,
	COL_LENGTH('#TableSchema#.#TableName#', INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME) AS COLUMN_LENGTH,
	COLUMNPROPERTY(OBJECT_ID('#TableSchema#.#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsComputed') AS IsComputed,
	COLUMNPROPERTY(OBJECT_ID('#TableSchema#.#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IsIdentity,
	COLUMNPROPERTY(OBJECT_ID('#TableSchema#.#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IsRowGuidCol,
	ExtendedProperties.Value AS ProgrammaticAlias
FROM
	INFORMATION_SCHEMA.COLUMNS
	LEFT JOIN ::fn_listextendedproperty('ProgrammaticAlias', 'user', 'dbo', 'table', '#TableSchema#.#TableName#', 'column', default) AS ExtendedProperties ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME COLLATE Latin1_General_CI_AS = ExtendedProperties.objName
WHERE
	INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '#TableName#'
	AND
	INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA = '#TableSchema#'