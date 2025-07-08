CREATE DATABASE GestionDeudas;
GO

USE GestionDeudas;
GO

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

CREATE TABLE Servicios (
    IdServicio INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    PrecioResidencial DECIMAL(10,2) NOT NULL,
    PrecioEmpresarial DECIMAL(10,2) NOT NULL,
    TipoServicio NVARCHAR(20) NOT NULL CHECK (TipoServicio IN ('Residencial', 'Empresarial', 'Ambos'))
);

CREATE TABLE Paquetes (
    IdPaquete INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    TipoPaquete NVARCHAR(20) NOT NULL CHECK (TipoPaquete IN ('Residencial', 'Empresarial')),
    Descripcion NVARCHAR(255) NULL
);

CREATE TABLE PaqueteServicios (
    IdPaquete INT,
    IdServicio INT,
    PRIMARY KEY (IdPaquete, IdServicio),
    FOREIGN KEY (IdPaquete) REFERENCES Paquetes(IdPaquete),
    FOREIGN KEY (IdServicio) REFERENCES Servicios(IdServicio)
);

CREATE TABLE Suscriptores (
    IdSuscriptor INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    IdColonia INT NOT NULL,
    FechaRegistro DATE NOT NULL,
    FOREIGN KEY (IdColonia) REFERENCES Colonias(IdColonia)
);

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

CREATE TABLE PromocionConfiguracion (
    IdPromocionConfiguracion INT PRIMARY KEY IDENTITY,
    IdPromocion INT NOT NULL,
    IdCiudad INT NULL,
    IdColonia INT NULL,
    IdPaquete INT NULL,
    FOREIGN KEY (IdPromocion) REFERENCES Promociones(IdPromocion),
    FOREIGN KEY (IdCiudad) REFERENCES Ciudades(IdCiudad),
    FOREIGN KEY (IdColonia) REFERENCES Colonias(IdColonia),
    FOREIGN KEY (IdPaquete) REFERENCES Paquetes(IdPaquete)
);

CREATE TABLE PromocionesAplicadas (
    IdPromocionAplicada INT PRIMARY KEY IDENTITY(1,1),
    IdContrato INT NOT NULL,
    IdPromocion INT NOT NULL,
    IdPromocionConfiguracion INT NULL,
    FechaAplicacion DATE NOT NULL,
    FechaTermino DATE NULL,
    FOREIGN KEY (IdContrato) REFERENCES Contratos(IdContrato),
    FOREIGN KEY (IdPromocion) REFERENCES Promociones(IdPromocion),
    FOREIGN KEY (IdPromocionConfiguracion) REFERENCES PromocionConfiguracion(IdPromocionConfiguracion)
);

INSERT INTO Ciudades (Nombre) VALUES
('Ciudad de México'),
('Guadalajara'),
('Monterrey'),
('Puebla'),
('Tijuana');

INSERT INTO Colonias (IdCiudad, Nombre) VALUES
(1, 'Polanco'),
(1, 'Condesa'),
(2, 'Providencia'),
(3, 'San Pedro'),
(4, 'Angelópolis');

INSERT INTO Servicios (Nombre, PrecioResidencial, PrecioEmpresarial, TipoServicio) VALUES
('Internet 100 Mbps', 650.00, 950.00, 'Ambos'),
('Telefonía Ilimitada', 120.00, 350.00, 'Ambos'),
('TV HD Interactiva', 180.00, 450.00, 'Ambos'),
('Soporte Técnico 24/7', 0.00, 250.00, 'Empresarial'),
('IP Fija Empresarial', 0.00, 550.00, 'Empresarial');

INSERT INTO Paquetes (Nombre, TipoPaquete, Descripcion) VALUES
('Doble Pack 100 Mbps', 'Residencial', 'Internet + Telefonía'),
('Triple Pack 100 Mbps', 'Residencial', 'Internet + Telefonía + TV'),
('Full Connected', 'Empresarial', 'Internet 1Gbps + TV + Telefonía + Soporte'),
('Solo Internet 100 Mbps', 'Residencial', 'Internet solo'),
('Soporte Empresarial', 'Empresarial', 'Soporte + IP Fija');

INSERT INTO PaqueteServicios (IdPaquete, IdServicio) VALUES
(1, 1), (1, 2),
(2, 1), (2, 2), (2, 3),
(3, 1), (3, 2), (3, 3), (3, 4),
(4, 1),
(5, 4), (5, 5);

