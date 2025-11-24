-- 1. Crear la tabla ComunicadoVisto (si no existe)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComunicadoVisto]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ComunicadoVisto](
        [Id_Comunicado] INT NOT NULL,
        [Id_Usuario] INT NOT NULL,
        [FechaVisto] DATETIME NOT NULL DEFAULT GETDATE(),
        PRIMARY KEY (Id_Comunicado, Id_Usuario),
        FOREIGN KEY (Id_Comunicado) REFERENCES Comunicado(Id_Comunicado),
        FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id)
    );
END
GO

-- 2. Crear SP para marcar como visto
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_MarcarComunicadoVisto]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_MarcarComunicadoVisto]
GO

CREATE PROCEDURE [dbo].[sp_MarcarComunicadoVisto]
    @Id_Comunicado INT,
    @Id_Usuario INT
AS
BEGIN
    -- Solo insertar si no existe ya
    IF NOT EXISTS (SELECT 1 FROM ComunicadoVisto WHERE Id_Comunicado = @Id_Comunicado AND Id_Usuario = @Id_Usuario)
    BEGIN
        INSERT INTO ComunicadoVisto (Id_Comunicado, Id_Usuario, FechaVisto)
        VALUES (@Id_Comunicado, @Id_Usuario, GETDATE())
    END
END
GO

-- 3. Modificar SP listar_comunicados para incluir estado Visto
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[listar_comunicados]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[listar_comunicados]
GO

CREATE PROCEDURE [dbo].[listar_comunicados]
    @Id_Usuario INT -- El usuario que está viendo la lista
AS
BEGIN
    SELECT 
        c.Id_Comunicado,
        c.Id_Usuario,
        u.NombreUsuario,
        u.Nombres,
        u.ApPaterno,
        u.ApMaterno,
        c.Nombre,
        c.Descripcion,
        c.FechaCreacion,
        c.FechaFinal,
        -- Columna calculada: 1 si existe en ComunicadoVisto, 0 si no
        CASE WHEN cv.Id_Comunicado IS NOT NULL THEN 1 ELSE 0 END AS Visto
    FROM 
        Comunicado c
    INNER JOIN 
        Usuario u ON c.Id_Usuario = u.Id
    LEFT JOIN
        ComunicadoVisto cv ON c.Id_Comunicado = cv.Id_Comunicado AND cv.Id_Usuario = @Id_Usuario
    WHERE 
        c.FechaFinal >= CAST(GETDATE() as DATE) OR c.FechaFinal IS NULL
    ORDER BY 
        c.FechaCreacion DESC
END
GO
