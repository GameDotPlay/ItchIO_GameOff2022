using UnityEngine;

public class DisableRenderer : MonoBehaviour
{
    void Start()
    {
        // Disables the mesh renderer at runtime.
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}