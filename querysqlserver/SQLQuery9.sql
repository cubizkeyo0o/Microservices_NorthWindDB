SELECT P.ProductID, P.ProductName,P.SupplierID, P.CategoryID, P.QuantityPerUnit,P.UnitPrice,s.CompanyName,s.ContactName,s.Address ,s.City,s.Country,s.Phone
FROM Products P
RIGHT JOIN Suppliers S ON P.SupplierID = S.SupplierID
WHERE s.CompanyName = 'Exotic Liquids'
