# Jelly Blocks
Jelly Blocks is an extended version of what should have been a simple homework. The project was initially a console based C++ program and then, it was written in C#, Unity.

This is a turn based board game. The goal of the game is to let the opponent without moves.
The game can be played:
1 vs 1 
1 vs AI

Each turn, the player chooses a empty square. This square will be covered with a 3x3 bigger square colored in a specific color representing the Player or the AI. 
After some turn, there will be no place left on the table. The one who made the last move wins.

The biggest challenge was the AI. To build and unbeatable AI, you have to think like the player and the player has a lot of strategies.
The AI thinking is build on a list of particular cases and a popular algorithm, Monte Carlo Tree Search.


<h2>:movie_camera: Click on the image bellow to see the video.</h2>

[![Watch the video](https://github.com/sabauandrei98/unity3d/blob/master/Omid/Screens/facebookBanner.png)](https://www.youtube.com/watch?v=F4Tq0k-h_TU)

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

