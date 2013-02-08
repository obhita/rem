CREATE PROCEDURE SecurityModule.GrantRoleRelationship
	@SystemRoleRelationshipKey  BIGINT,
    @GrantedSystemRoleKey       BIGINT,
    @OwnerSystemRoleKey         BIGINT,
    @SystemAccountKey           BIGINT
AS
	INSERT INTO SecurityModule.SystemRoleRelationship
    (
        SystemRoleRelationshipKey,
        GrantedSystemRoleKey,
        SystemRoleKey,
        CreatedTimestamp,
        CreatedBySystemAccountKey,
        UpdatedTimestamp,
        UpdatedBySystemAccountKey,
        [Version]
    )
    VALUES
    (
        @SystemRoleRelationshipKey,
        @GrantedSystemRoleKey,
        @OwnerSystemRoleKey,
        CURRENT_TIMESTAMP,
        @SystemAccountKey,
        CURRENT_TIMESTAMP,
        @SystemAccountKey,
        1
    )
