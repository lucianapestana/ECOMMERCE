USE [ECOMMERCE]
GO

INSERT INTO TB_USUARIOS (NOME, EMAIL)
VALUES ('Luciana', 'luciana.pestana@hotmail.com')
GO

INSERT INTO TB_CATEGORIAS (CATEGORIA_ID, ATIVO, USUARIO_INSERCAO_ID, DATA_INSERCAO, DESCRICAO, PORCENTAGEM_DESCONTO)
SELECT 1 AS CATEGORIA_ID, 1 AS ATIVO, 1 AS USUARIO_INSERCAO_ID, GETDATE() AS DATA_INSERCAO, 'REGULAR' AS DESCRICAO, 5 AS PORCENTAGEM_DESCONTO UNION ALL
SELECT 2 AS CATEGORIA_ID, 1 AS ATIVO, 1 AS USUARIO_INSERCAO_ID, GETDATE() AS DATA_INSERCAO, 'PREMIUM' AS DESCRICAO, 10 AS PORCENTAGEM_DESCONTO UNION ALL
SELECT 3 AS CATEGORIA_ID, 1 AS ATIVO, 1 AS USUARIO_INSERCAO_ID, GETDATE() AS DATA_INSERCAO, 'VIP' AS DESCRICAO, 15 AS PORCENTAGEM_DESCONTO
GO

INSERT INTO TB_CLIENTES (CLIENTE_ID, ATIVO, USUARIO_INSERCAO_ID, DATA_INSERCAO, NOME, CPF, CATEGORIA_ID)
SELECT '104B4852-228E-4DBA-AE12-345CDE66EA25' AS CLIENTE_ID, 
       1 AS ATIVO, 
	   1 AS USUARIO_INSERCAO_ID, 
	   GETDATE() AS DATA_INSERCAO,
       'Jo�o' AS NOME, 
	   '111.222.333-44' AS CPF,
	   1 AS CATEGORIA_ID
GO

INSERT INTO TB_PRODUTOS (ATIVO, USUARIO_INSERCAO_ID, DATA_INSERCAO, DESCRICAO, PRECO_UNITARIO)
SELECT 1 AS ATIVO, 1 AS USUARIO_INSERCAO_ID, GETDATE() AS DATA_INSERCAO, 'Camisa Polo Preta M' AS DESCRICAO, 149.99 AS PRECO_UNITARIO UNION ALL
SELECT 1 AS ATIVO, 1 AS USUARIO_INSERCAO_ID, GETDATE() AS DATA_INSERCAO, 'Cal�a Jeans 42' AS DESCRICAO, 89.90 AS PRECO_UNITARIO

INSERT INTO TB_STATUS_PEDIDOS (STATUS_PEDIDO_ID, ATIVO, USUARIO_INSERCAO_ID, DATA_INSERCAO, DESCRICAO)
SELECT 1 AS STATUS_PEDIDO_ID, 1 AS ATIVO, 1 AS USUARIO_INSERCAO_ID, GETDATE() AS DATA_INSERCAO, 'ABERTO' AS DESCRICAO UNION ALL
SELECT 2 AS STATUS_PEDIDO_ID, 1 AS ATIVO, 1 AS USUARIO_INSERCAO_ID, GETDATE() AS DATA_INSERCAO, 'CANCELADO' AS DESCRICAO UNION ALL
SELECT 3 AS STATUS_PEDIDO_ID, 1 AS ATIVO, 1 AS USUARIO_INSERCAO_ID, GETDATE() AS DATA_INSERCAO, 'CONCLUIDO' AS DESCRICAO
GO

