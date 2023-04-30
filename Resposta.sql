--Questão 4
SELECT assunto, ano, COUNT(*) quantidade from atendimentos 
GROUP BY assunto, ano
HAVING COUNT(*) > 3 
ORDER BY ano DESC