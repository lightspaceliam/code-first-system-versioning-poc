--  Ensure migration has run.
SELECT	*
FROM	__EFMigrationsHistory
ORDER BY MigrationId;

--  Insert a single person to dbo.People.
INSERT INTO People (FirstName, LastName, Email, Created, LastModified) VALUES
	('Pete', 'Mitchell', 'pete.m@topgun.com.au', GETUTCDATE(), GETUTCDATE());

--  Update that record.
UPDATE	People
SET		Created = DATEADD(DAY, 1, GETUTCDATE())
WHERE	Id = 1

UPDATE	People
SET		Age = 24
WHERE	Id = 1

--  Query both People(System-Versioned) & the system-versioned temporal table History.People. 
SELECT	*
FROM	People

SELECT	*
FROM	History.People


