rd /S /Q lib
rd /S /Q tools
rd /S /Q content
mkdir lib
mkdir tools
mkdir content
mkdir content\EasyPlug\Css\Images\Grid
mkdir content\EasyPlug\Css\Images\Icons
copy ..\Css content\EasyPlug\Css
copy ..\Css\Images content\EasyPlug\Css\Images
copy ..\Css\Images\Grid content\EasyPlug\Css\Images\Grid
copy ..\Css\Images\Icons content\EasyPlug\Css\Images\Icons
copy ..\ content\EasyPlug

nuget pack EasyFrameWork.Web.UI.nuspec -Exclude *.cmd

nuget push EasyFrameWork.Web.UI.*.nupkg
@pause