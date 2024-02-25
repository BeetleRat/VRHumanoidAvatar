using UnityEngine;

/// <summary>
/// <para>A script that attaches an object to an anchor hanging on a player.</para>
/// This class is used so that objects that are attached to the player do not have to be searched for a long time on the player himself.
/// <list type="number">
/// <listheader>
/// <description>The idea is this:</description>
/// </listheader>
/// <item>
/// <description>We attach an EmptyObject - anchor to the desired part(s) of the player's body;</description>
/// </item>
/// <item>
/// <description>Create the object we want, wherever we want it;</description>
/// </item>
/// <item>
/// <description>Add this script to the object and in its fields specify the anchor to which the object will be attached when the application starts;</description>
/// </item>
/// <item>
/// <description>*If the object is attached to the hands, then you need to specify a separate anchor for the controllers and for the hands*.</description>
/// </item>
/// </list>
/// <param name="controllerEvents">The scene <see cref="ControllerEvents"/></param>
/// <param name="controllerBodyPart">An anchor to which an object is attached</param>
/// <param name="handBodyPart">(*Optional*) If the object is attached to the hands, then this parameter specifies the anchor for attaching to the hands</param>
/// </summary>
public sealed class AttachToPlayersBody : MonoBehaviour
{
    [SerializeField] private ControllerEvents _controllerEvents;
    [SerializeField] private Transform _controllerBodyPart;

    [Header("[Optional]")] [SerializeField]
    private Transform _handBodyPart;


    private Vector3 _currentPosition;
    private Vector3 _currentRotation;


    private void Start()
    {
        if (_controllerEvents != null)
        {
            _controllerEvents.ControllerTypeChange += OnAttachChange;
        }

        _currentPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        _currentRotation = new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y,
            transform.localRotation.eulerAngles.z);

        gameObject.transform.parent = _controllerBodyPart;

        OnAttachChange(!OVRPlugin.GetHandTrackingEnabled());

        if (_handBodyPart == null)
        {
            _handBodyPart = _controllerBodyPart;
        }
    }

    private void OnDestroy()
    {
        if (_controllerEvents != null)
        {
            _controllerEvents.ControllerTypeChange -= OnAttachChange;
        }
    }

    private void OnAttachChange(bool isAttachToController)
    {
        if (isAttachToController)
        {
            gameObject.transform.parent = _controllerBodyPart;
            RestoreLocalTransform();
        }
        else
        {
            gameObject.transform.parent = _handBodyPart;
            RestoreLocalTransform();
        }
    }

    private void RestoreLocalTransform()
    {
        transform.localPosition = new Vector3(_currentPosition.x, _currentPosition.y, _currentPosition.z);
        transform.localRotation =
            Quaternion.Euler(new Vector3(_currentRotation.x, _currentRotation.y, _currentRotation.z));
    }
}