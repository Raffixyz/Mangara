using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI interactionText;

    [Header("Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0, 0.5f, 0);

    private Transform seedTarget;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (seedTarget != null && interactionText.gameObject.activeSelf)
        {
            Vector2 posisiLayar = Camera.main.WorldToScreenPoint(seedTarget.position + offset);
            interactionText.transform.position = posisiLayar;
        }
    }

    public void ShowText(string text, Transform target)
    {
        interactionText.text = text;
        seedTarget = target;
        interactionText.gameObject.SetActive(true);
    }

    public void HideText()
    {
        seedTarget = null;
        interactionText.gameObject.SetActive(false);
    }
}