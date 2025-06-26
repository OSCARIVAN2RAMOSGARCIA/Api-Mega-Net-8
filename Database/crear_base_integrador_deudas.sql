CREATE DATABASE MegaDeuda;
GO

USE MegaDeuda;
GO

-- Tablas básicas de ubicación
CREATE TABLE Ciudades (
    IdCiudad INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Colonias (
    IdColonia INT PRIMARY KEY IDENTITY(1,1),
    IdCiudad INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    FOREIGN KEY (IdCiudad) REFERENCES Ciudades(IdCiudad)
);

-- Tabla de Servicios con tipo (Empresarial/Residencial)
CREATE TABLE Servicios (
    IdServicio INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    PrecioResidencial DECIMAL(10,2) NOT NULL,
    PrecioEmpresarial DECIMAL(10,2) NOT NULL,
    TipoServicio NVARCHAR(20) NOT NULL CHECK (TipoServicio IN ('Residencial', 'Empresarial', 'Ambos'))
);

-- Tabla de Paquetes con tipo
CREATE TABLE Paquetes (
    IdPaquete INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    TipoPaquete NVARCHAR(20) NOT NULL CHECK (TipoPaquete IN ('Residencial', 'Empresarial')),
    Descripcion NVARCHAR(255) NULL
);

-- Relación Paquetes-Servicios
CREATE TABLE PaqueteServicios (
    IdPaquete INT,
    IdServicio INT,
    PRIMARY KEY (IdPaquete, IdServicio),
    FOREIGN KEY (IdPaquete) REFERENCES Paquetes(IdPaquete),
    FOREIGN KEY (IdServicio) REFERENCES Servicios(IdServicio)
);

-- Tabla de Clientes con tipo (Empresarial/Residencial)
CREATE TABLE Suscriptores (
    IdSuscriptor INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    IdColonia INT NOT NULL,
    FechaRegistro DATE NOT NULL,
    FOREIGN KEY (IdColonia) REFERENCES Colonias(IdColonia)
);

-- Tabla de Contratos
CREATE TABLE Contratos (
    IdContrato INT PRIMARY KEY IDENTITY(1,1),
    IdSuscriptor INT NOT NULL,
    IdPaquete INT NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaTermino DATE NULL,
    Activo BIT DEFAULT 1,
    TipoContrato NVARCHAR(20) NOT NULL CHECK (TipoContrato IN ('Residencial', 'Empresarial')),
    FOREIGN KEY (IdSuscriptor) REFERENCES Suscriptores(IdSuscriptor),
    FOREIGN KEY (IdPaquete) REFERENCES Paquetes(IdPaquete)
);

-- Tabla de Promociones
CREATE TABLE Promociones (
    IdPromocion INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    DescuentoResidencial DECIMAL(5,2) NOT NULL,
    DescuentoEmpresarial DECIMAL(5,2) NOT NULL,
    AplicaNuevos BIT DEFAULT 0,
    TipoPromocion NVARCHAR(20) NOT NULL CHECK (TipoPromocion IN ('Residencial', 'Empresarial', 'Ambos')),
    VigenciaDesde DATE NOT NULL,
    VigenciaHasta DATE NOT NULL,
    Activa BIT DEFAULT 1
);

-- Registro de promociones aplicadas
CREATE TABLE PromocionesAplicadas (
    IdPromocionAplicada INT PRIMARY KEY IDENTITY(1,1),
    IdContrato INT NOT NULL,
    IdPromocion INT NOT NULL,
    FechaAplicacion DATE NOT NULL,
    FechaTermino DATE NULL,
    DescuentoAplicado DECIMAL(5,2) NOT NULL,
    FOREIGN KEY (IdContrato) REFERENCES Contratos(IdContrato),
    FOREIGN KEY (IdPromocion) REFERENCES Promociones(IdPromocion)
);

-- Ciudades (5 inserts)
INSERT INTO Ciudades (Nombre) VALUES
('Ciudad de México'),
('Guadalajara'),
('Monterrey'),
('Puebla'),
('Tijuana');

-- Colonias (5 inserts, relacionados a ciudades)
INSERT INTO Colonias (IdCiudad, Nombre) VALUES
(1, 'Polanco'),
(1, 'Condesa'),
(2, 'Providencia'),
(3, 'San Pedro'),
(4, 'Angelópolis');

-- Servicios (5 inserts, con tipos y precios)
INSERT INTO Servicios (Nombre, PrecioResidencial, PrecioEmpresarial, TipoServicio) VALUES
('Internet 100 Mbps', 600.00, 900.00, 'Ambos'),
('Telefonía Ilimitada', 100.00, 300.00, 'Ambos'),
('TV HD Interactiva', 150.00, 400.00, 'Ambos'),
('Soporte Técnico 24/7', 0.00, 200.00, 'Empresarial'),
('IP Fija Empresarial', 0.00, 500.00, 'Empresarial');

-- Paquetes (5 inserts, tipo Residencial o Empresarial)
INSERT INTO Paquetes (Nombre, TipoPaquete, Descripcion) VALUES
('Doble Pack 100 Mbps', 'Residencial', 'Internet + Telefonía'),
('Triple Pack 100 Mbps', 'Residencial', 'Internet + Telefonía + TV'),
('Full Connected', 'Empresarial', 'Internet 1Gbps + TV + Telefonía + Soporte'),
('Solo Internet 100 Mbps', 'Residencial', 'Internet solo'),
('Soporte Empresarial', 'Empresarial', 'Soporte + IP Fija');

-- PaqueteServicios (asignando servicios a paquetes)
INSERT INTO PaqueteServicios (IdPaquete, IdServicio) VALUES
(1, 1), (1, 2),              -- Doble Pack 100 Mbps: Internet + Telefonía
(2, 1), (2, 2), (2, 3),      -- Triple Pack 100 Mbps: Internet + Telefonía + TV
(3, 1), (3, 2), (3, 3), (3, 4), -- Full Connected: Internet + Telefonía + TV + Soporte
(4, 1),                     -- Solo Internet 100 Mbps
(5, 4), (5, 5);              -- Soporte Empresarial: Soporte + IP Fija

-- Suscriptores (5 inserts, con colonia)
INSERT INTO Suscriptores (Nombre, IdColonia, FechaRegistro) VALUES
('Ana García Fernández', 1, '2023-04-05'),
('Pedro Ramírez Díaz', 3, '2023-05-12'),
('Empresa XYZ', 5, '2023-06-01'),
('Sofía Castro', 2, '2023-08-30'),
('Logística ABC', 4, '2023-09-10');

-- Contratos (5 inserts, algunos activos)
INSERT INTO Contratos (IdSuscriptor, IdPaquete, FechaInicio, FechaTermino, TipoContrato, Activo) VALUES
(1, 2, '2023-04-10', NULL, 'Residencial', 1),  -- Ana, Triple Pack Residencial
(2, 1, '2023-05-12', NULL, 'Residencial', 1),  -- Pedro, Doble Pack Residencial
(3, 3, '2023-06-01', NULL, 'Empresarial', 1),  -- Empresa XYZ, Full Connected Empresarial
(4, 4, '2023-08-30', NULL, 'Residencial', 1),  -- Sofía, Solo Internet Residencial
(5, 5, '2023-09-10', NULL, 'Empresarial', 1);  -- Logística ABC, Soporte Empresarial

-- Promociones (5 inserts, descuentos en porcentaje 0-1)
INSERT INTO Promociones (Nombre, DescuentoResidencial, DescuentoEmpresarial, AplicaNuevos, TipoPromocion, VigenciaDesde, VigenciaHasta, Activa) VALUES
('Promo Doble 10%', 0.10, 0.00, 1, 'Residencial', '2023-01-01', '2023-12-31', 1),
('Promo Triple 20%', 0.20, 0.00, 1, 'Residencial', '2023-01-01', '2023-12-31', 1),
('Promo Empresarial 15%', 0.00, 0.15, 1, 'Empresarial', '2023-01-01', '2023-12-31', 1),
('Bono Domiciliacion 5%', 0.05, 0.05, 0, 'Ambos', '2023-01-01', '2023-12-31', 1),
('Sin Descuento', 0.00, 0.00, 0, 'Ambos', '2023-01-01', '2023-12-31', 1);

-- PromocionesAplicadas (asociando promociones a contratos)
INSERT INTO PromocionesAplicadas (IdContrato, IdPromocion, FechaAplicacion, FechaTermino, DescuentoAplicado) VALUES
(1, 2, '2023-04-10', NULL, 0.20),  -- Ana con 20% Triple Pack
(2, 1, '2023-05-12', NULL, 0.10),  -- Pedro con 10% Doble Pack
(3, 3, '2023-06-01', NULL, 0.15),  -- Empresa XYZ 15% Empresarial
(4, 5, '2023-08-30', NULL, 0.00),  -- Sofía sin descuento
(5, 4, '2023-09-10', NULL, 0.05);  -- Logística ABC con bono 5%

-- Agregar segunda promoción para contrato 1 (Ana), que ya tiene la promo 2 (20%)
-- Añadimos promo 4 (Bono Domiciliación 5%)
INSERT INTO PromocionesAplicadas (IdContrato, IdPromocion, FechaAplicacion, DescuentoAplicado)
VALUES
(1, 4, '2023-04-15', 0.05);

-- Agregar segunda promoción para contrato 3 (Empresa XYZ), que ya tiene la promo 3 (15%)
-- Añadimos promo 1 (Promo Doble 10%)
INSERT INTO PromocionesAplicadas (IdContrato, IdPromocion, FechaAplicacion, DescuentoAplicado)
VALUES
(3, 1, '2023-06-05', 0.10);
