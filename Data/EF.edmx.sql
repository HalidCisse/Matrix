
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/27/2014 00:41:18
-- Generated from EDMX file: C:\Users\Halid\Documents\Visual Studio 2013\Projects\Matrix\Data\EF.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MatrixDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ETUDIANTS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ETUDIANTS];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ETUDIANTS'
CREATE TABLE [dbo].[ETUDIANTS] (
    [GUID] uniqueidentifier  NOT NULL,
    [MATRICULE] varchar(max)  NULL,
    [NOM] varchar(max)  NOT NULL,
    [PRENOM] varchar(max)  NOT NULL,
    [CIVILITE] varchar(max)  NULL,
    [PHOTO_IDENTITE] varbinary(max)  NULL,
    [NUMERO_ID] varchar(max)  NULL,
    [TYPE_ID] varchar(max)  NULL,
    [DATE_NAISSANCE] datetime  NULL,
    [NATIONALITE] varchar(max)  NULL,
    [LIEU_NAISSANCE] varchar(max)  NULL,
    [NUMERO_TEL] varchar(max)  NULL,
    [ADRESS_EMAIL] varchar(max)  NULL,
    [ADRESS_DOMICILE] varchar(max)  NULL,
    [NOM_TUTEUR] varchar(max)  NULL,
    [PRENOM_TUTEUR] varchar(max)  NULL,
    [NUMERO_TEL_TUTEUR] varchar(max)  NULL,
    [ADRESS_EMAIL_TUTEUR] varchar(max)  NULL,
    [ADRESS_DOMICIL_TUTEUR] varchar(max)  NULL,
    [STATUT] varchar(max)  NULL,
    [DATE_REGISTRATION] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [GUID] in table 'ETUDIANTS'
ALTER TABLE [dbo].[ETUDIANTS]
ADD CONSTRAINT [PK_ETUDIANTS]
    PRIMARY KEY CLUSTERED ([GUID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------