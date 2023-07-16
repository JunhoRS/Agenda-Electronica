CREATE PROCEDURE sp_InsertarContacto
    @Nombre varchar(50),
    @Apellido varchar(50),
    @Direccion varchar(100),
    @FechaNacimiento date,
    @Celular varchar(15)
AS
BEGIN
    INSERT INTO Contactos (Nombre, Apellido, Direccion, FechaNacimiento, Celular)
    VALUES (@Nombre, @Apellido, @Direccion, @FechaNacimiento, @Celular)
END

CREATE PROCEDURE sp_ModificarContacto
    @ID int,
    @Nombre varchar(50),
    @Apellido varchar(50),
    @Direccion varchar(100),
    @FechaNacimiento date,
    @Celular varchar(15)
AS
BEGIN
    UPDATE Contactos
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Direccion = @Direccion,
        FechaNacimiento = @FechaNacimiento,
        Celular = @Celular
    WHERE ID = @ID
END

CREATE PROCEDURE sp_BuscarContacto
    @ID int
AS
BEGIN
    SELECT ID, Nombre, Apellido, Direccion, FechaNacimiento, Celular
    FROM Contactos
    WHERE ID = @ID
END

CREATE PROCEDURE sp_EliminarContacto
    @ID int
AS
BEGIN
    DELETE FROM Contactos
    WHERE ID = @ID
END

CREATE TABLE Contactos
(
    ID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Direccion NVARCHAR(100),
    FechaNacimiento DATE,
    Celular NVARCHAR(20)
);
