CREATE DATABASE SistemaCalificacionesDB;
GO

-- TABLA: Estudiantes
CREATE TABLE Estudiantes (
    EstudianteID INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL
);

-- TABLA: Materias
CREATE TABLE Materias (
    MateriaID INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);

-- TABLA: Calificaciones
CREATE TABLE Calificaciones (
    CalificacionID INT PRIMARY KEY,

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
INSERT INTO Estudiantes (EstudianteID,Nombre, Apellido) VALUES
(1,'Miguel Angel', 'Ramirez Sanchez'),
(2,'Juan', 'Perez Gomez'),
(3,'Ana', 'Lopez Martinez'),
(4,'Carlos', 'Hernandez Diaz'),
(5,'Laura', 'Fernandez Cruz');

-- DATOS PARA MATERIAS
INSERT INTO Materias (MateriaID,Nombre) VALUES
(1,'Programacion I'),
(2,'Base de Datos'),
(3,'Matematica Discreta'),
(4,'Algoritmos'),
(5,'Ingenieria de Software');

-- DATOS PARA CALIFICACIONES
INSERT INTO Calificaciones 
(CalificacionID,EstudianteID, MateriaID, Calificacion1, Calificacion2, Calificacion3, Calificacion4, Examen, Total, Clasificacion, Estado)
VALUES
(1,1, 1, 80, 85, 90, 88, 92, 88.15, 'B', 'Aprobado'),
(2,2, 2, 70, 75, 78, 72, 80, 75.25, 'C', 'Aprobado'),
(3,3, 3, 60, 65, 58, 62, 70, 63.75, 'F', 'Reprobado'),
(4,4, 4, 90, 92, 95, 93, 96, 93.85, 'A', 'Aprobado'),
(5,5, 5, 85, 87, 83, 86, 88, 85.95, 'B', 'Aprobado');