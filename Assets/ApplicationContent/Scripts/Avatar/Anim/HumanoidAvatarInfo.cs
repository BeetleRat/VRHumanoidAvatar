using UnityEngine;

/// <summary>
/// <para>Class that stores information about avatar using the humanoid animator skeleton.</para>
/// <param name="avatarPrefab">the spawned avatar prefab</param>
/// <param name="avatarMapper">the <see cref="AnimatorAvatarMapper"/> of spawned avatar prefab</param>
/// </summary>
public sealed class HumanoidAvatarInfo : AvatarInfo
{
    [SerializeField] private Animator _avatarPrefab;
    [SerializeField] private AnimatorAvatarMapper _avatarMapper;

    private void Awake()
    {
        _avatarMapper.AvatarPrefab = _avatarPrefab;
    }
}