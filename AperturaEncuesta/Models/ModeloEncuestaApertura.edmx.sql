
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/11/2017 12:39:16
-- Generated from EDMX file: C:\Users\Kunio\Source\Repos\ItcEncuestas\AperturaEncuesta\Models\ModeloEncuestaApertura.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EncuestaApertura];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CiudadPlanCiudad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanCiudad] DROP CONSTRAINT [FK_CiudadPlanCiudad];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanPlanCiudad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanCiudad] DROP CONSTRAINT [FK_PlanPlanCiudad];
GO
IF OBJECT_ID(N'[dbo].[FK_CapacitacionPadron]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Padron] DROP CONSTRAINT [FK_CapacitacionPadron];
GO
IF OBJECT_ID(N'[dbo].[FK_PadronPlanCiudad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Padron] DROP CONSTRAINT [FK_PadronPlanCiudad];
GO
IF OBJECT_ID(N'[dbo].[FK_PadronEncuestaApertura]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Padron] DROP CONSTRAINT [FK_PadronEncuestaApertura];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Capacitacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Capacitacion];
GO
IF OBJECT_ID(N'[dbo].[Ciudad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ciudad];
GO
IF OBJECT_ID(N'[dbo].[Plan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Plan];
GO
IF OBJECT_ID(N'[dbo].[PlanCiudad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanCiudad];
GO
IF OBJECT_ID(N'[dbo].[Padron]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Padron];
GO
IF OBJECT_ID(N'[dbo].[EncuestaApertura]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EncuestaApertura];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Capacitacion'
CREATE TABLE [dbo].[Capacitacion] (
    [IdCapacitacion] int IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Contenido] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Ciudad'
CREATE TABLE [dbo].[Ciudad] (
    [IdCiudad] int IDENTITY(1,1) NOT NULL,
    [NombreCiudad] nvarchar(max)  NOT NULL,
    [HorasDuracion] int  NOT NULL,
    [Jornadas] int  NOT NULL,
    [FrecuenciaSemanal] int  NOT NULL,
    [SalonDireccion] nvarchar(max)  NOT NULL,
    [FechaInicio] datetime  NOT NULL
);
GO

-- Creating table 'Plan'
CREATE TABLE [dbo].[Plan] (
    [IdPlan] int IDENTITY(1,1) NOT NULL,
    [PlanDescripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlanCiudad'
CREATE TABLE [dbo].[PlanCiudad] (
    [IdPlanCiudad] int IDENTITY(1,1) NOT NULL,
    [IdCiudad] int  NOT NULL,
    [IdPlan] int  NOT NULL,
    [MatriculaValor] nvarchar(max)  NOT NULL,
    [CuotaCantidad] int  NOT NULL,
    [CuotaValor] nvarchar(max)  NOT NULL,
    [CuotaValorDescuento] nvarchar(max)  NOT NULL,
    [CertificadoValor] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Padron'
CREATE TABLE [dbo].[Padron] (
    [IdPadron] int IDENTITY(1,1) NOT NULL,
    [AlumnoNombre] nvarchar(max)  NOT NULL,
    [AlumnoDNI] int  NOT NULL,
    [AlumnoMail] nvarchar(max)  NOT NULL,
    [AlumnoCel] nvarchar(max)  NOT NULL,
    [AlumnoHora] nvarchar(max)  NOT NULL,
    [AlumnoDia] nvarchar(max)  NOT NULL,
    [IdCapacitacion] int  NOT NULL,
    [IdPlanCiudad] int  NOT NULL,
    [AlumnoEdad] int  NOT NULL,
    [AlumnoDomicilio] nvarchar(max)  NOT NULL,
    [AlumnoTel] nvarchar(max)  NOT NULL,
    [AlumnoBarrio] nvarchar(max)  NOT NULL,
    [IdEncuestaApertura] int  NOT NULL
);
GO

-- Creating table 'EncuestaApertura'
CREATE TABLE [dbo].[EncuestaApertura] (
    [IdEncuestaApertura] int IDENTITY(1,1) NOT NULL,
    [EncuestaAsesoramiento] int  NOT NULL,
    [EncuestaSugerencia] nvarchar(max)  NOT NULL,
    [Finalizado] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdCapacitacion] in table 'Capacitacion'
ALTER TABLE [dbo].[Capacitacion]
ADD CONSTRAINT [PK_Capacitacion]
    PRIMARY KEY CLUSTERED ([IdCapacitacion] ASC);
GO

-- Creating primary key on [IdCiudad] in table 'Ciudad'
ALTER TABLE [dbo].[Ciudad]
ADD CONSTRAINT [PK_Ciudad]
    PRIMARY KEY CLUSTERED ([IdCiudad] ASC);
GO

-- Creating primary key on [IdPlan] in table 'Plan'
ALTER TABLE [dbo].[Plan]
ADD CONSTRAINT [PK_Plan]
    PRIMARY KEY CLUSTERED ([IdPlan] ASC);
GO

-- Creating primary key on [IdPlanCiudad] in table 'PlanCiudad'
ALTER TABLE [dbo].[PlanCiudad]
ADD CONSTRAINT [PK_PlanCiudad]
    PRIMARY KEY CLUSTERED ([IdPlanCiudad] ASC);
GO

-- Creating primary key on [IdPadron] in table 'Padron'
ALTER TABLE [dbo].[Padron]
ADD CONSTRAINT [PK_Padron]
    PRIMARY KEY CLUSTERED ([IdPadron] ASC);
GO

-- Creating primary key on [IdEncuestaApertura] in table 'EncuestaApertura'
ALTER TABLE [dbo].[EncuestaApertura]
ADD CONSTRAINT [PK_EncuestaApertura]
    PRIMARY KEY CLUSTERED ([IdEncuestaApertura] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdCiudad] in table 'PlanCiudad'
ALTER TABLE [dbo].[PlanCiudad]
ADD CONSTRAINT [FK_CiudadPlanCiudad]
    FOREIGN KEY ([IdCiudad])
    REFERENCES [dbo].[Ciudad]
        ([IdCiudad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CiudadPlanCiudad'
CREATE INDEX [IX_FK_CiudadPlanCiudad]
ON [dbo].[PlanCiudad]
    ([IdCiudad]);
GO

-- Creating foreign key on [IdPlan] in table 'PlanCiudad'
ALTER TABLE [dbo].[PlanCiudad]
ADD CONSTRAINT [FK_PlanPlanCiudad]
    FOREIGN KEY ([IdPlan])
    REFERENCES [dbo].[Plan]
        ([IdPlan])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanPlanCiudad'
CREATE INDEX [IX_FK_PlanPlanCiudad]
ON [dbo].[PlanCiudad]
    ([IdPlan]);
GO

-- Creating foreign key on [IdCapacitacion] in table 'Padron'
ALTER TABLE [dbo].[Padron]
ADD CONSTRAINT [FK_CapacitacionPadron]
    FOREIGN KEY ([IdCapacitacion])
    REFERENCES [dbo].[Capacitacion]
        ([IdCapacitacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CapacitacionPadron'
CREATE INDEX [IX_FK_CapacitacionPadron]
ON [dbo].[Padron]
    ([IdCapacitacion]);
GO

-- Creating foreign key on [IdPlanCiudad] in table 'Padron'
ALTER TABLE [dbo].[Padron]
ADD CONSTRAINT [FK_PadronPlanCiudad]
    FOREIGN KEY ([IdPlanCiudad])
    REFERENCES [dbo].[PlanCiudad]
        ([IdPlanCiudad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PadronPlanCiudad'
CREATE INDEX [IX_FK_PadronPlanCiudad]
ON [dbo].[Padron]
    ([IdPlanCiudad]);
GO

-- Creating foreign key on [IdEncuestaApertura] in table 'Padron'
ALTER TABLE [dbo].[Padron]
ADD CONSTRAINT [FK_PadronEncuestaApertura]
    FOREIGN KEY ([IdEncuestaApertura])
    REFERENCES [dbo].[EncuestaApertura]
        ([IdEncuestaApertura])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PadronEncuestaApertura'
CREATE INDEX [IX_FK_PadronEncuestaApertura]
ON [dbo].[Padron]
    ([IdEncuestaApertura]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------