#!/usr/bin/env pwsh

<# Assuming the script is named "FileToRun.ps1" and is in the current directory issue the follwoing command:

PowerShell -ExecutionPolicy Bypass -File Start-StructurizrLite.ps1 [-HostPort <port>]

If host port is not provided 8080 will be assumed.
#>

param (
    [Int32] $HostPort = 8080
)

# Construct path to the structurizr directory
$relativeStructurizrPath = Join-Path $PSScriptRoot "../structurizr"
# Construct the url to the web interface
$structurizUrl = "http://localhost:$($HostPort)"

# Create background task to start browser after 15s delay to let the container initliaze.
Write-Host "Web UI will start in 15 seconds to give the container time to initialize"
$startBrowserAfterDelayJob = Start-Job -ScriptBlock {
    param($url)

    Start-Sleep -Seconds 15
    Start-Process $url
} -ArgumentList $structurizUrl

# Start docker container.
docker run --rm -p "$($HostPort):8080" -v "$($relativeStructurizrPath):/usr/local/structurizr" structurizr/lite

Receive-Job $startBrowserAfterDelayJob
Remove-Job $startBrowserAfterDelayJob