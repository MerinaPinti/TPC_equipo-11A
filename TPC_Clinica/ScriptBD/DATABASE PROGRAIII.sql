USE MASTER
GO
CREATE DATABASE CLINICA_DB;
GO
USE CLINICA_DB;
GO

CREATE TABLE TipoUsuario (
	idTipoUsuario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descripcion varchar(50) NOT NULL,
	activo BIT NOT NULL DEFAULT 1 -- 1: activa, 0: inactiva
);

GO

CREATE TABLE Especialidad(
	idEspecialidad INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descripcion varchar(50) NOT NULL,
	activo BIT NOT NULL DEFAULT 1 -- 1: activa, 0: inactiva
);

GO

CREATE TABLE Estado(
	idEstado INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	descripcion varchar(30) NOT NULL,
	activo BIT NOT NULL DEFAULT 1 -- 1: activa, 0: inactiva
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
	email VARCHAR(100) NOT NULL,
	activo BIT NOT NULL DEFAULT 1 -- 1: activa, 0: inactiva
);

GO

CREATE TABLE Usuario(
	idUsuario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idTipoUsuario INT NOT NULL FOREIGN KEY REFERENCES TipoUsuario(idTipoUsuario),
	usuario VARCHAR(15) NOT NULL,
	contraseña VARCHAR(255) NOT NULL,
	activo BIT NOT NULL DEFAULT 1 -- 1: activa, 0: inactiva, 
);

GO

CREATE TABLE Medico(
	idMedico INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	email VARCHAR(100) NOT NULL,
	telefono VARCHAR(15),
	nombre VARCHAR(100),
	apellido VARCHAR(100),
	matricula VARCHAR(90) NOT NULL UNIQUE, 
	idUsuario INT NULL, 
	activo BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario)
);

GO

CREATE TABLE Especialidades_Medicos (
	idEspecialidad INT NOT NULL FOREIGN KEY REFERENCES Especialidad(idEspecialidad),
    idMedico INT NOT NULL FOREIGN KEY REFERENCES Medico(idMedico),
	Activo bit NOT NULL DEFAULT 1,
    PRIMARY KEY(idEspecialidad, idMedico)
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
	ultimaModificacion DATE, 
	activo BIT NOT NULL DEFAULT 1 -- 1: activa, 0: inactiva
);

CREATE TABLE TurnoTrabajo (
	idTurnoTrabajo INT PRIMARY KEY IDENTITY(1,1),
	descripcion VARCHAR(50) NOT NULL,
	horaInicio TIME NOT NULL,
	horaFin TIME NOT NULL,
	activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE HorarioAtencion (
	idHorarioAtencion INT PRIMARY KEY IDENTITY(1,1),
	idMedico INT NOT NULL FOREIGN KEY REFERENCES Medico(idMedico),
	idEspecialidad INT NOT NULL FOREIGN KEY REFERENCES Especialidad(idEspecialidad),
	idTurnoTrabajo INT NOT NULL FOREIGN KEY REFERENCES TurnoTrabajo(idTurnoTrabajo),
	diaSemana TINYINT NOT NULL CHECK(diaSemana BETWEEN 1 AND 7), -- 1 = lunes, 7 = domingo
	activo BIT NOT NULL DEFAULT 1
);