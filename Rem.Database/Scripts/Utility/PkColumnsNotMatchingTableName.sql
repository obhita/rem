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
    (tc.constraint_type     in ('PRIMARY KEY')) and
    (tc.table_name + 'Key'  <> ccu.column_name)
  --(ccu.column_name    like '%_code%')     and
  --(tc.TABLE_NAME      like 'cd_%')