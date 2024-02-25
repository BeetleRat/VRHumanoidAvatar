using UnityEngine;

/// <summary>
/// <para>A class responsible for synchronizing the position of the avatar's hands.</para>
/// This class synchronizes the hands from the LeftHandSynthetic/RightHandSynthetic prefab.
/// 
/// <list type="bullet">
/// <listheader>
/// <description>The class focuses on the name of the bones in the hands. It searches the scene and the passed object for objects with the following names:</description>
/// </listheader>
/// <item><description>b_l_wrist;</description></item>
/// <item><description>b_r_wrist;</description></item>
/// <item><description>b_l_index1;</description></item>
/// <item><description>b_r_index1;</description></item>
/// <item><description>b_l_index2;</description></item>
/// <item><description>b_r_index2;</description></item>
/// <item><description>b_l_index3;</description></item>
/// <item><description>b_r_index3;</description></item>
/// <item><description>b_l_middle1;</description></item>
/// <item><description>b_r_middle1;</description></item>
/// <item><description>b_l_middle2;</description></item>
/// <item><description>b_r_middle2;</description></item>
/// <item><description>b_l_middle3;</description></item>
/// <item><description>b_r_middle3;</description></item>
/// <item><description>b_l_pinky0;</description></item>
/// <item><description>b_r_pinky0;</description></item>
/// <item><description>b_l_pinky1;</description></item>
/// <item><description>b_r_pinky1;</description></item>
/// <item><description>b_l_pinky2;</description></item>
/// <item><description>b_r_pinky2;</description></item>
/// <item><description>b_l_pinky3;</description></item>
/// <item><description>b_r_pinky3;</description></item>
/// <item><description>b_l_ring1;</description></item>
/// <item><description>b_r_ring1;</description></item>
/// <item><description>b_l_ring2;</description></item>
/// <item><description>b_r_ring2;</description></item>
/// <item><description>b_l_ring3;</description></item>
/// <item><description>b_r_ring3;</description></item>
/// <item><description>b_l_thumb0;</description></item>
/// <item><description>b_r_thumb0;</description></item>
/// <item><description>b_l_thumb1;</description></item>
/// <item><description>b_r_thumb1;</description></item>
/// <item><description>b_l_thumb2;</description></item>
/// <item><description>b_r_thumb2;</description></item>
/// <item><description>b_l_thumb3;</description></item>
/// <item><description>b_r_thumb3;</description></item>
/// </list>
/// This class also finds on the stage a display of the player’s hands by the name of the prefab:
/// OVRLeftHandVisual or OVRRightHandVisual
/// </summary>
public sealed class GostHandTransformUpdater : ControllerModel
{
    [SerializeField] private HandType _handType;

    private string _prefix;

    private Transform _wrist;

    private Transform _index1;
    private Transform _index2;
    private Transform _index3;

    private Transform _middle1;
    private Transform _middle2;
    private Transform _middle3;

    private Transform _pinky0;
    private Transform _pinky1;
    private Transform _pinky2;
    private Transform _pinky3;

    private Transform _ring1;
    private Transform _ring2;
    private Transform _ring3;

    private Transform _thumb0;
    private Transform _thumb1;
    private Transform _thumb2;
    private Transform _thumb3;

    private Transform _avatarWrist;

    private Transform _avatarIndex1;
    private Transform _avatarIndex2;
    private Transform _avatarIndex3;

    private Transform _avatarMiddle1;
    private Transform _avatarMiddle2;
    private Transform _avatarMiddle3;

    private Transform _avatarPinky0;
    private Transform _avatarPinky1;
    private Transform _avatarPinky2;
    private Transform _avatarPinky3;

    private Transform _avatarRing1;
    private Transform _avatarRing2;
    private Transform _avatarRing3;

    private Transform _avatarThumb0;
    private Transform _avatarThumb1;
    private Transform _avatarThumb2;
    private Transform _avatarThumb3;

    private void Start()
    {
        _prefix = "";
        switch (_handType)
        {
            case HandType.Left:
                _prefix = "b_l";
                break;
            case HandType.Right:
                _prefix = "b_r";
                break;
        }

        if (_prefix != "")
        {
            CreateHand();
        }
    }

