using System;
using UnityEngine;

namespace _Scripts.Interactable
{
    [RequireComponent(typeof(Collider))]
    public class DissolveObject : MonoBehaviour
    {
        [SerializeField] private Material _dissolveMaterial;

        private MeshRenderer _meshRenderer;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnMouseDown()
        {
            _meshRenderer.sharedMaterial = _dissolveMaterial;
        }
    }
}