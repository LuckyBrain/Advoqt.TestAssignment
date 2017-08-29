USE [master]
GO
CREATE DATABASE [AdvoqtTestAssignment]
GO
USE [AdvoqtTestAssignment]
GO
CREATE TABLE [dbo].[City](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ImageUrlSuffix] [nvarchar](256) NOT NULL,
	[ImageDescription] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT dbo.City
	(Id, Name, ImageUrlSuffix, ImageDescription)
VALUES
	(2643743, 'London, UK', '172599/big-ben.png', 'Big Ben'),
	(5128581, 'New York, US', '241326/statue-of-liberty-flag', 'Statue of Liberty Flag'),
	(4930956, 'Boston, US', '177284/usa-stripe-flag-boston-strong', 'USA Stripe Flag - Boston Strong'),
	(5391959, 'San Francisco, US', '177074/golden-gate-bridge-by-night', 'Golden Gate Bridge by Night')
GO
