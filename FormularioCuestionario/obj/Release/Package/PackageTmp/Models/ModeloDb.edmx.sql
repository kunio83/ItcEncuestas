
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/21/2017 13:41:06
-- Generated from EDMX file: C:\Users\Kunio\Documents\Visual Studio 2017\Projects\FormularioCuestionario\FormularioCuestionario\Models\ModeloDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Cuestionarios];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Cuestionarios_Capacitaciones]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cuestionarios] DROP CONSTRAINT [FK_Cuestionarios_Capacitaciones];
GO
IF OBJECT_ID(N'[dbo].[FK_Cuestionarios_Ciudades]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cuestionarios] DROP CONSTRAINT [FK_Cuestionarios_Ciudades];
GO
IF OBJECT_ID(N'[dbo].[FK_Referidos_Cuestionarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Referidos] DROP CONSTRAINT [FK_Referidos_Cuestionarios];
GO
IF OBJECT_ID(N'[dbo].[FK_CiudadesInstructores]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ciudades] DROP CONSTRAINT [FK_CiudadesInstructores];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Capacitaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Capacitaciones];
GO
IF OBJECT_ID(N'[dbo].[Ciudades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ciudades];
GO
IF OBJECT_ID(N'[dbo].[Cuestionarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cuestionarios];
GO
IF OBJECT_ID(N'[dbo].[Referidos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Referidos];
GO
IF OBJECT_ID(N'[dbo].[Instructores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Instructores];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Capacitaciones'
CREATE TABLE [dbo].[Capacitaciones] (
    [IdCapacitación] int IDENTITY(1,1) NOT NULL,
    [Descripción] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Ciudades'
CREATE TABLE [dbo].[Ciudades] (
    [IdCiudad] int IDENTITY(1,1) NOT NULL,
    [NombreCiudad] nvarchar(max)  NOT NULL,
    [Instructor] int  NOT NULL,
    [IdCiudadPadron] int  NOT NULL
);
GO

-- Creating table 'Cuestionarios'
CREATE TABLE [dbo].[Cuestionarios] (
    [IdCuestionario] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellido] nvarchar(max)  NULL,
    [DNI] int  NOT NULL,
    [Ciudad] int  NOT NULL,
    [Capacitación] int  NOT NULL,
    [EMail] nvarchar(max)  NOT NULL,
    [Teléfono] nvarchar(max)  NOT NULL,
    [AsesoramientoInscripcion] int  NOT NULL,
    [SalónEquipamiento] int  NOT NULL,
    [Contenidos] int  NOT NULL,
    [IstructorConocimiento] int  NOT NULL,
    [InstructorClaridad] int  NOT NULL,
    [InstructorTrato] int  NOT NULL,
    [ConocimientoAdquirido] int  NOT NULL,
    [Utilidad] int  NOT NULL,
    [SatisfacciónGral] int  NOT NULL,
    [HariaOtro] bit  NOT NULL,
    [Cual] nvarchar(max)  NULL,
    [Metodologia] int  NOT NULL,
    [Material] int  NOT NULL,
    [Duración] int  NOT NULL,
    [Predisposicion] int  NOT NULL,
    [Ejercicios] int  NOT NULL,
    [TratoAdministrativo] int  NOT NULL,
    [Sugerencias] nvarchar(max)  NULL,
    [FechaHora] datetime  NOT NULL,
    [Finalizado] bit  NOT NULL
);
GO

-- Creating table 'Referidos'
CREATE TABLE [dbo].[Referidos] (
    [IdReferido] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellido] nvarchar(max)  NOT NULL,
    [Teléfono] nvarchar(max)  NOT NULL,
    [IdCuestionario] int  NOT NULL
);
GO

-- Creating table 'Instructores'
CREATE TABLE [dbo].[Instructores] (
    [IdInstructor] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellido] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdCapacitación] in table 'Capacitaciones'
ALTER TABLE [dbo].[Capacitaciones]
ADD CONSTRAINT [PK_Capacitaciones]
    PRIMARY KEY CLUSTERED ([IdCapacitación] ASC);
GO

-- Creating primary key on [IdCiudad] in table 'Ciudades'
ALTER TABLE [dbo].[Ciudades]
ADD CONSTRAINT [PK_Ciudades]
    PRIMARY KEY CLUSTERED ([IdCiudad] ASC);
GO

-- Creating primary key on [IdCuestionario] in table 'Cuestionarios'
ALTER TABLE [dbo].[Cuestionarios]
ADD CONSTRAINT [PK_Cuestionarios]
    PRIMARY KEY CLUSTERED ([IdCuestionario] ASC);
GO

-- Creating primary key on [IdReferido] in table 'Referidos'
ALTER TABLE [dbo].[Referidos]
ADD CONSTRAINT [PK_Referidos]
    PRIMARY KEY CLUSTERED ([IdReferido] ASC);
GO

-- Creating primary key on [IdInstructor] in table 'Instructores'
ALTER TABLE [dbo].[Instructores]
ADD CONSTRAINT [PK_Instructores]
    PRIMARY KEY CLUSTERED ([IdInstructor] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Capacitación] in table 'Cuestionarios'
ALTER TABLE [dbo].[Cuestionarios]
ADD CONSTRAINT [FK_Cuestionarios_Capacitaciones]
    FOREIGN KEY ([Capacitación])
    REFERENCES [dbo].[Capacitaciones]
        ([IdCapacitación])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cuestionarios_Capacitaciones'
CREATE INDEX [IX_FK_Cuestionarios_Capacitaciones]
ON [dbo].[Cuestionarios]
    ([Capacitación]);
GO

-- Creating foreign key on [Ciudad] in table 'Cuestionarios'
ALTER TABLE [dbo].[Cuestionarios]
ADD CONSTRAINT [FK_Cuestionarios_Ciudades]
    FOREIGN KEY ([Ciudad])
    REFERENCES [dbo].[Ciudades]
        ([IdCiudad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cuestionarios_Ciudades'
CREATE INDEX [IX_FK_Cuestionarios_Ciudades]
ON [dbo].[Cuestionarios]
    ([Ciudad]);
GO

-- Creating foreign key on [IdCuestionario] in table 'Referidos'
ALTER TABLE [dbo].[Referidos]
ADD CONSTRAINT [FK_Referidos_Cuestionarios]
    FOREIGN KEY ([IdCuestionario])
    REFERENCES [dbo].[Cuestionarios]
        ([IdCuestionario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Referidos_Cuestionarios'
CREATE INDEX [IX_FK_Referidos_Cuestionarios]
ON [dbo].[Referidos]
    ([IdCuestionario]);
GO

-- Creating foreign key on [Instructor] in table 'Ciudades'
ALTER TABLE [dbo].[Ciudades]
ADD CONSTRAINT [FK_CiudadesInstructores]
    FOREIGN KEY ([Instructor])
    REFERENCES [dbo].[Instructores]
        ([IdInstructor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CiudadesInstructores'
CREATE INDEX [IX_FK_CiudadesInstructores]
ON [dbo].[Ciudades]
    ([Instructor]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------