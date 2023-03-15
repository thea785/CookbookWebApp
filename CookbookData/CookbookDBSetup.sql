USE MASTER
GO
ALTER DATABASE Cookbook set single_user with rollback immediate
DROP DATABASE IF EXISTS Cookbook;

CREATE DATABASE Cookbook;
GO
USE Cookbook
GO

CREATE TABLE [Roles] (
	 RoleID int IDENTITY(1,1) PRIMARY KEY,
	 RoleName varchar(255) UNIQUE
);

CREATE TABLE [Users] (
	UserID int IDENTITY(1,1) PRIMARY KEY,
	RoleID int FOREIGN KEY REFERENCES Roles(RoleID),
	Email varchar(20) NOT NULL UNIQUE,
	FirstName varchar(20) NOT NULL,
	LastName varchar(20) NOT NULL,
	HashedPassword varchar(MAX),
	Salt varchar(255)
);

CREATE TABLE Recipes (
	[RecipeID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(100) NOT NULL,
	[Servings] int,
	[PrepTime] int,
	[CookTime] int,
	[Directions] varchar(1000)
);

CREATE TABLE Ingredients (
	[IngredientID] int IDENTITY(1,1) PRIMARY KEY,
	[RecipeID] int FOREIGN KEY REFERENCES Recipes(RecipeID),
	[Name] varchar(100) NOT NULL,
	[Amount] int,
	[Units] varchar(100)
);

CREATE TABLE [dbo].[ExceptionLogging](
	[ExceptionLoggingID] [int] IDENTITY(1,1) PRIMARY KEY,
	[StackTrace] [nvarchar](1000) NULL,
	[Message] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](100) NULL,
	[Url] [nvarchar](100) NULL,
	[LogDate] [datetime] NOT NULL
);

USE Cookbook
GO
CREATE PROCEDURE DeleteRecipe
	@RecipeID int
AS
BEGIN
	DELETE FROM Recipes
	WHERE RecipeID=@RecipeID
END
GO

USE Cookbook
GO
CREATE PROCEDURE CreateRecipe
	@Name varchar(100),
	@Servings int,
	@PrepTime int,
	@CookTime int,
	@Directions varchar(1000),
	@OutRecipeID int output
AS
BEGIN
	INSERT INTO Recipes ([Name], [Servings], [PrepTime], [CookTime], [Directions])
	VALUES (@Name, @Servings, @PrepTime, @CookTime, @Directions)
	SELECT @OutRecipeID = SCOPE_IDENTITY();
END
GO

USE Cookbook
GO
CREATE PROCEDURE GetRecipeByID
	@RecipeID int
AS
BEGIN
	SELECT * FROM Recipes
	WHERE RecipeID=@RecipeID;
END
GO

USE Cookbook
GO
CREATE PROCEDURE GetRecipes
AS
BEGIN
	SELECT * FROM Recipes;
END
GO


USE Cookbook
GO
CREATE PROCEDURE UpdateRecipe
	@RecipeID int,
	@Name varchar(100),
	@Servings int,
	@PrepTime int,
	@CookTime int,
	@Directions varchar(1000)
AS
BEGIN
	UPDATE Recipes
	SET Name=@Name, Servings=@Servings, PrepTime=@PrepTime, CookTime=@CookTime, Directions=@Directions
	WHERE RecipeID=@RecipeID
END
GO

USE Cookbook
GO
CREATE PROCEDURE CreateIngredient
	@RecipeID int,
	@Name varchar(100),
	@Amount int,
	@Units varchar(100),
	@OutIngredientID int output
AS
BEGIN
	INSERT INTO Ingredients([RecipeID], [Name], [Amount], [Units])
	VALUES (@RecipeID, @Name, @Amount, @Units)
	SELECT @OutIngredientID = SCOPE_IDENTITY();
END
GO

USE Cookbook
GO
CREATE PROCEDURE GetIngredients
	@RecipeID int
AS
BEGIN
	SELECT * FROM Ingredients
	WHERE RecipeID=@RecipeID;
END
GO

USE Cookbook
GO
CREATE PROCEDURE DeleteIngredients
	@RecipeID int
AS
BEGIN
	DELETE FROM Ingredients
	WHERE RecipeID=@RecipeID;
END
GO

USE Cookbook
GO
CREATE PROCEDURE [dbo].[CreateExceptionLog]
	@StackTrace nvarchar(1000)
    ,@Message nvarchar(100)
    ,@Source nvarchar(100)
    ,@Url nvarchar(100)
    ,@LogDate datetime
	,@parmOutExceptionLoggingID int output
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[ExceptionLogging] ([StackTrace],[Message],[Source],[Url],[LogDate])
	VALUES (@StackTrace,@Message,@Source,@Url,@LogDate);
	SELECT @parmOutExceptionLoggingID = SCOPE_IDENTITY();
END
GO

DECLARE @Newline AS CHAR(2) = CHAR(13) + CHAR(10)

INSERT INTO Recipes ([Name], [Servings], [PrepTime], [CookTime], [Directions])
	VALUES ('Chicken Parmesean', 6, 20, 30, '1. Preheat oven to 400. Combine bread crumbs and seasoning.' + @Newline + '2. Dip chicken breasts in egg, then coat with crumbs.' + @Newline + '3. Bake chicken 20 mins.' + @Newline + '4. Pour over pasta sauce. Top with mozzarella cheese. Bake 10 mins.');

INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (1, 'Bread Crumbs', 1, 'cups');
INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (1, 'Italian Seasoning', 1, 'tsp');
INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (1, 'Chicken Breast Halves', 6, 'count');
INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (1, 'Pasta Sauce', 3, 'cups');
INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (1, 'Shredded Mozzarella Cheese', 1, 'cups');