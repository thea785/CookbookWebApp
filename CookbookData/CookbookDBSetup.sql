USE MASTER
GO
DROP DATABASE IF EXISTS Cookbook;

CREATE DATABASE Cookbook;
GO
USE Cookbook
GO

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

USE Cookbook
GO
CREATE PROCEDURE CreateRecipe
	@Name varchar(100),
	@Servings int,
	@PrepTime int,
	@CookTime int,
	@Directions varchar(1000),
	@OutRecipeID int
AS
BEGIN
	INSERT INTO Recipes ([Name], [Servings], [PrepTime], [CookTime], [Directions])
	VALUES (@Name, @Servings, @PrepTime, @CookTime, @Directions)
	SELECT @OutRecipeID = SCOPE_IDENTITY();
END
GO