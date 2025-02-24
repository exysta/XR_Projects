using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = false; // Initially hidden
    }

    public void Reveal(bool isRevealed)
    {
        objectRenderer.enabled = isRevealed;
    }
}
