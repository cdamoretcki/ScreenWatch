﻿CREATE TABLE [dbo].[TextTrigger]
(
	id UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
	matchThreshold INT NOT NULL,
	matchType VARCHAR(20) NOT NULL,
	tokenString VARCHAR(2048) NOT NULL
)
