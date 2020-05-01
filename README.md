# GenerativeGraphics

## Installation
### Requirements
### Hardwares Required:
Machine Capable of running Windows 10  
Intel Real Sense 415/435 Depth Cameras 
### Software Required:
Unity version 2017.4.38f1 - (required for development only)  
Nuitrack (required for client and developer)
### Step 1: Nuitrack
  [Installation Page](http://download.3divi.com/Nuitrack/doc/Installation_page.html)  
    
  Once you've downloaded navigate to where the Nuitrack File downloaded. (On my machine it automatically went to Program Files) and head over to the activation_tools and run the Nuitrack.exe  
  
  ![alt text](https://github.com/sumara523/GenerativeGraphics/blob/master/images/NuitrackExe.PNG)<br />
  
  ![alt_text](https://github.com/sumara523/GenerativeGraphics/blob/master/images/ActivationTool.PNG)<br /> 
    
   Once you find Nuitrack.exe run it and complete the compatability test (NOTE: Must have Real Sense Camera Plugged or .oni installed and configured int nuitrack_config to succesfully pass). After you pass the compatability test the text box to activate license should be ungreyed and you should be able to enter in a license key (log in [here](https://cognitive.3divi.com/app/nuitrack/login/) using account information provided by Clay and activate either a trial license (only works for 3 minutes) or the Pro License). You have to run compatibility test with camera plugged in to activate a license.<br />   
     
 ![alt text](https://github.com/sumara523/GenerativeGraphics/blob/master/images/NuitrackLogin.PNG)
 <br /> 
 [Here](https://github.com/3DiVi/nuitrack-sdk) is link to nuitrack sdk if you want to look at code examples. Should also be possible to [here](http://download.3divi.com/Nuitrack/doc/index.html) to find more info on API architecture and find Unity specific tutorials.
### Step 2: Running executable  
Once you've downloaded Nuitrack and activated the license the ISB.exe will be runnable and you will be able to see how the project looks at this point. (NOTE: If don't activate license .exe will run but skeleton will immediately freeze up. Trial License should run but has time limit. Pro License should run with no time limit)
### Installing Unity for develpoment
[Link for proper version download](https://unity3d.com/get-unity/download/archive)   
Navigate to 2017.x and get .4.38
## Unity Program Structure
### Camera
Our Unity scene must contain a Camera and a Directional Light. The 
![alt text](https://github.com/sumara523/GenerativeGraphics/blob/master/images/camera.png)
## Recommendations
