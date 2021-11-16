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

CREATE TABLE [Contacts] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(10) NOT NULL,
    [LastName] nvarchar(20) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Accounts] (
    [Id] int NOT NULL IDENTITY,
    [AccountName] nvarchar(max) NOT NULL,
    [ContactId] int NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Accounts_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Incidents] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [AccountId] int NOT NULL,
    CONSTRAINT [PK_Incidents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Incidents_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Accounts_ContactId] ON [Accounts] ([ContactId]);
GO

CREATE INDEX [IX_Incidents_AccountId] ON [Incidents] ([AccountId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211110174814_WebApp.Models.MyDbContext', N'5.0.0');
GO

COMMIT;
GO