INSERT INTO Suscriptores (Nombre, IdColonia, FechaRegistro) VALUES
('Ana García Fernández', 1, '2024-03-15'),
('Pedro Ramírez Díaz', 3, '2024-04-22'),
('Empresa XYZ', 5, '2024-05-10'),
('Sofía Castro', 2, '2024-07-18'),
('Logística ABC', 4, '2024-08-05');

INSERT INTO Contratos (IdSuscriptor, IdPaquete, FechaInicio, FechaTermino, TipoContrato, Activo) VALUES
(1, 2, '2024-03-20', NULL, 'Residencial', 1),
(2, 1, '2024-04-25', NULL, 'Residencial', 1),
(3, 3, '2024-05-15', NULL, 'Empresarial', 1),
(4, 4, '2024-07-25', NULL, 'Residencial', 1),
(5, 5, '2024-08-10', NULL, 'Empresarial', 1);

INSERT INTO Promociones (Nombre, DescuentoResidencial, DescuentoEmpresarial, AplicaNuevos, TipoPromocion, VigenciaDesde, VigenciaHasta, Activa) VALUES
('Promo Doble 10%', 0.10, 0.00, 1, 'Residencial', '2024-01-01', '2025-12-31', 1),
('Promo Triple 20%', 0.20, 0.00, 1, 'Residencial', '2024-01-01', '2025-06-30', 1),
('Promo Empresarial 15%', 0.00, 0.15, 1, 'Empresarial', '2024-01-01', '2025-12-31', 1),
('Bono Domiciliacion 5%', 0.05, 0.05, 0, 'Ambos', '2024-01-01', '2025-12-31', 1),
('Sin Descuento', 0.00, 0.00, 0, 'Ambos', '2024-01-01', '2025-12-31', 1);

INSERT INTO PromocionConfiguracion (IdPromocion, IdCiudad, IdColonia, IdPaquete) VALUES
(1, NULL, NULL, 1), (2, NULL, NULL, 2), 
(3, NULL, NULL, 3), (3, NULL, NULL, 5),
(4, NULL, NULL, NULL), (2, 1, NULL, NULL),
(2, 3, NULL, NULL), (3, NULL, 5, NULL),
(5, NULL, NULL, NULL);

INSERT INTO PromocionesAplicadas (IdContrato, IdPromocion, IdPromocionConfiguracion, FechaAplicacion) VALUES
(1, 2, 2, '2024-03-20'),
(1, 4, 4, '2024-03-25'),
(2, 1, 1, '2024-04-25'),
(3, 3, 3, '2024-05-15'),
(3, 1, NULL, '2024-05-20'),
(4, 5, 9, '2024-07-25'),
(5, 4, 4, '2024-08-10');

INSERT INTO Promociones (Nombre, DescuentoResidencial, DescuentoEmpresarial, AplicaNuevos, TipoPromocion, VigenciaDesde, VigenciaHasta, Activa)
VALUES ('Elite Providencia', 0.25, 0.00, 1, 'Residencial', '2024-09-01', '2024-11-30', 1);

INSERT INTO PromocionConfiguracion (IdPromocion, IdCiudad, IdColonia, IdPaquete)
VALUES (SCOPE_IDENTITY(), 2, 3, NULL);

INSERT INTO Promociones (Nombre, DescuentoResidencial, DescuentoEmpresarial, AplicaNuevos, TipoPromocion, VigenciaDesde, VigenciaHasta, Activa)
VALUES ('Norte Digital', 0.12, 0.18, 1, 'Ambos', '2024-10-01', '2025-01-31', 1);

INSERT INTO PromocionConfiguracion (IdPromocion, IdCiudad, IdColonia, IdPaquete)
VALUES (SCOPE_IDENTITY(), 3, NULL, NULL);

INSERT INTO Promociones (Nombre, DescuentoResidencial, DescuentoEmpresarial, AplicaNuevos, TipoPromocion, VigenciaDesde, VigenciaHasta, Activa)
VALUES ('Angelópolis Premium', 0.00, 0.20, 1, 'Empresarial', '2024-09-15', '2024-12-15', 1);

INSERT INTO PromocionConfiguracion (IdPromocion, IdCiudad, IdColonia, IdPaquete)
VALUES (SCOPE_IDENTITY(), 4, 5, NULL);

INSERT INTO Promociones (Nombre, DescuentoResidencial, DescuentoEmpresarial, AplicaNuevos, TipoPromocion, VigenciaDesde, VigenciaHasta, Activa)
VALUES ('Frontera Plus', 0.18, 0.12, 1, 'Ambos', '2024-10-01', '2025-02-28', 1);

INSERT INTO PromocionConfiguracion (IdPromocion, IdCiudad, IdColonia, IdPaquete)
VALUES (SCOPE_IDENTITY(), 5, NULL, NULL);