using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

/// <summary>
/// <para>Class linking Animation rigging element - MultiRotationConstraint and animator skeleton bone.</para>
/// </summary>
[Serializable]
sealed class MultiRotationConstraintOptions
{
    public MultiRotationConstraint ConstraintObject;
    public HumanBodyBones Bone;
}

/// <summary>
/// <para>Class linking Animation rigging element - TwoBoneIKConstraint and animator skeleton bone.</para>
/// </summary>
[Serializable]
sealed class TwoBoneConstraintOptions
{
    public TwoBoneIKConstraint ConstraintObject;
    public HumanBodyBones RootBone;
    public HumanBodyBones MidBone;
    public HumanBodyBones TipBone;
    public Vector3 HintOffset;
}

/// <summary>
/// <para>Class linking Animation rigging element - MultiParentConstraint and animator skeleton bone.</para>
/// </summary>
[Serializable]
sealed class MultiParentConstraintOptions
{
    public MultiParentConstraint ConstraintObject;
    public HumanBodyBones Bone;
}

/// <summary>
/// <para>Class that creates an avatar from an animator and binds the avatar's bones to Animation rigging elements.</para>
/// <param name="spineConstraintObjects">the <see cref="MultiRotationConstraintOptions"/> list</param>
/// <param name="spineConstraintObjects">the <see cref="TwoBoneConstraintOptions"/> list</param>
/// <param name="spineConstraintObjects">the <see cref="MultiParentConstraintOptions"/> list</param>
/// <param name="hideAvatarBodyparts">the list of parts of the avatar that will not be rendered</param>
/// </summary>
[RequireComponent(typeof(RigBuilder))]
public sealed class AnimatorAvatarMapper : MonoBehaviour
{
    [SerializeField] private List<MultiRotationConstraintOptions> _spineConstraintObjects;
    [SerializeField] private List<TwoBoneConstraintOptions> _armConstraintObjects;
    [SerializeField] private List<MultiParentConstraintOptions> _headConstraintObjects;
    private Animator avatarPrefab;
    private Animator spawnedAvatar;
    private RigBuilder rigBuilder;

    /// <summary>
    /// Spawned avatar prefab.
    /// </summary>
    public Animator AvatarPrefab
    {
        get => avatarPrefab;
        set => avatarPrefab = value;
    }

    private void OnEnable()
    {
        rigBuilder = GetComponent<RigBuilder>();
        if (avatarPrefab)
        {
            CreatePlayerFromAvatar(avatarPrefab);
        }
    }

    private void CreatePlayerFromAvatar(Animator avatarPrefab)
    {
        if (!avatarPrefab.avatar.isHuman)
        {
            LogError("The avatar's skeleton is not humanoid.");
            return;
        }

        SpawnAvatar(avatarPrefab);
        MapComponents();

        rigBuilder.Build();
    }

    private void SpawnAvatar(Animator avatarPrefab)
    {
        spawnedAvatar = Instantiate(avatarPrefab, transform, false);
    }

    private void MapComponents()
    {
        MapSpineBones();
        MapArmBones();
        MapHeadBones();
    }

    private void MapSpineBones()
    {
        foreach (MultiRotationConstraintOptions constraintObject in _spineConstraintObjects)
        {
            MapConstraints(constraintObject);
        }
    }

    private void MapConstraints(MultiRotationConstraintOptions multiRotationConstraintOptions)
    {
        MultiRotationConstraintData constraintData = multiRotationConstraintOptions.ConstraintObject.data;
        constraintData.constrainedObject = spawnedAvatar.GetBoneTransform(multiRotationConstraintOptions.Bone);

        multiRotationConstraintOptions.ConstraintObject.data = constraintData;
    }

    private void MapArmBones()
    {
        foreach (TwoBoneConstraintOptions constraintObject in _armConstraintObjects)
        {
            MapConstraints(constraintObject);
        }
    }

    private void MapConstraints(TwoBoneConstraintOptions twoBoneConstraintOptions)
    {
        TwoBoneIKConstraintData constraintData = twoBoneConstraintOptions.ConstraintObject.data;
        constraintData.root = spawnedAvatar.GetBoneTransform(twoBoneConstraintOptions.RootBone);
        constraintData.mid = spawnedAvatar.GetBoneTransform(twoBoneConstraintOptions.MidBone);
        constraintData.tip = spawnedAvatar.GetBoneTransform(twoBoneConstraintOptions.TipBone);
        constraintData.hint = new GameObject($"{twoBoneConstraintOptions.MidBone.ToString()}Hint").transform;
        constraintData.hint.position = constraintData.mid.position + twoBoneConstraintOptions.HintOffset;
        constraintData.hint.parent = transform;

        constraintData.target.transform.position = constraintData.tip.position;

        twoBoneConstraintOptions.ConstraintObject.data = constraintData;
    }

    private void MapHeadBones()
    {
        foreach (MultiParentConstraintOptions constraintObject in _headConstraintObjects)
        {
            MapConstraints(constraintObject);
        }
    }

    private void MapConstraints(MultiParentConstraintOptions multiParentConstraintOptions)
    {
        MultiParentConstraintData constraintData = multiParentConstraintOptions.ConstraintObject.data;
        constraintData.constrainedObject = spawnedAvatar.GetBoneTransform(multiParentConstraintOptions.Bone);

        multiParentConstraintOptions.ConstraintObject.data = constraintData;
    }

    private void LogError(string message)
    {
        Debug.LogError($"[{name}]: {message}");
    }
}