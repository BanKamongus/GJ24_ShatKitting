using UnityEngine;
using System.Collections;
using TMPro;

public class FlickerEffectTMP : MonoBehaviour
{
    public float minFlickerInterval = 0.3f;
    public float maxFlickerInterval = 0.5f;
    public int flickerCount = 5; // Adjust the number of flickers

    private bool isFlickering = false;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        if (textMeshPro == null)
        {
            Debug.LogError("FlickerEffectTMP script requires a TextMeshProUGUI component on the GameObject.");
            enabled = false;
            return;
        }

        // Start the flickering coroutine
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        for (int i = 0; i < flickerCount; i++)
        {
            // Wait for a random interval before toggling visibility
            float flickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval);
            yield return new WaitForSeconds(flickerInterval);

            // Toggle visibility using SetActive
            isFlickering = !isFlickering;
            textMeshPro.enabled = isFlickering;
        }

        // Restart the flickering coroutine after completing the flickerCount
        StartCoroutine(Flicker());
    }
}
