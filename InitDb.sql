USE [PcShop]
GO
INSERT INTO [dbo].[ProductComponent]
           ([Name]
           ,[ParentProductComponentId])
     VALUES
         ('Workstation',null),
           ('Personal Computer',1),
           ('Personal Computer Tower',2),
           ('Personal Computer Screen',2)
GO

INSERT INTO [dbo].[Product]
           ([Name]
           ,[ProductComponentId])
     VALUES
           ('Workstation 1',1),
           ('Workstation 2',1),
           ('Personal Computer 1',2),
           ('Personal Computer 2',2),
           ('Personal Computer Tower 1',3),
           ('Personal Computer Tower 2',3),
           ('Personal Computer Screen 1',4),
           ('Personal Computer Screen 2',4)
GO


INSERT INTO [dbo].[ProductAttribute]
           ([Name]
           ,[Unit])
     VALUES
           ('OS','OS'),
           ('Hard Drive','GB'),
           ('Screen','Inches'),
           ('Memory','GB'),
           ('CPU','GHz')
GO


INSERT INTO [dbo].[ProductComponentAttributeMap]
           ([ProductComponentId]
           ,[ProductAttributeId])
     VALUES
           (1,1),
       (2,2),
       (2,3),
       (3,4),
       (3,5),
       (4,3)
GO
