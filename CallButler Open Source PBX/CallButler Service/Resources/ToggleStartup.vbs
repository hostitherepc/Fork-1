Function AddToStartup()
  Dim objShell 
  Set objShell = CreateObject("WScript.Shell") 
  
  Dim pathToKey, productName, appLocation

  pathToKey =  "HKLM\Software\Microsoft\Windows\CurrentVersion\Run\" 

  appLocation = Session.Property("APPDIR") + "Service\CallButler Service.exe -a"

  productName = Session.Property("ProductName")

  objShell.RegWrite pathToKey + productName, appLocation
End Function 

Function RemoveFromStartup()
  Dim objShell 
  Set objShell = CreateObject("WScript.Shell") 
  
  Dim pathToKey, productName

  pathToKey =  "HKLM\Software\Microsoft\Windows\CurrentVersion\Run\"

  productName = Session.Property("ProductName")
  
  objShell.RegDelete pathToKey + productName

End Function
 


 