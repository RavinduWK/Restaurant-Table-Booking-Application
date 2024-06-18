BEGIN TRANSACTION;
GO

ALTER TABLE [Restaurants] ADD [Description] nvarchar(max) NULL;
GO

ALTER TABLE [Restaurants] ADD [OpenTime] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240617172724_UpdateRestaurantTable', N'7.0.9');
GO

COMMIT;
GO

