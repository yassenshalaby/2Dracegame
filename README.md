# 2D Race Game

A 2D endless runner mobile game built in Unity 6 for iOS. The player controls a car across three lanes, avoiding spike obstacles and collecting shields for temporary immunity. The game features Firebase authentication and cloud high score persistence.

## Description

The game consists of three scenes: a login screen, a main menu and the game itself. Players sign up or log in using Firebase Authentication. Once in the game, the player switches lanes by tapping the left or right side of the screen. Spike obstacles scroll down from the top of the screen and increase in speed over time. Collecting a shield grants five seconds of immunity. The score increases continuously and is saved to Firebase Realtime Database at the end of each run if it exceeds the player's previous high score.

## Third Party Plugins

- Firebase Authentication (com.google.firebase.auth)
- Firebase Realtime Database (com.google.firebase.database)
- TextMeshPro (com.unity.textmeshpro)

## Steps to Run the Project

1. Clone the repository
2. Open the project in Unity 6 using the Universal 2D template
3. Import the Firebase Unity SDK via Unity Package Manager
4. Add your own `GoogleService-Info.plist` file to the Assets folder
5. Open Build Settings and switch platform to iOS
6. Build to Xcode and run CocoaPods (`pod install`) in the generated Xcode project folder
7. Open the `.xcworkspace` file in Xcode and run on a connected iOS device