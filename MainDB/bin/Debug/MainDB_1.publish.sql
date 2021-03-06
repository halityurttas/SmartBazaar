﻿/*
Deployment script for sbtemp

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "sbtemp"
:setvar DefaultFilePrefix "sbtemp"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key 9c35226c-1d04-45e9-806e-fb5dcfa7027a is skipped, element [dbo].[Catalog_Products_Properties].[PropName] (SqlSimpleColumn) will not be renamed to Title';


GO
PRINT N'Rename refactoring operation with key 1c997f9a-5fb2-4466-ab20-16eee23d67f5 is skipped, element [dbo].[Catalog_Products].[EAN] (SqlSimpleColumn) will not be renamed to Barcode';


GO
PRINT N'Rename refactoring operation with key 511523b8-0b04-444b-90b7-5569b921a17e is skipped, element [dbo].[Catalog_Products].[MetaDescription] (SqlSimpleColumn) will not be renamed to MetaUrl';


GO
PRINT N'Rename refactoring operation with key 2b1873b5-ed18-4607-8e2b-c4f09f361f0d is skipped, element [dbo].[Pos_Cards].[Referance] (SqlSimpleColumn) will not be renamed to SettingId';


GO
PRINT N'Rename refactoring operation with key 962355b5-c473-4667-9332-082fa2cfdad6, d003dc61-c129-4e1b-8b8f-768d3dc39962 is skipped, element [dbo].[Catalog_Product_Comments].[Title] (SqlSimpleColumn) will not be renamed to UserId';


GO
PRINT N'Rename refactoring operation with key b13e318f-9b12-4ee4-b718-d5f352cb6716 is skipped, element [dbo].[Catalog_Products_Relations].[Id] (SqlSimpleColumn) will not be renamed to GroupId';


GO
PRINT N'Creating [dbo].[Catalog_Brands]...';


GO
CREATE TABLE [dbo].[Catalog_Brands] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (100) NOT NULL,
    [Status]       SMALLINT       NOT NULL,
    [ExternalRef1] VARCHAR (100)  NULL,
    [ExternalRef2] VARCHAR (100)  NULL,
    [ExternalRef3] VARCHAR (100)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Campaigns]...';


GO
CREATE TABLE [dbo].[Catalog_Campaigns] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CampaignMethod] SMALLINT       NOT NULL,
    [StartDate]      DATETIME       NOT NULL,
    [EndDate]        DATETIME       NOT NULL,
    [DiscountMethod] SMALLINT       NOT NULL,
    [DiscountValue]  FLOAT (53)     NOT NULL,
    [Title]          NVARCHAR (250) NOT NULL,
    [Description]    NTEXT          NULL,
    [SliderUrl]      VARCHAR (250)  NULL,
    [Status]         SMALLINT       NOT NULL,
    [MaxUsage]       INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Campaigns_Destinations]...';


GO
CREATE TABLE [dbo].[Catalog_Campaigns_Destinations] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [CampaignId] INT NOT NULL,
    [ProductId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Campaigns_Sources]...';


GO
CREATE TABLE [dbo].[Catalog_Campaigns_Sources] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [CampaignId] INT NOT NULL,
    [ProductId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Categories]...';


GO
CREATE TABLE [dbo].[Catalog_Categories] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ParentId]        INT            NULL,
    [Title]           NVARCHAR (100) NOT NULL,
    [Description]     NTEXT          NULL,
    [Pos]             INT            NOT NULL,
    [Level]           INT            NOT NULL,
    [Status]          SMALLINT       NOT NULL,
    [ExternalRef1]    VARCHAR (100)  NULL,
    [ExternalRef2]    VARCHAR (100)  NULL,
    [ExternalRef3]    VARCHAR (100)  NULL,
    [IsDisplayInMenu] BIT            NOT NULL,
    [ImageUrl]        NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Product_Comments]...';


GO
CREATE TABLE [dbo].[Catalog_Product_Comments] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      NVARCHAR (128) NOT NULL,
    [UserTitle]   NVARCHAR (150) NOT NULL,
    [Rating]      SMALLINT       NOT NULL,
    [Description] NTEXT          NOT NULL,
    [Status]      SMALLINT       NOT NULL,
    [ProductId]   INT            NOT NULL,
    [Created]     DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Product_Images]...';


GO
CREATE TABLE [dbo].[Catalog_Product_Images] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [ProductId] INT           NOT NULL,
    [ImageUrl]  VARCHAR (250) NOT NULL,
    [Sort]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Products]...';


GO
CREATE TABLE [dbo].[Catalog_Products] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ProductCode]      VARCHAR (50)   NOT NULL,
    [CategoryId]       INT            NOT NULL,
    [Title]            NVARCHAR (200) NOT NULL,
    [Description]      NTEXT          NULL,
    [BrandId]          INT            NULL,
    [ImageUrl]         VARCHAR (250)  NULL,
    [Stock]            INT            NOT NULL,
    [SearchText]       NVARCHAR (255) NULL,
    [Price1]           MONEY          NOT NULL,
    [Price2]           MONEY          NULL,
    [Price3]           MONEY          NULL,
    [Price4]           MONEY          NULL,
    [Price5]           MONEY          NULL,
    [TaxGroup]         INT            NOT NULL,
    [Status]           SMALLINT       NOT NULL,
    [IsFeatured]       BIT            NOT NULL,
    [IsNew]            BIT            NOT NULL,
    [MetaTitle]        NVARCHAR (100) NULL,
    [MetaKeywords]     NVARCHAR (150) NULL,
    [MetaDescription]  NVARCHAR (250) NULL,
    [MaxInstallment]   INT            NOT NULL,
    [ShortDescription] NTEXT          NULL,
    [ManagerNotes]     NTEXT          NULL,
    [IsHomepage]       BIT            NOT NULL,
    [SKU]              VARCHAR (25)   NULL,
    [MPN]              VARCHAR (25)   NULL,
    [Barcode]          NVARCHAR (250) NULL,
    [IsBuyable]        BIT            NOT NULL,
    [IsShipable]       BIT            NOT NULL,
    [IsFreeShip]       BIT            NOT NULL,
    [Tare]             FLOAT (53)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Products_Properties]...';


GO
CREATE TABLE [dbo].[Catalog_Products_Properties] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ProductId] INT            NOT NULL,
    [Title]     NVARCHAR (150) NOT NULL,
    [Value]     NVARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Catalog_Products_Relations]...';


GO
CREATE TABLE [dbo].[Catalog_Products_Relations] (
    [Id]        INT              IDENTITY (1, 1) NOT NULL,
    [GroupId]   UNIQUEIDENTIFIER NOT NULL,
    [ProductId] INT              NOT NULL,
    CONSTRAINT [PK_Catalog_Products_Relations] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Customer_Addresses]...';


GO
CREATE TABLE [dbo].[Customer_Addresses] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [CustomerId] INT            NOT NULL,
    [Title]      NVARCHAR (150) NOT NULL,
    [City]       NVARCHAR (50)  NOT NULL,
    [District]   NVARCHAR (50)  NULL,
    [Town]       NVARCHAR (50)  NULL,
    [Detail]     NTEXT          NULL,
    [Status]     SMALLINT       NOT NULL,
    [IsDefault]  BIT            NOT NULL,
    [PostalCode] VARCHAR (15)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Customer_Entities]...';


GO
CREATE TABLE [dbo].[Customer_Entities] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserId]       NVARCHAR (128) NOT NULL,
    [GroupId]      INT            NULL,
    [Title]        NVARCHAR (150) NOT NULL,
    [TaxNr]        VARCHAR (25)   NULL,
    [TaxOffice]    NVARCHAR (100) NULL,
    [CustomerType] SMALLINT       NOT NULL,
    [BirthDate]    DATETIME       NULL,
    [Gender]       SMALLINT       NULL,
    [Company]      NVARCHAR (250) NULL,
    [ContactPhone] VARCHAR (25)   NULL,
    [Status]       SMALLINT       NOT NULL,
    [Created]      DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Customer_Groups]...';


GO
CREATE TABLE [dbo].[Customer_Groups] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (150) NOT NULL,
    [PriceIndex] SMALLINT       NOT NULL,
    [Status]     SMALLINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[HtmlBlocks]...';


GO
CREATE TABLE [dbo].[HtmlBlocks] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (150) NOT NULL,
    [Detail]   NTEXT          NULL,
    [Status]   SMALLINT       NOT NULL,
    [Location] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Order_Heads]...';


GO
CREATE TABLE [dbo].[Order_Heads] (
    [Id]               INT      IDENTITY (1, 1) NOT NULL,
    [CustomerId]       INT      NOT NULL,
    [ShipAddressId]    INT      NOT NULL,
    [InvoiceAddressId] INT      NULL,
    [OrderDate]        DATETIME NOT NULL,
    [OrderTotal]       MONEY    NOT NULL,
    [TaxTotal]         MONEY    NOT NULL,
    [ShipmentTypeId]   INT      NOT NULL,
    [ShipCost]         MONEY    NOT NULL,
    [GrandTotal]       MONEY    NOT NULL,
    [Status]           SMALLINT NOT NULL,
    [PaymentFee]       MONEY    NOT NULL,
    [InstallmentFee]   MONEY    NOT NULL,
    [Note]             NTEXT    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Order_Lines]...';


GO
CREATE TABLE [dbo].[Order_Lines] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [OrderId]    INT        NOT NULL,
    [ProductId]  INT        NOT NULL,
    [Quantity]   INT        NOT NULL,
    [Price]      MONEY      NOT NULL,
    [TaxRate]    FLOAT (53) NOT NULL,
    [Total]      MONEY      NOT NULL,
    [CampaignId] INT        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Pages]...';


GO
CREATE TABLE [dbo].[Pages] (
    [Id]      INT            NOT NULL,
    [Status]  SMALLINT       NOT NULL,
    [Title]   NVARCHAR (250) NOT NULL,
    [Detail]  NTEXT          NULL,
    [IsFixed] BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Params]...';


GO
CREATE TABLE [dbo].[Params] (
    [Id]      INT            NOT NULL,
    [Title]   NVARCHAR (250) NOT NULL,
    [GroupId] INT            NOT NULL,
    [Value]   NVARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Params_Groups]...';


GO
CREATE TABLE [dbo].[Params_Groups] (
    [Id]    INT            NOT NULL,
    [Title] NVARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Payment_Entities]...';


GO
CREATE TABLE [dbo].[Payment_Entities] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [OrderId]        INT      NOT NULL,
    [PaymentTypeId]  INT      NOT NULL,
    [InstallmentId]  INT      NULL,
    [OrderPrice]     MONEY    NOT NULL,
    [PaymentFee]     MONEY    NOT NULL,
    [InstallmentFee] MONEY    NOT NULL,
    [ShipmentFee]    MONEY    NOT NULL,
    [Total]          MONEY    NOT NULL,
    [PaymentDate]    DATETIME NOT NULL,
    [Log]            NTEXT    NOT NULL,
    [Status]         SMALLINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Payment_Installment]...';


GO
CREATE TABLE [dbo].[Payment_Installment] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [PaymentId] INT        NOT NULL,
    [NumberOf]  INT        NOT NULL,
    [Rate]      FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Payment_Types]...';


GO
CREATE TABLE [dbo].[Payment_Types] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Method]         SMALLINT       NOT NULL,
    [CommissionRate] FLOAT (53)     NOT NULL,
    [ProcessFee]     MONEY          NOT NULL,
    [Title]          NVARCHAR (100) NOT NULL,
    [Status]         SMALLINT       NOT NULL,
    [Description]    NTEXT          NULL,
    [PosType]        VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Pos_Settings]...';


GO
CREATE TABLE [dbo].[Pos_Settings] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Data]         NTEXT          NOT NULL,
    [AssemblyData] NVARCHAR (100) NOT NULL,
    [Referance]    NVARCHAR (100) NOT NULL,
    [Title]        NVARCHAR (250) NOT NULL,
    [Status]       SMALLINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Settings]...';


GO
CREATE TABLE [dbo].[Settings] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Value] VARCHAR (50)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Shipment_Types]...';


GO
CREATE TABLE [dbo].[Shipment_Types] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [PricingMethod] SMALLINT       NOT NULL,
    [PricingValue]  FLOAT (53)     NOT NULL,
    [Title]         NVARCHAR (250) NOT NULL,
    [Status]        SMALLINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Slider]...';


GO
CREATE TABLE [dbo].[Slider] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (250) NOT NULL,
    [Detail]   NTEXT          NULL,
    [ImageUrl] NVARCHAR (100) NOT NULL,
    [Status]   SMALLINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Tax_Products]...';


GO
CREATE TABLE [dbo].[Tax_Products] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Title]  NVARCHAR (150) NOT NULL,
    [Rate]   FLOAT (53)     NOT NULL,
    [Status] SMALLINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Brands]...';


GO
ALTER TABLE [dbo].[Catalog_Brands]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Campaigns]...';


GO
ALTER TABLE [dbo].[Catalog_Campaigns]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Campaigns]...';


GO
ALTER TABLE [dbo].[Catalog_Campaigns]
    ADD DEFAULT 0 FOR [MaxUsage];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Categories]...';


GO
ALTER TABLE [dbo].[Catalog_Categories]
    ADD DEFAULT 1 FOR [Pos];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Categories]...';


GO
ALTER TABLE [dbo].[Catalog_Categories]
    ADD DEFAULT 0 FOR [Level];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Categories]...';


GO
ALTER TABLE [dbo].[Catalog_Categories]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Categories]...';


GO
ALTER TABLE [dbo].[Catalog_Categories]
    ADD DEFAULT 0 FOR [IsDisplayInMenu];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Product_Images]...';


GO
ALTER TABLE [dbo].[Catalog_Product_Images]
    ADD DEFAULT 1 FOR [Sort];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT -1 FOR [Stock];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 0 FOR [Price1];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 0 FOR [IsFeatured];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 0 FOR [IsNew];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 0 FOR [MaxInstallment];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 0 FOR [IsHomepage];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 1 FOR [IsBuyable];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 1 FOR [IsShipable];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 0 FOR [IsFreeShip];


GO
PRINT N'Creating unnamed constraint on [dbo].[Catalog_Products]...';


GO
ALTER TABLE [dbo].[Catalog_Products]
    ADD DEFAULT 0 FOR [Tare];


GO
PRINT N'Creating unnamed constraint on [dbo].[Customer_Addresses]...';


GO
ALTER TABLE [dbo].[Customer_Addresses]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Customer_Addresses]...';


GO
ALTER TABLE [dbo].[Customer_Addresses]
    ADD DEFAULT 0 FOR [IsDefault];


GO
PRINT N'Creating unnamed constraint on [dbo].[Customer_Entities]...';


GO
ALTER TABLE [dbo].[Customer_Entities]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Customer_Entities]...';


GO
ALTER TABLE [dbo].[Customer_Entities]
    ADD DEFAULT GETDATE() FOR [Created];


GO
PRINT N'Creating unnamed constraint on [dbo].[Customer_Groups]...';


GO
ALTER TABLE [dbo].[Customer_Groups]
    ADD DEFAULT 1 FOR [PriceIndex];


GO
PRINT N'Creating unnamed constraint on [dbo].[Customer_Groups]...';


GO
ALTER TABLE [dbo].[Customer_Groups]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[HtmlBlocks]...';


GO
ALTER TABLE [dbo].[HtmlBlocks]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[HtmlBlocks]...';


GO
ALTER TABLE [dbo].[HtmlBlocks]
    ADD DEFAULT 0 FOR [Location];


GO
PRINT N'Creating unnamed constraint on [dbo].[Order_Heads]...';


GO
ALTER TABLE [dbo].[Order_Heads]
    ADD DEFAULT GETDATE() FOR [OrderDate];


GO
PRINT N'Creating unnamed constraint on [dbo].[Order_Heads]...';


GO
ALTER TABLE [dbo].[Order_Heads]
    ADD DEFAULT 0 FOR [ShipCost];


GO
PRINT N'Creating unnamed constraint on [dbo].[Order_Heads]...';


GO
ALTER TABLE [dbo].[Order_Heads]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Pages]...';


GO
ALTER TABLE [dbo].[Pages]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Pages]...';


GO
ALTER TABLE [dbo].[Pages]
    ADD DEFAULT 0 FOR [IsFixed];


GO
PRINT N'Creating unnamed constraint on [dbo].[Payment_Entities]...';


GO
ALTER TABLE [dbo].[Payment_Entities]
    ADD DEFAULT GETDATE() FOR [PaymentDate];


GO
PRINT N'Creating unnamed constraint on [dbo].[Payment_Entities]...';


GO
ALTER TABLE [dbo].[Payment_Entities]
    ADD DEFAULT 0 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Payment_Installment]...';


GO
ALTER TABLE [dbo].[Payment_Installment]
    ADD DEFAULT 0 FOR [Rate];


GO
PRINT N'Creating unnamed constraint on [dbo].[Payment_Types]...';


GO
ALTER TABLE [dbo].[Payment_Types]
    ADD DEFAULT 0 FOR [CommissionRate];


GO
PRINT N'Creating unnamed constraint on [dbo].[Payment_Types]...';


GO
ALTER TABLE [dbo].[Payment_Types]
    ADD DEFAULT 0 FOR [ProcessFee];


GO
PRINT N'Creating unnamed constraint on [dbo].[Payment_Types]...';


GO
ALTER TABLE [dbo].[Payment_Types]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Shipment_Types]...';


GO
ALTER TABLE [dbo].[Shipment_Types]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating unnamed constraint on [dbo].[Tax_Products]...';


GO
ALTER TABLE [dbo].[Tax_Products]
    ADD DEFAULT 0 FOR [Rate];


GO
PRINT N'Creating unnamed constraint on [dbo].[Tax_Products]...';


GO
ALTER TABLE [dbo].[Tax_Products]
    ADD DEFAULT 1 FOR [Status];


GO
PRINT N'Creating [dbo].[CatalogCampaignsDestinationsProductConstraint]...';


GO
ALTER TABLE [dbo].[Catalog_Campaigns_Destinations] WITH NOCHECK
    ADD CONSTRAINT [CatalogCampaignsDestinationsProductConstraint] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Catalog_Products] ([Id]);


GO
PRINT N'Creating [dbo].[CatalogCampaignsDestinationConstraint]...';


GO
ALTER TABLE [dbo].[Catalog_Campaigns_Destinations] WITH NOCHECK
    ADD CONSTRAINT [CatalogCampaignsDestinationConstraint] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Catalog_Campaigns] ([Id]);


GO
PRINT N'Creating [dbo].[CatalogCampaignsSourceProductConstraint]...';


GO
ALTER TABLE [dbo].[Catalog_Campaigns_Sources] WITH NOCHECK
    ADD CONSTRAINT [CatalogCampaignsSourceProductConstraint] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Catalog_Products] ([Id]);


GO
PRINT N'Creating [dbo].[CatalogCampaignsSourcesConstraint]...';


GO
ALTER TABLE [dbo].[Catalog_Campaigns_Sources] WITH NOCHECK
    ADD CONSTRAINT [CatalogCampaignsSourcesConstraint] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Catalog_Campaigns] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Catalog_Product_Comments_Product]...';


GO
ALTER TABLE [dbo].[Catalog_Product_Comments] WITH NOCHECK
    ADD CONSTRAINT [FK_Catalog_Product_Comments_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Catalog_Products] ([Id]);


GO
PRINT N'Creating [dbo].[ProductImagesConstraints]...';


GO
ALTER TABLE [dbo].[Catalog_Product_Images] WITH NOCHECK
    ADD CONSTRAINT [ProductImagesConstraints] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Catalog_Products] ([Id]);


GO
PRINT N'Creating [dbo].[ProductTaxConstraint]...';


GO
ALTER TABLE [dbo].[Catalog_Products] WITH NOCHECK
    ADD CONSTRAINT [ProductTaxConstraint] FOREIGN KEY ([TaxGroup]) REFERENCES [dbo].[Tax_Products] ([Id]);


GO
PRINT N'Creating [dbo].[ProductCategoryConstraint]...';


GO
ALTER TABLE [dbo].[Catalog_Products] WITH NOCHECK
    ADD CONSTRAINT [ProductCategoryConstraint] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Catalog_Categories] ([Id]);


GO
PRINT N'Creating [dbo].[ProductBrandsConstraint]...';


GO
ALTER TABLE [dbo].[Catalog_Products] WITH NOCHECK
    ADD CONSTRAINT [ProductBrandsConstraint] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Catalog_Brands] ([Id]);


GO
PRINT N'Creating [dbo].[ProductPropertiesConstraint]...';


GO
ALTER TABLE [dbo].[Catalog_Products_Properties] WITH NOCHECK
    ADD CONSTRAINT [ProductPropertiesConstraint] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Catalog_Products] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Catalog_Products_Relations_CatalogProducts]...';


GO
ALTER TABLE [dbo].[Catalog_Products_Relations] WITH NOCHECK
    ADD CONSTRAINT [FK_Catalog_Products_Relations_CatalogProducts] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Catalog_Products] ([Id]);


GO
PRINT N'Creating [dbo].[CustomerAddressConstraint]...';


GO
ALTER TABLE [dbo].[Customer_Addresses] WITH NOCHECK
    ADD CONSTRAINT [CustomerAddressConstraint] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer_Entities] ([Id]);


GO
PRINT N'Creating [dbo].[CustomerGroupConstraint]...';


GO
ALTER TABLE [dbo].[Customer_Entities] WITH NOCHECK
    ADD CONSTRAINT [CustomerGroupConstraint] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Customer_Groups] ([Id]);


GO
PRINT N'Creating [dbo].[OrderShipmentConstraint]...';


GO
ALTER TABLE [dbo].[Order_Heads] WITH NOCHECK
    ADD CONSTRAINT [OrderShipmentConstraint] FOREIGN KEY ([ShipmentTypeId]) REFERENCES [dbo].[Shipment_Types] ([Id]);


GO
PRINT N'Creating [dbo].[OrderShipAddressConstraint]...';


GO
ALTER TABLE [dbo].[Order_Heads] WITH NOCHECK
    ADD CONSTRAINT [OrderShipAddressConstraint] FOREIGN KEY ([ShipAddressId]) REFERENCES [dbo].[Customer_Addresses] ([Id]);


GO
PRINT N'Creating [dbo].[OrderInvoiceAddressConstraint]...';


GO
ALTER TABLE [dbo].[Order_Heads] WITH NOCHECK
    ADD CONSTRAINT [OrderInvoiceAddressConstraint] FOREIGN KEY ([InvoiceAddressId]) REFERENCES [dbo].[Customer_Addresses] ([Id]);


GO
PRINT N'Creating [dbo].[OrderCustomerConstraint]...';


GO
ALTER TABLE [dbo].[Order_Heads] WITH NOCHECK
    ADD CONSTRAINT [OrderCustomerConstraint] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer_Entities] ([Id]);


GO
PRINT N'Creating [dbo].[CatalogCampaignsOrderLinesConstraint]...';


GO
ALTER TABLE [dbo].[Order_Lines] WITH NOCHECK
    ADD CONSTRAINT [CatalogCampaignsOrderLinesConstraint] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Catalog_Campaigns] ([Id]);


GO
PRINT N'Creating [dbo].[OrderLinesConstraint]...';


GO
ALTER TABLE [dbo].[Order_Lines] WITH NOCHECK
    ADD CONSTRAINT [OrderLinesConstraint] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order_Heads] ([Id]);


GO
PRINT N'Creating [dbo].[OrderLineProductConstraint]...';


GO
ALTER TABLE [dbo].[Order_Lines] WITH NOCHECK
    ADD CONSTRAINT [OrderLineProductConstraint] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Catalog_Products] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Params_ToParamsGroups]...';


GO
ALTER TABLE [dbo].[Params] WITH NOCHECK
    ADD CONSTRAINT [FK_Params_ToParamsGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Params_Groups] ([Id]);


GO
PRINT N'Creating [dbo].[PaymentEntityTypeConstraint]...';


GO
ALTER TABLE [dbo].[Payment_Entities] WITH NOCHECK
    ADD CONSTRAINT [PaymentEntityTypeConstraint] FOREIGN KEY ([PaymentTypeId]) REFERENCES [dbo].[Payment_Types] ([Id]);


GO
PRINT N'Creating [dbo].[PaymentEntityOrderConstraint]...';


GO
ALTER TABLE [dbo].[Payment_Entities] WITH NOCHECK
    ADD CONSTRAINT [PaymentEntityOrderConstraint] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order_Heads] ([Id]);


GO
PRINT N'Creating [dbo].[PaymentEntityInstallmentConstraint]...';


GO
ALTER TABLE [dbo].[Payment_Entities] WITH NOCHECK
    ADD CONSTRAINT [PaymentEntityInstallmentConstraint] FOREIGN KEY ([InstallmentId]) REFERENCES [dbo].[Payment_Installment] ([Id]);


GO
PRINT N'Creating [dbo].[PaymentInstallmentConstraint]...';


GO
ALTER TABLE [dbo].[Payment_Installment] WITH NOCHECK
    ADD CONSTRAINT [PaymentInstallmentConstraint] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payment_Types] ([Id]);


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9c35226c-1d04-45e9-806e-fb5dcfa7027a')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9c35226c-1d04-45e9-806e-fb5dcfa7027a')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '1c997f9a-5fb2-4466-ab20-16eee23d67f5')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('1c997f9a-5fb2-4466-ab20-16eee23d67f5')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '511523b8-0b04-444b-90b7-5569b921a17e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('511523b8-0b04-444b-90b7-5569b921a17e')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '2b1873b5-ed18-4607-8e2b-c4f09f361f0d')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('2b1873b5-ed18-4607-8e2b-c4f09f361f0d')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '962355b5-c473-4667-9332-082fa2cfdad6')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('962355b5-c473-4667-9332-082fa2cfdad6')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'd003dc61-c129-4e1b-8b8f-768d3dc39962')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('d003dc61-c129-4e1b-8b8f-768d3dc39962')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'b13e318f-9b12-4ee4-b718-d5f352cb6716')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('b13e318f-9b12-4ee4-b718-d5f352cb6716')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Catalog_Campaigns_Destinations] WITH CHECK CHECK CONSTRAINT [CatalogCampaignsDestinationsProductConstraint];

ALTER TABLE [dbo].[Catalog_Campaigns_Destinations] WITH CHECK CHECK CONSTRAINT [CatalogCampaignsDestinationConstraint];

ALTER TABLE [dbo].[Catalog_Campaigns_Sources] WITH CHECK CHECK CONSTRAINT [CatalogCampaignsSourceProductConstraint];

ALTER TABLE [dbo].[Catalog_Campaigns_Sources] WITH CHECK CHECK CONSTRAINT [CatalogCampaignsSourcesConstraint];

ALTER TABLE [dbo].[Catalog_Product_Comments] WITH CHECK CHECK CONSTRAINT [FK_Catalog_Product_Comments_Product];

ALTER TABLE [dbo].[Catalog_Product_Images] WITH CHECK CHECK CONSTRAINT [ProductImagesConstraints];

ALTER TABLE [dbo].[Catalog_Products] WITH CHECK CHECK CONSTRAINT [ProductTaxConstraint];

ALTER TABLE [dbo].[Catalog_Products] WITH CHECK CHECK CONSTRAINT [ProductCategoryConstraint];

ALTER TABLE [dbo].[Catalog_Products] WITH CHECK CHECK CONSTRAINT [ProductBrandsConstraint];

ALTER TABLE [dbo].[Catalog_Products_Properties] WITH CHECK CHECK CONSTRAINT [ProductPropertiesConstraint];

ALTER TABLE [dbo].[Catalog_Products_Relations] WITH CHECK CHECK CONSTRAINT [FK_Catalog_Products_Relations_CatalogProducts];

ALTER TABLE [dbo].[Customer_Addresses] WITH CHECK CHECK CONSTRAINT [CustomerAddressConstraint];

ALTER TABLE [dbo].[Customer_Entities] WITH CHECK CHECK CONSTRAINT [CustomerGroupConstraint];

ALTER TABLE [dbo].[Order_Heads] WITH CHECK CHECK CONSTRAINT [OrderShipmentConstraint];

ALTER TABLE [dbo].[Order_Heads] WITH CHECK CHECK CONSTRAINT [OrderShipAddressConstraint];

ALTER TABLE [dbo].[Order_Heads] WITH CHECK CHECK CONSTRAINT [OrderInvoiceAddressConstraint];

ALTER TABLE [dbo].[Order_Heads] WITH CHECK CHECK CONSTRAINT [OrderCustomerConstraint];

ALTER TABLE [dbo].[Order_Lines] WITH CHECK CHECK CONSTRAINT [CatalogCampaignsOrderLinesConstraint];

ALTER TABLE [dbo].[Order_Lines] WITH CHECK CHECK CONSTRAINT [OrderLinesConstraint];

ALTER TABLE [dbo].[Order_Lines] WITH CHECK CHECK CONSTRAINT [OrderLineProductConstraint];

ALTER TABLE [dbo].[Params] WITH CHECK CHECK CONSTRAINT [FK_Params_ToParamsGroups];

ALTER TABLE [dbo].[Payment_Entities] WITH CHECK CHECK CONSTRAINT [PaymentEntityTypeConstraint];

ALTER TABLE [dbo].[Payment_Entities] WITH CHECK CHECK CONSTRAINT [PaymentEntityOrderConstraint];

ALTER TABLE [dbo].[Payment_Entities] WITH CHECK CHECK CONSTRAINT [PaymentEntityInstallmentConstraint];

ALTER TABLE [dbo].[Payment_Installment] WITH CHECK CHECK CONSTRAINT [PaymentInstallmentConstraint];


GO
PRINT N'Update complete.';


GO
