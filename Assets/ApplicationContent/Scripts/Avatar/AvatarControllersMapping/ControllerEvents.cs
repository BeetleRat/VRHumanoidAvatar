using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// <para>Class that tracks the switch from controllers to hands</para>
/// </summary>
public sealed class ControllerEvents : MonoBehaviour
{
    /// <summary>
    /// The event of switching controllers to hands or vice versa.
    /// </summary>
    public UnityAction<bool> ControllerTypeChange;

    private bool _isAttachToController;

    /// <summary>
    /// <para>Attach current state.</para>
    /// <returns>true if controllers are currently in use; false if hands are currently in use</returns>
    /// </summary>
    public bool IsAttachToController => _isAttachToController;

    private void Start()
    {
        _isAttachToController = OVRPlugin.GetHandTrackingEnabled();
        ControllerTypeChange?.Invoke(_isAttachToController);
    }

    private void Update()
    {
        if (OVRPlugin.GetHandTrackingEnabled())
        {
            if (_isAttachToController)
            {
                _isAttachToController = false;
                ControllerTypeChange?.Invoke(_isAttachToController);
            }
        }
        else
        {
            if (!_isAttachToController)
            {
                _isAttachToController = true;
                ControllerTypeChange?.Invoke(_isAttachToController);
            }
        }
    }
}