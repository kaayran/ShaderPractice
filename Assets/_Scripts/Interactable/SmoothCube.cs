using System;
using System.Threading.Tasks;
using _Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Interactable
{
    [RequireComponent(typeof(Collider))]
    public class SmoothCube : MonoBehaviour, IDissolvable, IRefreshable
    {
        [SerializeField] private AnimationCurve _speedCurve;
        [SerializeField] private float _dissolveSpeed = 1f;

        private MeshRenderer _meshRenderer;

        private readonly int _dissolveProperty = Shader.PropertyToID("_Dissolve");
        private readonly int _noiseStrength = Shader.PropertyToID("_Noise_Strength");
        private readonly int _edgeColor = Shader.PropertyToID("_Edge_Color");

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            InitializeDissolve();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Refresh();
            }
        }

        private void OnMouseDown()
        {
            Dissolve();
        }

        public void InitializeDissolve()
        {
            var material = _meshRenderer.sharedMaterial;
            material.SetFloat(_dissolveProperty, 1f);
            material.SetFloat(_noiseStrength, Random.Range(0.1f, 0.5f));
            material.SetColor(_edgeColor, Random.ColorHSV(0f, 1f,
                .9f, 1f, .9f, 1f));
        }

        public async void Dissolve()
        {
            var material = _meshRenderer.sharedMaterial;

            while (material.GetFloat(_dissolveProperty) > 0f)
            {
                var dissolveValue = material.GetFloat(_dissolveProperty);

                var value = _speedCurve.Evaluate(Time.deltaTime * _dissolveSpeed);
                material.SetFloat(_dissolveProperty, dissolveValue - value);
                await Task.Yield();
            }
        }

        public void Refresh()
        {
            InitializeDissolve();
        }
    }
}