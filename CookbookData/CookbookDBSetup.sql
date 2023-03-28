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

CREATE TABLE Reviews (
	[ReviewID] int IDENTITY(1,1) PRIMARY KEY,
	[RecipeID] int FOREIGN KEY REFERENCES Recipes(RecipeID),
	[UserID] int FOREIGN KEY REFERENCES Users(UserID),
	[Text] varchar(100)
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


USE Cookbook
GO
CREATE PROCEDURE CreateUser
	-- Add the parameters for the stored procedure here
	@RoleID int,
	@Email varchar(20),
	@FirstName varchar(20),
	@LastName varchar(20),
	@HashedPassword varchar(MAX),
	@Salt varchar(255),
	@paramOutUserID int output
AS
BEGIN
	-- Insert statements for procedure here
	INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (@RoleID,@Email,@FirstName,@LastName,@HashedPassword,@Salt);
	SELECT @paramOutUserID = SCOPE_IDENTITY();
END
GO

USE Cookbook
GO
CREATE PROCEDURE GetAllUsers
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- Insert statements for procedure here
	SELECT * FROM Users;
END
GO


USE Cookbook
GO
CREATE PROCEDURE GetUserByEmail
	-- Add the parameters for the stored procedure here
	@paramEmail varchar(255)
AS
BEGIN
	-- Insert statements for procedure here
	SELECT * 
	FROM Users
	WHERE Users.Email=@paramEmail;
END
GO

USE Cookbook
GO
CREATE PROCEDURE DeleteUser
	-- Add the parameters for the stored procedure here
	@paramUserID int
AS
BEGIN
	-- Insert statements for procedure here
	DELETE FROM Users
	WHERE UserID = @paramUserID;
END
GO


INSERT INTO Roles (RoleName) VALUES ('Guest');
INSERT INTO Roles (RoleName) VALUES ('Viewer');
INSERT INTO Roles (RoleName) VALUES ('Creator');


INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (3,'joseph@gmail.com','Joseph', 'Marker', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'mary@gmail.com', 'Mary', 'Holland', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'david@gmail.com', 'David', 'Duncan', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (3,'samuel@gmail.com', 'Samuel', 'Martin', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'ruth@gmail.com', 'Ruth', 'Jenkins', 'SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==', 'GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==');

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

INSERT INTO Recipes ([Name], [Servings], [PrepTime], [CookTime], [Directions])
VALUES ('Grilled Cheese Sandwich', 1, 2, 10, '1. Spread butter on the outer side of the bread.' + @Newline + '2. Heat a skillet on medium. Place bread on skillet.' + @Newline + '3. Stack cheeses on the bread. Once toasted, close the sandwich.' + @Newline + '4. Cook for six minutes or until brown. Cut diagonally and serve.');

INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (2, 'Sandwich Bread', 2, 'count');
INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (2, 'Unsalted Butter', 1, 'Tbsp');
INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (2, 'Cheddar Cheese', 1, 'slice');
INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (2, 'Gouda Cheese', 1, 'slice');
INSERT INTO Ingredients ([RecipeID], [Name], [Amount], [Units])
	VALUES (2, 'Havarti Cheese', 1, 'slice');
