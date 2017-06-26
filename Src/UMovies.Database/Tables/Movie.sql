CREATE TABLE [dbo].[Movie] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (255)  NOT NULL,
    [Year]              INT             DEFAULT ((0)) NOT NULL,
    [Sinopsis]          NVARCHAR (2500) NULL,
    [MovieFolder]       NVARCHAR (255)  DEFAULT ('') NOT NULL,
    [ThumbnailFileName] NVARCHAR (255)  NULL,
    CONSTRAINT [PK_dbo.Movie] PRIMARY KEY CLUSTERED ([Id] ASC)
);



