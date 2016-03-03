properties {
    $TargetDirectory;
    $SourceDirectory;
    $DocumentationSourceDirectory;
}

Task Run-It {
    $scriptPath = GetScriptDirectory;
    Import-Module "$scriptPath\..\psake\4.3.0.0\psake.psm1"

    $DocumentationCompiler = $SourceDirectory + "\tools\Documentation\Compiler\UCommerce.DocsCompilerRunner.exe"
    $DocumentationDirectory = $SourceDirectory + "\documentation";

    #Step 01: Generate app documentation
    $GenerateDocumentationProperties = @{
      "OutputDirectory" = $DocumentationDirectory
      "InputDirectory" = $DocumentationSourceDirectory
      "DocumentationCompiler" = $DocumentationCompiler
    };

    Invoke-PSake "$ScriptPath\Generate.App.Documentation.ps1" "Run-It" -parameters $GenerateDocumentationProperties

    #Step 02: Add the documentation to the package
    $AddDocumentationToPackageProperties = @{
      "TargetDirectory" = $TargetDirectory      
      "SourceDirectory" = $DocumentationDirectory
    };

    Invoke-PSake "$ScriptPath\Add.Documentation.To.Package.ps1" "Run-It" -parameters $AddDocumentationToPackageProperties

}