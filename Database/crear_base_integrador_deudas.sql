-- Crear base de datos
CREATE DATABASE IntegradorDeudasMega;
GO
USE IntegradorDeudasMega;
GO

-- Tabla Servicios
CREATE TABLE Servicios (
    IdServicio INT PRIMARY KEY,
    Nombre NVARCHAR(100),
    PrecioMensual DECIMAL(10,2),
    PrecioContratacion DECIMAL(10,2)
);

INSERT INTO Servicios VALUES 
(1, 'Internet', 350.00, 100.00),
(2, 'Telefonía', 150.00, 100.00),
(3, 'Televisión', 200.00, 100.00);

-- Tabla Paquetes
CREATE TABLE Paquetes (
    IdPaquete INT PRIMARY KEY,
    NombrePaquete NVARCHAR(100)
);

INSERT INTO Paquetes VALUES
(1, 'DoblePack Residencial'),
(2, 'TriplePack Residencial'),
(3, 'DoblePack Empresarial'),
(4, 'TriplePack Empresarial');

-- Tabla intermedia PaqueteServicios
CREATE TABLE PaqueteServicios (
    IdPaquete INT,
    IdServicio INT,
    PRIMARY KEY (IdPaquete, IdServicio),
    FOREIGN KEY (IdPaquete) REFERENCES Paquetes(IdPaquete),
    FOREIGN KEY (IdServicio) REFERENCES Servicios(IdServicio)
);

INSERT INTO PaqueteServicios VALUES
(1, 1), (1, 2),
(2, 1), (2, 2), (2, 3),
(3, 1), (3, 2),
(4, 1), (4, 2), (4, 3);

-- Tabla Ciudades
CREATE TABLE Ciudades (
    IdCiudad INT PRIMARY KEY,
    NombreCiudad NVARCHAR(100)
);

INSERT INTO Ciudades VALUES
(1, 'Guadalajara'),
(2, 'Oaxaca'),
(3, 'Monterrey');

-- Tabla Colonias
CREATE TABLE Colonias (
    IdColonia INT PRIMARY KEY,
    NombreColonia NVARCHAR(100),
    IdCiudad INT,
    FOREIGN KEY (IdCiudad) REFERENCES Ciudades(IdCiudad)
);

INSERT INTO Colonias VALUES
(1, 'Americana', 1),
(2, 'Oblatos', 1),
(3, 'Santa Lucía', 2),
(4, 'Centro Oaxaca', 2),
(5, 'San Jerónimo', 3),
(6, 'Cumbres Elite', 3);

-- Tabla Suscriptores
CREATE TABLE Suscriptores (
    IdSuscriptor INT PRIMARY KEY,
    Nombre NVARCHAR(100),
    IdPaquete INT,
    EsNuevo BIT,
    IdColonia INT,
    FOREIGN KEY (IdPaquete) REFERENCES Paquetes(IdPaquete),
    FOREIGN KEY (IdColonia) REFERENCES Colonias(IdColonia)
);

INSERT INTO Suscriptores VALUES
(1, 'Jorge Lopez', 1, 1, 1),
(2, 'Alejandro Garcia', 2, 0, 2),
(3, 'Martha Hernandez', 3, 1, 3),
(4, 'Jesus Sanchez', 4, 0, 5),
(5, 'Gabriela Aguirre', 1, 0, 1),
(6, 'Alexia Palomar', 2, 1, 6);

-- Tabla Promociones
CREATE TABLE Promociones (
    IdPromocion INT PRIMARY KEY,
    Descripcion NVARCHAR(255),
    Condicion NVARCHAR(255)
);

INSERT INTO Promociones VALUES
(1, 'Descuento 50% para Oaxaca', 'Ciudad=Oaxaca AND EsNuevo=0'),
(2, 'Día del padre: $150 descuento en GDL y Oaxaca', 'Ciudad IN (Guadalajara, Oaxaca) AND EsNuevo=0'),
(3, 'Día del padre: $100 descuento en Monterrey', 'Ciudad=Monterrey AND EsNuevo=0'),
(4, 'Promoción nuevo suscriptor', 'EsNuevo=1');

-- Tabla PromocionAplicada
CREATE TABLE PromocionAplicada (
    IdAplicacion INT PRIMARY KEY,
    IdSuscriptor INT,
    IdPromocion INT,
    FechaAplicacion DATE,
    FOREIGN KEY (IdSuscriptor) REFERENCES Suscriptores(IdSuscriptor),
    FOREIGN KEY (IdPromocion) REFERENCES Promociones(IdPromocion)
);

INSERT INTO PromocionAplicada VALUES
(1, 1, 4, '2025-06-01'),
(2, 2, 2, '2025-06-01'),
(3, 3, 4, '2025-06-01'),
(4, 4, 2, '2025-06-01'),
(5, 5, 2, '2025-06-01'),
(6, 6, 2, '2025-06-01');
