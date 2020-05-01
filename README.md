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
  Link to download http://download.3divi.com/Nuitrack/doc/Installation_page.html  
    
  Once you've downloaded navigate to where the Nuitrack File downloaded. (On my machine it automatically went to Program Files) and head over to the activation_tools and run the Nuitrack.exe  
    
   Once you have Nuitrack.exe the compatability test (NOTE: Must have Real Sense Camera Plugged or .oni installed and configured int nuitrack_config to succesfully pass). After you pass the compatability test the text box to activate license should be ungreyed and you should be able to enter in a license key (information 
### Step 2: Running executable  
Once you've downloaded Nuitrack the ISB.exe will be runnable and you can test out the code
### Installing Unity for develpoment

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

Finally, drag the two sliders from this box over to the 
## Recommendations
