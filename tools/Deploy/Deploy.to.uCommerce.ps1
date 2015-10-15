Param(
    [Parameter(Mandatory=$True)]
    [string]$OutPutDir = ""
)

function GetDeploymentDirectories {
  return @(
	"C:\inetpub\U6Dev\Website",
	"c:\inetpub\U7Dev\Website",
    "C:\inetpub\SC8\Website"
  )
}

function GetAppName {
  return "SampleApp";  
}

function FileExtensionBlackList {
  return "*.cd", "*.dll", "*.xml", "*.pdb", "*.pdb", "*.designer.cs";  
}

function DllExtensionBlackList {
  return "UCommerce.*";  
}

function LocateAppFolder($deployment_directory){
	$folderName = "Apps"
	$pathToAppsFolders = (gci -path $deployment_directory -filter $foldername -Recurse).FullName
		
	foreach($pathToAppsFolder in $pathToAppsFolders)
    {
		if($pathToAppsFolder -like '*uCommerce\Apps*'){			
			return $pathToAppsFolder;
		}
    }	 
}

function GetFilesToCopy($path){
	return Get-ChildItem $path -name -recurse -include *.* -exclude (FileExtensionBlackList);
}

function CopyFiles ($appDirectory) {
	$filesToCopy = GetFilesToCopy($OutPutDir);
	foreach($fileToCopy in $filesToCopy)
	{
		$sourceFile = $OutPutDir + $fileToCopy;
		$targetFile = $appDirectory + "\" + $fileToCopy;
		
		# Create the folder structure and empty destination file,
		New-Item -ItemType File -Path $targetFile -Force
		
		Copy-Item $sourceFile $targetFile -Force
	}
}


function GetDllesToCopy($path){
	return Get-ChildItem $path -name -recurse -include *.dll* -exclude (DllExtensionBlackList);
}

function CopyDllToBin ($appDirectory) {
	$filesToCopy = GetDllesToCopy($OutPutDir);
	write-host $filesToCopy;
	foreach($fileToCopy in $filesToCopy)
	{
		$sourceFile = $OutPutDir + $fileToCopy;
		$targetFile = $appDirectory + "\" + $fileToCopy;		
		Copy-Item $sourceFile $targetFile -Force
	}
}

function Run-It () {
    try {  
		$deployment_directories = GetDeploymentDirectories;
		$appName = GetAppName;      
		
        foreach($deployment_directory in $deployment_directories)
        {
			write-host 'Deploying app to' . $deployment_directory;
			$pathToAppsFolder = LocateAppFolder($deployment_directory);

			#Creates app directory
			$appDirectory = $pathToAppsFolder + "\" + $appName;
			New-Item $appDirectory -type directory -force #-force ignore exception if the directory already exixts. 

			
			CopyFiles($appDirectory);

			#Delete this section when dlles can be load from apps folder --> (update black list)
			$pathToBinFolder = $deployment_directory + "\bin";
			CopyDllToBin($pathToBinFolder);

			write-host 'Deployed app to' + $deployment_directory;
        }	 
    } catch {  
        Write-Error $_.Exception.Message -ErrorAction Stop  
    }
}

Run-It