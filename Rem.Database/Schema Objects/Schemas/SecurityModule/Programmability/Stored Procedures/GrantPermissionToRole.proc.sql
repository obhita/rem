CREATE PROCEDURE SecurityModule.GrantPermissionToRole
    @SystemRolePermissionKey    BIGINT,
    @SystemPermissionKey        BIGINT,
    @SystemRoleKey              BIGINT,
    @SystemAccountKey           BIGINT
AS
	INSERT INTO SecurityModule.SystemRolePermission
    (
        SystemRolePermissionKey, 
        SystemRoleKey,
        SystemPermissionKey,
        CreatedTimestamp, 
        CreatedBySystemAccountKey, 
        UpdatedTimestamp, 
        UpdatedBySystemAccountKey, 
        [Version]
    ) 
    VALUES 
    ( 
        @SystemRolePermissionKey,
        @SystemRoleKey,
        @SystemPermissionKey,
        current_timestamp,
        @SystemAccountKey,
        current_timestamp,
        @SystemAccountKey,
        1
    )
