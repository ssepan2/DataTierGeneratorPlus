SELECT
	TABLE_CATALOG,
	TABLE_SCHEMA,
	TABLE_NAME,
	TABLE_TYPE,
	ExtendedProperty.Value AS ProgrammaticAlias
FROM
	INFORMATION_SCHEMA.TABLES
	LEFT JOIN ::FN_LISTEXTENDEDPROPERTY('ProgrammaticAlias', 'user', 'dbo', 'table', default, default, default) AS ExtendedProperty ON INFORMATION_SCHEMA.TABLES.TABLE_NAME COLLATE Latin1_General_CI_AS = ExtendedProperty.objName
WHERE
	TABLE_TYPE = 'BASE TABLE'
	AND TABLE_NAME != 'dtProperties'
	AND TABLE_CATALOG = '#DatabaseName#'
