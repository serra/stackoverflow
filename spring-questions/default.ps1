#This build assumes the following directory structure
#
#  \build          - This is where the project build code lives
#  \BuildArtifacts - This folder is created if it is missing and contains output of the build
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

Task BuildAllAndTest -Depends Clean, Build, Test

Task Build -Depends Clean {	
	Write-Host "Building spring-questions.sln" -ForegroundColor Green
	Exec { msbuild "$code_dir\spring-questions.sln" /t:Build /p:Configuration=Release /v:quiet /p:OutDir=$build_artifacts_dir } 
}

Task Clean {
	Write-Host "Creating BuildArtifacts directory" -ForegroundColor Green
	# Write-Host $build_dir
    # Write-Host $build_artifacts_dir -ForegroundColor Green
	
    if (Test-Path $build_artifacts_dir) 
	{	
		rd $build_artifacts_dir -rec -force | out-null
	}
	
	mkdir $build_artifacts_dir | out-null
	
	Write-Host "Cleaning spring-questions.sln" -ForegroundColor Green
	Exec { MSBuild "$code_dir\spring-questions.sln" /t:Clean /p:Configuration=Release /v:quiet } 
}

Task Test -Depends Build {
	Write-Host "Running tests for spring-questions.sln" -ForegroundColor Green
    Exec { nunit-console.exe "$code_dir\spring-questions.sln" /exclude Integration }
}

Task IntegrationTest -Depends Build {
	Write-Host "Running tests for spring-questions.sln" -ForegroundColor Green
    Exec { nunit-console.exe "$code_dir\spring-questions.sln" }
}

