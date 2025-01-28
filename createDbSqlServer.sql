CREATE DATABASE RestauranteDB;

SELECT name, database_id, create_date 
FROM sys.databases 
WHERE name = 'RestauranteDB';

USE RestauranteDB;

CREATE TABLE PlatoPrincipal (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Ingredientes NVARCHAR(MAX) NOT NULL
);

CREATE TABLE postre(
id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Nombre NVARCHAR(100) NOT NULL,
Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
Calorias int NOT NULL CHECK (Calorias >= 0),
);

CREATE TABLE bebida(
id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Nombre NVARCHAR(100) NOT NULL,
Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
esAlcoholica TINYINT NOT null
);

CREATE TABLE usuario(
id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Nombre NVARCHAR(100) NOT NULL,
Apellidos NVARCHAR(100) NOT NULL,
NombreUsuario NVARCHAR(100) NOT NULL
);

CREATE TABLE Compra(
Id INT IDENTITY(1,1) PRIMARY KEY,
FkIdUsuario INT NOT NULL,
FechaCompra DATETIME NOT NULL,
    CONSTRAINT FK_IdUsuario FOREIGN KEY (FkIdUsuario)
        REFERENCES Usuario(Id)
);

CREATE TABLE LineaDePedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FkIdCompra INT NOT NULL,
    PlatoPrincipalId INT NULL,
    PostreId INT NULL,
    BebidaId INT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    CONSTRAINT FK_IdCompra FOREIGN KEY (FkIdCompra)
        REFERENCES Compra(Id),
    CONSTRAINT FK_PlatoPrincipal FOREIGN KEY (PlatoPrincipalId)
        REFERENCES PlatoPrincipal(Id),
    CONSTRAINT FK_Postre FOREIGN KEY (PostreId)
        REFERENCES Postre(Id),
    CONSTRAINT FK_Bebida FOREIGN KEY (BebidaId)
        REFERENCES Bebida(Id),
    -- Restricciones de unicidad por tipo de producto
    CONSTRAINT UQ_LineaDePedido_PlatoPrincipal UNIQUE (FkIdCompra, PlatoPrincipalId),
    CONSTRAINT UQ_LineaDePedido_Postre UNIQUE (FkIdCompra, PostreId),
    CONSTRAINT UQ_LineaDePedido_Bebida UNIQUE (FkIdCompra, BebidaId)
);




INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
VALUES 
('Plato combinado', 12.50, 'Pollo, patatas, tomate'),
('Plato vegetariano', 10.00, 'Tofu, verduras, arroz');

SELECT * FROM PlatoPrincipal;

SELECT * 
FROM PlatoPrincipal
WHERE Ingredientes LIKE '%Tofu%';

SELECT * 
FROM PlatoPrincipal
ORDER BY Precio ASC;

SELECT DISTINCT Nombre, Precio FROM PlatoPrincipal;
