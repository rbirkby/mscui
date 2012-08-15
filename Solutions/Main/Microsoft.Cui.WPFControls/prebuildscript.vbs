wscript.echo "------------------------------------"
Wscript.echo "Starting assembly name rewrite"
wscript.echo "------------------------------------"

Const ForReading = 1
Const ForWriting = 2

filePath = Wscript.Arguments(0) + "Themes\Generic.xaml"

wscript.echo "------------------------------------"
wscript.echo "FilePath"
Wscript.echo filePath
Wscript.echo "------------------------------------"

Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFile = objFSO.OpenTextFile(filePath, ForReading)

strText = objFile.ReadAll
objFile.Close

strNewText = Replace(strText, "<ResourceDictionary Source=""/Microsoft.Cui.Controls;component", "<ResourceDictionary Source=""/Microsoft.Cui.WPFControls;component")

Set objFile = objFSO.OpenTextFile(filePath, ForWriting)
objFile.WriteLine strNewText
objFile.Close

wscript.echo "------------------------------------"
Wscript.echo "Finished assembly name rewrite"
wscript.echo "------------------------------------"