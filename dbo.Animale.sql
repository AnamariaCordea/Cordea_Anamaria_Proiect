CREATE TABLE [dbo].[Animale] (
    [AnimalId] INT NOT NULL IDENTITY,
    [Tip]      NVARCHAR (50) NULL,
    [Rasa]     NVARCHAR (50) NULL,
    [Varsta]   NVARCHAR (50) NULL,
    [Problema] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([AnimalId] ASC)
);

