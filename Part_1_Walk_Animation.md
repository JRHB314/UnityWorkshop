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
Change `Samples` to 7. This is sample rate, essentially frames per second. For a simple pixel animation like this it needs to be rather low. <br> 
In Project pane, find your character's spritesheet, open up the individual sprites, and drag the very first sprite into the beginning of the animation timeline (0:0). <br> 
Drag the next 7 sprites into 0:1, 0:2, etc. They should all be facing back.<br> 

![](/WorkshopImages/Animation.png)

Hit play to view the animation!<br> 
<br> 
Now we need walkFront, walkLeft, and walkRight; faceBack, faceLeft, faceFront, and faceLeft.  To create these other animations, you can go to the animator and hit the dropdown that is currently on "walkBack" (near top left of animation pane) and hit `Create Clip`. Alternatively, you can create these (and in future, all eight) in the Animations folder (Right click ->  Create -> Animation) and then drag and drop them onto the character Game Object in the Hierarchy pane. <br>
<br>
For the other walk animations, drag a set of 8 sprites onto each, making sure the character direction matches the name of the clip.<br> 
<br>
For the facing animations, simple drag and drop the first sprite of each set, again making sure the direction matches the clip.<br>
<br>
Tip: If you ever find you can’t drag and drop a sprite, make sure the character’s Game Object is still selected.<br>

### Setting Up Animator

Animations are the actual clips. The animator, or animation controller, is what controls the flow of animations. Here you can set conditions for when which clip should play at what time.<br>
<br>
Going to the animations folder, there should be a new animation controller. Double click it to open up the Animator view. Drag the tab next to the animation tab (in the central section). <br>
<br>
Later, we will have a script to detect when the player is moving up, right, left, or down, and adjust the animation accordingly. For the moment, let’s just get the four walking clips to loop. We won't worry about the facing animations yet, either. <br>
<br>
All animations should be visible as bubbles on the screen. (Zoom out if you don’t see them all.) Right now, however, only one is connected to our entry point.<br>
I recommend moving the four walking bubbles so walkBack is near the top, walkRight is near the right, walkFront is near the bottom, and walkLeft is near the left. <br>
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
<br><br>
Hit play. The character should animate as if walking. Note, however, the character does not move around the scene, even if you press the arrow keys. That will need to be done using a script.  

### Movement Script

In Project pane, navigate to the top folder, then create a new folder called "Scripts". <br>
Inside this folder, create a new C# script called CharacterMovement.<br>
Drag and drop it on the character's Game Object in order to connect them.<br>
<br>
Now, if you double click this, it will open up Visual Basic. I personally do not like Visual Basic. To use something else, you can instead right click on the script, hit `Reveal in Finder`, and then open with your text editor of choice.<br>
<br> 
Once you open up the script you will see that it has a few “using” statements, a public class that uses Monobehavior as a base class, and two functions. Do not remove these, as they are necessary for the script to work with Unity.<br>
<br>
Most actions will be performed in either the `Start` or the `Update` functions.
* Start runs once, when the script is first run (usually on scene load.)
* Update runs continuously in a loop, executing once per frame. 

Right under `public class CharacterMovment : MonoBehaviour { ` (before the functions) insert this line:
```
public float moveSpeed;
```
First declare a public float "moveSpeed". One nice thing about Unity is that publicly declared variables will be visible inside the program itself.<br>
<br>
Back in Unity, click on the character'S Game Object, and scroll down on the Inspector pane until you see the `Script` element. There should now be a box called Move Speed with an input box you can change.<br>
<br>
Note, changing the variable should only change that value for this object, this “instance” of the script. If you had this script on two objects, they could have two different speeds.<br>
Also note this only works for public variables; private variables will not be visible inside Unity. <br>
<br>
Inside Update, insert these lines: 
```
float horiz = Input.GetAxisRaw("Horizontal");
float vert = Input.GetAxisRaw("Vertical");

if(horiz > 0f || horiz < 0f) {
	float movement = horiz * moveSpeed * Time.deltaTime;
	transform.Translate(new Vector3(movement, 0f, 0f));
}
if(vert > 0f || vert < 0f) {
	float movement = vert * moveSpeed * Time.deltaTime;
	transform.Translate(new Vector3(0f, movement, 0f));
}
```
First it gets the horizontal and vertical data from our input device. These values are floats from -1 to 1. Right and Up return positive values. Left and Down return negative values. They are floats because it is possible to have a stronger or weaker push in a direction; think a joystick that's only pushed halfway. A smaller push returns a smaller (magnitude) value and would correlate to slower movement.<br>
<br>
It then checks if input is greater than or less than zero for either horizontal or vertical input. <br>
When input is detected, it calculates how much the character should be moved.<br>
<br>
First we have the input value. This is multiplied by the moveSpeed. That probably makes sense. However, we also need to multiply by something called time.deltaTime.  This is “the time between the current and previous frame.” That is, each frame takes a certain (very small) amount of time. This checks how far along we are in the frame when the line of code runs. <br>
If we don’t take this into account, and simply move the character the same amount every single time, the movement will be choppy. <br>
<br>
Taking this movement amount, we want to transform our game object by translating it. Translating in this case means moving through space. We do this using a vector object. A vector being a direction + magnitude. Specifically, we’ll use a vector3 object, a vector in 3 dimensions. Even though we’re only using 2 (x and y) for this game, Unity operates in 3. <br>
<br>
Vector3's are declared: `Vector3(horizontalValue, verticalValue, depthValue)`. As mentioned, this game won't use the depth, or z axis. <br>
<br>
Go back to Unity, and press play. If you hit the arrow keys you should be able to move the character! It doesn’t match the animation at all yet, but both components are in place. 

### Connecting Script to Animator

First we need to set up some parameters for the animation controller. <br>
<br>
In the Animator pane, go to the left bar and hit the tab that says `Parameters`. <br>
<br>
Hit the plus symbol to add these parameters:
* xMvmt and yMvmt as floats - take in the current movement input values
* lastX and lastY as floats - hold the last x or y value before input stopped, so animator knows which direction to leave the character facing
* isMoving as a bool - controls whether a walking or a facing animation is playing
<br>
These parameters will be updated by our script.<br>
<br>
Declare our new private variables under the moveSpeed float.

```
private Animator anim;

private float holdX;
private float holdY;
private bool moving;
```

On intialization, it should get the animator component for the current Game Object. In `Start`, add this line:
```
anim = GetComponent<Animator>();
```

At the beginning of `Update`, set moving to false; the code will assume moving is false unless movement is detected. 
```
moving = false;
```

If movement is detected, set moving to true, set our holdX or holdY value, and reset the other value (ie if holdX was set, reset holdY, and vice versa.) <br>
<br>
For horizontal:
```
moving = true;
holdX = horiz;
holdY = 0;
```

For vertical:
```
moving = true;
holdY = vert;
holdX = 0;
```

Finally, pass all these values to the Animator.
```
anim.SetFloat("xMvmt", horiz);
anim.SetFloat("yMvmt", vert); 
anim.SetFloat("lastX", holdX);
anim.SetFloat("lastY", holdY);
anim.SetBool("isMoving", moving);
```

All together, your code should look like this:<br>
![](/WorkshopImages/ConnectedScript.png)


Drag the Animator pane to the bottom right for a second so we can view it and the Game pane at the same time. Press play and move around. The parameters should change as the character moves.<br>
<br>

### Blend Trees

Blend trees control which animation is playing based on input values. Rather than creating many individual transitions with individual conditions, you can simple create a blend tree that tracks one or more parameters and plays the correct animation when a condition is met. 


