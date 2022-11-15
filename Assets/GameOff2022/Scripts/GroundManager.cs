using System.Collections.Generic;
using UnityEngine;

namespace LighterThanAir
{
    public class GroundManager : MonoBehaviour
    {
        [SerializeField] private GameObject groundChunkPrefab;
        [SerializeField] private float panSpeed = 100.0f;

        private Material groundMaterial;

        private void Start()
        {
            groundMaterial = groundChunkPrefab.GetComponent<MeshRenderer>().sharedMaterial;
        }

        private void Update()
        {
            // Scroll the texture.
            groundMaterial.mainTextureOffset += -Vector2.up * panSpeed * Time.deltaTime;
        }
    }
}