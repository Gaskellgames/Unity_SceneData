# Unity - SceneData

Custom editor scripts for Unity3D that enables easier SceneAsset referencing at runtime:

- SceneData uses a SceneAsset in the editor to cache the values of buildIndex, sceneName, sceneFilePath and guid.
- New SceneData can be created at runtime from: a scene, a buildIndex or a sceneFilePath.

![Example SceneData useage](Images/IMG_Script.png)

- Toggle to show/hide the cached scene info.
- Coloured line to easily sisualise if a scene is:

  (Green) in the build scenes and enabled,

  (Orange) not in the build scenes and enabled,

  (Red) Null.

![SceneData In Build](Images/IMG_InBuild.png)

![SceneData Not In Build](Images/IMG_NotInBuild.png)

![SceneData Null](Images/IMG_Null.png)
