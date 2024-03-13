CREATE TABLE [dbo].[funcionario] (
    [codigo]       INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED ([codigo] ASC),
    [nome]         NVARCHAR (255) NOT NULL,
    [cdcargo]      INT            NULL,
    [valorSalario] MONEY          CONSTRAINT [DEFAULT_funcionario_valorSalario] DEFAULT 0 NULL,
    [ativo]        BIT            CONSTRAINT [DEFAULT_funcionario_ativo] DEFAULT 1 NOT NULL,
    CONSTRAINT [FK_funcionario_cargo] FOREIGN KEY ([cdcargo]) REFERENCES [dbo].[cargo] ([codigo]) ON DELETE SET NULL
);
