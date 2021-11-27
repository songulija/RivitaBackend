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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [Companies] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Companies] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [CompanyId] int NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUsers_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [Transportations] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [UserId1] nvarchar(450) NULL,
        [TransportationNumber] int NOT NULL,
        [Weight] int NOT NULL,
        [WagonsCount] int NOT NULL,
        [TransportationStatus] nvarchar(max) NULL,
        [TransportationType] nvarchar(max) NULL,
        [TransportationSubCode] int NOT NULL,
        [CargoAcceptanceDate] datetime2 NOT NULL,
        [MovementStartDateInBelarus] datetime2 NOT NULL,
        [MovementEndDateInBelarus] datetime2 NOT NULL,
        [EtsngCargoCode] int NOT NULL,
        [EtsngCargoTitle] nvarchar(max) NULL,
        [GngCargoCode] int NOT NULL,
        [GngCargoTitle] nvarchar(max) NULL,
        [DepartureStationCode] int NOT NULL,
        [DepartureStationTitle] nvarchar(max) NULL,
        [DepartureCountryCode] int NOT NULL,
        [DepartureCountryTitle] nvarchar(max) NULL,
        [DestinationStationCode] int NOT NULL,
        [DestinationStationTitle] nvarchar(max) NULL,
        [DestinationCountryCode] int NOT NULL,
        [DestinationCountryTitle] nvarchar(max) NULL,
        [StationMovementBeginingBelarusCode] int NOT NULL,
        [StationMovementBeginingBelarusTitle] nvarchar(max) NULL,
        [StationMovementEndBelarusCode] int NOT NULL,
        [StationMovementEndBelarusTitle] nvarchar(max) NULL,
        CONSTRAINT [PK_Transportations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Transportations_AspNetUsers_UserId1] FOREIGN KEY ([UserId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE TABLE [Wagons] (
        [Id] uniqueidentifier NOT NULL,
        [TransportationId] uniqueidentifier NOT NULL,
        [NumberOfWagon] int NOT NULL,
        [TypeOfWagon] nvarchar(max) NULL,
        [LiftingCapacityTons] int NOT NULL,
        [Weight] int NOT NULL,
        CONSTRAINT [PK_Wagons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Wagons_Transportations_TransportationId] FOREIGN KEY ([TransportationId]) REFERENCES [Transportations] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''b75243f9-b3ba-4bb2-b1a7-7cfe4028f95e'', N''b75243f9-b3ba-4bb2-b1a7-7cfe4028f95e'', N''Administrator'', N''ADMINISTRATOR'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''c5485f28-6a30-401f-9552-0a1d507e0391'', N''530f65b0-b87c-46c6-9a66-3ff2e134ca36'', N''User'', N''USER'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Companies]'))
        SET IDENTITY_INSERT [Companies] ON;
    EXEC(N'INSERT INTO [Companies] ([Id], [Name])
    VALUES (1, N''Rivita'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Companies]'))
        SET IDENTITY_INSERT [Companies] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'CompanyId', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] ON;
    EXEC(N'INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [CompanyId], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
    VALUES (N''c9490c27-1b89-4e39-8f2e-99b48dcc709e'', 0, 1, N''a7628d44-ed52-49de-94ad-8ff307596d84'', N''rivitaadmin@gmail.com'', CAST(1 AS bit), CAST(0 AS bit), NULL, NULL, N''RIVITAADMIN@GMAIL.COM'', N''AQAAAAEAACcQAAAAEActPmUyP2FFbEvq33S0hew8FXWHiWoGyzKyIA2Mas3/eESMNYZnVo0KwesRN6Ii/Q=='', NULL, CAST(0 AS bit), N''360d22a7-a8c6-4e65-9991-cf0673f55d34'', CAST(0 AS bit), N''rivitaadmin@gmail.com'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'CompanyId', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] ON;
    EXEC(N'INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
    VALUES (N''b75243f9-b3ba-4bb2-b1a7-7cfe4028f95e'', N''c9490c27-1b89-4e39-8f2e-99b48dcc709e'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE INDEX [IX_AspNetUsers_CompanyId] ON [AspNetUsers] ([CompanyId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE UNIQUE INDEX [IX_Transportations_TransportationNumber] ON [Transportations] ([TransportationNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE INDEX [IX_Transportations_UserId1] ON [Transportations] ([UserId1]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    CREATE INDEX [IX_Wagons_TransportationId] ON [Wagons] ([TransportationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211127130249_createDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211127130249_createDatabase', N'5.0.11');
END;
GO

COMMIT;
GO

