#This build assumes the following directory structure
#
#  \build          - This is where the project build code lives
#  \src            - This folder contains the source code or solutions you want to build
#
Properties {
	$build_dir = Split-Path $psake.build_script_file	# directory of this build file
	$build_artifacts_dir = "$build_dir\bin\"
	$code_dir = "$build_dir"
    $nunit_dir = "$code_dir\packages\NUnit.Runners.2.6.1\tools"
   
    $env:Path += ";$nunit_dir"
}

FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

Task Default -Depends BuildAllAndTest

Task BuildAllAndTest -Depends Clean, Build

Task Build -Depends Clean {	
	Write-Host "Building spring-questions.sln" -ForegroundColor Green
	Exec { msbuild "$code_dir\spring-questions.sln" /t:Build /p:Configuration=Release /v:quiet } 
}

Task Clean {
	Write-Host "Cleaning spring-questions.sln" -ForegroundColor Green
	Exec { MSBuild "$code_dir\spring-questions.sln" /t:Clean /p:Configuration=Release /v:quiet } 
}

Task Test -Depends Build {
	Write-Host "Running tests for spring-questions.sln" -ForegroundColor Green
    Exec { nunit-console-x86.exe "$code_dir\spring-questions.sln" /exclude Integration }
}

Task IntegrationTest -Depends Build {
	Write-Host "Running tests for spring-questions.sln" -ForegroundColor Green
    # all assmblies are console apps (-> x86)
    Exec { nunit-console-x86.exe "$code_dir\spring-questions.sln" /config:Release }
}

