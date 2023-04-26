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
	[UserEmail] varchar(20),
	[ReviewText] varchar(100)
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

USE Cookbook
GO
CREATE PROCEDURE CreateReview
	@RecipeID int,
	@UserEmail varchar(20),
	@ReviewText varchar(100),
	@OutReviewID int output
AS
BEGIN
	INSERT INTO [Reviews] (RecipeID, UserEmail, ReviewText)
	VALUES (@RecipeID, @UserEmail, @ReviewText);
	SELECT @OutReviewID = SCOPE_IDENTITY();
END
GO

USE Cookbook
GO
CREATE PROCEDURE DeleteReview
	@ReviewID int
AS
BEGIN
	DELETE FROM Reviews
	WHERE ReviewID = @ReviewID;
END
GO

USE Cookbook
GO
CREATE PROCEDURE GetReviewsByRecipeID
	@RecipeID int
AS
BEGIN
	SELECT * 
	FROM Reviews
	WHERE Reviews.RecipeID=@RecipeID;
END
GO

USE Cookbook
GO
CREATE PROCEDURE GetReviewsByUserEmail
	@UserEmail varchar(20)
AS
BEGIN
	SELECT * 
	FROM Reviews
	WHERE Reviews.UserEmail=@UserEmail;
END
GO

USE Cookbook
GO
CREATE PROCEDURE DeleteReviewsByRecipeID
	@RecipeID int
AS
BEGIN
	DELETE FROM Reviews
	WHERE Reviews.RecipeID=@RecipeID;
END
GO

USE Cookbook
GO
CREATE PROCEDURE DeleteReviewsByUserID
	@UserEmail varchar(20)
AS
BEGIN
	DELETE FROM Reviews
	WHERE Reviews.UserEmail=@UserEmail;
END
GO

INSERT INTO Roles (RoleName) VALUES ('Guest');
INSERT INTO Roles (RoleName) VALUES ('Viewer');
INSERT INTO Roles (RoleName) VALUES ('Creator');


INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (3,'joseph@gmail.com','Joseph', 'Marker', 'MJqi0O99xjsxOL4WWZpaFlWjOUjz/VYZ2OR5bDYXZJc7qrv3ri+AaSOkUCizVZzJD/dOmK2jumQgFvcJvMX0SLXcpj/vjx7ftOecqT+vGVUNQ1GMKFDVGXo6infsz2jWcWE9yjbyksx2N8YheuCGIRRCPGDFLAmDyTLtUkVmOr5Sq5zznP4RfKuzTYqmhESDNWoBGu9j11338nUhEOhlvFKZoFMveQSr5YIbx2LzRNujZ7g5J2sFYvZNmiL0zG3lwTtcS3tPfPDNm43Jj5K7swcVRwx3QPd74rUmGv+H1RgLlX1LeOjm5trrrSvOClwTDvzTi60fNMosm0DtVgGwlg==', 'b2jURzfEzFnQ9Sh5F5VpS+fCydc9WTsm2gF6Wuvw70eYzSjEpAa6rg6KyDoQWFY2imXysL+L2cPy4h78fzj50g==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'mary@gmail.com', 'Mary', 'Holland', 'MJqi0O99xjsxOL4WWZpaFlWjOUjz/VYZ2OR5bDYXZJc7qrv3ri+AaSOkUCizVZzJD/dOmK2jumQgFvcJvMX0SLXcpj/vjx7ftOecqT+vGVUNQ1GMKFDVGXo6infsz2jWcWE9yjbyksx2N8YheuCGIRRCPGDFLAmDyTLtUkVmOr5Sq5zznP4RfKuzTYqmhESDNWoBGu9j11338nUhEOhlvFKZoFMveQSr5YIbx2LzRNujZ7g5J2sFYvZNmiL0zG3lwTtcS3tPfPDNm43Jj5K7swcVRwx3QPd74rUmGv+H1RgLlX1LeOjm5trrrSvOClwTDvzTi60fNMosm0DtVgGwlg==', 'b2jURzfEzFnQ9Sh5F5VpS+fCydc9WTsm2gF6Wuvw70eYzSjEpAa6rg6KyDoQWFY2imXysL+L2cPy4h78fzj50g==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'david@gmail.com', 'David', 'Duncan', 'MJqi0O99xjsxOL4WWZpaFlWjOUjz/VYZ2OR5bDYXZJc7qrv3ri+AaSOkUCizVZzJD/dOmK2jumQgFvcJvMX0SLXcpj/vjx7ftOecqT+vGVUNQ1GMKFDVGXo6infsz2jWcWE9yjbyksx2N8YheuCGIRRCPGDFLAmDyTLtUkVmOr5Sq5zznP4RfKuzTYqmhESDNWoBGu9j11338nUhEOhlvFKZoFMveQSr5YIbx2LzRNujZ7g5J2sFYvZNmiL0zG3lwTtcS3tPfPDNm43Jj5K7swcVRwx3QPd74rUmGv+H1RgLlX1LeOjm5trrrSvOClwTDvzTi60fNMosm0DtVgGwlg==', 'b2jURzfEzFnQ9Sh5F5VpS+fCydc9WTsm2gF6Wuvw70eYzSjEpAa6rg6KyDoQWFY2imXysL+L2cPy4h78fzj50g==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (3,'samuel@gmail.com', 'Samuel', 'Martin', 'MJqi0O99xjsxOL4WWZpaFlWjOUjz/VYZ2OR5bDYXZJc7qrv3ri+AaSOkUCizVZzJD/dOmK2jumQgFvcJvMX0SLXcpj/vjx7ftOecqT+vGVUNQ1GMKFDVGXo6infsz2jWcWE9yjbyksx2N8YheuCGIRRCPGDFLAmDyTLtUkVmOr5Sq5zznP4RfKuzTYqmhESDNWoBGu9j11338nUhEOhlvFKZoFMveQSr5YIbx2LzRNujZ7g5J2sFYvZNmiL0zG3lwTtcS3tPfPDNm43Jj5K7swcVRwx3QPd74rUmGv+H1RgLlX1LeOjm5trrrSvOClwTDvzTi60fNMosm0DtVgGwlg==', 'b2jURzfEzFnQ9Sh5F5VpS+fCydc9WTsm2gF6Wuvw70eYzSjEpAa6rg6KyDoQWFY2imXysL+L2cPy4h78fzj50g==');
INSERT INTO Users (RoleID, Email, FirstName, LastName, HashedPassword, Salt)
	VALUES (2,'ruth@gmail.com', 'Ruth', 'Jenkins', 'MJqi0O99xjsxOL4WWZpaFlWjOUjz/VYZ2OR5bDYXZJc7qrv3ri+AaSOkUCizVZzJD/dOmK2jumQgFvcJvMX0SLXcpj/vjx7ftOecqT+vGVUNQ1GMKFDVGXo6infsz2jWcWE9yjbyksx2N8YheuCGIRRCPGDFLAmDyTLtUkVmOr5Sq5zznP4RfKuzTYqmhESDNWoBGu9j11338nUhEOhlvFKZoFMveQSr5YIbx2LzRNujZ7g5J2sFYvZNmiL0zG3lwTtcS3tPfPDNm43Jj5K7swcVRwx3QPd74rUmGv+H1RgLlX1LeOjm5trrrSvOClwTDvzTi60fNMosm0DtVgGwlg==', 'b2jURzfEzFnQ9Sh5F5VpS+fCydc9WTsm2gF6Wuvw70eYzSjEpAa6rg6KyDoQWFY2imXysL+L2cPy4h78fzj50g==');

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

INSERT INTO Reviews ([RecipeID], [UserEmail], [ReviewText])
	VALUES (1, 'mary@gmail.com', 'Great taste and very easy to make. I used a bottled tomato sauce with basil.');
INSERT INTO Reviews ([RecipeID], [UserEmail], [ReviewText])
	VALUES (1, 'david@gmail.com', 'My family loved it. It was easy to follow.');

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

INSERT INTO Reviews ([RecipeID], [UserEmail], [ReviewText])
	VALUES (2, 'mary@gmail.com', 'I cannot recommend. This recipe uses too much cheese.');