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

### Creating Animations

Drag and drop the whole sheet (not one of the individual sprites) onto the scene. <br>
Delete the animation and animation controller that is created automatically. <br>
<br>
Hit ctrl+6 (on Windows) or ⌘+6 (on Mac) to open up the Animation pane. Drag and drop this new view as a tab next to `Game`.
