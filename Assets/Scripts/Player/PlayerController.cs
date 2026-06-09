using UnityEngine;
using UnityEngine.InputSystem;

public class BestiaryTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bestiaryUI;

    void Update()
    {
        // Movement
        float moveX = 0f;
        float moveZ = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) moveZ += 1f;
            if (Keyboard.current.sKey.isPressed) moveZ -= 1f;
            if (Keyboard.current.aKey.isPressed) moveX -= 1f;
            if (Keyboard.current.dKey.isPressed) moveX += 1f;
        }
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        transform.Translate(moveDirection * Time.deltaTime * 5f);

        // Jika pemain menekan tombol 'J', toggle tampilan buku bestiary
        if (Keyboard.current != null && Keyboard.current.jKey.wasPressedThisFrame)
        {
            bool isCurrentlyActive = bestiaryUI.activeSelf;
            bool newState = !isCurrentlyActive;
            
            bestiaryUI.SetActive(newState);

            // Atur kursor berdasarkan status buku
            if (newState == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}