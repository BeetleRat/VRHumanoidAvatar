using System.Collections.Generic;
using UnityEngine;

/**

@param avatarsListContent UI поле, в которое будет отображен список аватаров.
@param avatarListItemPrefab Prefab отображаемого элемента списка. Данный prefab должен содержать компонент AvatarListItem.
@see ComponentCatcher; NetworkManager; AvatarListItem; AvatarInfo
 */
/// <summary>
/// <para>A class that controls all items in the list of available avatars.</para>
/// This class searches the resources folder for AvatarInfo avatars
/// and from them compiles a list of avatars
/// and displays it in the specified UI.
/// <param name="avatarsListContent">UI field that will display avatars list</param>
/// <param name="avatarListItemPrefab">
/// Prefab of the displayed list item.
/// This prefab must contain an <see cref="AvatarListItem"/> component
/// </param>
/// <param name="avatarsFolder">Path to folder containing avatar prefabs</param>
/// </summary>
public class AvatarList : MonoBehaviour
{
    [SerializeField] private Transform _avatarsListContent;
    [SerializeField] private GameObject _avatarListItemPrefab;

    [Tooltip("All avatars should be stored in a subfolder of the Resources folder.")] [SerializeField]
    private string _avatarsFolderPath;

    private List<AvatarListItem> avatarListItems;
    private AvatarListItem currentAvatar;

    public GameObject CurrentAvatar => currentAvatar?.AvatarPrefab;

    private void Start()
    {
        UpdateAvatarList();
    }

    /// <summary>
    /// <para>Method that changes the currently selected list item.</para>
    /// This method makes all items in the list unselected, except the one passed to it in the input parameter.
    /// </summary>
    /// <param name="newSelectedElement">The currently selected <see cref="AvatarListItem"/></param>
    public void ChangeCurrentElement(AvatarListItem newSelectedElement)
    {
        foreach (AvatarListItem item in avatarListItems)
        {
            if (item == newSelectedElement)
            {
                item.IsSelected = true;
                currentAvatar = item;
            }
            else
            {
                item.IsSelected = false;
            }
        }
    }

    private void UpdateAvatarList()
    {
        DestroyPreviousList();

        List<AvatarListItem> newAvatarList = CreateEmptyList();

        FillListFromResourcesFolder(ref newAvatarList);

        SetFirstItemSelected(ref newAvatarList);
    }

    private void DestroyPreviousList()
    {
        foreach (Transform trans in _avatarsListContent)
        {
            Destroy(trans.gameObject);
        }
    }

    private List<AvatarListItem> CreateEmptyList()
    {
        if (avatarListItems == null)
        {
            avatarListItems = new List<AvatarListItem>();
        }

        avatarListItems.Clear();

        return avatarListItems;
    }

    private void FillListFromResourcesFolder(ref List<AvatarListItem> avatarsList)
    {
        AvatarInfo[] avatars = Resources.LoadAll<AvatarInfo>(_avatarsFolderPath);
        foreach (AvatarInfo avatar in avatars)
        {
            if (avatar.IsActive)
            {
                AvatarListItem item =
                    Instantiate(_avatarListItemPrefab, _avatarsListContent)
                        .GetComponent<AvatarListItem>();
                item.ControllerList = this;
                item.IsSelected = false;
                item.AvatarImage = avatar.AvatarImage;
                item.AvatarPrefab = avatar.AvatarPrefab;
                avatarsList.Add(item);
            }
        }
    }

    private void SetFirstItemSelected(ref List<AvatarListItem> avatarsList)
    {
        if (avatarsList.Count > 0)
        {
            avatarsList[0].IsSelected = true;
            currentAvatar = avatarsList[0];
        }
    }
}