
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/26/2015 17:48:26
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
    [StartLocation] geography  NOT NULL,
    [AVGPace] time  NULL,
    [Circular] bit  NOT NULL,
    [EndLocation] geography  NULL,
    [Distance] float  NULL,
    [MaxNumberPeople] smallint  NULL,
    [City] nvarchar(max)  NULL,
    [Region] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [ElevationGain] float  NULL
);
GO

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [AspNetUserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'WorkoutsOldSet'
CREATE TABLE [dbo].[WorkoutsOldSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UsersId] bigint  NOT NULL,
    [StartDateTime] datetime  NOT NULL,
    [StartLocation] geography  NOT NULL,
    [AVGPace] time  NULL,
    [Circular] bit  NOT NULL,
    [EndLocation] geography  NULL,
    [Distance] float  NULL,
    [MaxNumberPeople] smallint  NULL,
    [City] nvarchar(max)  NULL,
    [Region] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [ElevationGain] float  NULL
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