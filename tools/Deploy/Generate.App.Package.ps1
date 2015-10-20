[CmdletBinding()]
Param(
    [Parameter(Mandatory=$False)]
    [string]$TargetDirectory = "C:\tmp\SampleApp",
    
    [Parameter(Mandatory=$False)]
    [string]$OutPutDir = "C:\Projects\uCommerce Sample App\src\SampleApp.Web\bin"
)

function GetScriptDirectory { 
    Split-Path -parent $PSCommandPath 
}

function MoveNuspecFile {
    $scriptPath = GetScriptDirectory;
    $nugetPath = $scriptPath + "\..\NuGet"

    Copy-Item -Path $nugetPath\App.Manifest.nuspec -Destination $TargetDirectory
}

function GetSolutionFile { 
   $scriptPath = GetScriptDirectory;
   $srcFolder = "$scriptPath\..\..\src";
   return Get-ChildItem -Path $srcFolder -Filter *.sln -Recurse;
}


function Run-It () {
    try {  
        $scriptPath = GetScriptDirectory;
        Import-Module "$scriptPath\..\psake\4.3.0.0\psake.psm1"
                   
        #Step 01 rebuild solution
        $SolutionFile = GetSolutionFile;
        $rebuildProperties = @{
          "Solution_file" = $SolutionFile;
          "srcDir" = Resolve-Path "$scriptPath\..\..\src";
        };

        Invoke-PSake "$ScriptPath\Rebuild.App.Solution.ps1" "Rebuild" -parameters $rebuildProperties

        #Step 02 Extract files
        $extractProperties = @{
          "TargetDirectory" = $TargetDirectory + "\Content";
          "OutPutDir" = $OutPutDir;
        };

        Invoke-PSake "$ScriptPath\Extract.Files.For.App.ps1" "Run-It" -parameters $extractProperties
   
        #Step 03 bin to ..\lib
        $pathToTargetBinDir = $TargetDirectory+ "\Content\bin"
        $pathToTargetLibDir = $TargetDirectory+ "\lib\net400"
        
        New-Item $pathToTargetLibDir -type directory
        Move-Item $pathToTargetBinDir\*.dll $pathToTargetLibDir
        Remove-Item $pathToTargetBinDir

        #Step 04 pack it up
        MoveNuspecFile;
        $nuget = $scriptPath + "\..\NuGet";
        $nuspecFilePath = $TargetDirectory + "\App.Manifest.nuspec";

 	    & "$nuget\nuget.exe" pack $nuspecFilePath -OutputDirectory $TargetDirectory;

        #Step 05 remove/delete files. 
        Remove-Item $TargetDirectory\* -exclude *.nupkg -recurse
    } catch {  
        Write-Error $_.Exception.Message -ErrorAction Stop  
    }
}

Run-It