using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para>Class of <see cref="AvatarList"/> item.</para>
/// <param name="avatarImage">the avatar image</param>
/// <param name="selectCheckboxImage">the selected checkbox image</param>
/// <param name="unselectCheckboxImage">the unselected checkbox image</param>
/// <param name="selectFrameImage">the selected frame image</param>
/// </summary>
public class AvatarListItem : MonoBehaviour
{
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Image _selectCheckboxImage;
    [SerializeField] private Image _unselectCheckboxImage;
    [SerializeField] private Image _selectFrameImage;

    private AvatarList _controllerList;

    private bool _isSelected;
    private GameObject _avatarPrefab;

    /// <summary>
    /// The avatar image.
    /// </summary>
    public Sprite AvatarImage
    {
        set => _avatarImage.sprite = value;
    }

    /// <summary>
    /// <see cref="AvatarList"/> controlling this item.
    /// </summary>
    public AvatarList ControllerList
    {
        set => _controllerList = value;
    }

    /// <summary>
    /// Whether this list item is selected.
    /// </summary>
    public bool IsSelected
    {
        set => _isSelected = value;
    }

    /// <summary>
    /// Spawned avatar prefab.
    /// </summary>
    public GameObject AvatarPrefab
    {
        get => _avatarPrefab;
        set => _avatarPrefab = value;
    }

    private void Update()
    {
        _selectCheckboxImage.enabled = _isSelected;
        _selectFrameImage.enabled = _isSelected;
        _unselectCheckboxImage.enabled = !_isSelected;
    }

    /// <summary>
    /// <para>Method that selects the given element if it is not selected.</para>
    /// </summary>
    public void Select()
    {
        if (!_isSelected)
        {
            _controllerList?.ChangeCurrentElement(this);
        }
    }
}