# Overview
Package which allows you to fade in and out between scenes when creating games for VR headsets or other devices with [Stereoscopic Rendering](https://docs.unity.cn/550/Documentation/Manual/SinglePassStereoRendering.html).
# Get Files
[Download Unity Package Here](https://github.com/XRRCA/UnityScripts/raw/main/VR/VRSceneLoader/VRSceneLoader.unitypackage)
# Instructions

1. Download and Import the .UnityPackage
2. Add the `VRSceneManager` prefab to your scene.
3. Call the `GoToSceneAsync("Your scene index here")`. You get the scene index in `File` -> `Build Settings` once you've clicked `Add Open Scenes`.
4. You can call this method in various ways:
    1. Direct Method Call: If you have a reference to the object that contains the public method, you can call it directly using the dot operator. For example, if you have a script attached to a game object called "MyScript" with a public method called "MyMethod", you can call it like this:
      ```
      Public MyScript script // and attach in the inspector
      // OR 
      MyScript script = GetComponent<MyScript>();
      
      script.MyMethod();
      ```
    2. UnityEvent: If you are using Unity's UI system or the new UnityEvent system, you can assign a public method to a UnityEvent and invoke it through the event. This approach is commonly used for UI button click events. Here's an example of how you can set up a UnityEvent:
      ```
      using UnityEngine.Events;

      public UnityEvent myEvent; // Attach the Script in the inspector like you would a button.

      // Invoke the method through the event
      myEvent.Invoke();
      ```
