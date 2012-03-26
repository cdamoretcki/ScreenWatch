
sp_configure 'filestream access level', 2
RECONFIGURE WITH OVERRIDE

ALTER database ScreenWatch
ADD FILEGROUP FileStreamGroup_ScreenShot
CONTAINS FILESTREAM
GO
 
--Add a file for storing database photos to FILEGROUP
ALTER database ScreenWatch
ADD FILE
(
    NAME= 'FileStreamGroup_ScreenShot',
    FILENAME = 'C:\ScreenWatch\FileStreamGroup_ScreenShot'
)
TO FILEGROUP FileStreamGroup_ScreenShot
GO

CREATE TABLE [dbo].[ScreenShot]
(
	id UNIQUEIDENTIFIER ROWGUIDCOL PRIMARY KEY NOT NULL,
	userName VARCHAR(256) NOT NULL,
	timeStamp DATETIME NOT NULL,
	image VARBINARY(MAX) FILESTREAM NOT NULL
)

ALTER TABLE dbo.ScreenShot
  SET (FILESTREAM_ON = FileStreamGroup_ScreenShot)
  GO
  
  CREATE TABLE [dbo].[TextTrigger]
(
	id UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
	userName VARCHAR(256) NOT NULL,
	triggerString VARCHAR(2048) NOT NULL
)
GO

CREATE TABLE [dbo].[ToneTrigger]
(
	id UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
	userName VARCHAR(256) NOT NULL,
	lowerColorBound INT NOT NULL,
	upperColorBound INT NOT NULL,
	sensitivity INT NOT NULL
)
GO

CREATE TABLE [dbo].[User]
(
	userName VARCHAR(256) PRIMARY KEY,
	email VARCHAR(256) NOT NULL,
	isMonitored BIT NOT NULL,
	isAdmin BIT NOT NULL 
)
GO