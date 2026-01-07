//выборка для проверки
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

  create trigger trg_unique_Persons_Relation_and_Reverse
  on [Krivosinnyy].[dbo].[Relationship]
  after insert, update
  as 
  begin
	SET NOCOUNT ON;

	declare @m  int = 0;

	select max(m.p) p
	into m
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
	if( @m > 1) 
	BEGIN
        RAISERROR('Person - RelPerson and RelPerson-Person Should be Unique', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
  end;
