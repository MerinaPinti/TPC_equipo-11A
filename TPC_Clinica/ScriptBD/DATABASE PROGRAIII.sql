USE MASTER
GO
CREATE DATABASE CLINICA_DB;
GO
USE CLINICA_DB;
GO

CREATE TABLE TipoUsuario (
	idTipoUsuario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descripcion varchar(50) NOT NULL
);

GO

CREATE TABLE Especialidad(
	idEspecialidad INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descripcion varchar(50) NOT NULL
);

GO

CREATE TABLE Estado(
	idEstado INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	descripcion varchar(30) NOT NULL
);

GO

CREATE TABLE Paciente(
	idPaciente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(60) NOT NULL,
	apellido VARCHAR(60) NOT NULL,
	DNI VARCHAR(8) NOT NULL UNIQUE,
	fechaNac DATE,
	telefono VARCHAR(15),
	direccion VARCHAR(60) NOT NULL,
	email VARCHAR(100) NOT NULL
);

GO

CREATE TABLE Usuario(
	idUsuario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idTipoUsuario INT NOT NULL FOREIGN KEY REFERENCES TipoUsuario(idTipoUsuario),
	usuario VARCHAR(15) NOT NULL,
	contraseña VARCHAR(255) NOT NULL
);

GO

CREATE TABLE Medico(
	idMedico INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	email VARCHAR(100) NOT NULL,
	telefono VARCHAR(15),
	nombre VARCHAR(100),
	apellido VARCHAR(100),
	matricula VARCHAR(90) NOT NULL UNIQUE,
	idEspecialidad INT FOREIGN KEY REFERENCES Especialidad(idEspecialidad),
	idUsuario INT UNIQUE FOREIGN KEY REFERENCES Usuario(idUsuario)
);

GO

CREATE TABLE Turno(
	idTurno INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idPaciente INT FOREIGN KEY REFERENCES Paciente(idPaciente),
	idMedico INT NOT NULL FOREIGN KEY REFERENCES Medico(idMedico),
	fecha DATE NOT NULL,
	hora INT NOT NULL,
	idEstado INT NOT NULL FOREIGN KEY REFERENCES Estado(idEstado),
	observaciones VARCHAR(200),
	diagnostico VARCHAR(200),
	fechaAlta DATE,
	ultimaModificacion DATE
);

GO

-- Tabla TipoUsuario
INSERT INTO TipoUsuario (descripcion) VALUES
('Administrador'),
('Médico'),
('Recepcionista');

-- Tabla Especialidad
INSERT INTO Especialidad (descripcion) VALUES
('Cardiología'),
('Dermatología'),
('Endocrinología'),
('Ginecología'),
('Hematología'),
('Nefrología'),
('Obstetricia'),
('Oftalmología'),
('Otorrinolaringología'),
('Pediatría'),
('Traumatología');

-- Tabla Estado
INSERT INTO Estado (descripcion) VALUES
('Nuevo'),
('Confirmado'),
('Reprogramado'),
('Cancelado'),
('No Asistió'),
('Cerrado');

-- Tabla Paciente
INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) VALUES
('Juan', 'Pérez', '12345678', '12/05/1980', '1122334455', 'Av. Siempre Viva 123', 'juan.perez@mail.com'),
('María', 'González', '87654321', '25/10/1990', '1144556677', 'Calle Falsa 456', 'maria.gonzalez@mail.com'),
('Carlos', 'López', '11223344', '08/03/1975', '1133557799', 'Pasaje 123', 'carlos.lopez@mail.com');

-- Tabla Usuario
INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES
(1, 'admin01', 'passAdmin123'),
(2, 'medico01', 'passMedico123'),
(3, 'recep01', 'passRecep123');

-- Tabla Medico
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idEspecialidad, idUsuario) VALUES
('alejandro.ramos@clinica.com', '1199887766', 'Alejandro', 'Ramos', '654321', 1, 1),
('laura.sanchez@clinica.com', '1177665544', 'Laura', 'Sánchez', '765432', 2, 2),
('fernando.martinez@clinica.com', '1166554433', 'Fernando', 'Martínez', '876543', 3, 3);