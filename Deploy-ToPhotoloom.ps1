#should see if newer version of instructions are in current directory then copy it to the WebGLBuildDynaDraw directory
Write-S3Object -BucketName photoloom.com -File ..\..\WebGLBuildDynaDraw\DynaDrawInstructions.pdf -Key dynadraw/DynaDrawInstructions.pdf
Write-S3Object -BucketName photoloom.com -File ..\..\WebGLBuildDynaDraw\index.html -Key dynadraw/index.html

Write-S3Object -BucketName photoloom.com -Folder ..\..\WebGLBuildDynaDraw\TemplateData -KeyPrefix dynadraw/TemplateData
Write-S3Object -BucketName photoloom.com -Folder ..\..\WebGLBuildDynaDraw\Build -KeyPrefix dynadraw/Build
<# br compression
$metaDataBr = @{'content-encoding' = 'br'}
$metaDataBr.Add( 'content-type', 'application/octet-stream')
$metaDataJsBr = @{'content-encoding' = 'br'}
$metaDataJsBr.Add( 'content-type', 'application/javascript')
$metaDataWasm = @{ 'content-encoding'= 'br'}
$metaDataWasm.Add( 'content-type', 'application/wasm')

Write-S3Object -BucketName photoloom.com -file ..\..\WebGLBuildDynaDraw\Build\WebGLBuildDynaDraw.data.br -Key dynadraw/Build/WebGLBuildDynaDraw.data.br -Metadata $metaDataBr
Write-S3Object -BucketName photoloom.com -file ..\..\WebGLBuildDynaDraw\Build\WebGLBuildDynaDraw.framework.js.br -Key dynadraw/Build/WebGLBuildDynaDraw.framework.js.br -Metadata $metaDataJsBr
Write-S3Object -BucketName photoloom.com -file ..\..\WebGLBuildDynaDraw\Build\WebGLBuildDynaDraw.loader.js -Key dynadraw/Build/WebGLBuildDynaDraw.loader.js
Write-S3Object -BucketName photoloom.com -file ..\..\WebGLBuildDynaDraw\Build\WebGLBuildDynaDraw.wasm.br -Key dynadraw/Build/WebGLBuildDynaDraw.wasm.br -Metadata $metaDataWasm
#>
$metaDataData = @{'content-type' = 'application/octet-stream'}
#$metaDataData.Add('content-encoding', 'br')
$metaDataJs = @{'content-type'= 'application/javascript'}
#$metaDataJs.Add('content-encoding', 'br' )
$metaDataWasm = @{ 'content-type' = 'application/wasm'}
#$metaDataWasm.Add( 'content-encoding', 'br')
Write-S3Object -BucketName photoloom.com -file ..\..\WebGLBuildDynaDraw\Build\WebGLBuildDynaDraw.data -Key dynadraw/Build/WebGLBuildDynaDraw.data -Metadata $metaDataData
Write-S3Object -BucketName photoloom.com -file ..\..\WebGLBuildDynaDraw\Build\WebGLBuildDynaDraw.framework.js -Key dynadraw/Build/WebGLBuildDynaDraw.framework.js -Metadata $metaDataJr
Write-S3Object -BucketName photoloom.com -file ..\..\WebGLBuildDynaDraw\Build\WebGLBuildDynaDraw.loader.js -Key dynadraw/Build/WebGLBuildDynaDraw.loader.js
Write-S3Object -BucketName photoloom.com -file ..\..\WebGLBuildDynaDraw\Build\WebGLBuildDynaDraw.wasm -Key dynadraw/Build/WebGLBuildDynaDraw.wasm -Metadata $metaDataWasm
