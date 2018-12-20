# Character Movement

### Importing your character spritesheet

The first thing is to choose a spritesheet. You can use any of the spritesheets in this repo's `Assets` folder, or anything else that follows the "LPC" format. For other size spritesheets these instructions would be slightly different. <br>
<br>
Go to the Projects pane, right click and create folder "Characters". Double click to open the folder in the pane. <br>
<br>
Drag and drop spritesheet of choice into the folder, then click on it to open up Inspector view. <br>
<br>
Make the following changes:
* Change `Sprite Mode` from `Single` to `Multiple`. This is to tell Unity this is a spritesheet with multiple sprites within it.
* Change `Pixels per Unit` to "32". This indicates the height and width of a single “cell” or tile. In general it's good for all image assets to have the same or close to the level Pixels per Unit, so they will have the same level of detail. 
* Go to `Advanced` and change `Filter Mode` to `Point (no filter)`. This is important for pixel art! Otherwise it will appear blurry.
* Change `Max Size` to `1024`. In general we want this to be as low as possible while still being higher than the width and height of the image. This helps keep project size down. 
* Change `Compression` to `None`. With pixel art, compression is both unecessary and can cause problems.

Note, these settings will have to be changed every time you import an image asset. Not all images will be spritesheets, but almost all will need to have their `Pixels per Unit` and `Max Size` adjusted. All pixel art needs to be `Point (no filter)` and have no compression. 

### Slicing spritesheet
Now open Sprite editor (there is a button in the Inspector). You may have to hit `Apply` to `Import Changes`. <br>
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
Now, if you double click this, it will open up Visual Studio (not Visual Studio Code). If you prefer something else, you can instead right click on the script, hit `Reveal in Finder`, and then open with your text editor of choice.<br>
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

Blend trees control which animation is playing based on input values. Rather than creating many individual transitions with individual conditions, you can simple create a blend tree that tracks one or more parameters and plays the correct animation when a condition is met. <br>
<br>
Compare this:<br>
![](/WorkshopImages/normal.jpeg)
<br>
To this:<br>
![](/WorkshopImages/blend.jpeg)
<br>
Go ahead and delete all the existing states and transitions (except for `Exit`, `Any State`, and `Entry`). We're going to replace them with a Blend Tree.<br>
<br>
On the Animator view, right click -> `Create State` -> `From New Blend Tree`. Name it "PlayerWalk", then double click it to open up the editor. <br>
By default in takes in the first parameter only, which should be `xMove`. Go to `Blend Type` and change type to `2D Simple Directional`. Add `yMove` as the second parameter.<br>
Add a `Motion` field by hitting the plus sign, then hitting `Add Motion Field`. A row should be added with a field that says `None (Motion)`. Click the circle icon to the right of it, then select the walkBack animation. Repeat with the other three walking animations.<br>
<br>
Set the fields like so:<br>
* walkBack:&nbsp;&nbsp;Pos X = 0&nbsp;&nbsp;| Pos Y = 1
* walkRight:&nbsp;Pos X = 1&nbsp;&nbsp;&nbsp;| Pos Y = 0
* walkFront:&nbsp;Pos X = 0&nbsp;&nbsp;| Pos Y = -1
* walkLeft:&nbsp;&nbsp;&nbsp;Pos X = -1&nbsp;| Pos Y = 0
<br>
Play the game and try moving around. The correct animations should now play! However, there is no idle animation, and the character will continue to walk in place continuously.<br>
<br>
Go back to animator and hit the `Base Layer` (label near top middle) to see the whole flow again. <br>
<br>
Create new blend Tree playerIdle. <br>
<br>
Double click it. This time, set the Parameters as lastX and lastY and use the facing animations.<br>

* faceBack:&nbsp;&nbsp;Pos X = 0&nbsp;&nbsp;| Pos Y = 1
* faceRight:&nbsp;Pos X = 1&nbsp;&nbsp;&nbsp;| Pos Y = 0
* faceFront:&nbsp;Pos X = 0&nbsp;&nbsp;| Pos Y = -1
* faceLeft:&nbsp;&nbsp;&nbsp;Pos X = -1&nbsp;| Pos Y = 0

Go back the `Base Layer`. Right click on playerIdle and select `Set as Layer Default State`, i.e., the state that starts the flow on the Base Layer.  <br>
Add a transition from playerIdle to playerWalk. 
* Uncheck exit time
* Set transition time to 0 
* Add `Condition` by selecting `isMoving` in the first dropdown, and `true` in the second. 
Add a transition from playerWalk to playerIdle.
* Uncheck exit time
* Set transition time to 0 
* Add `Condition` by selecting `isMoving` in the first dropdown, and `false` in the second. 
<br>
Go back to the game and hit play.<br>  
<br>
If everything was done correctly, the character should now walk around, with walking and idle animations playing correctly.<br>

![](https://media.giphy.com/media/9uI997mC531DAUQmaU/giphy.gif)

### Camera Tracking

There's one last component to proper character movement. We want the camera to track the player as they move in the world. <br>
<br>
Go to the Script folder and create a new C# Script "CameraController". Drag and drop it onto camera to add it as a component, then open it in your preferred editor.<br>
<br>
First we will declare a few variables before the `Start` function.
```
public GameObject target;
private Vector3 targetPos; 
public float cameraSpeed;
```
Target is the GameObject the camera should follow. Right now that's the player character, but there may be times we want the camera to follow something else. The targetPos is target position. Then we have camera movement speed. We'll set it to be slightly less than character movement speed, so it takes a second to "catch up". This makes the camera feel more lively.<br>
<br>
Inside `Update`, put this code.
```
float xPos = target.transform.position.x;
float yPos = target.transform.position.y;
float zPos = transform.position.z;
targetPos = new Vector3(xPos, yPos, zPos);
transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
```
xPos and yPos are our target's x and y position. zPos, meanwhile, is our camera's z position. This is because the camera should be above the scene looking down on it; you wouldn't be able to see anything if it was "inside" the rest of the scene.<br>
targetPos is set by creating a new Vector3 that takes in our three position values. Then we want to take that Vector3 and move the camera's position. "Lerp" stands for "linear interpolation"; it takes two vectors and finds a position between them. In this case it uses our speed to see how much it should be moved from our startpoint vector (current position) to our endpoint vector (target position).<br>
<br>
Save, then go back to Unity. Change Camera Speed to 8, and drag and drop the character Game Object onto the `Target` field.<br>
If you go to the game and hit play, try moving around. You can see the camera now follows a bit behind the player, and catches up when you stop moving. It may seem like the player stops moving entirely because the background is a flat color, this can easily be changed by adding in a patterned background. <br>
<br>
Go to the `Assets` folder in this workshop, and download the "grass.png" file. In your Unity Assets, create a folder called "Background" and import the grass file.<br>
Click on the grass to open inspector. <br>
<br>
Change:
* `Pixels Per Unit` to 32
* `Filter Mode` to `Point (no filter)`
* `Max Size` to `1024`
* `Compression` to `None`.
Drag and drop the grass onto your scene so it's fairly centered under the camera. <br>
<br>
Tip: If at any point your character becomes covered by the background, go to the inspector for the character's Game Object (not the spritesheet) and under `Sprite Renderer` change `Order in Layer` to 1.<br>
<br>
Hit play.<br>
<br>
Good job! You have finished getting a character to move around your game world.<br>
<br>
![](https://media.giphy.com/media/kFMNKCTrcszFIEwruO/giphy.gif)
