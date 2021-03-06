
-- Delete all records first
DELETE FROM [dbo].[test_detail]
GO

-- Delete all records first
DELETE FROM [dbo].[test_master]
GO



INSERT INTO [dbo].[test_master] ([id], [description], [notes], [someint], [someint_nullable], [somedate], [somedate_nullable], [somefloat], [somefloat_nullable], [somebool], [somebool_nullable])
VALUES (1, N'Desc1                                             ', N'Notes1                                                                                                                                                                                                                                                    ', 1, 1, '20090101', '20090101', 1.1, 1.1, 1, 1)

INSERT INTO [dbo].[test_master] ([id], [description], [notes], [someint], [someint_nullable], [somedate], [somedate_nullable], [somefloat], [somefloat_nullable], [somebool], [somebool_nullable])
VALUES (2, N'Desc2                                             ', NULL, 2, NULL, '20090101', NULL, 2.2, NULL, 1, NULL)


GO


INSERT INTO [dbo].[test_detail] ([master_id], [id], [description], [qty], [amt])
VALUES (1, N'A         ', N'Alpha                                             ', 1, 10)

INSERT INTO [dbo].[test_detail] ([master_id], [id], [description], [qty], [amt])
VALUES (2, N'B         ', N'Beta                                              ', 2, 20)


GO

