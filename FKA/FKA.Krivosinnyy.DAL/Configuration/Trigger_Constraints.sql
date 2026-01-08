--выборка для проверки
SELECT --TOP (1000) [Id]
      [PersonId]
      --,[Relation]
      ,[RelatedPersonId]
	  ,cast(PersonId as varchar) + ',' + cast(RelatedPersonId as varchar)  pers_relpers
	  ,cast(RelatedPersonId as varchar) + ',' + cast(PersonId as varchar)  relpers_pers
	  ,case when cast(PersonId as varchar) + ',' + cast(RelatedPersonId as varchar)  
					<> cast(RelatedPersonId as varchar) + ',' + cast(PersonId as varchar)  
			then 'OK'
			else 'FAIL'
		end is_ok
  FROM [Krivosinnyy].[dbo].[Relationship]

  alter table [Krivosinnyy].[dbo].[Relationship]
  add constraint chk_Not_Myself check ( PersonId <> RelatedPersonId )

  alter table [Krivosinnyy].[dbo].[Relationship]
  add constraint chk_unique_Persons_Relation unique( PersonId, RelatedPersonId )


  use Krivosinnyy

  --drop trigger trg_unique_Persons_Relation_and_Reverse
  create or alter trigger trg_unique_Persons_Relation_and_Reverse
  on [Krivosinnyy].[dbo].[Relationship]
  after insert, update
  as 
  begin
	SET NOCOUNT ON;

	declare @m  int = 0;

	select @m = max(s.p) 
	from (
		select count(*) over(partition by t.p) p
		from
		(
			select cast(PersonId as varchar) + ',' + cast(RelatedPersonId as varchar) p
			FROM [Krivosinnyy].[dbo].[Relationship]
			union all
			select cast(RelatedPersonId as varchar) + ',' + cast(PersonId as varchar) p
			FROM [Krivosinnyy].[dbo].[Relationship]
		) t
	) s;
	--print 'rels count: ' + @m;

	if( @m > 1) 
	BEGIN
        RAISERROR('Person - RelPerson and RelPerson-Person Should be Unique', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
  end;

  insert into Relationship(PersonId,Relation,RelatedPersonId)
	values(1007,1,2)

drop table m

SELECT  
    o.name AS object_name,
    o.type_desc,
    s.name AS schema_name,
	o.type,
    CASE o.type
        WHEN 'U' THEN 'Таблица'
        WHEN 'V' THEN 'Представление'
        WHEN 'P' THEN 'Процедура'
        WHEN 'FN' THEN 'Функция'
        WHEN 'TR' THEN 'Триггер'
        WHEN 'PK' THEN 'Первичный ключ'
        WHEN 'F' THEN 'Внешний ключ'
        WHEN 'D' THEN 'Ограничение DEFAULT'
        WHEN 'C' THEN 'CHECK ограничение'
        WHEN 'IT' THEN 'Внутренняя таблица'
        ELSE o.type
    END AS object_type,
    o.create_date
FROM sys.objects o
JOIN sys.schemas s ON o.schema_id = s.schema_id
WHERE o.name = 'm'
ORDER BY o.type, s.name, o.name;

  	select max(m.p) p
	from (
		select count(*) over(partition by t.p) p
		from
		(
			select cast(PersonId as varchar) + ',' + cast(RelatedPersonId as varchar) p
			FROM [Krivosinnyy].[dbo].[Relationship]
			union all
			select cast(RelatedPersonId as varchar) + ',' + cast(PersonId as varchar) p
			FROM [Krivosinnyy].[dbo].[Relationship]
		) t
	) m;
