CREATE TABLE [dbo].[cargo] (
    [codigo]         INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED ([codigo] ASC),
    [nome]           VARCHAR (255) NULL,
    [descricaoCargo] VARCHAR (MAX) NULL,
    [ativo]          BIT           CONSTRAINT [DEFAULT_cargo_ativo] DEFAULT 1 NOT NULL
);
