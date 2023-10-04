/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO GAME (Title, [Resume], DateSortie)
       VALUES ('Final Fantasy 9','Best Game','2001-09-10')

INSERT INTO GAME (Title, [Resume], DateSortie)
       VALUES ('Fallout 2','Bethesda game','1995-10-22')

INSERT INTO Genre([Label])
       VALUES ('RPG') 
INSERT INTO Genre([Label])
       VALUES ('Action') 

INSERT INTO GameGenre(GameId,GenreId)
       VALUES(1,1)
INSERT INTO GameGenre(GameId,GenreId)
       VALUES(2,2)

exec UserRegister 'admin@gmail.com','Pouyette91','Admin'

exec UserRegister 'petch@gmail.com','Pouyette91','Petch'

UPDATE Users
SET RoleId = 3
Where Id = (SELECT TOP 1 Id
FROM Users)