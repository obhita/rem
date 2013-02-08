ALTER TABLE [SecurityModule].[SystemRole]
	ADD CONSTRAINT [SystemRole_SystemRoleType_CHK] 
	CHECK 
	(
	[SystemRoleTypeEnum] = 'JobFunction' OR 
	[SystemRoleTypeEnum] = 'Task' OR 
	[SystemRoleTypeEnum] = 'TaskGroup' 
	)