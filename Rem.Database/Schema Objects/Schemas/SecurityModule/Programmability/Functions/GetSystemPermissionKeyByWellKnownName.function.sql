CREATE FUNCTION [SecurityModule].[GetSystemPermissionKeyByWellKnownName]
(
	@WellKnownName  NVARCHAR (100)
)
RETURNS BIGINT 
AS 
BEGIN 
RETURN ( SELECT SystemPermissionKey FROM SecurityModule.SystemPermission WHERE WellKnownName = @WellKnownName )
END