IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Restaurants] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Address] nvarchar(200) NOT NULL,
    [Phone] nvarchar(20) NULL,
    [Email] nvarchar(100) NULL,
    [ImageUrl] nvarchar(500) NULL,
    CONSTRAINT [PK_Restaurants] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [AdObjId] nvarchar(128) NULL,
    [ProfileImageUrl] nvarchar(512) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RestaurantBranches] (
    [Id] int NOT NULL IDENTITY,
    [RestaurantId] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Address] nvarchar(200) NOT NULL,
    [Phone] nvarchar(20) NULL,
    [Email] nvarchar(100) NULL,
    [ImageUrl] nvarchar(500) NULL,
    CONSTRAINT [PK_RestaurantBranches] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RestaurantBranches_Restaurants_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [DiningTables] (
    [Id] int NOT NULL IDENTITY,
    [RestaurantBranchId] int NOT NULL,
    [TableName] nvarchar(100) NULL,
    [Capacity] int NOT NULL,
    CONSTRAINT [PK_DiningTables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DiningTables_RestaurantBranches_RestaurantBranchId] FOREIGN KEY ([RestaurantBranchId]) REFERENCES [RestaurantBranches] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TimeSlots] (
    [Id] int NOT NULL IDENTITY,
    [DiningTableId] int NOT NULL,
    [ReservationDay] datetime2 NOT NULL,
    [MealType] nvarchar(max) NOT NULL,
    [TableStatus] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TimeSlots] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TimeSlots_DiningTables_DiningTableId] FOREIGN KEY ([DiningTableId]) REFERENCES [DiningTables] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reservations] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [TimeSlotId] int NOT NULL,
    [ReservationDate] datetime2 NOT NULL,
    [ReservationStatus] nvarchar(max) NOT NULL,
    [ReminderSent] bit NULL,
    CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reservations_TimeSlots_TimeSlotId] FOREIGN KEY ([TimeSlotId]) REFERENCES [TimeSlots] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reservations_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_DiningTables_RestaurantBranchId] ON [DiningTables] ([RestaurantBranchId]);
GO

CREATE INDEX [IX_Reservations_TimeSlotId] ON [Reservations] ([TimeSlotId]);
GO

CREATE INDEX [IX_Reservations_UserId] ON [Reservations] ([UserId]);
GO

CREATE INDEX [IX_RestaurantBranches_RestaurantId] ON [RestaurantBranches] ([RestaurantId]);
GO

CREATE INDEX [IX_TimeSlots_DiningTableId] ON [TimeSlots] ([DiningTableId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240617172344_CreateTables', N'7.0.9');
GO

COMMIT;
GO

