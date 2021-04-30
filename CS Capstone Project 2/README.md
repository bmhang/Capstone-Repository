# Let Us Play With Emotion Recognition and Virtual Reality Capstone Project – Fall 2020/Spring 2021

Welcome to our capstone repository! We hope the information in this file will be helpful to future teams in setting up both the Oculus Quest and Emotiv EPOC+ headsets.

PLEASE NOTE: This project will only run on Unity Version 2018.4.18f1. This repository may not compile for you since we excluded one of the files from the Unity Emotiv Plugin for our login information. However, you are free to look at our project structure and the plugins we used.

## Setting up the Oculus Quest

Follow the steps in the following link. In "Configure Unity Settings," you don't need to set rendering preferences or define quality settings. Those are optional (we didn't do them since it took forever to recompile our project each time a setting was changed). Additionally, you don't need to do the "Build Your First App" step if you don't want to.

[Tutorial to set up Oculus Quest with Unity](https://developer.oculus.com/documentation/unity/unity-gs-overview/).

## Setting up Emotiv Stuff with Unity

The below excerpt from the Emotiv repository describes how to set up the Emotiv App and create an example project. We followed these steps closely, then built the rest of our project around the example project Emotiv provides.

This example demonstrates how to work with Emotiv Cortex Service (aka Cortex) in Unity.

### Prerequisites

* Install Unity. You can get it for free at [www.unity3d.com](https://unity3d.com/get-unity/download).
* Get the latest version of [Emotiv Unity Plugin](https://github.com/Emotiv/unity-plugin) as a submodule.
```
       git submodule update --init
```
* Install and run the EMOTIV App with Cortex from (https://www.emotiv.com/developer/).
* Login to the Emotiv website with a valid EmotivID, register an application at https://www.emotiv.com/my-account/cortex-apps/ to a pair of client id and client secret. If you don't have a EmotivID, you can [register here](https://id.emotivcloud.com/eoidc/account/registration/).
* We have updated our Terms of Use, Privacy Policy and EULA to comply with GDPR. Please login via EMOTIV App to read and accept our latest policies in order to proceed with the following examples.

### How to compile
<!-- how to compile  -->
1. Open EmotivUnityPlugin.unity by Unity Editor.
1. Put your client id and client secret to AppConfig.cs.
1. You can run the examples directly from the Unity Editor.


### Code structure

There are some main controller scripts:

**ConnectToCortex.cs**: Initialize connection to Cortex.

**1_Cortex**: Contain commponent scripts to control authorization procedure.

**ConnectHeadset**: Contain commponent scripts to create headset element and list headset information.

**ConnectionIndicator**: Contain commponent scripts to show battery indicator, after headset is connected and device information is subscribed.

**ContactQuality**: Contain commponent scripts to show contact quality of headset sensors.

**DataSubscriber.cs**: The script to show subscribe and unsubscribe data. The header and data of corresponding streams will be displayed and updated. Note that the MARKERS channel of EEG data will not be shown.

**Emotiv-Unity-Plugin**: The plugin that works behind the scene. Please refer to [Emotiv Unity Plugin](https://github.com/Emotiv/unity-plugin).

### How to use
1. Put clientId, clientSecret of your application in AppConfig.cs. You also can configure application name, version and TmpAppDataDir to create log directory.
1. Make sure you have logged in via EMOTIV App, and the headset has been turned on.
1. Run the example from editor. The example will connect to Cortex for authorization. You might need to grant access right for the example via EMOTIV App at the first time. After that, the example will get the token to work with Cortex. The token will be saved for subsequent use.
After authorizing successfully, the example will list available headsets. 
1. Click on a headset button to create a working session with that headset, and subscribe to the device information.
1. Please make sure the headset is at good contact quality and click Done to proceed.
1. Now, you can subscribe or unsubscribe more data such as EEG, Motion and Performance Metrics.
1. When you run the application in standalone mode, the log files will be located at "%LocalAppData%/${TmpAppDataDir}/logs/" on Windows or "~/Library/Application Support/${TmpAppDataDir}/logs" on macOS.
