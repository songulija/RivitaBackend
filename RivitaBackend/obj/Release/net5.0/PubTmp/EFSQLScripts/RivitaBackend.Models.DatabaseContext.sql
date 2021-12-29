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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE TABLE [Companies] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Companies] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE TABLE [UserTypes] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_UserTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [Username] nvarchar(450) NULL,
        [Password] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [TypeId] int NOT NULL,
        [CompanyId] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Users_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Users_UserTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [UserTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE TABLE [Transportations] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [CompanyName] nvarchar(max) NULL,
        [TransportationNumber] int NOT NULL,
        [Weight] int NOT NULL,
        [WagonsCount] int NOT NULL,
        [TransportationType] nvarchar(max) NULL,
        [CargoAcceptanceDate] datetime2 NOT NULL,
        [MovementStartDateInBelarus] datetime2 NOT NULL,
        [MovementEndDateInBelarus] datetime2 NULL,
        [EtsngCargoCode] int NOT NULL,
        [GngCargoCode] int NOT NULL,
        [DepartureStationTitle] nvarchar(max) NULL,
        [DepartureCountryTitle] nvarchar(max) NULL,
        [DestinationStationTitle] nvarchar(max) NULL,
        [DestinationCountryTitle] nvarchar(max) NULL,
        [StationMovementBeginingBelarusTitle] nvarchar(max) NULL,
        [StationMovementEndBelarusTitle] nvarchar(max) NULL,
        CONSTRAINT [PK_Transportations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Transportations_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE TABLE [Wagons] (
        [Id] uniqueidentifier NOT NULL,
        [TransportationId] uniqueidentifier NOT NULL,
        [NumberOfWagon] int NOT NULL,
        [TypeOfWagon] nvarchar(max) NULL,
        [Weight] int NOT NULL,
        CONSTRAINT [PK_Wagons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Wagons_Transportations_TransportationId] FOREIGN KEY ([TransportationId]) REFERENCES [Transportations] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Companies]'))
        SET IDENTITY_INSERT [Companies] ON;
    EXEC(N'INSERT INTO [Companies] ([Id], [Name])
    VALUES (1, N''Rivita''),
    (2, N''Linas Agro''),
    (3, N''Mestilla'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Companies]'))
        SET IDENTITY_INSERT [Companies] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Title') AND [object_id] = OBJECT_ID(N'[UserTypes]'))
        SET IDENTITY_INSERT [UserTypes] ON;
    EXEC(N'INSERT INTO [UserTypes] ([Id], [Title])
    VALUES (1, N''ADMINISTRATOR''),
    (2, N''USER'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Title') AND [object_id] = OBJECT_ID(N'[UserTypes]'))
        SET IDENTITY_INSERT [UserTypes] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CompanyId', N'Password', N'PhoneNumber', N'TypeId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [CompanyId], [Password], [PhoneNumber], [TypeId], [Username])
    VALUES (''c9490c27-1b89-4e39-8f2e-99b48dcc709e'', 1, N''$2a$11$4ntVpLM4Nr9vkvWGAvWeg.Zuh75A447BL.Ng2JMaEA5EJO2keL2SS'', N''+37061816214'', 1, N''jevgenijrivita'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CompanyId', N'Password', N'PhoneNumber', N'TypeId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CompanyId', N'Password', N'PhoneNumber', N'TypeId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [CompanyId], [Password], [PhoneNumber], [TypeId], [Username])
    VALUES (''b9490c27-1b89-4e39-8f2e-99b48dcc901d'', 1, N''$2a$11$sAIwj8U8elLt4cuJxsDco.SzPJet88AV8ywYMUS/wzhVHA4.85Axe'', N''+37060855183'', 1, N''lukasrivita'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CompanyId', N'Password', N'PhoneNumber', N'TypeId', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE UNIQUE INDEX [IX_Transportations_TransportationNumber] ON [Transportations] ([TransportationNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE INDEX [IX_Transportations_UserId] ON [Transportations] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE INDEX [IX_Users_CompanyId] ON [Users] ([CompanyId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE INDEX [IX_Users_TypeId] ON [Users] ([TypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Users_Username] ON [Users] ([Username]) WHERE [Username] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    CREATE INDEX [IX_Wagons_TransportationId] ON [Wagons] ([TransportationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211229142819_createdDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211229142819_createdDB', N'5.0.11');
END;
GO

COMMIT;
GO

