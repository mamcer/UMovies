@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  base-script.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir=%CD%\..\..
set opencover_bin=C:\root\bin\open-cover\tools\OpenCover.Console.exe
set mstest_bin=c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\MSTest.exe
set discover_test_path=Src\UMovies.Core.Test\bin\Debug\
set application_test_path=Src\UPictures.Application.Tests\bin\Debug\
set core_test_path=Src\UPictures.Core.Tests\bin\Debug\
set data_test_path=Src\UMovies.Data.Test\bin\Debug\
set opencover_xml=open-cover.xml

@REM  Shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

CD "%working_dir%\%discover_test_path%"
echo "%opencover_bin%" -register:user -target:"%mstest_bin%" -targetargs:"UMovies.Core.Test.dll" -filter:"+[*]* -[*.Tests]* -[*.Test]* -[xunit.*]* -[Microsoft.*]*" -output:"%opencover_xml%"
@if %errorlevel%  NEQ 0  goto :error

REM  Restore the command prompt and exit
@goto :success

:error
echo an error has occured: %errorLevel%
echo start time: %start_time%
echo end time: %time%
goto :finish

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

:finish
popd
set prompt=%savedPrompt%

ENDLOCAL
echo on