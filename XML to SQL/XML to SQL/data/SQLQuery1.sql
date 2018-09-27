use SandboxLoginTest

GO

select * from dbo.Applicants where SSN = '775-90-3560'

CREATE TABLE [dbo].Applicants(
	[ApplicantID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[SSN] [varchar](12) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Gender] [nchar](10) NOT NULL)

	GO

CREATE PROCEDURE InsertApplicant
@FirstName NVARCHAR(50),
@LastName NVARCHAR(50), 
@SSN VARCHAR(12),
@Email VARCHAR(50),
@Gender NCHAR(10),
@AppID INT OUTPUT

AS
BEGIN

IF EXISTS (Select * from dbo.Applicants WHERE SSN = @SSN)

UPDATE Applicants
SET
FirstName = @FirstName,
LastName = @LastName,
Email = @Email,
Gender = @Gender
Where SSN = @SSN



ELSE

INSERT INTO Applicants (FirstName, LastName, SSN, Email, Gender)
VALUES (@FirstName, @LastName, @SSN, @Email, @Gender)
SET @AppID = SCOPE_IDENTITY();

END 

GO




