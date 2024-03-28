SELECT P.ProductID, P.ProductName, S.CompanyName, C.CategoryName, P.QuantityPerUnit, P.UnitsInStock
FROM Products P 
JOIN Suppliers S
ON P.SupplierID = S.SupplierID
JOIN Categories C
ON C.CategoryID = P.CategoryID
WHERE P.ProductID = 3