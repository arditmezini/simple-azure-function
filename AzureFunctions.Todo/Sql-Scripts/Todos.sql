--Create a new database Todos
--Then run the script

CREATE TABLE [dbo].[Todos]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Description] NVARCHAR(50) NOT NULL, 
    [IsCompleted] BIT NOT NULL, 
    [CreatedTime] DATETIME NOT NULL
)