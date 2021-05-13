#Log

## 2021-05-13
- Add animation for the frog
- Use script to change animation state of the frog(*face direction must be changed before movement, if not the frog may jump to same direction but the display was reversed to another direction*)
- Add another enemy Eagle
- Use script to make the eagle fly down and up
- Add death animation to the frog
- Use script and animation event to trigger frog death animation play, after that destory frog from the scene
- [[video] Chapter 16-Animation Event](https://www.bilibili.com/video/BV1v441127vP)
- [[video] Chapter 17-Add destory animation & add Eagle](https://www.bilibili.com/video/BV1i4411m7fK)

## 2021-05-12
- Use script make the frog movement 
- Add two margin point for the frog so that the frog will movement between the two margin point 
- [[video] Chapter 15-The frog movement](https://www.bilibili.com/video/BV1v4411q7ZK)


## 2021-05-11
- Add an enemy(frog) to the scene
- Create an idle animation for the frog
- Use script to implement when the player falling to the frog then destory the frog and jump again
- Add hurt animation to the player
- Use script to control the hurt animation play or not and when the player get hurt then make a short distance reverse movement to the player for make the game more funny.
- Add crouch animation to the player
- Use script to control the player enter crouch state or exit crouch state and crouch animation play or not 
- [[video] Chapter 13-Add an enemy](https://www.bilibili.com/video/BV1F4411z7Jy)
- [[video] Chapter 14-Make the player get hury](https://www.bilibili.com/video/BV1F4411z7Jy)


## 2021-05-10
- Add physics material to the player and set friction zero so that the player will not hang on the wall.
- Enrich the environment element of the scene.
- Limit the player counld not jump until the player was landed to the ground.
- Add canvas to the scene.
- Add UI.Text component to the scene.
- Add UI.Image component to the scene.
- Set the relative posion for component so that the component will display well in different aspect.
- Use script update the cherry number collected by the player to the text component of the scene.
- Found Bug:
  + Sometime the number of cherry collected by the player will increase two time in the function. Why?
- [[video] Chapter 11- Physics Material & limit jump time](https://www.bilibili.com/video/BV1A441167MF)
- [[video] Chapter 12- UI.Text & UI.Image & update UI.Text value from script](https://www.bilibili.com/video/BV1Z4411z7MZ)


## 2021-05-09

- Use script to make the main camera follw the player scene
- Add cinemachine and use 2D Camera bound the camera to the player
- Create goods let the player collect
- Use script to make the player collect goods
- Enrich the environment element of the scene
- [[video] Chapter 9-Cinemachine - Camera contrl](https://www.bilibili.com/video/BV1r4411d7Zv)
- [[video] Chapter 10-Collect goods & Prefabs](https://www.bilibili.com/video/BV1Q441197nf)

## 2021-05-08

- Bug fixed.
- [[video] Chapter 8-Bug fixed](https://www.bilibili.com/video/BV194411o7WG)

## 2021-05-07

- Create Animation for the player
- Use the script to switch the player's different animation when the condition was reached(Such as idle, running, jumping, falling).
  + found a bug: If I press left or right movement key down and release right now and then hold on same direction movement button, the player will not movement. Why?
- [[video] Chapter 6-Animation](https://www.bilibili.com/video/BV1d4411d79u)
- [[video] Chapter 7-Animation of jump & Layermask](https://www.bilibili.com/video/BV1z4411o7W4)


## 2021-05-06

- Create Unity 2D project
- Import a free unity asset resource. ☞ [🔗Sunny land](https://assetstore.unity.com/packages/2d/characters/sunny-land-103349)
- Use the asset create very simple tilemap
  + how to use a picture resouce create a tilemap(create a Tilemap and use Sprite Editor to slice the picture)
- Create the player
- Add a script to the player for control the play behavior.
  + create a method for control the player movement.
  + create a method for control the player jump and flip direction.
- [[video] Chapter 1-Unity install](https://www.bilibili.com/video/BV1W4411Z7UC)
- [[video] Chapter 2-asset resource import & Tilemap learning](https://www.bilibili.com/video/BV1W4411Z7xs)
- [[video] Chapter 3-Layer & The player](https://www.bilibili.com/video/BV1r4411Z7dD)
- [[video] Chapter 4-The player movement](https://www.bilibili.com/video/BV1f4411Z7oL)
- [[video] Chapter 5-Flip the player direction & jump](https://www.bilibili.com/video/BV154411f7Pa)



### video from [Bilibili.com](https://www.bilibili.com/)

### [The series learning video](https://space.bilibili.com/370283072/channel/detail?cid=85776&ctype=0)