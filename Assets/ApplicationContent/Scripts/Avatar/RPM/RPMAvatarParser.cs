using UnityEngine;

/// <summary>
/// <para>Class used to replace the mesh and material of the original model with the specified one.</para>
/// </summary>
[System.Serializable]
public class MeshSwapper
{
    /// <summary>
    /// Whether this model should be used.
    /// </summary>
    public bool IsActive;

    /// <summary>
    /// SkinnedMeshRenderer of the original model.
    /// In this model meshes and material will be replaced.
    /// </summary>
    public SkinnedMeshRenderer Original;

    /// <summary>
    /// <para>SkinnedMeshRenderer, from which the meshes and materials will be taken.</para>
    /// <remarks>
    /// This model must be present in the scene.
    /// Before using it, it must be spawned using Instantiate.
    /// </remarks>
    /// </summary>
    /// <returns></returns>
    public SkinnedMeshRenderer NewMesh;

    /// <summary>
    /// <para>
    /// The method of replacing the mesh and materials in the original model
    /// with the mesh and materials from the specified one.
    /// </para>
    /// </summary>
    public void SwapMesh()
    {
        Original.sharedMesh = NewMesh.sharedMesh;
        Original.materials = NewMesh.materials;
    }
}

/// <summary>
/// Gender of the model's skeleton.
/// </summary>
public enum Gender
{
    Male,
    Female
};

/// <summary>
/// <para>Component for parsing the Ready Player Me model into avatar.</para>
/// This component takes the model from <see cref="RPMAvatarInfo"/>
/// and attaches selected parts of it to the skeleton on which this component is used.
/// <param name="skeletonGender">the gender of the model's skeleton</param>
/// <param name="skeletonGender">the <see cref="RPMAvatarInfo"/></param>
/// <param name="eyeLeft">Whether to use the left eye model in the avatar</param>
/// <param name="eyeRight">Whether to use the right eye model in the avatar</param>
/// <param name="head">Whether to use the head model in the avatar</param>
/// <param name="teeth">Whether to use the teeth model in the avatar</param>
/// <param name="body">Whether to use the body model in the avatar</param>
/// <param name="outfitBottom">Whether to use the pants model in the avatar</param>
/// <param name="outfitFootwear">Whether to use a shoe model in the avatar</param>
/// <param name="outfitTop">Whether to use a model of outerwear in the avatar</param>
/// <param name="hair">Whether to use the hair model in the avatar</param>
/// <param name="beard">Whether to use the beard model in the avatar</param>
/// <param name="glasses">Whether to use the glasses model in the avatar</param>
/// </summary>
public class RPMAvatarParser : MonoBehaviour
{
    [SerializeField] private Gender _skeletonGender;
    [SerializeField] private RPMAvatarInfo _rpmAvatarInfo;

    [Header("Active avatar parts")] [SerializeField]
    private bool _eyeLeft;

    [SerializeField] private bool _eyeRight;
    [SerializeField] private bool _head;
    [SerializeField] private bool _teeth;
    [SerializeField] private bool _body;
    [SerializeField] private bool _outfitBottom;
    [SerializeField] private bool _outfitFootwear;
    [SerializeField] private bool _outfitTop;
    [SerializeField] private bool _hair;
    [SerializeField] private bool _beard;
    [SerializeField] private bool _glasses;

    private MeshSwapper _eyeLeftMS;
    private MeshSwapper _eyeRightMS;
    private MeshSwapper _headMS;
    private MeshSwapper _teethMS;
    private MeshSwapper _bodyMS;
    private MeshSwapper _outfitBottomMS;
    private MeshSwapper _outfitFootwearMS;
    private MeshSwapper _outfitTopMS;
    private MeshSwapper _hairMS;
    private MeshSwapper _beardMS;
    private MeshSwapper _glassesMS;

    /// <summary>
    /// The gender of the model's skeleton.
    /// </summary>
    public Gender SkeletonGender => _skeletonGender;

    private void Start()
    {
        CreateMeshSwappers();
        SetMeshesToSkeleton();
    }

    private void CreateMeshSwappers()
    {
        _eyeLeftMS = new MeshSwapper();
        _eyeLeftMS.IsActive = _eyeLeft;
        _eyeRightMS = new MeshSwapper();
        _eyeRightMS.IsActive = _eyeRight;
        _headMS = new MeshSwapper();
        _headMS.IsActive = _head;
        _teethMS = new MeshSwapper();
        _teethMS.IsActive = _teeth;
        _bodyMS = new MeshSwapper();
        _bodyMS.IsActive = _body;
        _outfitBottomMS = new MeshSwapper();
        _outfitBottomMS.IsActive = _outfitBottom;
        _outfitFootwearMS = new MeshSwapper();
        _outfitFootwearMS.IsActive = _outfitFootwear;
        _outfitTopMS = new MeshSwapper();
        _outfitTopMS.IsActive = _outfitTop;
        _hairMS = new MeshSwapper();
        _hairMS.IsActive = _hair;
        _beardMS = new MeshSwapper();
        _beardMS.IsActive = _beard;
        _glassesMS = new MeshSwapper();
        _glassesMS.IsActive = _glasses;
    }

    private void SetMeshesToSkeleton()
    {
        SkinnedMeshRenderer[] children = GetComponentsInChildren<SkinnedMeshRenderer>();
        GameObject tempObject = Instantiate(_rpmAvatarInfo.GetReadyPlayerMeAvatar());
        SkinnedMeshRenderer[] rpmChildrens = tempObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < children.Length; i++)
        {
            switch (children[i].name)
            {
                case "Renderer_EyeLeft":
                    SetMeshes("Renderer_EyeLeft", ref _eyeLeftMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_EyeRight":
                    SetMeshes("Renderer_EyeRight", ref _eyeRightMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Head":
                    SetMeshes("Renderer_Head", ref _headMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Teeth":
                    SetMeshes("Renderer_Teeth", ref _teethMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Body":
                    SetMeshes("Renderer_Body", ref _bodyMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Outfit_Bottom":
                    SetMeshes("Renderer_Outfit_Bottom", ref _outfitBottomMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Outfit_Footwear":
                    SetMeshes("Renderer_Outfit_Footwear", ref _outfitFootwearMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Outfit_Top":
                    SetMeshes("Renderer_Outfit_Top", ref _outfitTopMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Hair":
                    SetMeshes("Renderer_Hair", ref _hairMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Beard":
                    SetMeshes("Renderer_Beard", ref _beardMS, ref children[i], rpmChildrens);
                    break;
                case "Renderer_Glasses":
                    SetMeshes("Renderer_Glasses", ref _glassesMS, ref children[i], rpmChildrens);
                    break;
            }
        }

        Destroy(tempObject);
    }


    private void SetMeshes(string name,
        ref MeshSwapper meshSwapper, ref SkinnedMeshRenderer child,
        SkinnedMeshRenderer[] rpmChildrens)
    {
        if (meshSwapper.IsActive)
        {
            meshSwapper.Original = child;
            foreach (SkinnedMeshRenderer rpmChildren in rpmChildrens)
            {
                if (rpmChildren.name == name)
                {
                    meshSwapper.NewMesh = rpmChildren;
                    break;
                }
            }

            if (meshSwapper.NewMesh == null)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                meshSwapper.SwapMesh();
            }
        }
        else
        {
            child.gameObject.SetActive(false);
        }
    }
}