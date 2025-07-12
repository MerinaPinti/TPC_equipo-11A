
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
('Neurología'),
('Ginecología'),
('Traumatología'),
('Oftalmología'),
('Endocrinología'),
('Psiquiatría');          
GO

-- ESTADOS DE TURNOS
INSERT INTO Estado (descripcion) VALUES ('Asignado') ;
INSERT INTO Estado (descripcion) VALUES('Reprogramado');  
INSERT INTO Estado (descripcion) VALUES('Cancelado');     
INSERT INTO Estado (descripcion) VALUES('No Asistió');    
INSERT INTO Estado (descripcion) VALUES('Cerrado');
INSERT INTO Estado (descripcion) VALUES ('En sala de espera');
GO



-- Usuario ADMIN
INSERT INTO Usuario (idTipoUsuario, usuario, contraseña)
VALUES (1, 'admin', 'admin123');

-- Usuario Recepcionista
INSERT INTO Usuario (idTipoUsuario, usuario, contraseña)
VALUES (2, 'recepcion1', 'recep123');
GO

-- USUARIOS MÉDICOS Y MÉDICOS USUARIO
INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '236785', '236785');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('juan.perez@mail.com', '1123456789', 'Juan', 'Pérez', '236785', SCOPE_IDENTITY());

INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '582903', '582903');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('maria.lopez@mail.com', '1198785432', 'María', 'López', '582903', SCOPE_IDENTITY());

INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '790412', '790412');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('carlos.gomez@mail.com', '1176543210', 'Carlos', 'Gómez', '790412', SCOPE_IDENTITY());

INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '413278', '413278');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('lucia.fernandez@mail.com', '1144556677', 'Lucía', 'Fernández', '413278', SCOPE_IDENTITY());

INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '629845', '629845');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('diego.ramirez@mail.com', '1155667788', 'Diego', 'Ramírez', '629845', SCOPE_IDENTITY());

INSERT INTO Usuario (idTipoUsuario, usuario, contraseña) VALUES (3, '817362', '817362');
INSERT INTO Medico (email, telefono, nombre, apellido, matricula, idUsuario) 
VALUES ('veronica.martinez@mail.com', '1166778899', 'Verónica', 'Martínez', '817362', SCOPE_IDENTITY());

GO

-- PACIENTES
INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Lucía', 'Ramírez', '38562047', '1990-03-15', '1144441234', 'Calle Falsa 123', 'lucia.ramirez@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Federico', 'Martínez', '40234895', '1985-07-10', '1155556789', 'Av. Siempreviva 742', 'federico.martinez@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Valentina', 'Suárez', '42783914', '2005-11-23', '1122223344', 'Pasaje Los Robles 543', 'valentina.suarez@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Matías', 'González', '31845678', '1973-02-08', '1133332211', 'Ruta 8 km 35', 'matias.gonzalez@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Camila', 'Pereyra', '44678231', '2012-06-19', '1166667777', 'Barrio San Jorge Mz 12', 'camila.pereyra@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Bruno', 'Alvarez', '35890245', '1997-12-01', '1188889999', 'Calle Tucumán 457', 'bruno.alvarez@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Martina', 'Torres', '41356982', '2002-04-30', '1177773333', 'Av. Pilar Centro 112', 'martina.torres@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Santiago', 'Reyes', '29561034', '1968-09-17', '1112123434', 'Camino de las Lomas 89', 'santiago.reyes@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Florencia', 'Molina', '32569741', '1980-01-27', '1110102020', 'Av. Del Libertador 770', 'florencia.molina@mail.com');

INSERT INTO Paciente (nombre, apellido, DNI, fechaNac, telefono, direccion, email) 
VALUES ('Julián', 'Herrera', '45982317', '2016-08-05', '1130304040', 'Calle Alberti 321', 'julian.herrera@mail.com');

GO

-- RELACIÓN MEDICO - ESPECIALIDAD
-- Juan Pérez (IDMEDICO = 1)
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (1, 1); -- Clínica General
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (10, 1); -- Psiquiatría

-- María López (IDMEDICO = 2)
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (3, 2); -- Cardiología
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (9, 2); -- Endocrinología

-- Carlos Gómez (IDMEDICO = 3)
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (2, 3); -- Pediatría
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (5, 3); -- Neurología
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (8, 3); -- Oftalmología

-- Lucía Ruiz (IDMEDICO = 4)
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (4, 4); -- Dermatología
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (6, 4); -- Ginecología

-- Diego Morales (IDMEDICO = 5)
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (5, 5); -- Neurología
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (7, 5); -- Traumatología

-- Verónica Martínez (IDMEDICO = 6)
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (6, 6); -- Ginecología
INSERT INTO Especialidades_Medicos (IDESPECIALIDAD, IDMEDICO) VALUES (9, 6); -- Endocrinología

GO

-- TURNOS DE TRABAJO
INSERT INTO TurnoTrabajo (descripcion, horaInicio, horaFin) VALUES
('Mañana', '08:00', '12:00'),
('Tarde', '14:00', '18:00'),
('Noche', '18:00', '22:00');

GO

-- HORARIOS DE ATENCION
-- Juan Pérez (IDMEDICO = 1) – Clínica General y Psiquiatría
INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES
(1, 1, 1, 1),  -- Lunes Mañana – Clínica
(1, 1, 1, 3),  -- Miércoles Mañana – Clínica
(1, 10, 2, 5); -- Viernes Tarde – Psiquiatría

-- María López (IDMEDICO = 2) – Cardiología y Endocrinología
INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES
(2, 3, 2, 2),  -- Martes Tarde – Cardiología
(2, 3, 2, 4),  -- Jueves Tarde – Cardiología
(2, 9, 1, 5);  -- Viernes Mañana – Endocrinología

