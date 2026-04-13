param(
    [string]$mode = "all"
)

Write-Host "Test mode: $mode" -ForegroundColor Cyan

Write-Host "Starting TaskManagerApi..." -ForegroundColor Cyan

# Start API in background
$apiProcess = Start-Process "dotnet" -ArgumentList "run --project .\TaskManagerApi\TaskManagerApi.csproj" -PassThru

# URL to check
$apiUrl = "https://localhost:7238/swagger/index.html"

# Wait for API to become available
$maxAttempts = 30
$attempt = 0

Write-Host "Waiting for API to start..." -ForegroundColor Yellow

while ($attempt -lt $maxAttempts) {
    try {
        Invoke-WebRequest -Uri $apiUrl -UseBasicParsing -TimeoutSec 2 | Out-Null
        Write-Host "API is running!" -ForegroundColor Green
        break
    }
    catch {
        Start-Sleep -Seconds 1
        $attempt++
    }
}

if ($attempt -eq $maxAttempts) {
    Write-Host "API failed to start after waiting." -ForegroundColor Red
    Stop-Process -Id $apiProcess.Id -Force
    exit 1
}

Write-Host "Running TestRunner..." -ForegroundColor Cyan

dotnet run --project .\TestRunner\TestRunner.csproj -- $mode

Write-Host "Stopping API..." -ForegroundColor Yellow
Stop-Process -Id $apiProcess.Id -Force

Write-Host "Done." -ForegroundColor Green
