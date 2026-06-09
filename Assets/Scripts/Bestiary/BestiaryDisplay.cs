using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public struct MangrovePageUI
{
    [Header("Left Side (BackContent)")]
    public Image imageVisual;
    public TextMeshProUGUI name;

    [Header("Right Side (FrontContent)")]
    public TextMeshProUGUI description;
}

public class BestiaryDisplay : MonoBehaviour
{
    [SerializeField] private List<MangrovePageUI> UIlist;

    public void UpdatePage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= UIlist.Count)
        {
            return;
        }

        var manager = BestiaryManager.Instance;
        if (manager == null)
        {
            Debug.LogWarning("BestiaryManager instance is not available.", this);
            return;
        }

        if (!manager.TryGetMangroveData(pageIndex, out MangroveData data))
        {
            Debug.LogWarning($"No bestiary data for page index {pageIndex}.", this);
            return;
        }

        bool isUnlocked = manager.IsUnlocked(data.type);
        MangrovePageUI ui = UIlist[pageIndex];

        if (isUnlocked)
        {
            ui.name.text = data.mangroveName;
            ui.description.text = data.description;
            ui.imageVisual.sprite = data.mangroveImage;
            ui.imageVisual.color = Color.white;
        }
        else
        {
            ui.name.text = "???";
            ui.description.text = "Temukan bibit mangrove ini untuk membuka informasinya.";
            ui.imageVisual.sprite = data.mangroveImage;
            ui.imageVisual.color = Color.black;
        }
    }
}