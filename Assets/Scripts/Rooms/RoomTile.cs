using TMPro;
using UnityEngine;
using Data;

namespace Rooms
{
    public class RoomTile : MonoBehaviour
    {
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                _WeightText.SetText(_weight.ToString());
                _Renderer.material.color = GetColorFromWeight(_weight);
            }
        }
        
        [SerializeField] private TextMeshPro _WeightText;
        [SerializeField] private MeshRenderer _Renderer;
        [SerializeField] private RoomGenerationSettings _Settings;
        [SerializeField] private Gradient _WeightColorScale;

        private int _weight;

        private void Start()
        {
            transform.localScale = new Vector3(_Settings.TileSize, 1f, _Settings.TileSize);
        }

        private Color GetColorFromWeight(int weight)
        {
            var t = (float)weight / _Settings.MaxTileWeight;
            return _WeightColorScale.Evaluate(t);
        }
    }
}
