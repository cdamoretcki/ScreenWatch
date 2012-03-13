CREATE TABLE [dbo].[User]
(
	userName VARCHAR(256) PRIMARY KEY,
	email VARCHAR(256) NOT NULL,
	isMonitored BIT NOT NULL,
	isAdmin BIT NOT NULL 
)
