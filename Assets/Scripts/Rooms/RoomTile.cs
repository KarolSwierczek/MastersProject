using TMPro;
using UnityEngine;
using Data;
using UnityEngine.Serialization;

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
                _TypeText.SetText(_weight.ToString());
                _Renderer.material.color = GetColorFromWeight(_weight);
            }
        }
        
        public TileType Type 
        {             
            get => _type;
            set
            {
                _type = value;
                _TypeText.SetText(value.ToString());
            } 
        }
        
        [FormerlySerializedAs("_WeightText")] 
        [SerializeField] private TextMeshPro _TypeText;
        [SerializeField] private MeshRenderer _Renderer;
        [SerializeField] private RoomGenerationSettings _Settings;
        [SerializeField] private Gradient _WeightColorScale;

        private int _weight;
        private TileType _type;

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
