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
                if (_type != TileType.Floor)
                {
                    return;
                }
                _weight = value;
                _Label.SetText(value.ToString());
                _Renderer.material.color = GetColorFromWeight(_weight);
            }
        }
        
        public TileType Type 
        {             
            get => _type;
            set
            {
                _type = value;
                _Label.SetText(value.ToString().Substring(0, 1));
                _Renderer.material.color = Color.grey;
            } 
        }
        
        [SerializeField] private TextMeshPro _Label;
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
