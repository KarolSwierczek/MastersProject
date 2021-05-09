using System;
using System.Collections.Generic;
using Pathfinding;
using Random = UnityEngine.Random;

namespace Rooms.ObstacleGeneration
{
    public class WeightArray
    {
        private readonly int[] _weights;
        private int _sumOfWeights;

        public WeightArray(IReadOnlyList<Node> nodes)
        {
            _weights = new int[nodes.Count];
            for (var i = 0; i < nodes.Count; i++)
            {
                _weights[i] = nodes[i].Weight + _sumOfWeights;
                _sumOfWeights += nodes[i].Weight;
            }
        }

        public int GetRandomWeightedIndex()
        {
            var targetValue = Random.Range(0, _sumOfWeights);
            var leftIndex = 0;
            var rightIndex = _weights.Length -1;

            while (leftIndex <= rightIndex)
            {
                var midIndex = (leftIndex + rightIndex) / 2;
                if (midIndex - 1 > 0 && _weights[midIndex - 1] > targetValue)
                {
                    rightIndex = midIndex - 1;
                }
                else if (midIndex + 1 < _weights.Length && _weights[midIndex + 1] <= targetValue)
                {
                    leftIndex = midIndex + 1;
                }
                else
                {
                    return midIndex;
                }
            }

            throw new Exception($"cannot find the index for value {targetValue} in the weights array!");
        }

        public void UpdateWeight(int index, int weight)
        {
            var previousValue = _weights[index];
            if (index - 1 >= 0)
            {
                previousValue -= _weights[index - 1];
            }

            var difference = weight - previousValue;
            
            for (var i = index; i < _weights.Length; i++)
            {
                _weights[i] += difference;
            }

            _sumOfWeights += difference;
        }
    }
}