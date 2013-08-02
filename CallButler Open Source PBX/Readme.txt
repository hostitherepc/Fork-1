Compiler Requirements

 - Visual Studio 2005 (with SP1) or Higher



Before Compilation

The CallButler Open Source PBX relies on some COM components to be registered in order to run properly.
The following components located in the \Libraries directory must be registered for COM with regsvr32:

 - WOSI.NET.RTP.dll
 - NCT*.dll
 - ToneDecoder.dll
 - ToneEncoder.dll
 - WOSISpAudio.dll
 - WaveExCtrl.dll
 - secman.dll