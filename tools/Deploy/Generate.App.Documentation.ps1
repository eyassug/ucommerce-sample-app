properties {
  $OutputDirectory;
  $InputDirectory;
  $DocumentationCompiler;
}

. ./Documentation.Helpers.ps1

task Run-It {    
    if(GuardAgainstEmpty $InputDirectory "InputDirectory") { return }
    if(GuardAgainstEmpty $OutputDirectory "OutputDirectory") { return }
    if(GuardAgainstEmpty $DocumentationCompiler "DocumentationCompiler") { return }

    if(EnsurePathExists $DocumentationCompiler "DocumentationCompiler") { return }
    if(EnsurePathExists $InputDirectory "InputDirectory") { return }

    & $DocumentationCompiler $OutputDirectory $InputDirectory
 
}