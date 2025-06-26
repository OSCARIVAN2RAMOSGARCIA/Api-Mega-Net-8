CREATE DATABASE GestionDeudas;
GO

USE GestionDeudas;
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
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
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

-- Ciudades
INSERT INTO Ciudades (Nombre) VALUES 
('Ciudad de México'),
('Guadalajara'),
('Monterrey'),
('Puebla'),
('Tijuana'),
('León'),
('Querétaro'),
('Mérida'),
('Cancún'),
('Aguascalientes');

-- Colonias
-- Ciudad de México
INSERT INTO Colonias (IdCiudad, Nombre) VALUES 
(1, 'Polanco'),
(1, 'Condesa'),
(1, 'Roma'),
(1, 'Del Valle'),
(1, 'Nápoles');

-- Guadalajara
INSERT INTO Colonias (IdCiudad, Nombre) VALUES 
(2, 'Providencia'),
(2, 'Americana'),
(2, 'Chapalita'),
(2, 'Jardines del Bosque'),
(2, 'Vallarta');

-- Monterrey
INSERT INTO Colonias (IdCiudad, Nombre) VALUES 
(3, 'San Pedro'),
(3, 'Contry'),
(3, 'Del Valle'),
(3, 'Tecnológico'),
(3, 'Centro');

-- Puebla
INSERT INTO Colonias (IdCiudad, Nombre) VALUES 
(4, 'Angelópolis'),
(4, 'Centro Histórico'),
(4, 'Las Fuentes'),
(4, 'San Manuel'),
(4, 'La Paz');

-- Tijuana
INSERT INTO Colonias (IdCiudad, Nombre) VALUES 
(5, 'Zona Río'),
(5, 'Centro'),
(5, 'Playas de Tijuana'),
(5, 'Otay'),
(5, 'Soler');

INSERT INTO Servicios (Nombre, PrecioResidencial, PrecioEmpresarial, TipoServicio) VALUES 
('Internet 300 Mbps', 600.00, 900.00, 'Residencial'),
('Internet 1 Gbps', 1000.00, 1200.00, 'Residencial'),
('TV HD Interactiva (80 canales + Xview+)', 150.00, 900.00, 'Residencial'),
('Telefonía Ilimitada Nacional', 100.00, 300.00, 'Residencial'),
('Telefonía Ilimitada Internacional', 150.00, 200.00, 'Residencial'),
('Extensor WiFi Ultra', 0.00, 200.00, 'Residencial'),
('Extensor Mesh', 0.00, 200.00, 'Residencial'),
('Amazon Prime', 0.00, 200.00, 'Residencial'),
('Paramount+', 0.00, 200.00, 'Residencial'),
('Max', 0.00, 200.00, 'Residencial'),
('YouTube Premium', 0.00, 200.00, 'Residencial'),
('TikTok Premium', 0.00, 200.00, 'Residencial');

-- Paquetes Dobles (Internet + Telefonía)
INSERT INTO Paquetes (Nombre, TipoPaquete, Descripcion) VALUES 
('Doble Pack 100 Mbps', 'Residencial', 'Internet 100 Mbps + Telefonía fija con llamadas ilimitadas'),
('Doble Pack 200 Mbps', 'Residencial', 'Internet 200 Mbps simétrico + Telefonía'),
('Doble Pack 300 Mbps', 'Residencial', 'Internet 300 Mbps simétrico + Telefonía'),
('Doble Pack 500 Mbps', 'Residencial', 'Internet 500 Mbps simétrico + Telefonía');

-- Paquetes Triples (Internet + TV + Telefonía)
INSERT INTO Paquetes (Nombre, TipoPaquete, Descripcion) VALUES 
('Triple Pack 100 Mbps', 'Residencial', 'TV HD interactiva (80 canales + Xview+) + Telefonía ilimitada + Internet 100 Mbps'),
('Triple Pack 200 Mbps', 'Residencial', 'TV HD + Telefonía + Internet 200 Mbps simétrico con extensor WiFi'),
('Triple Pack 300 Mbps', 'Residencial', 'TV HD + Telefonía + Internet 300 Mbps'),
('Triple Pack 500 Mbps', 'Residencial', 'TV HD + Telefonía + Internet 500 Mbps');

-- Full Connected
INSERT INTO Paquetes (Nombre, TipoPaquete, Descripcion) VALUES 
('Full Connected', 'Residencial', '1 Gbps + TV interactiva (2 TV) + telefonía ilimitada + extensor Mesh + Paramount+, Amazon Prime, Max, YouTube, TikTok');

-- Paquetes Solo Internet
INSERT INTO Paquetes (Nombre, TipoPaquete, Descripcion) VALUES 
('Solo Internet 100 Mbps', 'Residencial', 'Internet 100 Mbps'),
('Solo Internet 200 Mbps', 'Residencial', 'Internet 200 Mbps'),
('Solo Internet 300 Mbps', 'Residencial', 'Internet 300 Mbps'),
('Solo Internet 500 Mbps', 'Residencial', 'Internet 500 Mbps');

-- Paquetes Dobles
INSERT INTO PaqueteServicios (IdPaquete, IdServicio) VALUES 
(1, 1), (1, 4),  -- Doble Pack 100 Mbps
(2, 2), (2, 4),  -- Doble Pack 200 Mbps
(3, 1), (3, 4),  -- Doble Pack 300 Mbps
(4, 2), (4, 4);  -- Doble Pack 500 Mbps

