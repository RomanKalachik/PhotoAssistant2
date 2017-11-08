SET SourcePath=C:\2017.2\Bin\Framework4\
FOR /F %%F IN (%2) DO (
	xcopy %SourcePath%%%F %1  /y /r
)