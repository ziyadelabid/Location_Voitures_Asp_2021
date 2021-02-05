
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/05/2021 14:49:51
-- Generated from EDMX file: C:\Users\OMAR\Desktop\Projet_ASP\Location_Voitures_Asp_2021\Projet-Asp-Location-Voitures-2021\LocationVoituresModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [locationVoitures];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Voiture_Proprietaire]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Voiture] DROP CONSTRAINT [FK_Voiture_Proprietaire];
GO
IF OBJECT_ID(N'[dbo].[FK_LocataireReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationSet] DROP CONSTRAINT [FK_LocataireReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationVoiture]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationSet] DROP CONSTRAINT [FK_ReservationVoiture];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationProprietaire]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationSet] DROP CONSTRAINT [FK_ReservationProprietaire];
GO
IF OBJECT_ID(N'[dbo].[FK_Locataire_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Locataire] DROP CONSTRAINT [FK_Locataire_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Proprietaire_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Proprietaire] DROP CONSTRAINT [FK_Proprietaire_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Locataire]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Locataire];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Proprietaire]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Proprietaire];
GO
IF OBJECT_ID(N'[dbo].[Voiture]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Voiture];
GO
IF OBJECT_ID(N'[dbo].[ReservationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReservationSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Phone_number] nvarchar(max)  NOT NULL,
    [Adresse] nvarchar(max)  NOT NULL,
    [Role] nvarchar(max)  NOT NULL,
    [Nom] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserSet_Locataire'
CREATE TABLE [dbo].[UserSet_Locataire] (
    [image_Locataire] varbinary(max)  NULL,
    [Cin] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'UserSet_Proprietaire'
CREATE TABLE [dbo].[UserSet_Proprietaire] (
    [image_Proprietaire] varbinary(max)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Voiture'
CREATE TABLE [dbo].[Voiture] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Marque] nvarchar(50)  NOT NULL,
    [Couleur] nvarchar(50)  NOT NULL,
    [Annee] int  NULL,
    [image_Voiture] varbinary(max)  NOT NULL,
    [Id_Proprietaire] int  NOT NULL,
    [Prix] decimal(18,2)  NOT NULL,
    [Promotion] smallint  NULL
);
GO

-- Creating table 'ReservationSet'
CREATE TABLE [dbo].[ReservationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date_reservation] datetime  NOT NULL,
    [Date_retour] datetime  NOT NULL,
    [Montant] decimal(18,2)  NOT NULL,
    [LocataireId] int  NOT NULL,
    [VoitureId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Locataire'
ALTER TABLE [dbo].[UserSet_Locataire]
ADD CONSTRAINT [PK_UserSet_Locataire]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Proprietaire'
ALTER TABLE [dbo].[UserSet_Proprietaire]
ADD CONSTRAINT [PK_UserSet_Proprietaire]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Voiture'
ALTER TABLE [dbo].[Voiture]
ADD CONSTRAINT [PK_Voiture]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [PK_ReservationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_Proprietaire] in table 'Voiture'
ALTER TABLE [dbo].[Voiture]
ADD CONSTRAINT [FK_Voiture_Proprietaire]
    FOREIGN KEY ([Id_Proprietaire])
    REFERENCES [dbo].[UserSet_Proprietaire]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Voiture_Proprietaire'
CREATE INDEX [IX_FK_Voiture_Proprietaire]
ON [dbo].[Voiture]
    ([Id_Proprietaire]);
GO

-- Creating foreign key on [LocataireId] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [FK_LocataireReservation]
    FOREIGN KEY ([LocataireId])
    REFERENCES [dbo].[UserSet_Locataire]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocataireReservation'
CREATE INDEX [IX_FK_LocataireReservation]
ON [dbo].[ReservationSet]
    ([LocataireId]);
GO

-- Creating foreign key on [VoitureId] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [FK_ReservationVoiture]
    FOREIGN KEY ([VoitureId])
    REFERENCES [dbo].[Voiture]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationVoiture'
CREATE INDEX [IX_FK_ReservationVoiture]
ON [dbo].[ReservationSet]
    ([VoitureId]);
GO

-- Creating foreign key on [Id] in table 'UserSet_Locataire'
ALTER TABLE [dbo].[UserSet_Locataire]
ADD CONSTRAINT [FK_Locataire_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserSet_Proprietaire'
ALTER TABLE [dbo].[UserSet_Proprietaire]
ADD CONSTRAINT [FK_Proprietaire_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------