properties {
  $deployment_directory = $TargetDirectory;
  $OutPutDir = $OutPutDir;
}

function FileExtensionBlackList {
  return "*.cd","*.cs", "*.dll", "*.xml", "*.pdb", "*.designer.cs";  
}

function DllExtensionBlackList {
  return "UCommerce.*";  
}

function GetFilesToCopy($path){
	return Get-ChildItem $path -name -recurse -include *.* -exclude (FileExtensionBlackList);
}

function CopyFiles ($appDirectory) {
	$filesToCopy = GetFilesToCopy($OutPutDir);
	foreach($fileToCopy in $filesToCopy)
	{
		$sourceFile = $OutPutDir + "\" + $fileToCopy;
		$targetFile = $appDirectory + "\" + $fileToCopy;
		
		# Create the folder structure and empty destination file,
		New-Item -ItemType File -Path $targetFile -Force
		
		Copy-Item $sourceFile $targetFile -Force
	}
}


function GetDllesToCopy($path){
	return Get-ChildItem $path -name -recurse -include "*.dll*","*.pdb*"  -exclude (DllExtensionBlackList);
}

function CopyDllToBin ($appDirectory) {
	$filesToCopy = GetDllesToCopy($OutPutDir);

	foreach($fileToCopy in $filesToCopy)
	{
		$sourceFile = $OutPutDir + "\" + $fileToCopy;
		$targetFile = $appDirectory + "\" + $fileToCopy;	
		
		# Create the folder structure and empty destination file,
		New-Item -ItemType File -Path $targetFile -Force	
		
		Copy-Item $sourceFile $targetFile -Force
	}
}

task Run-It {
        
	write-host 'Extract app to' . $deployment_directory;
    
    #Creates app directory
    If (!(Test-Path $deployment_directory)) {
	    New-Item $deployment_directory -type directory 
    }	
	
	CopyFiles($deployment_directory);

	$pathToBinFolder = $deployment_directory + "\bin";
	CopyDllToBin($pathToBinFolder);
    
    write-host 'Extracted app to' + $deployment_directory;   
}
