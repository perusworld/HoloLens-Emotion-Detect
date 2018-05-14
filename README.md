# HoloLens Emotion Detect using Microsoft Cognitive Services - Face API

## Setup
### Checkout Using Recursive
```bash
git clone https://github.com/perusworld/holoLens-emotion-detect.git --recursive
```
### Link MixedRealityToolkit-Unity
Open a command prompt, go to the root of the checked out folder
```bash
mklink /j .\Assets\HoloToolkit .\MixedRealityToolkit-Unity\Assets\HoloToolkit
```
### Open Root Folder In Unity
Expand Scene in Project and double click Emotion

### Apply Mixed Reality Project Settings
from the menu Mixed Reality Toolkit -> Configure 

### Get Face API Key
[Get Face API Key](https://azure.microsoft.com/en-us/try/cognitive-services/?api=face-api) and set it in Root -> HUD (Face API Key), if your Face API Endpoint is different; set that as well.

### Switch to UWP in File -> Build Settings.
Also click Player Settings and ensure that the following are enabled in the Publishing Settings -> Capabilities
 * Internet Client 
 * Webcam
 * Microphone

### Build As C# Project

### Deploy
Deploy to hololens (Debug, x86, Device), focus on someone and click the clicker or air tap