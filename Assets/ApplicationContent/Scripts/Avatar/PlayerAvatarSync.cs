using UnityEngine;

/// <summary>
/// <para>Class responsible for synchronizing the player and his avatar.</para>
/// <para> This class is used in the player's prefab located at the Assets/Resources/Avatars.</para>
/// <list type="bullet">
/// <listheader>
/// <description>This class is responsible for:</description>
/// </listheader>
/// <item>
/// <description>head position synchronization (oculus helmet);</description>
/// </item>
/// <item>
/// <description>synchronization of hand position (oculus controllers/traceable human hands);</description>
/// </item>
/// <item>
/// <description>type of controllers (oculus controllers/traceable human hands).</description>
/// </item>
/// </list>
/// <remarks>
/// <list type="bullet">
/// <listheader>
/// <description>To work correctly, this class requires that scripts are present in the scene:</description>
/// </listheader>
/// <item>
/// <description><see cref="HandAnchor"/>;</description>
/// </item>
/// <item>
/// <description><see cref="OVRCameraRig"/>;</description>
/// </item>
/// <item>
/// <description><see cref="ControllerEvents"/>.</description>
/// </item>
/// </list>
/// </remarks>
/// <param name="head">player's avatar head</param>
/// <param name="leftHand">player's avatar left hand</param>
/// <param name="rightHand">player's avatar right hand</param>
/// <param name="controllertTypeController">An array of <see cref="ControllerTypeManager"/> that are managed by this class.</param>
/// <param name="dublicateMainPlayer"></param>
/// </summary>
public class PlayerAvatarSync : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    [SerializeField] private ControllerTypeManager[] _controllertTypeController;

    private bool _isAttachToController;

    private Transform _headRig;
    private Transform _leftHandRig;
    private Transform _rightHandRig;

    private HandAnchor[] _handViews;
    private ControllerEvents _controllerChangeTypeEvent;

    private void Start()
    {
        CreatePlayer();
    }

    private void Update()
    {
        MapPosition(_head, _headRig);
        MapPosition(_leftHand, _leftHandRig);
        MapPosition(_rightHand, _rightHandRig);
    }

    private void OnDestroy()
    {
        if (_controllerChangeTypeEvent)
        {
            _controllerChangeTypeEvent.ControllerTypeChange -= OnControllerChange;
        }
    }

    private void CreatePlayer()
    {
        SetHeadRigTransform();
        SetHandRigsTransform();
    }

    private void SetHeadRigTransform()
    {
        OVRCameraRig ovrCameraRig = FindObjectOfType<OVRCameraRig>();
        _headRig = ovrCameraRig.transform.Find("TrackingSpace/CenterEyeAnchor");
    }

    private void SetHandRigsTransform()
    {
        _handViews = FindObjectsOfType<HandAnchor>();
        foreach (HandAnchor handAnchor in _handViews)
        {
            if (handAnchor.HandType == HandType.Left)
            {
                _leftHandRig = handAnchor.transform;
            }
            else
            {
                _rightHandRig = handAnchor.transform;
            }

            SetControllerChangeTypeEvent(handAnchor);
        }
    }

    private void SetControllerChangeTypeEvent(HandAnchor handAnchor)
    {
        if (_controllerChangeTypeEvent == null)
        {
            _controllerChangeTypeEvent = handAnchor.ControllerChangeTypeEvents;
            if (_controllerChangeTypeEvent != null)
            {
                _controllerChangeTypeEvent.ControllerTypeChange += OnControllerChange;
                OnControllerChange(_controllerChangeTypeEvent.IsAttachToController);
            }
        }
    }

    private void OnControllerChange(bool isAttachToController)
    {
        _isAttachToController = isAttachToController;
        if (isAttachToController)
        {
            ChangeControllerView(ControllerType.OculusController);
        }
        else
        {
            ChangeControllerView(ControllerType.HandsPrefabs);
        }
    }

    private void ChangeControllerView(ControllerType type)
    {
        foreach (ControllerTypeManager myControllerrPrefab in _controllertTypeController)
        {
            myControllerrPrefab.SwitchControllerView(type);
        }
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}