using UnityEngine;

/// <summary>
/// <para>Сlass that plays the animation of avatar controllers.</para>
/// This class updates the animation of avatar controller models,
/// depending on the buttons/sticks/triggers pressed on the local controller.
/// <param name="animator">The avatar controller animator</param>
/// </summary>
public sealed class ControllerAnimationUpdater : ControllerModel
{
    [SerializeField] private Animator _animator;

    private void Update()
    {
        UpdateAnimation(_animator);
    }

    private void UpdateAnimation(Animator animator)
    {
        animator.SetFloat("Button 1", OVRInput.Get(OVRInput.Button.One, _controllerType) ? 1.0f : 0.0f);
        animator.SetFloat("Button 2", OVRInput.Get(OVRInput.Button.Two, _controllerType) ? 1.0f : 0.0f);
        animator.SetFloat("Button 3", OVRInput.Get(OVRInput.Button.Start, _controllerType) ? 1.0f : 0.0f);

        animator.SetFloat("Joy X", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, _controllerType).x);
        animator.SetFloat("Joy Y", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, _controllerType).y);

        animator.SetFloat("Trigger", OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, _controllerType));
        animator.SetFloat("Grip", OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, _controllerType));
    }
}