﻿param(
    [int]$Year = (Get-Date).Year
)

$template = @"
namespace AdventOfCode.Solutions.Year<YEAR>.Day<DAY>
{
    class Solution : SolutionBase
    {
        public Solution() : base(<DAY>, <YEAR>, `"`") { }

        protected override string SolvePartOne()
        {
            return "";
        }

        protected override string SolvePartTwo()
        {
            return "";
        }
    }
}

"@

$newDirectory = [IO.Path]::Combine($PSScriptRoot, "..", "Year$Year")

if(!(Test-Path $newDirectory)) {
    New-Item $newDirectory -ItemType Directory | Out-Null
}

for($i = 1; $i -le 25; $i++) {
    $newFile = [IO.Path]::Combine($newDirectory, "Day$("{0:00}" -f $i)", "Solution.cs")
    if(!(Test-Path $newFile)) {
        New-Item $newFile -ItemType File -Value ($template -replace "<YEAR>", $Year -replace "<DAY>", "$("{0:00}" -f $i)") -Force | Out-Null
    }
}

Write-Host "Files Generated"
