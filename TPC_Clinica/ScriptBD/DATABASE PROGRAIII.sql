
USE MASTER
GO
CREATE DATABASE CLINICA_DB;
GO
USE CLINICA_DB;
GO

CREATE TABLE TipoUsuario (
	idTipoUsuario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descripcion varchar(50) NOT NULL,
	activo BIT NOT NULL DEFAULT 1
);

GO

CREATE TABLE Especialidad(
	idEspecialidad INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	descripcion varchar(50) NOT NULL,
	activo BIT NOT NULL DEFAULT 1
);

GO

CREATE TABLE Estado(
	idEstado INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	descripcion varchar(30) NOT NULL,
	activo BIT NOT NULL DEFAULT 1
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
	activo BIT NOT NULL DEFAULT 1
);

GO

CREATE TABLE Usuario(
	idUsuario INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idTipoUsuario INT NOT NULL FOREIGN KEY REFERENCES TipoUsuario(idTipoUsuario),
	usuario VARCHAR(15) NOT NULL,
	contraseña VARCHAR(255) NOT NULL,
	activo BIT NOT NULL DEFAULT 1
);

GO

CREATE TABLE Medico(
	idMedico INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	email VARCHAR(100) NOT NULL,
	telefono VARCHAR(15),
	nombre VARCHAR(100),
	apellido VARCHAR(100),
	matricula VARCHAR(8) NOT NULL UNIQUE, 
	idUsuario INT NULL, 
	activo BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario)
);

GO

CREATE TABLE Especialidades_Medicos (
	IDESPECIALIDAD INT NOT NULL FOREIGN KEY REFERENCES Especialidad(idEspecialidad),
    IDMEDICO INT NOT NULL FOREIGN KEY REFERENCES Medico(idMedico),
	Activo bit NOT NULL DEFAULT 1,
    PRIMARY KEY(IDESPECIALIDAD, IDMEDICO)
);

GO
CREATE TABLE Turno(
	idTurno INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idPaciente INT NOT NULL FOREIGN KEY REFERENCES Paciente(idPaciente),
	idMedico INT NOT NULL FOREIGN KEY REFERENCES Medico(idMedico),
	fecha DATE NOT NULL,
	hora TIME NOT NULL,
	idEstado INT NOT NULL FOREIGN KEY REFERENCES Estado(idEstado),
	idEspecialidad INT NOT NULL FOREIGN KEY REFERENCES Especialidad(idEspecialidad),
	observaciones VARCHAR(200),
	diagnostico VARCHAR(200),
	fechaAlta DATE,
	ultimaModificacion DATE, 
	activo BIT NOT NULL DEFAULT 1
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
	diaSemana TINYINT NOT NULL CHECK(diaSemana BETWEEN 1 AND 7),
	activo BIT NOT NULL DEFAULT 1
);

GO

USE CLINICA_DB;
GO

-- TIPO DE USUARIO
INSERT INTO TipoUsuario (descripcion) VALUES ('Administrador');
INSERT INTO TipoUsuario (descripcion) VALUES ('Recepcionista');
INSERT INTO TipoUsuario (descripcion) VALUES ('Médico');
GO

-- ESPECIALIDADES
INSERT INTO Especialidad (descripcion) VALUES 
('Clínica General'),
('Pediatría'),           
('Cardiología'),       
('Dermatología'),        
('Neurología');          
GO

-- ESTADOS DE TURNOS
INSERT INTO Estado (descripcion) VALUES ('Asignado') ;
INSERT INTO Estado (descripcion) VALUES('Reprogramado');  
INSERT INTO Estado (descripcion) VALUES('Cancelado');     
INSERT INTO Estado (descripcion) VALUES('No Asistió');    
INSERT INTO Estado (descripcion) VALUES('Cerrado');        
GO

-- USUARIOS MÉDICOS Y MÉDICOS USUARIO
INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '236785', '236785');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('juan.perez@mail.com', '11-2345-6789', 'Juan', 'Pérez', '236785', SCOPE_IDENTITY());

INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '582903', '582903');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('maria.lopez@mail.com', '11-9876-5432', 'María', 'López', '582903', SCOPE_IDENTITY());

INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '790412', '790412');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('carlos.gomez@mail.com', '11-7654-3210', 'Carlos', 'Gómez', '790412', SCOPE_IDENTITY());
GO

-- Usuario ADMIN
INSERT INTO Usuario (idTipoUsuario, usuario, contraseña)
VALUES (1, 'admin', 'admin123');

-- Usuario Recepcionista
INSERT INTO Usuario (idTipoUsuario, usuario, contraseña)
VALUES (2, 'recepcion1', 'recep123');
GO

-- PACIENTES
INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Lucía', 'Ramírez', '38562047', '1990-03-15', '11-4444-1234', 'Calle Falsa 123', 'lucia.ramirez@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Federico', 'Martínez', '40234895', '1985-07-10', '11-5555-6789', 'Av. Siempreviva 742', 'federico.martinez@mail.com');
GO

-- RELACIÓN MEDICO - ESPECIALIDAD
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (1, 1); -- Juan Pérez - Clínico
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (2, 2); -- María López - Cardióloga
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (3, 3); -- Carlos Gómez - Pediatra
GO

-- TURNOS DE TRABAJO
INSERT INTO TurnoTrabajo (descripcion, horaInicio, horaFin) VALUES
('Mañana', '08:00', '12:00'),
('Tarde', '14:00', '18:00'),
('Noche', '18:00', '22:00');
GO

-- HORARIOS DE ATENCION
INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES
(1, 1, 1, 1), -- lunes
(1, 1, 1, 3); -- miércoles
GO

INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES
(2, 2, 2, 2), -- martes
(2, 2, 2, 4); -- jueves
GO

-- TURNOS
INSERT INTO Turno (idPaciente, idMedico, fecha, hora, idEstado, idEspecialidad, observaciones, diagnostico, fechaAlta, ultimaModificacion) VALUES
(1, 1, '2025-07-10', '09:00', 1, 1, 'Chequeo general', NULL, GETDATE(), NULL);

INSERT INTO Turno (idPaciente, idMedico, fecha, hora, idEstado, idEspecialidad, observaciones, diagnostico, fechaAlta, ultimaModificacion) VALUES
(2, 2, '2025-07-11', '15:00', 2, 2, 'Control pediátrico para su hijo', NULL, GETDATE(), NULL);
GO