-- Carlos Gómez (IDMEDICO = 3) – Pediatría, Neurología y Oftalmología
INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES
(3, 2, 1, 1),  -- Lunes Mañana – Pediatría
(3, 5, 2, 3),  -- Miércoles Tarde – Neurología
(3, 8, 3, 5);  -- Viernes Noche – Oftalmología

-- Lucía Ruiz (IDMEDICO = 4) – Dermatología y Ginecología
INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES
(4, 4, 1, 2),  -- Martes Mañana – Dermatología
(4, 6, 2, 4);  -- Jueves Tarde – Ginecología

-- Diego Morales (IDMEDICO = 5) – Neurología y Traumatología
INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES
(5, 5, 1, 3),  -- Miércoles Mañana – Neurología
(5, 7, 2, 5);  -- Viernes Tarde – Traumatología

-- Verónica Martínez (IDMEDICO = 6) – Ginecología y Endocrinología
INSERT INTO HorarioAtencion (idMedico, idEspecialidad, idTurnoTrabajo, diaSemana) VALUES
(6, 6, 2, 1),  -- Lunes Tarde – Ginecología
(6, 9, 1, 4);  -- Jueves Mañana – Endocrinología

GO

SELECT * FROM Medico
SELECT * FROM Turno
SELECT * FROM Paciente
SELECT * FROM Especialidades_Medicos

-- TURNOS
INSERT INTO Turno (idPaciente, idMedico, fecha, hora, idEstado, idEspecialidad, observaciones, diagnostico, fechaAlta, ultimaModificacion) 
VALUES
(1, 1, '2025-07-14', '08:00:00', 4, 1, NULL, NULL, GETDATE(), NULL),

(5, 5, '2025-07-18', '14:00:00', 4, 7, NULL, NULL, GETDATE(), DATEADD(DAY, -1, GETDATE())),

(3, 1, '2025-07-18', '15:00:00', 3, 10, NULL, NULL, GETDATE(), DATEADD(DAY, -1, GETDATE())),

(4, 2, '2025-07-17', '14:00:00', 1, 3, NULL, NULL, GETDATE(), NULL),

(6, 3, '2025-07-14', '09:00:00', 1, 2, NULL, NULL, GETDATE(), NULL),

(7, 6, '2025-07-14', '15:00:00', 3, 6, NULL, NULL, GETDATE(), GETDATE()),

(9, 4, '2025-07-15', '08:00:00', 3, 4, NULL, NULL, GETDATE(), GETDATE()),

(2, 5, '2025-07-16', '09:00:00', 1, 5, NULL, NULL, GETDATE(), NULL),

(7, 3, '2025-07-16', '14:00:00', 3, 5, NULL, NULL, GETDATE(), DATEADD(DAY, -2, GETDATE())),

(10, 6, '2025-07-17', '08:00:00', 3, 9, NULL, NULL, GETDATE(), DATEADD(DAY, -1, GETDATE())),

(10, 4, '2025-07-17', '14:00:00', 1, 6, NULL, NULL, GETDATE(), NULL),

(5, 2, '2025-07-25', '08:00:00', 3, 9, NULL, NULL, GETDATE(), GETDATE()),

(8, 3, '2025-07-18', '18:00:00', 1, 8, NULL, NULL, GETDATE(), NULL);

GO

-- TURNOS CERRADOS (HISTORIA CLÍNICA)
INSERT INTO Turno (idPaciente, idMedico, fecha, hora, idEstado, idEspecialidad, observaciones, diagnostico, fechaAlta, ultimaModificacion) 
VALUES
(1, 1, '2025-06-28', '09:00:00', 5, 1, 'Chequeo general anual sin hallazgos', 'Paciente sano, sin afecciones', '2025-06-28', '2025-06-29'),

(3, 1, '2025-06-30', '15:00:00', 5, 10, 'Consulta por ansiedad leve', 'Trastorno de ansiedad generalizada - Derivado a terapia', '2025-06-30', '2025-07-01'),

(4, 2, '2025-06-27', '15:00:00', 5, 3, 'Evaluación cardíaca post-examen ergométrico', 'Ritmo sinusal normal, sin signos de isquemia', '2025-06-27', '2025-06-27'),

(5, 2, '2025-07-04', '08:00:00', 5, 9, 'Control de TSH y T4 libre', 'Hipotiroidismo subclínico - Continuar Levotiroxina', '2025-07-04', '2025-07-05'),

(6, 3, '2025-07-01', '09:00:00', 5, 2, 'Fiebre persistente y vómitos', 'Faringitis viral - Control en 48h', '2025-07-01', '2025-07-01'),

(9, 4, '2025-07-03', '10:30:00', 5, 4, 'Revisión de mancha oscura en cuello', 'Melanosis benigna - sin signos de malignidad', '2025-07-03', '2025-07-04'),

(10, 4, '2025-07-05', '15:00:00', 5, 6, 'Primera consulta ginecológica adolescente', 'Sin patología - Educación y anticoncepción iniciada', '2025-07-05', '2025-07-05'),

(2, 5, '2025-07-02', '09:00:00', 5, 5, 'Dolor de cabeza y hormigueo en brazo', 'Migraña con aura - Tratamiento sintomático', '2025-07-02', '2025-07-03'),

(5, 5, '2025-07-06', '14:00:00', 5, 7, 'Evaluación de lumbalgia crónica', 'Contractura muscular con pinzamiento leve - Fisioterapia indicada', '2025-07-06', '2025-07-06'),

(7, 6, '2025-07-01', '14:00:00', 5, 6, 'Control ginecológico anual postmenstrual', 'Sin hallazgos clínicos - PAP negativo', '2025-07-01', '2025-07-02');

GO


