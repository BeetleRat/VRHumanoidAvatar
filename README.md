# VRHumanoidAvatar
## To work with the project it is necessary to install the following packages
- [Oculus Integration](https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022);
## Description
What does an avatar represent? An avatar is a model that has a skeleton. This skeleton attaches itself to the player's controllers when it spawned in the scene. Attaching the skeleton to the player's controllers is done using the Animation rigging package.

Example of attaching a skeleton to player controllers: 
[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/RaDSUd6GSjs/0.jpg)](https://www.youtube.com/watch?v=RaDSUd6GSjs)


The following ways of changing the avatar are implemented in this demo:
### Replacing meshes
Avatars stored in the Assets\Resources\Avatars\RPM path use the meshes replacement principle. In prefab avatar two models(male and female) are already prepared. When this avatar is spawned in the scene, depending on the selected gender, one of the models becomes inactive. The meshes of the active model are replaced by meshes from the passed avatar model. 

The obvious disadvantage of this method is the strict avatar model requirement. In this demo we use models created with Ready Player Me service. All models of this service have the same structure and model part names. This allows you to easily replace meshes with meshes from another model. However, for models with a different structure and names you will have to create a new avatar blank. 
### Using a humanoid model from animator
Avatars stored in the Assets\Resources\Avatars\Anim path use the Humanoid animator principle. The prefab stores the preset for Animation rigging. When the avatar spawns in the scene, it spawns the model and adds parts of its humanoid skeleton to the preset. Once the preset is filled with parts of the humanoid avatar - it builds, allowing the model to move.

To fill the preset with avatar model parts, the avatar model must have an animator with a humanoid avatar skeleton.