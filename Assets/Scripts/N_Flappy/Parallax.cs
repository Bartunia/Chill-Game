using System;
using UnityEngine;

namespace N_Flappy
{
    public class Parallax : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        public float animationSpeed = 1f;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            _meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime,0);
        }
    }
}
