using System;
using UnityEngine;

/// <summary>
/// <para>Class for setting the position and rotation of a skeleton bone to the target position.</para> 
/// </summary>
[Serializable]
public sealed class MapRigTransform
{
    /// <summary>
    /// The position at which the specified bone will be installed.
    /// </summary>
    public Transform Target;

    /// <summary>
    /// The skeletal bone that will be placed in the specified position.
    /// </summary>
    public Transform Rig;

    /// <summary>
    /// The offset of the bone position from the target.
    /// </summary>
    public Vector3 TrackingPositionOffset;

    /// <summary>
    /// The offset of the bone rotaion from the target.
    /// </summary>
    public Vector3 TrackingRotationOffset;

    /// <summary>
    /// <para>Place the bone in the specified position, taking into account the offsets.</para>
    /// </summary>
    public void MapRig()
    {
        Rig.position = Target.TransformPoint(TrackingPositionOffset);
        Rig.rotation = Target.rotation * Quaternion.Euler(TrackingRotationOffset);
    }
}

/// <summary>
/// <para>Class responsible for synchronizing the avatar’s body position with control elements.</para>
/// This class connects the displayed avatar model and the position of the helmet, controllers and hands.
/// Without this class, the model will spawn in a random place and will be drawn to the skeleton.
/// <param name="head"><see cref="MapRigTransform"/> for head controller</param>
/// <param name="rightHand"><see cref="MapRigTransform"/> for right hand controller</param>
/// <param name="leftHand"><see cref="MapRigTransform"/> for left hand controller</param>
/// <param name="bodyOffset">Offset distance of the displayed body from the central camera of the helmet</param>
/// <param name="turningSmoothness">Smoothness of body rotation following the head</param>
/// </summary>
public sealed class MapAvatarBody : MonoBehaviour
{
    [SerializeField] private MapRigTransform _head;
    [SerializeField] private MapRigTransform _rightHand;
    [SerializeField] private MapRigTransform _leftHand;

    [SerializeField] private float _turningSmoothness;
    [SerializeField] private Vector3 _bodyOffset;

    private void LateUpdate()
    {
        transform.position = _head.Rig.position + _bodyOffset;

        transform.forward = Vector3.Lerp(transform.forward,
            Vector3.ProjectOnPlane(_head.Rig.forward, Vector3.up).normalized, Time.deltaTime * _turningSmoothness);

        _head.MapRig();
        _rightHand.MapRig();
        _leftHand.MapRig();
    }
}