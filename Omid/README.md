# Omid
Omid is a project that came to life about an year ago when taking some courses about exponential functions. 
The game was developed by putting together small pieces of work, the whole process took around one month.

Omid is a 2D mobile game having as a main character an caterpillar.  
The game inherits the idea from the well known game Flappy Bird, relying on fancy effects, awesome skins and a not easy to beat drunk mode. 
The player controls the main character, attempting to fly between columns of pipes without hitting them. 
The more pipes player avoids, the more points are earned. 
These points come under leaves form and allows player to buy skins, changing the main character color and effects.


<h2>:movie_camera: Click on the image bellow to see the video.</h2>

[![Watch the video](https://github.com/sabauandrei98/unity3d/blob/master/Omid/Screens/facebookBanner.png)](https://www.youtube.com/watch?time_continue=2&v=TaGFiH1qBNo)

<h1>Short game presentation</h1>

<h3>Main menu</h3>
Allows the player to choose one of the three difficulties (easy/medium/hard),  view the best scores for each difficulty, see the number of leaves points, switch to player customize, learn how to play the game and control the sound effects.

<h3>Levels</h3>
Each level has a different difficulty, this describing the distance between the current and the next pipe and the size of the gap between the two pipes. Moreover, as the player's score increases, the harder is to continue.

<h3>Drunk mode</h3>
After a couple of points, the game reverses, so do the controllers, the player will have to tap to go down, and to release the key to go up. It may seem simple but, it's not, believe me. The pink number below the score indicates the remaining points until Drunk mode, so be careful !

<h3>Customize</h3>
This feature allows the player to choose the skin of the main character in exchange for some leaves points. Each skin has a unique color combination and particles effect.


<h1>Risks & Challenges</h1>

There were a lot of problems I encountered during the development of this game.

First of all, the "map". As you might thing, the line is moving infinitely to the right.. but this is not true. To avoid huge coordinates I came up with a simple solution which allows me to use periodic system by using a circle. The whole "map" is nothing more but a circle. Setting the camera in the center and choosing a consistent radius produced the same effect, avoiding huge coordinates systems.

Secondly, a real problem was the motion of the line. It was really hard to create an easy to handle line due to height which should increase and decrease exponentially. It took me like a week to find a the right formula for a nice behavior of the line.

Finally, everything in the game is generated in real-time. It was harder than I expected to generate the pipes because they are colored lines, not solids. This meant that I had to create scripts to handle the collisions between two lines, because there were no collisions for these objects I chose to work with,


<h2>:camera: Screens</h2>

![alt text](https://github.com/sabauandrei98/unity3d/blob/master/Omid/Screens/mainmenu.png)
![alt text](https://github.com/sabauandrei98/unity3d/blob/master/Omid/Screens/customidze.png)
![alt text](https://github.com/sabauandrei98/unity3d/blob/master/Omid/Screens/drunkmode.png)
![alt text](https://github.com/sabauandrei98/unity3d/blob/master/Omid/Screens/hard.png)
![alt text](https://github.com/sabauandrei98/unity3d/blob/master/Omid/Screens/skins.png)

