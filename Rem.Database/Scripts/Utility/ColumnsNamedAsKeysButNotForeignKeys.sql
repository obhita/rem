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
    ),

    pk_cols as
    (
        select
            tc.table_catalog,
            tc.table_schema,
            tc.table_name,
            tc.constraint_type,
            ccu.column_name,
            ccu.constraint_name
        from
                        information_schema.constraint_column_usage  ccu
                 join   information_schema.table_constraints        tc              on  (ccu.constraint_name        = tc.constraint_name)       and
                                                                                        (ccu.constraint_catalog     = tc.constraint_catalog)    and
                                                                                        (ccu.constraint_schema      = tc.constraint_schema)     and
                                                                                        (ccu.table_catalog          = tc.table_catalog)         and
                                                                                        (ccu.table_schema           = tc.table_schema)          and
                                                                                        (ccu.table_name             = tc.table_name)
        where
            (tc.constraint_type in ('PRIMARY KEY'))
    ),

    fk_cols as
    (
        select
            tc.table_catalog,
            tc.table_schema,
            tc.table_name,
            tc.constraint_type,
            ccu.column_name,
            rc_obj_parent.name                                                  as pk_table_name,
            kcu.column_name                                                     as pk_column_name,
            ccu.constraint_name
        from
                        information_schema.constraint_column_usage  ccu
                 join   information_schema.table_constraints        tc              on  (ccu.constraint_name        = tc.constraint_name)       and
                                                                                        (ccu.constraint_catalog     = tc.constraint_catalog)    and
                                                                                        (ccu.constraint_schema      = tc.constraint_schema)     and
                                                                                        (ccu.table_catalog          = tc.table_catalog)         and
                                                                                        (ccu.table_schema           = tc.table_schema)          and
                                                                                        (ccu.table_name             = tc.table_name)
            left join   information_schema.referential_constraints  rc              on  (ccu.constraint_name        = rc.constraint_name)       and
                                                                                        (ccu.constraint_catalog     = rc.constraint_catalog)    and
                                                                                        (ccu.constraint_schema      = rc.constraint_schema)
            left join   sysobjects                                  rc_obj          on  (rc.unique_constraint_name  = rc_obj.name)
            left join   sysobjects                                  rc_obj_parent   on  (rc_obj.parent_obj          = rc_obj_parent.id)
            left join   information_schema.key_column_usage         kcu             on  (ccu.constraint_catalog     = kcu.constraint_catalog)   and
                                                                                        (ccu.table_catalog          = kcu.table_catalog)        and
                                                                                        (rc_obj_parent.name         = kcu.table_name)           and
                                                                                        (rc.unique_constraint_name  = kcu.constraint_name)
        where
            (tc.constraint_type in ('FOREIGN KEY'))
    ),
    chk_cols as
    (
        select
            ccu.table_catalog,
            ccu.table_schema,
            ccu.table_name,
            ccu.column_name,
            cc.constraint_catalog,
            cc.constraint_schema,
            cc.constraint_name,
            cc.check_clause
        from
                        information_schema.constraint_column_usage  ccu
                 join   information_schema.check_constraints        cc  on  (ccu.constraint_name    = cc.constraint_name)       and
                                                                            (ccu.constraint_catalog = cc.constraint_catalog)    and
                                                                            (ccu.constraint_schema  = cc.constraint_schema)
    )
select
    tcols.*,
    fkcols.pk_table_name,
    fkcols.pk_column_name,
    chkcols.constraint_name,
    chkcols.check_clause,
    pkcols.*
from
                table_cols  tcols
    left join   pk_cols     pkcols  on  (tcols.table_catalog    = pkcols.table_catalog)     and
                                        (tcols.table_schema     = pkcols.table_schema)      and
                                        (tcols.table_name       = pkcols.table_name)        and
                                        (tcols.column_name      = pkcols.column_name)                
    left join   fk_cols     fkcols  on  (tcols.table_catalog    = fkcols.table_catalog)     and
                                        (tcols.table_schema     = fkcols.table_schema)      and
                                        (tcols.table_name       = fkcols.table_name)        and
                                        (tcols.column_name      = fkcols.column_name)
    left join   chk_cols    chkcols on  (tcols.table_catalog    = chkcols.table_catalog)    and
                                        (tcols.table_schema     = chkcols.table_schema)     and
                                        (tcols.table_name       = chkcols.table_name)       and
                                        (tcols.column_name      = chkcols.column_name)
where
    (tcols.column_name      like '%Key')    and
    (pkcols.constraint_type is null)        and
    (fkcols.pk_column_name  is null)
order by
    tcols.table_catalog,
    tcols.table_schema,
    tcols.table_name,
    tcols.ordinal_position   