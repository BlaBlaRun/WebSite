
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/31/2017 23:00:23
-- Generated from EDMX file: C:\Projects\BlaBlaRunProject\BlaBlaRunProject.Domain\Concrete\EFDBContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BlaBlaRunData];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_WorkoutsUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkoutsSet] DROP CONSTRAINT [FK_WorkoutsUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[WorkoutsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkoutsSet];
GO
IF OBJECT_ID(N'[dbo].[UsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersSet];
GO
IF OBJECT_ID(N'[dbo].[WorkoutsOldSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkoutsOldSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'WorkoutsSet'
CREATE TABLE [dbo].[WorkoutsSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UsersId] bigint  NOT NULL,
    [StartDateTime] datetime  NOT NULL,
    [StartLocation] geography  NULL,
    [AVGPace] time  NULL,
    [Circular] bit  NOT NULL,
    [EndLocation] geography  NULL,
    [Distance] float  NULL,
    [MaxNumberPeople] smallint  NULL,
    [Zone] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Region] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [ElevationGain] float  NULL
);
GO

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [AspNetUserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WorkoutsOldSet'
CREATE TABLE [dbo].[WorkoutsOldSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UsersId] bigint  NOT NULL,
    [StartDateTime] datetime  NOT NULL,
    [StartLocation] geography  NULL,
    [AVGPace] time  NULL,
    [Circular] bit  NOT NULL,
    [EndLocation] geography  NULL,
    [Distance] float  NULL,
    [MaxNumberPeople] smallint  NULL,
    [Zone] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Region] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [ElevationGain] float  NULL
);
GO

-- Creating table 'AuditSet'
CREATE TABLE [dbo].[AuditSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UserIp] nvarchar(max)  NOT NULL,
    [UserAgent] nvarchar(max)  NOT NULL,
    [ActionType] nvarchar(max)  NOT NULL,
    [Element] nvarchar(max)  NOT NULL,
    [ActionUTCDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'WorkoutsSet'
ALTER TABLE [dbo].[WorkoutsSet]
ADD CONSTRAINT [PK_WorkoutsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkoutsOldSet'
ALTER TABLE [dbo].[WorkoutsOldSet]
ADD CONSTRAINT [PK_WorkoutsOldSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuditSet'
ALTER TABLE [dbo].[AuditSet]
ADD CONSTRAINT [PK_AuditSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsersId] in table 'WorkoutsSet'
ALTER TABLE [dbo].[WorkoutsSet]
ADD CONSTRAINT [FK_WorkoutsUsers]
    FOREIGN KEY ([UsersId])
    REFERENCES [dbo].[UsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkoutsUsers'
CREATE INDEX [IX_FK_WorkoutsUsers]
ON [dbo].[WorkoutsSet]
    ([UsersId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------