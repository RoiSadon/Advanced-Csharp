# Entity framework project
1. Create DB in Microsoft SQL

```sql
USE master
GO
CREATE DATABASE BookStore
GO

USE BookStore
GO
CREATE TABLE [dbo].[Authors] (
    [AuthorID]   INT           IDENTITY (1, 1) NOT NULL,
    [AuthorAge] INT  NOT NULL,
    [AuthorName] NVARCHAR (20) NOT  NULL UNIQUE, 
    [AuthorImage] NVARCHAR (MAX) NOT  NULL, 
    CONSTRAINT [PK_Author] PRIMARY KEY ([AuthorID])
);


CREATE TABLE [dbo].[Books] (
    [BookID]     INT           IDENTITY (1, 1) NOT NULL,
    [BookName]   NVARCHAR (15) NOT NULL UNIQUE,
	[BookPrice]  DECIMAL           NOT NULL,
    [AuthorID]   INT           NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([BookID]),
    CONSTRAINT [FK_Books_ToTable] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Authors]([AuthorID])

);

```
```sql
USE BookStore
GO


-- Create stored procedure to insert new record to the books table
create procedure InsertBook(@bookName nvarchar(15), @bookPrice decimal(18,0), @authorID int) 
								as
	insert into dbo.Books(BookName, BookPrice, AuthorID)
	values(@bookName, @bookPrice,@authorID)
go


-- Create function: 
go
create function GetAuthorName(@firstName nvarchar(9), 
							@lastName nvarchar(10)) returns nvarchar(20) 
							as

begin
	declare @fullName nvarchar(20) 
	set @fullName = @firstName + ' ' + @lastName 
	return @fullName 
end

go
```
# DAL - Data Access Layer: 
The only layer to access directly to DB.
* new project -> Class Library(.NET Framework)
* add -> new item -> ADO.NET Entity Data Model -> next -> New Connection (insert your connection DB) -> test connection -> choose 'V' all Tables -> Finish
* In DAL -> BookStore.edmx - there are DB diagrams
* Install in `Magane NuGet Packages` : `Entity Framework`

# BOL - Bussinesss Object layer
* new project -> Class Library(.NET Framework)
* Create Class for each table in DB with columns as props. in our case : 
    1. class Book
    2. class Author
* Install in `Magane NuGet Packages` : `Entity Framework`

# BLL - Bussinesss Logic layer
* new project -> Class Library(.NET Framework)
* Add references - DAL, BOL
* create class - AuthorManager - will call DB by `using` 
* Install in `Magane NuGet Packages` : `Entity Framework`

# UIL - User Interface Layer
* new project -> ConsoleApp(.NET Framework)
* Add references - BOL, BLL
* class Program - Create list of authors and prints them to console.
* Install in `Magane NuGet Packages` : `Entity Framework`

___
* Note: every class must be `public` so any other project will be able to use it in it's file.