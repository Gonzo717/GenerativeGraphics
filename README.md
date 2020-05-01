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
    
   Once you find Nuitrack.exe run it and complete the compatability test (NOTE: Must have Real Sense Camera Plugged or .oni installed and configured in nuitrack_config file to succesfully pass). After you pass the compatability test the text box to activate license should be ungreyed and you should be able to enter in a license key (log in [here](https://cognitive.3divi.com/app/nuitrack/login/) using account information provided by Clay and activate either a trial license (only works for 3 minutes) or the Pro License). You have to run compatibility test with camera plugged in to activate a license.<br />   
     
 ![alt text](https://github.com/sumara523/GenerativeGraphics/blob/master/images/NuitrackLogin.PNG)
 <br /> 
 [Here](https://github.com/3DiVi/nuitrack-sdk) is link to nuitrack sdk repo if you want to look at code examples. Should also be possible  [here](http://download.3divi.com/Nuitrack/doc/index.html) to find more info on API architecture and find Unity specific tutorials.  
 
 We imported the Nuitrack Unity Package found [here](https://github.com/3DiVi/nuitrack-sdk/tree/master/Unity3D) in the aboce mentioned repo and imported it as an asset in the unity project to access the api but you shouldn't have to as it should be included in the New Unity Project.  
 
### Ease Development 
If you don't want to have to connect the camera every time you make a small change and want to see it  you can download a .oni file or use the one provided in the repo and add the file path of the oni file to the nuitrack_config file under the OpenNI in the FileRecord Section
![alt text](https://github.com/sumara523/GenerativeGraphics/blob/master/images/ConfigLocation.PNG)
![alt text](https://github.com/sumara523/GenerativeGraphics/blob/master/images/NuitrackConfig.PNG)  

and now Unity will run the video contained in the .oni file instead of a live capture from the depth camera.  
Some things to look out for is if you copy the file path(make sure to place between quotation marks) you may have to flip the slashes in order for it to work. Using the .oni file also only works if you have the trial license applied but for development it shoudn't be a problem, if the time runs up you can just close the Unity program and run again.
### Step 2: Running executable  
Once you've downloaded Nuitrack and activated the license the ISB.exe will be runnable and you will be able to see how the project looks at this point. (NOTE: If don't activate license .exe will run but skeleton will immediately freeze up. Trial License should run but has time limit. Pro License should run with no time limit)
### Installing Unity for develpoment
[Link for proper version download](https://unity3d.com/get-unity/download/archive)   
Navigate to 2017.x and get .4.38  
Once you have Unity you can download the New Unity Project and open it up in Unity and the correct scene should be loaded in and if not the scene you are looking to load in is Generative Graphics.
## Unity Program Structure
### Camera and Directional Light
Our Unity scene must contain a Camera and a Directional Light. The default settings should be okay, except the background on the camera should be changed to black.
![alt text](https://github.com/sumara523/GenerativeGraphics/blob/master/images/camera.png)

### Nuitrack Scripts
The default values should be okay here as well, though all we need is skeletal tracking for this project.

### GameObject
This 'GameObject' contains all of the different objects that will be used in the program. On the right side, each of the fields should be set as follows:
![alt text](https://github.com/sumara523/GenerativeGraphics/blob/bens_readme/images/game_objects.png)
#### Type Joint
In our program we use 16 joints. Set the size to 16, and ensure to set the elements in the correct order as shown here, as these indexes are used in the code.
For the remaining objects, under the "project" tab on the lower part of the screen, open up assets -> Generative Graphics and you should see the following objects.
![alt text](https://github.com/sumara523/GenerativeGraphics/blob/bens_readme/images/assets.png)
Drag Particle_System_1 from this box over to the right into both the "Prefab Joint" and "Prefab Connection" fields as shown under the list of joints. This will render the particle system we have created on each of the joints and connections that we create in the code.

Next, drag the particle system called "Particle System" to the field "Prefab Updward" as shown

Finally, drag the two sliders from this box over to their respective prefab fields. "Emission Slider" goes into the "Emission" field and "Emission Upward Slider" goes into "Emission Slider"

