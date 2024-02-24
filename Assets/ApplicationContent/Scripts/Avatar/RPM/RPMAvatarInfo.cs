using UnityEngine;

/// <summary>
/// <para>Class that stores information about avatar using the Ready Player Me model.</para>
/// <param name="redyPlayerMeAvatar">Avatar prefab created through Ready Player Me</param>
/// <param name="gender">Gender of the model.
/// Depending on this parameter, a skeleton will be selected, to which the model will be attached.
/// </param>
/// </summary>
public class RPMAvatarInfo : AvatarInfo
{
    [SerializeField] private GameObject _redyPlayerMeAvatar;
    [SerializeField] private Gender _gender;

    private void Awake()
    {
        RPMAvatarParser[] avatarSkeleton = GetComponentsInChildren<RPMAvatarParser>();
        foreach (RPMAvatarParser skeleton in avatarSkeleton)
        {
            if (skeleton.SkeletonGender != _gender)
            {
                skeleton.gameObject.SetActive(false);
            }
        }
    }


    /// <returns>Avatar prefab created through Ready Player Me</returns>
    public GameObject GetReadyPlayerMeAvatar()
    {
        return _redyPlayerMeAvatar;
    }
}