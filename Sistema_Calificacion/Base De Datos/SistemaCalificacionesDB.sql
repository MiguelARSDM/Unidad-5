CREATE DATABASE SistemaCalificacionesDB;
GO

USE SistemaCalificacionesDB;
GO

-- TABLA: Estudiantes
CREATE TABLE Estudiantes (
    EstudianteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL
);

-- TABLA: Materias
CREATE TABLE Materias (
    MateriaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);

-- TABLA: Calificaciones
CREATE TABLE Calificaciones (
    CalificacionID INT PRIMARY KEY IDENTITY(1,1),

    EstudianteID INT NOT NULL,
    MateriaID INT NOT NULL,

    Calificacion1 DECIMAL(5,2) NOT NULL,
    Calificacion2 DECIMAL(5,2) NOT NULL,
    Calificacion3 DECIMAL(5,2) NOT NULL,
    Calificacion4 DECIMAL(5,2) NOT NULL,
    Examen DECIMAL(5,2) NOT NULL,

    Total DECIMAL(5,2) NOT NULL,
    Clasificacion CHAR(1) NOT NULL,
    Estado VARCHAR(20) NOT NULL,

    CONSTRAINT FK_Calificaciones_Estudiantes 
    FOREIGN KEY (EstudianteID) 
    REFERENCES Estudiantes(EstudianteID),

    CONSTRAINT FK_Calificaciones_Materias 
    FOREIGN KEY (MateriaID) 
    REFERENCES Materias(MateriaID),

    CONSTRAINT UQ_Estudiante_Materia 
    UNIQUE (EstudianteID, MateriaID),

    CONSTRAINT CHK_Notas_Rango
    CHECK (
        Calificacion1 BETWEEN 0 AND 100 AND
        Calificacion2 BETWEEN 0 AND 100 AND
        Calificacion3 BETWEEN 0 AND 100 AND
        Calificacion4 BETWEEN 0 AND 100 AND
        Examen BETWEEN 0 AND 100
    )
);

-- DATOS PARA ESTUDIANTES
INSERT INTO Estudiantes (Nombre, Apellido) VALUES
('Miguel Angel', 'Ramirez Sanchez'),
('Juan', 'Perez Gomez'),
('Ana', 'Lopez Martinez'),
('Carlos', 'Hernandez Diaz'),
('Laura', 'Fernandez Cruz');

-- DATOS PARA MATERIAS
INSERT INTO Materias (Nombre) VALUES
('Programacion I'),
('Base de Datos'),
('Matematica Discreta'),
('Algoritmos'),
('Ingenieria de Software');

-- DATOS PARA CALIFICACIONES
INSERT INTO Calificaciones 
(EstudianteID, MateriaID, Calificacion1, Calificacion2, Calificacion3, Calificacion4, Examen, Total, Clasificacion, Estado)
VALUES
(1, 1, 80, 85, 90, 88, 92, 88.15, 'B', 'Aprobado'),
(2, 2, 70, 75, 78, 72, 80, 75.25, 'C', 'Aprobado'),
(3, 3, 60, 65, 58, 62, 70, 63.75, 'F', 'Reprobado'),
(4, 4, 90, 92, 95, 93, 96, 93.85, 'A', 'Aprobado'),
(5, 5, 85, 87, 83, 86, 88, 85.95, 'B', 'Aprobado');