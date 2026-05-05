using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float rotationSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the sun
        sun.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        // Adjust the intensity of the sun based on its angle
        sun.intensity = Mathf.Clamp01(-sun.transform.forward.y);
    }
}
