using System;
using UnityEngine;

/// <summary>
/// <para>Class that stores avatar information.</para>
/// This class is used to display avatars in the UI.
/// <param name="isActive">
/// If avatar is not active,
/// it will not be added to the avatar list
/// and will not be displayed on the UI
/// </param>
/// <param name="avatarImage">Image displayed on the UI</param>
/// </summary>
public class AvatarInfo : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private Sprite _avatarImage;

    /// <summary>
    /// Avatar active state.
    /// </summary>
    public bool IsActive => _isActive;

    /// <summary>
    /// Sprite avatar image.
    /// </summary>
    public Sprite AvatarImage => _avatarImage;

    /// <summary>
    /// Spawned avatar prefab.
    /// </summary>
    public GameObject AvatarPrefab => gameObject;
}