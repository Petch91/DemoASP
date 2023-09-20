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
INSERT INTO GAME (Title, [Resume], DateSortie, Genre)
       VALUES ('Final Fantasy 9','Best Game','2001-09-10','rpg')

INSERT INTO GAME (Title, [Resume], DateSortie, Genre)
       VALUES ('Fallout 2','Bethesda game','1995-10-22','aventure')