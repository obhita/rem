CREATE PROCEDURE SecurityModule.AddSystemPermission
    @SystemPermissionKey        BIGINT,
    @WellKnownName              NVARCHAR (100),
    @DisplayName                NVARCHAR (100),
    @Description                NVARCHAR (500),
    @SystemAccountKey           BIGINT
AS
    INSERT INTO SecurityModule.SystemPermission 
    ( 
        SystemPermissionKey,
        WellKnownName, 
        DisplayName,
        [Description], 
        CreatedTimestamp, 
        CreatedBySystemAccountKey, 
        UpdatedTimestamp, 
        UpdatedBySystemAccountKey, 
        [Version]
    ) 
    VALUES 
    (
        @SystemPermissionKey,
        @WellKnownName,
        @DisplayName,
        @Description,
        CURRENT_TIMESTAMP,
        @SystemAccountKey,
        CURRENT_TIMESTAMP,
        @SystemAccountKey,
        1
    )
