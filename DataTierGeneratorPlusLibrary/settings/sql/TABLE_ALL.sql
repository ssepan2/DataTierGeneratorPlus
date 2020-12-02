SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*DROP CONSTRAINT*/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_test_detail_test_master]') AND parent_object_id = OBJECT_ID(N'[dbo].[test_detail]'))
ALTER TABLE [dbo].[test_detail] DROP CONSTRAINT [FK_test_detail_test_master]
GO

/*DROP DETAIL*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test_detail]') AND type in (N'U'))
DROP TABLE [dbo].[test_detail]
GO

/*DROP MASTER*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test_master]') AND type in (N'U'))
DROP TABLE [dbo].[test_master]
GO

/*ADD MASTER*/
CREATE TABLE [dbo].[test_master](
	[id] [int] NOT NULL,
	[description] [nchar](50) NOT NULL,
	[notes] [nchar](250) NULL,
	[someint] [int] NOT NULL,
	[someint_nullable] [int] NULL,
	[somedate] [datetime] NOT NULL,
	[somedate_nullable] [datetime] NULL,
	[somefloat] [float] NOT NULL,
	[somefloat_nullable] [float] NULL,
	[somebool] [bit] NOT NULL,
	[somebool_nullable] [bit] NULL,
 CONSTRAINT [PK_test_master] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/*ADD DETAIL*/
CREATE TABLE [dbo].[test_detail](
	[master_id] [int] NOT NULL,
	[id] [nchar](10) NOT NULL,
	[description] [nchar](50) NOT NULL,
	[qty] [int] NOT NULL,
	[amt] [float] NOT NULL,
 CONSTRAINT [PK_test_detail] PRIMARY KEY CLUSTERED 
(
	[master_id] ASC,
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/*ADD CONSTRAINT*/
ALTER TABLE [dbo].[test_detail]  WITH CHECK ADD  CONSTRAINT [FK_test_detail_test_master] FOREIGN KEY([master_id])
REFERENCES [dbo].[test_master] ([id])
GO

ALTER TABLE [dbo].[test_detail] CHECK CONSTRAINT [FK_test_detail_test_master]
GO







