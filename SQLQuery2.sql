
--select sum(Charged-Price) from (select price,Charged from Transactions
--where month(TransactionTime) = 11) t

--select max(profit),name,province from (
--select sum(t.charged) - sum(t.price) as profit,c.name name, c.province province from transactions t

--right join customers c

--on t.customerid = c.id
--group by c.name,c.province
--) ta
--group by name,province


