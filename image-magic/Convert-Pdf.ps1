
$userFolder = $ENV:UserProfile
$pdfPath  = $userFolder + "\Google Drive"
$pdfs = Get-ChildItem ("$pdfPath\*") -Filter *.pdf

$outputDirectory = "$userFolder\OneDrive\Desktop\converted-pdfs"

# Replace specific path as needed
$magickExePath = "C:\Program Files\ImageMagick-7.0.10-Q16-HDRI\magick.exe"

ForEach($pdf in $pdfs) {
    Write-Host $pdf
    $pdfQuoted = '"' + $pdf + '"'
	$outputFile = Split-Path $pdf.Basename -leaf
	$outputFile = $outputFile + '.jpg'
	
	$pdfPath = Join-Path -Path $outputDirectory -ChildPath $outputFile
	$pdfPath = '"' + $pdfPath + '"'

	$arguments = 'convert','-density','200',$pdfQuoted,'-resize', '25%', $pdfPath
	& $magickExePath $arguments
}