CREATE TABLE [dbo].[MovieFiles] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [FileName] NVARCHAR (MAX) NULL,
    [Movie_Id] INT            NOT NULL,
    CONSTRAINT [PK_dbo.MovieFiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.MovieFiles_dbo.Movie_Movie_Id] FOREIGN KEY ([Movie_Id]) REFERENCES [dbo].[Movie] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Movie_Id]
    ON [dbo].[MovieFiles]([Movie_Id] ASC);

