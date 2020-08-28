# Enhanced Google Universal Analytics

A set of components and utilities to more easily dispatch events through Google Universal Analytics.

## Installation

1. Import the [Google Universal Analytics](https://assetstore.unity.com/packages/tools/integration/google-universal-analytics-11098) plugin into your project.
1. Add an assembly definition file inside of the newly imported plugin named `GoogleUniversalAnalytics.asmdef` under `Analytics/Scripts`.
1. Install the [SerializableCallback](https://github.com/Siccity/SerializableCallback) package into your project.
1. Open the `manifest.json` file in the `Packages` folder of your project and add the following to the `"dependencies"` section:

    ```json
    "com.900lbs.enhancedgua": "https://github.com/dcolina900lbs/com.900lbs.enhancedgua.git#upm"
    ```

## Getting Started

1. Open the `Instructions_GoogleUniversalAnalyticsForUnity.pdf` file inside of [Google Universal Analytics](https://assetstore.unity.com/packages/tools/integration/google-universal-analytics-11098) plugin and follow through the **Set Up a Profile in Google Analytics**.
1. Import the **UI Sample** into your project through the Unity Package Manager.
1. Open the `UI Analytics Example` scene.
1. Select `Analytics` object in the hierarchy and change its `Tracking ID`, `Debug Tracking ID`, `App Name`, and `App Version` you set up earlier.
1. Play the scene and try clicking the buttons and switching between the screens and see your analytics page update!