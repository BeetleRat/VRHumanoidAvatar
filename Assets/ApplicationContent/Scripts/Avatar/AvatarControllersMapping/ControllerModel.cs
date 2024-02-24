using UnityEngine;

/// <summary>
/// Type of appearance of controllers.
/// </summary>
public enum ControllerType
{
    OculusController,
    HandsPrefabs
}

/// <summary>
/// <para>Superclass storing information about the displayed avatar controller.</para>
/// </summary>
public class ControllerModel : MonoBehaviour
{
    /// <summary>
    /// Displayed controller model.
    /// </summary>
    [SerializeField] protected GameObject _avatarControllerModel;

    /// <summary>
    /// <see cref="ControllerType"/> of displayed controller.
    /// </summary>
    [SerializeField] protected ControllerType _type;

    /// <summary>
    /// <list type="bullet">
    /// <listheader>
    /// <description>Controller type:</description>
    /// </listheader>
    /// <item>
    /// <description>OVRInput.Controller.LTouch;</description>
    /// </item>
    /// <item>
    /// <description>OVRInput.Controller.RTouch;</description>
    /// </item>
    /// <item>
    /// <description>OVRInput.Controller.LHand;</description>
    /// </item>
    /// <item>
    /// <description>OVRInput.Controller.RHand.</description>
    /// </item>
    /// </list>
    /// </summary>
    [SerializeField] protected OVRInput.Controller _controllerType;

    /// <summary>
    /// Displayed controller model.
    /// </summary>
    public GameObject AvatarControllerModel => _avatarControllerModel;

    /// <summary>
    /// <list type="bullet">
    /// <listheader>
    /// <description>Controller type:</description>
    /// </listheader>
    /// <item>
    /// <description>OVRInput.Controller.LTouch;</description>
    /// </item>
    /// <item>
    /// <description>OVRInput.Controller.RTouch;</description>
    /// </item>
    /// <item>
    /// <description>OVRInput.Controller.LHand;</description>
    /// </item>
    /// <item>
    /// <description>OVRInput.Controller.RHand.</description>
    /// </item>
    /// </list>
    /// </summary>
    public ControllerType Type => _type;

    /// <summary>
    /// <para>Sets the existence status of this object.</para>
    /// When we switch to another controller mapping,
    /// we must disable the previous one and enable the new mapping.
    /// </summary>
    /// <param name="isActive">the existence status of this object</param>
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}