    private void Update()
    {
        MapPosition(_avatarWrist, _wrist);

        MapPosition(_avatarThumb0, _thumb0);
        MapPosition(_avatarThumb1, _thumb1);
        MapPosition(_avatarThumb2, _thumb2);
        MapPosition(_avatarThumb3, _thumb3);

        MapPosition(_avatarIndex1, _index1);
        MapPosition(_avatarIndex2, _index2);
        MapPosition(_avatarIndex3, _index3);

        MapPosition(_avatarMiddle1, _middle1);
        MapPosition(_avatarMiddle2, _middle2);
        MapPosition(_avatarMiddle3, _middle3);

        MapPosition(_avatarRing1, _ring1);
        MapPosition(_avatarRing2, _ring2);
        MapPosition(_avatarRing3, _ring3);

        MapPosition(_avatarPinky0, _pinky0);
        MapPosition(_avatarPinky1, _pinky1);
        MapPosition(_avatarPinky2, _pinky2);
        MapPosition(_avatarPinky3, _pinky3);
    }

    private void CreateHand()
    {
        ParseAvatarHand();
        FindLocalHand();
    }

    private void FindLocalHand()
    {
        var rootLocalHand = GetRootLocalHand();
        _wrist = rootLocalHand.Find(_prefix + "_wrist");

        _index1 = _wrist.Find(_prefix + "_index1").transform;
        _index2 = _index1.Find(_prefix + "_index2").transform;
        _index3 = _index2.Find(_prefix + "_index3").transform;

        _middle1 = _wrist.Find(_prefix + "_middle1").transform;
        _middle2 = _middle1.Find(_prefix + "_middle2").transform;
        _middle3 = _middle2.Find(_prefix + "_middle3").transform;

        _pinky0 = _wrist.Find(_prefix + "_pinky0").transform;
        _pinky1 = _pinky0.Find(_prefix + "_pinky1").transform;
        _pinky2 = _pinky1.Find(_prefix + "_pinky2").transform;
        _pinky3 = _pinky2.Find(_prefix + "_pinky3").transform;

        _ring1 = _wrist.Find(_prefix + "_ring1").transform;
        _ring2 = _ring1.Find(_prefix + "_ring2").transform;
        _ring3 = _ring2.Find(_prefix + "_ring3").transform;

        _thumb0 = _wrist.Find(_prefix + "_thumb0").transform;
        _thumb1 = _thumb0.Find(_prefix + "_thumb1").transform;
        _thumb2 = _thumb1.Find(_prefix + "_thumb2").transform;
        _thumb3 = _thumb2.Find(_prefix + "_thumb3").transform;
    }

    private Transform GetRootLocalHand()
    {
        return GameObject.Find($"OVR{_handType.ToString()}HandVisual")
            .transform.GetChild(0);
    }

    private void ParseAvatarHand()
    {
        _avatarWrist = _avatarControllerModel.transform.Find(_prefix + "_wrist");

        _avatarIndex1 = _avatarWrist.Find(_prefix + "_index1").transform;
        _avatarIndex2 = _avatarIndex1.Find(_prefix + "_index2").transform;
        _avatarIndex3 = _avatarIndex2.Find(_prefix + "_index3").transform;

        _avatarMiddle1 = _avatarWrist.Find(_prefix + "_middle1").transform;
        _avatarMiddle2 = _avatarMiddle1.Find(_prefix + "_middle2").transform;
        _avatarMiddle3 = _avatarMiddle2.Find(_prefix + "_middle3").transform;

        _avatarPinky0 = _avatarWrist.Find(_prefix + "_pinky0").transform;
        _avatarPinky1 = _avatarPinky0.Find(_prefix + "_pinky1").transform;
        _avatarPinky2 = _avatarPinky1.Find(_prefix + "_pinky2").transform;
        _avatarPinky3 = _avatarPinky2.Find(_prefix + "_pinky3").transform;

        _avatarRing1 = _avatarWrist.Find(_prefix + "_ring1").transform;
        _avatarRing2 = _avatarRing1.Find(_prefix + "_ring2").transform;
        _avatarRing3 = _avatarRing2.Find(_prefix + "_ring3").transform;

        _avatarThumb0 = _avatarWrist.Find(_prefix + "_thumb0").transform;
        _avatarThumb1 = _avatarThumb0.Find(_prefix + "_thumb1").transform;
        _avatarThumb2 = _avatarThumb1.Find(_prefix + "_thumb2").transform;
        _avatarThumb3 = _avatarThumb2.Find(_prefix + "_thumb3").transform;
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}