Function Show-TreeWithCsFiles {
    [CmdletBinding()]
    Param(
        [string]$TargetDirectory = (Get-Location).Path
    )

    # 1. Găsește toate fișierele .cs și directoarele lor părinte
    $csFiles = Get-ChildItem -Path $TargetDirectory -Recurse -Filter "*.cs" -File
    if (-not $csFiles) {
        Write-Host "Nu s-au găsit fișiere .cs în directorul specificat: $TargetDirectory"
        return
    }

    $csFileBaseNames = $csFiles | ForEach-Object { $_.Name } | Get-Unique

    # Colectează numele tuturor directoarelor unice care conțin fișiere .cs sau sunt părinți ai acestora
    $relevantDirectoryNames = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
    foreach ($file in $csFiles) {
        $currentDir = $file.DirectoryName
        # Adaugă toate componentele căii directorului
        $currentDir.Split([System.IO.Path]::DirectorySeparatorChar) | ForEach-Object {
            if (-not [string]::IsNullOrWhiteSpace($_)) {
                $relevantDirectoryNames.Add($_) | Out-Null
            }
        }
    }
    # Adaugă și numele directorului țintă dacă este relevant (pentru rădăcina arborelui)
    $relevantDirectoryNames.Add((Split-Path $TargetDirectory -Leaf)) | Out-Null


    # 2. Obține ieșirea comenzii tree
    Write-Host "Se generează structura arborescentă pentru: $TargetDirectory ..."
    $treeOutput = tree /f /a $TargetDirectory

    $filteredLines = [System.Collections.Generic.List[string]]::new()

    # 3. Filtrează ieșirea tree
    # Adaugă liniile de antet
    if ($treeOutput.Length -gt 0) { $filteredLines.Add($treeOutput[0]) } # Folder PATH listing
    if ($treeOutput.Length -gt 1 -and $treeOutput[1] -match "Volume serial number") { $filteredLines.Add($treeOutput[1]) }
    if ($treeOutput.Length -gt 2) { $filteredLines.Add($treeOutput[2]) } # Rădăcina, ex: C:.

    foreach ($line in $treeOutput | Select-Object -Skip 3) {
        $lineContentForMatching = $line.TrimStart(' |`+-\t') # Conținutul efectiv al liniei (nume folder/fișier)
        $isProcessed = $false

        # Verifică dacă linia este un fișier .cs
        foreach ($csBaseName in $csFileBaseNames) {
            # Verifică dacă conținutul liniei este exact numele fișierului .cs
            # sau dacă linia se termină cu numele fișierului .cs precedat de caractere de spațiere/structură
            if ($lineContentForMatching -eq $csBaseName -or $lineContentForMatching.EndsWith(" $csBaseName")) {
                $filteredLines.Add($line)
                $isProcessed = $true
                break
            }
        }

        if ($isProcessed) {
            continue
        }

        # Verifică dacă linia este un director relevant
        if ($line -match '[+\\]---') {
            $folderNameFromTree = ($line -split '[+\\]---', 2)[-1].Trim()
            if ($relevantDirectoryNames.Contains($folderNameFromTree)) {
                $filteredLines.Add($line)
            }
        }
    }

    # Afișează liniile filtrate și unice (pentru a evita duplicatele minore dacă logica se suprapune)
    # Get-Unique poate schimba ordinea dacă există linii structurale identice, dar de obicei liniile de fișiere/foldere sunt unice.
    $filteredLines | Get-Unique | ForEach-Object { Write-Host $_ }
}

# Cum se utilizează:
# 1. Salvați codul de mai sus într-un fișier .ps1 (de ex., Show-TreeCs.ps1)
# 2. Deschideți PowerShell, navigați la directorul unde ați salvat fișierul.
# 3. Rulați: .\Show-TreeCs.ps1
# Sau pentru un director specific: .\Show-TreeCs.ps1 -TargetDirectory "C:\Calea\Proiectului\Tau"