
/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_masterInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_masterInsert]
GO

CREATE PROCEDURE [dbo].[test_masterInsert] (
	@id int,
	@description nchar(50),
	@notes nchar(250),
	@someint int,
	@someint_nullable int,
	@somedate datetime,
	@somedate_nullable datetime,
	@somefloat float(53),
	@somefloat_nullable float(53),
	@somebool bit,
	@somebool_nullable bit
)

AS

SET NOCOUNT ON

INSERT INTO [dbo].[test_master] (
	[id],
	[description],
	[notes],
	[someint],
	[someint_nullable],
	[somedate],
	[somedate_nullable],
	[somefloat],
	[somefloat_nullable],
	[somebool],
	[somebool_nullable]
) VALUES (
	@id,
	@description,
	@notes,
	@someint,
	@someint_nullable,
	@somedate,
	@somedate_nullable,
	@somefloat,
	@somefloat_nullable,
	@somebool,
	@somebool_nullable
)
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_masterUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_masterUpdate]
GO

CREATE PROCEDURE [dbo].[test_masterUpdate] (
	@id int,
	@description nchar(50),
	@notes nchar(250),
	@someint int,
	@someint_nullable int,
	@somedate datetime,
	@somedate_nullable datetime,
	@somefloat float(53),
	@somefloat_nullable float(53),
	@somebool bit,
	@somebool_nullable bit
)

AS

SET NOCOUNT ON

UPDATE
	[dbo].[test_master]
SET
	[description] = @description,
	[notes] = @notes,
	[someint] = @someint,
	[someint_nullable] = @someint_nullable,
	[somedate] = @somedate,
	[somedate_nullable] = @somedate_nullable,
	[somefloat] = @somefloat,
	[somefloat_nullable] = @somefloat_nullable,
	[somebool] = @somebool,
	[somebool_nullable] = @somebool_nullable
WHERE
	 [id] = @id
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_masterDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_masterDelete]
GO

CREATE PROCEDURE [dbo].[test_masterDelete] (
	@id int
)

AS

SET NOCOUNT ON

DELETE FROM
	[dbo].[test_master]
WHERE
	[id] = @id
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_masterSelect]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_masterSelect]
GO

CREATE PROCEDURE [dbo].[test_masterSelect] (
	@id int
)

AS

SET NOCOUNT ON

SELECT
	[id],
	[description],
	[notes],
	[someint],
	[someint_nullable],
	[somedate],
	[somedate_nullable],
	[somefloat],
	[somefloat_nullable],
	[somebool],
	[somebool_nullable]
FROM
	[dbo].[test_master]
WHERE
	[id] = @id
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_masterSelectAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_masterSelectAll]
GO

CREATE PROCEDURE [dbo].[test_masterSelectAll]

AS

SET NOCOUNT ON

SELECT
	[id],
	[description],
	[notes],
	[someint],
	[someint_nullable],
	[somedate],
	[somedate_nullable],
	[somefloat],
	[somefloat_nullable],
	[somebool],
	[somebool_nullable]
FROM
	[dbo].[test_master]
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_detailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_detailInsert]
GO

CREATE PROCEDURE [dbo].[test_detailInsert] (
	@master_id int,
	@id nchar(10),
	@description nchar(50),
	@qty int,
	@amt float(53)
)

AS

SET NOCOUNT ON

INSERT INTO [dbo].[test_detail] (
	[master_id],
	[id],
	[description],
	[qty],
	[amt]
) VALUES (
	@master_id,
	@id,
	@description,
	@qty,
	@amt
)
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_detailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_detailUpdate]
GO

CREATE PROCEDURE [dbo].[test_detailUpdate] (
	@master_id int,
	@id nchar(10),
	@description nchar(50),
	@qty int,
	@amt float(53)
)

AS

SET NOCOUNT ON

UPDATE
	[dbo].[test_detail]
SET
	[description] = @description,
	[qty] = @qty,
	[amt] = @amt
WHERE
	 [master_id] = @master_id	AND [id] = @id
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_detailDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_detailDelete]
GO

CREATE PROCEDURE [dbo].[test_detailDelete] (
	@master_id int,
	@id nchar(10)
)

AS

SET NOCOUNT ON

DELETE FROM
	[dbo].[test_detail]
WHERE
	[master_id] = @master_id
	AND [id] = @id
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_detailDeleteByMaster_id]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_detailDeleteByMaster_id]
GO

CREATE PROCEDURE [dbo].[test_detailDeleteByMaster_id] (
	@master_id int
)

AS

SET NOCOUNT ON

DELETE FROM
	[dbo].[test_detail]
WHERE
	[master_id] = @master_id
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_detailSelect]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_detailSelect]
GO

CREATE PROCEDURE [dbo].[test_detailSelect] (
	@master_id int,
	@id nchar(10)
)

AS

SET NOCOUNT ON

SELECT
	[master_id],
	[id],
	[description],
	[qty],
	[amt]
FROM
	[dbo].[test_detail]
WHERE
	[master_id] = @master_id
	AND [id] = @id
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_detailSelectAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_detailSelectAll]
GO

CREATE PROCEDURE [dbo].[test_detailSelectAll]

AS

SET NOCOUNT ON

SELECT
	[master_id],
	[id],
	[description],
	[qty],
	[amt]
FROM
	[dbo].[test_detail]
GO

/******************************************************************************
******************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[test_detailSelectByMaster_id]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[test_detailSelectByMaster_id]
GO

CREATE PROCEDURE [dbo].[test_detailSelectByMaster_id] (
	@master_id int
)

AS

SET NOCOUNT ON

SELECT
	[master_id],
	[id],
	[description],
	[qty],
	[amt]
FROM
	[dbo].[test_detail]
WHERE
	[master_id] = @master_id
GO
