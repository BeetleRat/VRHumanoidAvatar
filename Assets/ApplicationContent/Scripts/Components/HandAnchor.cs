using UnityEngine;

/// <summary>
/// Hand type. 
/// </summary>
public enum HandType
{
    None = 0,
    Right = 1,
    Left = 2
};

/// <summary>
/// <para>Anchor attached to the hand controllers.</para>
/// This component is used to bind other components/objects to the local player's hand controllers
/// <param name="handType"><see cref="HandType"/> of the controller that will be bound to this component</param>
/// <param name="controllerEvents">The scene <see cref="ControllerEvents"/></param>
/// </summary>
public class HandAnchor : MonoBehaviour
{
    [SerializeField] private HandType _handType;
    [SerializeField] private ControllerEvents _controllerEvents;

    /// <summary>
    /// <see cref="HandType"/>
    /// </summary>
    public HandType HandType => _handType;

    /// <summary>
    /// <see cref="ControllerEvents"/>
    /// </summary>
    public ControllerEvents ControllerChangeTypeEvents => _controllerEvents;
}