# Odacity-Gacha

Odacity is a script-only gacha game made in 2023. You will be able to collect, play and enhance characters.


## In-depth information and game mechanics

* ('any character') means you should press 'any chanracter' to access the function
* When in doubt press 'enter'
* Displaying character images does not work yet in the terminal. Maybe downloading specific packages might solve this.
* Keep in mind as long as this comment is here, the project is not completed ! :)
* Have fun !

## Prerequisits

Have any supported .Net version installed

## Installation

On macos, after cloning the repository, follow these commands in order to get started and play the game :

Enter the correct repository :

```bash
cd Odacity-Gacha
```

Build the project using the following command :

```bash
dotnet build
```

Publish the project as a self-contained executable using the following command :

```bash
dotnet publish -c Release -r osx-x64 --self-contained true /p:PublishSingleFile=true
```

Navigate to the publish folder using the following command (replace 'net6.0' with your .Net version) :

```bash
cd Odacity/bin/Release/net6.0/osx-x64/publish/
```

Move and navigate the executable to the correct emplacements :

```bash
#Move the executable
mv Odacity ../../
#Navigate
cd ../../
```

Run the 'Odacity' executable using the following command :

```bash
./Odacity
```

Congratulations, you are done!

## Game feel and view

Each character is unique and generated randomly from pieces (suffix/preffix for name, multipliers and skills (Q, W and Ultimate with a turn cooldown) as well as images and stats) at the time of the creation of your account.

In-game combat is turn-based and has the following look :

![combat](Spare_Images/combat.png)

You will be able to select, view and attack opposing characters as well as compose a strong lineup.

Collection page view :

![Collection](Spare_Images/Collection.png)
