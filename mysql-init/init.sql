CREATE DATABASE IF NOT EXISTS TiendaVirtual;
USE TiendaVirtual;

-- 1. Tabla Zona
CREATE TABLE Zona (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreZona VARCHAR(90) NOT NULL
);

-- 2. Tabla Barrio
CREATE TABLE Barrio (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreBarrio VARCHAR(50),
    ZonaId INT,
    FOREIGN KEY (ZonaId) REFERENCES Zona(Id)
);

-- 3. Tabla Usuario
CREATE TABLE Usuario (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL,
    Correo VARCHAR(100) UNIQUE NOT NULL,
    Contrasena VARCHAR(255) NOT NULL,
    TipoUsuario VARCHAR(20) NOT NULL
);

-- 4. Tabla Cliente
CREATE TABLE Cliente (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Direccion VARCHAR(255),
    Telefono VARCHAR(50),
    BarrioId INT,
    Edad INT,
    Genero VARCHAR(10),
    UsuarioId INT,
    FOREIGN KEY (BarrioId) REFERENCES Barrio(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);

-- 5. Tabla ClienteNATURAL
CREATE TABLE ClienteNATURAL (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    ApellidoPaterno VARCHAR(50),
    ApellidoMaterno VARCHAR(50),
    ClienteId INT UNIQUE NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id)
);

-- 6. Tabla Juridico
CREATE TABLE Juridico (
    Id VARCHAR(50) PRIMARY KEY,
    RazonSocial VARCHAR(100),
    RepresentanteLegal VARCHAR(100),
    ClienteId INT UNIQUE NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id)
);

-- 7. Tabla DireccionEnvio
CREATE TABLE DireccionEnvio (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioId INT,
    Direccion VARCHAR(255),
    Ciudad VARCHAR(100),
    Departamento VARCHAR(100),
    Latitud DECIMAL(10, 8),
    Longitud DECIMAL(11, 8),
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);


-- 8. Tabla Categoria
CREATE TABLE Categoria (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreCategoria VARCHAR(90) NOT NULL,
    Descripcion VARCHAR(90)
);

-- 9. Tabla Producto
CREATE TABLE Producto (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreProducto VARCHAR(90) NOT NULL,
    PrecioVenta DECIMAL(12, 2) NOT NULL,
    IdCategoria INT,
    Imagen VARCHAR(512),
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(Id)
);

-- 10. Tabla Proveedor
CREATE TABLE Proveedor (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreProveedor VARCHAR(50) NOT NULL,
    Direccion VARCHAR(90),
    Telefono VARCHAR(50)
);

-- 11. Tabla ProveedorProducto
CREATE TABLE ProveedorProducto (
    IdProveedor INT,
    IdProducto INT,
    NombreEspecifico VARCHAR(50) NOT NULL,
    PRIMARY KEY (IdProveedor, IdProducto),
    FOREIGN KEY (IdProveedor) REFERENCES Proveedor(Id),
    FOREIGN KEY (IdProducto) REFERENCES Producto(Id)
);

-- 12. Tabla Carrito
CREATE TABLE Carrito (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioId INT,
    FechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);

-- 13. Tabla CarritoProducto
CREATE TABLE CarritoProducto (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CarritoId INT,
    ProductoId INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(12, 2),
    FOREIGN KEY (CarritoId) REFERENCES Carrito(Id),
    FOREIGN KEY (ProductoId) REFERENCES Producto(Id)
);

-- 14. Tabla Pedido
CREATE TABLE Pedido (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FechaPedido DATETIME NOT NULL,
    EstadoPedido VARCHAR(50) DEFAULT 'Pendiente',
    IdCliente INT,
    DireccionEnvioId INT,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(Id),
    FOREIGN KEY (DireccionEnvioId) REFERENCES DireccionEnvio(Id)
);

-- 15. Tabla DetallePedido
CREATE TABLE delivery (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    PedidoId INT,
    Nombre VARCHAR(90) NOT NULL,
    Telefono VARCHAR(50) NOT NULL,
    TelefonoDelyvery VARCHAR(50),
    FOREIGN KEY (PedidoId) REFERENCES Pedido(Id)
);

-- 16. Tabla DetallePedido
CREATE TABLE DetallePedido (
    IdPedido INT,
    IdProducto INT,
    Cantidad FLOAT NOT NULL,
    Precio DECIMAL(12, 2) NOT NULL,
    PRIMARY KEY (IdPedido, IdProducto),
    FOREIGN KEY (IdPedido) REFERENCES Pedido(Id),
    FOREIGN KEY (IdProducto) REFERENCES Producto(Id)
);

-- 17. Tabla Factura
CREATE TABLE Factura (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Monto DECIMAL(12, 2) NOT NULL,
    FechaEmision DATETIME NOT NULL,
    CodigoControl VARCHAR(90) NOT NULL,
    Qr VARCHAR(255) NOT NULL,
    IdPedido INT,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(Id)
);

-- 18. Tabla Pago
CREATE TABLE Pago (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IdPedido INT NOT NULL,
    FechaPago DATETIME NOT NULL,
    Monto DECIMAL(12, 2) NOT NULL,
    TipoPago VARCHAR(50) NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(Id)
);

-- 19. Tabla Credito
CREATE TABLE Credito (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Monto DECIMAL(12, 2) NOT NULL,
    InteresMensual DECIMAL(12, 2) NOT NULL,
    FechaDesembolso DATETIME NOT NULL,
    IdPedido INT,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(Id)
);

-- 20. Tabla Cuota
CREATE TABLE Cuota (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Monto DECIMAL(12, 2) NOT NULL,
    Interes DECIMAL(12, 2) NOT NULL,
    FechaPagoProgramado DATETIME NOT NULL,
    FechaPago DATETIME NULL,
    IdCredito INT,
    FOREIGN KEY (IdCredito) REFERENCES Credito(Id)
);

-- 21. Tabla EstadoPedidoHistorial
CREATE TABLE EstadoPedidoHistorial (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IdPedido INT,
    EstadoAnterior VARCHAR(50),
    EstadoNuevo VARCHAR(50),
    FechaCambio DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(Id)
);

-- 22. Tabla NotificacionUsuario
CREATE TABLE NotificacionUsuario (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioId INT,
    Mensaje VARCHAR(255),
    FechaEnvio DATETIME DEFAULT CURRENT_TIMESTAMP,
    Leido BOOLEAN DEFAULT 0,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);

-- 23. Tabla Tienda
CREATE TABLE Tienda (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreTienda VARCHAR(80),
    Direccion VARCHAR(255),
    Ciudad VARCHAR(100),
    Latitud DECIMAL(10, 8),
    Longitud DECIMAL(11, 8)
);

-- 24. Tabla ProductoTienda
CREATE TABLE ProductoTienda(
	IdProducto INT,
    IdTienda INT,
    Stock INT NOT NULL,
    PRIMARY KEY (IdProducto, IdTienda),
    FOREIGN KEY (IdProducto) REFERENCES Producto(Id),
    FOREIGN KEY (IdTienda) REFERENCES Tienda(Id)
);

