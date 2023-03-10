USE [master]
GO
/****** Object:  Database [PruebaTec]    Script Date: 09/03/2023 04:34:25 p. m. ******/
CREATE DATABASE [PruebaTec]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PruebaTec', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PruebaTec.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PruebaTec_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PruebaTec_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PruebaTec] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaTec].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PruebaTec] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PruebaTec] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PruebaTec] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PruebaTec] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PruebaTec] SET ARITHABORT OFF 
GO
ALTER DATABASE [PruebaTec] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PruebaTec] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PruebaTec] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PruebaTec] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PruebaTec] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PruebaTec] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PruebaTec] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PruebaTec] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PruebaTec] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PruebaTec] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PruebaTec] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PruebaTec] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PruebaTec] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PruebaTec] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PruebaTec] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PruebaTec] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PruebaTec] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PruebaTec] SET RECOVERY FULL 
GO
ALTER DATABASE [PruebaTec] SET  MULTI_USER 
GO
ALTER DATABASE [PruebaTec] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PruebaTec] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PruebaTec] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PruebaTec] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PruebaTec] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PruebaTec] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PruebaTec', N'ON'
GO
ALTER DATABASE [PruebaTec] SET QUERY_STORE = OFF
GO
USE [PruebaTec]
GO
/****** Object:  Table [dbo].[usuariosPruebaTec]    Script Date: 09/03/2023 04:34:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuariosPruebaTec](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[FechaNac] [datetime] NULL,
	[Sexo] [varchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_actualizar_usuario]    Script Date: 09/03/2023 04:34:27 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_actualizar_usuario]
@Idusario int,
@Nombre varchar(100),
@FechaNac datetime,
@Sexo varchar(1)
as
update usuariosPruebaTec
set Nombre=@Nombre,
FechaNac=@FechaNac,
Sexo=@Sexo
where IdUsuario=@Idusario;
GO
/****** Object:  StoredProcedure [dbo].[sp_crear_usuario]    Script Date: 09/03/2023 04:34:27 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_crear_usuario]
@Nombre varchar(100),
@FechaNac datetime,
@Sexo varchar(1)
as
INSERT INTO usuariosPruebaTec(Nombre,FechaNac,Sexo) values(@Nombre,@FechaNac,@Sexo);
GO
/****** Object:  StoredProcedure [dbo].[sp_eliminar_usuario]    Script Date: 09/03/2023 04:34:27 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*create procedure sp_crear_usuario
@Nombre varchar(100),
@FechaNac datetime,
@Sexo varchar(1)
as
INSERT INTO usuariosPruebaTec(Nombre,FechaNac,Sexo) values(@Nombre,@FechaNac,@Sexo);
go*/
/*set dateformat dmy;*/
create procedure [dbo].[sp_eliminar_usuario]
@Idusuario int
as
delete from usuariosPruebaTec 
where IdUsuario = @Idusuario;
GO
/****** Object:  StoredProcedure [dbo].[sp_leer_todos]    Script Date: 09/03/2023 04:34:27 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_leer_todos]
as 
select * from usuariosPruebaTec;
GO
/****** Object:  StoredProcedure [dbo].[sp_leer_usuario]    Script Date: 09/03/2023 04:34:27 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_leer_usuario]
@Idusuario int
as
select * from usuariosPruebaTec where IdUsuario=@Idusuario;
GO
USE [master]
GO
ALTER DATABASE [PruebaTec] SET  READ_WRITE 
GO
