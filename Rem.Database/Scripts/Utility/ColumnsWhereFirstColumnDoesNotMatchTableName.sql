with 
    table_cols as
    (
        select
            tbls.table_type,
            tbls.table_catalog,
            tbls.table_schema,
            tbls.table_name,
            cols.column_name,
            cols.ordinal_position,
            cols.column_default,
            cols.is_nullable,
            cols.data_type,
            cols.character_maximum_length
        from
                    information_schema.columns  cols
            join    information_schema.tables   tbls    on  (cols.table_catalog = tbls.table_catalog)   and
                                                            (cols.table_schema  = tbls.table_schema)    and
                                                            (cols.table_name    = tbls.table_name)
        where
            (tbls.table_type    = 'BASE TABLE')
    )
select
    tcols.*
from
    table_cols  tcols
where
    (tcols.ordinal_position     = 1)                    and
    (tcols.table_name + 'Key'   <> tcols.column_name) 
order by
    tcols.table_catalog,
    tcols.table_schema,
    tcols.table_name,
    tcols.ordinal_position
    
