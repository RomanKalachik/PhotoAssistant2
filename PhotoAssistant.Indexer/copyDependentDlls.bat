SET SourcePath=%3
FOR /F %%F IN (%2) DO (
	xcopy %SourcePath%%%F %1  /y /r
)