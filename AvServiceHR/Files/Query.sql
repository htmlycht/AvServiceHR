


-----------------------
-- FirstName di persona della tabella PERSON che hanno piu di 50 occorrenze


SELECT FirstName, COUNT(*) AS Occurrences
FROM Person.Person
GROUP BY FirstName
HAVING COUNT(*) > 50;


-------------------------------
----Sulla base della prima query trova in FirstName con piu' occorrenze 

SELECT TOP 1 FirstName, COUNT(*) AS Occurrences
FROM Person.Person
GROUP BY FirstName
ORDER BY COUNT(*) DESC;


--------------------------------------------------
----Per ogni Regione americana il numero di occorrenze del FirstName 'Richard' 

SELECT sp.Name AS Region, COUNT(*) AS Occurrences
FROM Person.Person p
JOIN Person.BusinessEntityAddress bea ON p.BusinessEntityID = bea.BusinessEntityID
JOIN Person.Address a ON bea.AddressID = a.AddressID
JOIN Person.StateProvince sp ON a.StateProvinceID = sp.StateProvinceID
WHERE p.FirstName = 'Richard'
GROUP BY sp.Name
ORDER BY RichardOccurrences DESC;