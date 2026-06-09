using Manager;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    public string GetInteractText()
    {
        return "Sleep";
    }

    public void Interact()
    {
        GameManager.Instance.Sleep();
    }
}
