using UnityEngine;

public class DestroyWhen : MonoBehaviour
{
    public float Time = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Invoke the DestroyObject method after 2 seconds
        Invoke("DestroyObject", Time);
    }

    // Method to destroy the GameObject
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
