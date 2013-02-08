CREATE PROCEDURE SecurityModule.AddSystemRole
    @SystemRoleKey              BIGINT,
    @Name                       NVARCHAR (100),
    @WellKnownName              NVARCHAR (100),
    @Description                NVARCHAR (500),
    @SystemRoleType             NVARCHAR (50),
    @SystemAccountKey           BIGINT
AS
	INSERT INTO SecurityModule.SystemRole
    (
        SystemRoleKey, 
        Name, 
        WellKnownName,
        [Description],
        SystemRoleTypeEnum,
        CreatedTimestamp, 
        CreatedBySystemAccountKey, 
        UpdatedTimestamp, 
        UpdatedBySystemAccountKey, 
        [Version]
    ) 
    VALUES 
    ( 
        @SystemRoleKey,
        @Name,
        @WellKnownName,
        @Description,
        @SystemRoleType,
        current_timestamp,
        @SystemAccountKey,
        current_timestamp,
        @SystemAccountKey,
        1
    )
