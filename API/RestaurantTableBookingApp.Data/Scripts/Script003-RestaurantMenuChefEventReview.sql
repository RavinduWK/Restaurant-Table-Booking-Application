BEGIN TRANSACTION;
GO

ALTER TABLE [Restaurants] ADD [hasBranches] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [Chefs] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Bio] nvarchar(max) NOT NULL,
    [Speciality] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [RestaurantId] int NOT NULL,
    CONSTRAINT [PK_Chefs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_dbo_Chefs_dbo_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Events] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [Date] datetime2 NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [RestaurantId] int NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_dbo_Events_dbo_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Menus] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Category] nvarchar(max) NOT NULL,
    [RestaurantId] int NOT NULL,
    CONSTRAINT [PK_Menus] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_dbo_Menus_dbo_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [ReviewerName] nvarchar(max) NOT NULL,
    [Comment] nvarchar(max) NOT NULL,
    [Rating] int NOT NULL,
    [Date] datetime2 NOT NULL,
    [RestaurantId] int NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_dbo_Reviews_dbo_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Chefs_RestaurantId] ON [Chefs] ([RestaurantId]);
GO

CREATE INDEX [IX_Events_RestaurantId] ON [Events] ([RestaurantId]);
GO

CREATE INDEX [IX_Menus_RestaurantId] ON [Menus] ([RestaurantId]);
GO

CREATE INDEX [IX_Reviews_RestaurantId] ON [Reviews] ([RestaurantId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240723102114_RestaurantMenuChefEventReview', N'7.0.9');
GO

COMMIT;
GO

