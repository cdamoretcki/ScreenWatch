CREATE TABLE [dbo].[ToneTrigger]
(
	id UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
	userName VARCHAR(256) NOT NULL,
	lowerColorBound INT NOT NULL,
	upperColorBound INT NOT NULL,
	sensitivity INT NOT NULL
)
