echo off

set baseJobDir=%1
set expectedFile=%2
set inputFile=%3
set testName=%4
set logLevel=Basic

%PDI_PATH%\kitchen.bat /param:base.job.dir=%baseJobDir% /param:expected.file=%expectedFile% /param:input.file=%inputFile% /param:test.name=%testName% /file:%baseJobDir%/repository/test/TestBddCaseRunner.kjb /level:%logLevel%