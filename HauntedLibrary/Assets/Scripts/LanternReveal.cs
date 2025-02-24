using UnityEngine;

public class LanternReveal : MonoBehaviour
{
    public Light lanternLight; // The light source on the lantern
    public float revealRange = 3.0f; // How far the lantern reveals objects
    public LayerMask hiddenObjectsLayer; // Layer for hidden objects

    void Update()
    {
        RevealHiddenObjects();
    }

    void RevealHiddenObjects()
    {
        Collider[] hiddenObjects = Physics.OverlapSphere(transform.position, revealRange, hiddenObjectsLayer);

        foreach (Collider obj in hiddenObjects)
        {
            HiddenObject hiddenScript = obj.GetComponent<HiddenObject>();
            if (hiddenScript != null)
            {
                hiddenScript.Reveal(true);
            }
        }
    }
}