-- Paquetes Triples
INSERT INTO PaqueteServicios (IdPaquete, IdServicio) VALUES 
(5, 1), (5, 3), (5, 4),   -- Triple Pack 100 Mbps
(6, 2), (6, 3), (6, 4), (6, 6),  -- Triple Pack 200 Mbps
(7, 1), (7, 3), (7, 4),   -- Triple Pack 300 Mbps
(8, 2), (8, 3), (8, 4);   -- Triple Pack 500 Mbps

-- Full Connected
INSERT INTO PaqueteServicios (IdPaquete, IdServicio) VALUES 
(9, 2), (9, 3), (9, 4), (9, 7),  -- Servicios principales
(9, 8), (9, 9), (9, 10), (9, 11);  -- Servicios de streaming

-- Solo Internet
INSERT INTO PaqueteServicios (IdPaquete, IdServicio) VALUES 
(10, 1),  -- Solo Internet 100 Mbps
(11, 2),  -- Solo Internet 200 Mbps
(12, 1),  -- Solo Internet 300 Mbps
(13, 2);  -- Solo Internet 500 Mbps

INSERT INTO Promociones (Nombre, DescuentoResidencial, DescuentoEmpresarial, AplicaNuevos, TipoPromocion, VigenciaDesde, VigenciaHasta, Activa) VALUES 
('Promo Doble Pack 100 Mbps', 40.00, 0.00, 1, 'Residencial', '2023-01-01', '2023-12-31', 1),
('Promo Triple Pack 200 Mbps', 27.00, 0.00, 1, 'Residencial', '2023-01-01', '2023-12-31', 1),
('Promo Full Connected', 0.00, 0.00, 1, 'Residencial', '2023-01-01', '2023-12-31', 1),
('Bono Domiciliación', 10.00, 0.00, 0, 'Residencial', '2023-01-01', '2023-12-31', 1);

INSERT INTO Suscriptores (Nombre, IdColonia, FechaRegistro) VALUES 
('Ana García Fernández', 7, '2023-04-05'),
('Pedro Ramírez Díaz', 9, '2023-05-12'),
('Diseño Creativo', 7, '2023-04-25' ),
('Logística Express', 9, '2023-05-30'),
('Sofía Castro Jiménez', 6, '2023-08-30' ),
('Miguel Torres Reyes', 8, '2023-09-14'),
('Elena Ruiz Ortega', 10, '2023-10-25'),
('Constructora Edifica', 6, '2023-08-15'),
('Agencia de Viajes', 8, '2023-09-20'),
('Inmobiliaria Premium', 10, '2023-10-30');

INSERT INTO Contratos (IdSucriptor, IdPaquete, FechaInicio, TipoContrato) VALUES 
(1, 5, '2023-04-10', 'Residencial'),  -- Ana con Triple Pack 100 Mbps
(3, 9, '2023-04-28', 'Empresarial'),  -- Diseño Creativo con Full Connected
(5, 2, '2023-09-01', 'Residencial');  -- Sofía con Doble Pack 200 Mbps

INSERT INTO PromocionesAplicadas (IdContrato, IdPromocion, FechaAplicacion, DescuentoAplicado) VALUES 
(1, 1, '2023-04-10', 40.00),  -- Ana con Promo Doble Pack
(3, 3, '2023-04-28', 0.00);    -- Diseño Creativo con Promo Full Connected




UPDATE servicios
SET PrecioEmpresarial = 0.00
WHERE TipoServicio= 'Residencial';   

INSERT INTO Servicios (Nombre, PrecioResidencial, PrecioEmpresarial, TipoServicio)
VALUES
('Soporte Técnico 24/7 Empresarial', 0.00, 350.00, 'Empresarial'),
('IP Fija Empresarial', 0.00, 500.00, 'Empresarial'),
('Telefonía IP Empresarial con múltiples líneas', 0.00, 600.00, 'Empresarial');

INSERT INTO Paquetes (Nombre, TipoPaquete, Descripcion)
VALUES
('Triple Pack 300 Mbps Empresarial', 'Empresarial', 'Internet 300 Mbps simétrico + Telefonía IP empresarial + TV HD (opcional) + Soporte técnico 24/7'), 
('Triple Pack 500 Mbps Empresarial', 'Empresarial', 'Internet 500 Mbps simétrico + Telefonía IP con múltiples líneas + TV HD (opcional) + Extensor WiFi Pro'),
('Triple Pack 1 Gbps Empresarial', 'Empresarial', 'Internet 1 Gbps dedicado + Telefonía empresarial + IP fija incluida + Soporte técnico prioritario + TV HD (opcional)'); 

INSERT INTO PaqueteServicios (IdPaquete, IdServicio) VALUES 
(14, 12),  -- Soporte Técnico 24/7 Empresarial
(14, 14),  -- Telefonía IP Empresarial con múltiples líneas

(15, 12),  -- Soporte Técnico 24/7 Empresarial
(15, 14),  -- Telefonía IP Empresarial con múltiples líneas
(15, 13),  -- IP Fija Empresarial

(16, 12),  -- Soporte Técnico 24/7 Empresarial
(16, 13),  -- IP Fija Empresarial
(16, 14);  -- Telefonía IP Empresarial con múltiples líneas
