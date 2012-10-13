
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/13/2012 14:33:22
-- Generated from EDMX file: D:\Users\everm_000\SkyDrive\Development\Edge\Base\DB\DatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[IPCameraSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IPCameraSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'IPCameras'
CREATE TABLE [dbo].[IPCameras] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(2147483647)  NOT NULL,
    [Vendor] int  NOT NULL,
    [Host] nvarchar(2147483647)  NOT NULL,
    [Port] int  NOT NULL,
    [UseSSL] bit  NOT NULL,
    [Username] nvarchar(2147483647)  NOT NULL,
    [Password] nvarchar(2147483647)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'IPCameras'
ALTER TABLE [dbo].[IPCameras]
ADD CONSTRAINT [PK_IPCameras]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------