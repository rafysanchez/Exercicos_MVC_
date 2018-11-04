
CREATE PROCEDURE dbo.ImplementNamingStandard
    @SELECT_Only        BIT = 1,
    @PrimaryKeys        BIT = 1,
    @ForeignKeys        BIT = 1,
    @Indexes            BIT = 1,
    @UniqueConstraints  BIT = 1,
    @DefaultConstraints BIT = 1,
    @CheckConstraints   BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    --DECLARE @SELECT_Only        BIT = 1
    --DECLARE @PrimaryKeys        BIT = 1
    --DECLARE @ForeignKeys        BIT = 0
    --DECLARE @Indexes            BIT = 0
    --DECLARE @UniqueConstraints  BIT = 0
    --DECLARE @DefaultConstraints BIT = 0
    --DECLARE @CheckConstraints   BIT = 0

    DECLARE @TableLimit TINYINT, @ColumnLimit TINYINT;
    SELECT @TableLimit = 40, @ColumnLimit = 40;
	
	CREATE TABLE #tmp_comandos_rename([id] int identity, [name] VARCHAR(MAX), [newname] VARCHAR(MAX))

    IF @PrimaryKeys = 1
    BEGIN		
		INSERT INTO #tmp_comandos_rename
		SELECT '['+ ISNULL(s.name, 'dbo') +'].[' + kc.name + ']' + '', 'PK_'  + LEFT(REPLACE(OBJECT_NAME(kc.parent_object_id), '''', ''), @TableLimit)
        FROM sys.key_constraints kc (nolock)
		JOIN sys.schemas s (nolock) on s.schema_id = kc.schema_id
        WHERE kc.type = 'PK'
        AND kc.is_ms_shipped = 0;
    END


    IF @ForeignKeys = 1
    BEGIN
		INSERT INTO #tmp_comandos_rename
        SELECT '['+ ISNULL(s.name, 'dbo') +'].[' + REPLACE(fk.name, '''', '''''') + ']' + '' as name,
		'FK_' + LEFT(REPLACE(OBJECT_NAME(fk.parent_object_id), '''', ''), @TableLimit) + '_' + LEFT(REPLACE(OBJECT_NAME(fk.referenced_object_id), '''', ''), @TableLimit) + '' as newname
        FROM sys.foreign_keys fk
		JOIN sys.schemas s on s.schema_id = fk.schema_id
        WHERE is_ms_shipped = 0
    END


    IF (@UniqueConstraints = 1 OR @Indexes = 1)
    BEGIN		
		INSERT INTO #tmp_comandos_rename
        SELECT  		
			QUOTENAME(REPLACE(s.name, '''', '''''')) + '.'
			+ CASE is_unique_constraint WHEN 0 THEN QUOTENAME(REPLACE(OBJECT_NAME(i.[object_id]), '''', '''''')) + '.' ELSE '' END
            + QUOTENAME(REPLACE(i.name, '''', '''''')) + ''
			, '' 
			+ CASE is_unique_constraint WHEN 1 THEN 'UQ_' ELSE 'IX_'
				+ CASE is_unique WHEN 1 THEN 'U_'  ELSE '' END 
            END 
			+ CASE has_filter WHEN 1 THEN 'F_'  ELSE '' END
            + LEFT(REPLACE(OBJECT_NAME(i.[object_id]), '''', ''), @TableLimit) 
            + '_' + STUFF((SELECT '_' + LEFT(REPLACE(c.name, '''', ''), @ColumnLimit)
                FROM sys.columns AS c 
                    INNER JOIN sys.index_columns AS ic
                    ON ic.column_id = c.column_id
                    AND ic.[object_id] = c.[object_id]
                WHERE ic.[object_id] = i.[object_id] 
                AND ic.index_id = i.index_id
                AND is_included_column = 0
                ORDER BY ic.index_column_id FOR XML PATH(''), 
                TYPE).value('.', 'nvarchar(max)'), 1, 1, '') + ''
        FROM sys.indexes i
			join sys.index_columns ic on ic.index_id = i.index_id
			join sys.columns c on c.column_id = ic.column_id and c.object_id = ic.object_id
			join sys.tables t on t.object_id = c.object_id
			join sys.schemas s on s.schema_id = t.schema_id
			WHERE i.index_id > 0 AND is_primary_key = 0 AND i.type IN (1,2)
        AND OBJECTPROPERTY(i.[object_id], 'IsMsShipped') = 0;
    END


    IF @DefaultConstraints = 1
    BEGIN
		INSERT INTO #tmp_comandos_rename
        SELECT '['+ ISNULL(s.name, 'dbo') +'].' + REPLACE(dc.name, '''', '''''') + '', 'DF_'  + LEFT(REPLACE(OBJECT_NAME(dc.parent_object_id), '''',''), @TableLimit) + '_' + LEFT(REPLACE(c.name, '''', ''), @ColumnLimit) + ''
        FROM sys.default_constraints AS dc
        INNER JOIN sys.columns  c
		INNER JOIN sys.tables t on t.object_id = c.object_id
		INNER JOIN sys.schemas s on s.schema_id = t.schema_id
        ON dc.parent_object_id = c.[object_id]
        AND dc.parent_column_id = c.column_id
        AND dc.is_ms_shipped = 0;
    END


    IF @CheckConstraints = 1
    BEGIN
		INSERT INTO #tmp_comandos_rename
        SELECT '['+ ISNULL(s.name, 'dbo') +'].' + REPLACE(cc.name, '''', '''''') + '', 'CK_' + LEFT(REPLACE(OBJECT_NAME(cc.parent_object_id), '''',''), @TableLimit) + '_' + LEFT(REPLACE(c.name, '''', ''), @ColumnLimit) + ''
        FROM sys.check_constraints AS cc
        INNER JOIN sys.columns AS c
		INNER JOIN sys.tables t on t.object_id = c.object_id
		INNER JOIN sys.schemas s on s.schema_id = t.schema_id
        ON cc.parent_object_id = c.[object_id]
        AND cc.parent_column_id = c.column_id
        AND cc.is_ms_shipped = 0;
    END

	IF(EXISTS(select * from #tmp_comandos_rename))
    BEGIN		
		declare @nameAlreadyChecked VARCHAR(MAX)
		declare @nameAlreadyCheckedCount INT = 1
		
		declare @id INT
		declare @name VARCHAR(MAX)
		declare @newname VARCHAR(MAX)
		declare cur CURSOR LOCAL for
			select * from #tmp_comandos_rename ORDER BY newname

		open cur

		fetch next from cur into @id, @name, @newname
		
		while @@FETCH_STATUS = 0
		BEGIN
			IF(@nameAlreadyChecked = @newname)
			BEGIN
				SET @nameAlreadyCheckedCount = @nameAlreadyCheckedCount + 1
			END
			ELSE
			BEGIN
				SET @nameAlreadyCheckedCount = 1
			END

			IF(@nameAlreadyCheckedCount > 1)
			BEGIN
				SET @newname = @nameAlreadyChecked + '_' + CAST(@nameAlreadyCheckedCount AS VARCHAR)
				update #tmp_comandos_rename set newname = @newname where id = @id

				PRINT(@nameAlreadyChecked + ' já existe, incrementando valor ao nome')
			END

			print 'exec sp_rename @objname= ''' + @name + ''', @newname= ''' + @newname+ '''';
			
			IF @SELECT_Only = 0
			BEGIN
				BEGIN TRY				
					exec sp_rename @name, @newname;
				END TRY
				BEGIN CATCH
					PRINT('Erro: ' + CAST(@@Error AS VARCHAR) )
				END CATCH
			END

			SET @nameAlreadyChecked = @newname

			fetch next from cur into @id, @name, @newname
		END

		close cur
		deallocate cur
	END

	IF @SELECT_Only = 1
	BEGIN	
		SELECT * FROM #tmp_comandos_rename order by newname
		--SELECT * FROM (SELECT COUNT(*) as contagem, newname FROM #tmp_comandos_rename GROUP BY newname) x WHERE contagem > 1 ORDER BY contagem DESC
	END
			
	drop table #tmp_comandos_rename
END