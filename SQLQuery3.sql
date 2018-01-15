

--select top (1) p.name as productName,c.Name as categoryName , sum(p.Charged)- sum(p.Price) profit from Products p
--inner join TransactionProducts tp
--on p.Id = tp.ProductId
--inner join Categories c
--on p.CategoryId = c.Id
--group by p.name,c.Name
--order by profit  DESC

--select top(1) p.brand,p.Type,p.Price,p.ExpectedTime,
--Count(p.brand) as transactionTimes from Transactions t
--inner join Posts p
--on t.PostId = p.Id
--group by p.brand,p.Type,p.Price,p.ExpectedTime
--order by transactionTimes DESC

--select  top(1) v.id visit,v.Shop shop,CONVERT (date, v.StartedTime) date
--,datediff(minute,v.StartedTime,v.FinishedTime) timeElapsed,sum(t.charged) - sum(t.price) profit
--from Transactions t
--inner join Visits v
--on t.VisitId = v.Id
--group by v.Id,shop,v.FinishedTime,v.StartedTime

--select sum(t.charged - t.price) profit from Transactions t

--select Count(v.Id) visitTimes from Visits v

select Count(t.Id) transactionTimes from Transactions t







