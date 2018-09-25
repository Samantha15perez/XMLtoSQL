use sandbox

GO

CREATE TABLE [dbo].[Applicants](
	[ApplicantID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[SSN] [varchar](12) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Gender] [nchar](10) NOT NULL)