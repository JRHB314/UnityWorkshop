# Walk Animation

### Importing your character spritesheet

The first thing is to choose a spritesheet. You can use any of the spritesheets in this repo's Sprites folder, or anything else that follows the "LPC" format. For other size spritesheets these instructions would be slightly different. <br>
<br>
Go to the Projects pane, right click and create folder "Characters". Double click to open the folder in the pane. <br>
<br>
Drag and drop spritesheet of choice into the folder, then click on it to open up Inspector view. <br>
<br>
Make the following changes:
* Change sprite mode from Single to Multiple. This is to tell Unity this is a spritesheet with multiple sprites within it.
* Change Pixels per Unit to 32. This indicates the height and width of a single “cell” or tile. 
* Go to advanced and change Filter Mode to `Point (no filter)`. This is important for pixel art! Otherwise it will appear blurry.

### Slicing spritesheet
Now open Sprite editor. You may have to hit `Apply` to `Import Changes`. <br>
In the top bar of the new window hit Slice. We want to cut up our spritesheet into individual sprites. <br>
Choose Type `Grid by Cell Count`. This will have Unity automatically slice based on how many sprites are on the sheet, rather than having to enter the dimensions of the sprites. Both options work, but I find this way easier.<br>
Set C (columns) to 9, and R (rows) to 4.<br>
<br>
Slice!<br>
<br>
There are now thin white lines indicating where the cuts have been made.<br>
Hit `Apply` in the top right and then close the Sprite Editor window.<br>
<br>
In Project view, click the small arrow on the right of the spritesheet. It should open up the individual sprites contained. If the images appear cut off, you may have entered in the wrong number of rows or columns, or your spritesheet may not be the correct size for this workshop. 

![](/WorkshopImages/Sprites.png)

### Creating Animations

Drag and drop the whole sheet (not one of the individual sprites) onto the scene. <br>
Delete the animation and animation controller that is created automatically. <br>
<br>
Hit ctrl+6 (on Windows) or ⌘+6 (on Mac) to open up the Animation pane. Drag and drop this new view as a tab next to the Game view's tab.<br> 
<br> 
In Project view, go back to the top Assets folder and create a folder called "Animations". <br> 
Click on your character's Game Object in the Hierarchy pane. (Should be right after "camera".)<br> 
In the Animation pane, it should say “To begin animating [character], create an Animation Clip.<br> 
Hit create. <br> 
Next to the `Save As` input box, hit the dropdown arrow for full view.<br> 
Name the clip “walkBack” and save it in the Animations folder.<br> 
<br>
Change sample rate to 7. This is essentially your frames per second. For a simple pixel animation like this it needs to be rather low. <br> 
In Project pane, find your character's spritesheet, open up the individual sprites, and drag the very first sprite into the beginning of the animation timeline (0:0). <br> 
Drag the next 7 sprites into 0:1, 0:2, etc. They should all be facing back.<br> 

![](/WorkshopImages/Animation.png)

Hit play to view the animation!<br> 
<br> 
Now we need walkFront, walkLeft, and walkRight. To create these other animations, you can go to the animator and hit the dropdown that is currently on "walkBack" (near top left of animation pane) and hit `Create Clip`. Alternatively, you can create these (and in future, all four) in the Animations folder (Right click ->  Create -> Animation) and then drag and drop them onto the character Game Object in the Hierarchy pane. <br>
<br>
Drag 8 sprites onto each animation, making sure the character direction matches the name of the clip.<br> 
<br>
Tip: If you ever find you can’t drag and drop a sprite, make sure the character’s Game Object is still selected.<br>

### Setting Up Animator

Animations are the actual clips. The animator, or animation controller, is what controls the flow of animations. Here you can set conditions for when which clip should play at what time.<br>
<br>
Going to the animations folder, there should be a new animation controller. Double click it to open up the Animator view. Drag the tab next to the animation tab (in the central section). <br>
<br>
Later, we will have a script to detect when the player is moving up, right, left, or down, and adjust the animation accordingly. For the moment, let’s just get the four clips to loop.<br>
<br>
All four animations should be visible as bubbles on the screen. (Zoom out if you don’t see them all.) Right now, however, only one is connected to our entry point.<br>
I recommend moving the four bubbles so walkBack is near the top, walkRight is near the right, walkFront is near the bottom, and walkLeft is near the left. <br>
<br>
Right click on walkBack and hit `Make Transition`. An arrow will be created, and follow your mouse as you move.<br>
Hover over walkRight and click it to connect the transition to it. <br>
Tap on walkBack to select it, and then in the Inspector pane, select the transition we just made. <br>
* Open up `Settings`
* Set `Exit Time` to zero
* Set `Transition Duration` to zero

Note, do not uncheck “Has Exit Time”! This will cause the clip to be ignored as there are no other conditions currently. <br>
<br>
Repeat the process so walkRight is connected to walkFront, walkFront is connected to walkLeft, and walkLeft is connected back to walkBack.<br>
![](/WorkshopImages/Animator.png)
<br>
Hit play. The character should animate as if walking. Note, however, there is no actual motionm, even if you press the arrow keys. That will need to be done using a script.  




