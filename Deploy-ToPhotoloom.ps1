#should see if newer version of instructions are in current directory then copy it to the WebGLBuildDynaDraw directory
Copy-Item '.\DynaDrawInstructions.pdf' .\WebGLBuild

Write-S3Object -BucketName photoloom.com -File .\WebGLBuild\DynaDrawInstructions.pdf -Key dynadraw/DynaDrawInstructions.pdf
Write-S3Object -BucketName photoloom.com -File .\WebGLBuild\index.html -Key dynadraw/index.html

Write-S3Object -BucketName photoloom.com -Folder .\WebGLBuild\TemplateData -KeyPrefix dynadraw/TemplateData
Write-S3Object -BucketName photoloom.com -Folder .\WebGLBuild\Build -KeyPrefix dynadraw/Build
<# br compression 
$metaDataBr = @{'content-encoding' = 'br'}
$metaDataBr.Add( 'content-type', 'application/octet-stream')
$metaDataJsBr = @{'content-encoding' = 'br'}
$metaDataJsBr.Add( 'content-type', 'application/javascript')
$metaDataWasm = @{ 'content-encoding'= 'br'}
$metaDataWasm.Add( 'content-type', 'application/wasm')

Write-S3Object -BucketName photoloom.com -file .\WebGLBuild\Build\WebGLBuild.data.br -Key dynadraw/Build/WebGLBuild.data.br -Metadata $metaDataBr
Write-S3Object -BucketName photoloom.com -file .\WebGLBuild\Build\WebGLBuild.framework.js.br -Key dynadraw/Build/WebGLBuild.framework.js.br -Metadata $metaDataJsBr
Write-S3Object -BucketName photoloom.com -file .\WebGLBuild\Build\WebGLBuild.loader.js -Key dynadraw/Build/WebGLBuild.loader.js
Write-S3Object -BucketName photoloom.com -file .\WebGLBuild\Build\WebGLBuild.wasm.br -Key dynadraw/Build/WebGLBuild.wasm.br -Metadata $metaDataWasm
#>
<# no compression #>
$metaDataData = @{'content-type' = 'application/octet-stream'}
#$metaDataData.Add('content-encoding', 'br')
$metaDataJs = @{'content-type'= 'application/javascript'}
#$metaDataJs.Add('content-encoding', 'br' )
$metaDataWasm = @{ 'content-type' = 'application/wasm'}
#$metaDataWasm.Add( 'content-encoding', 'br')
Write-S3Object -BucketName photoloom.com -file .\WebGLBuild\Build\WebGLBuild.data -Key dynadraw/Build/WebGLBuild.data -Metadata $metaDataData
Write-S3Object -BucketName photoloom.com -file .\WebGLBuild\Build\WebGLBuild.framework.js -Key dynadraw/Build/WebGLBuild.framework.js -Metadata $metaDataJr
Write-S3Object -BucketName photoloom.com -file .\WebGLBuild\Build\WebGLBuild.loader.js -Key dynadraw/Build/WebGLBuild.loader.js
Write-S3Object -BucketName photoloom.com -file .\WebGLBuild\Build\WebGLBuild.wasm -Key dynadraw/Build/WebGLBuild.wasm -Metadata $metaDataWasm
