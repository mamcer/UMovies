CREATE TABLE [dbo].[Movie] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (255) NOT NULL,
    [FilePath] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_dbo.Movie] PRIMARY KEY CLUSTERED ([Id] ASC)
);

