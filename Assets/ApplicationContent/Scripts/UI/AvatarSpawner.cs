using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>The class responsible for spawning the avatar selected in the <see cref="AvatarList"/></para>
/// <param name="avatarList">The <see cref="AvatarList"/></param>
/// </summary>
public sealed class AvatarSpawner : MonoBehaviour
{
    [SerializeField] private AvatarList _avatarList;
    private List<GameObject> spawnedAvatars = new List<GameObject>();

    public void SpawnSelectedAvatar()
    {
        GameObject spawnedAvatar = _avatarList.CurrentAvatar;
        if (spawnedAvatar)
        {
            DestroyOldAvatars();
            SpawnAvatar(spawnedAvatar);
        }
    }

    private void DestroyOldAvatars()
    {
        foreach (var lastSpawnedAvatar in spawnedAvatars)
        {
            Destroy(lastSpawnedAvatar);
        }
    }

    private void SpawnAvatar(GameObject spawnedAvatar)
    {
        GameObject avatarInstance = Instantiate(spawnedAvatar);
        spawnedAvatars.Add(avatarInstance);
    }
}