using System;

namespace Utils
{
    
    public class MinHeap<T> where T : IComparable
    {
        protected readonly T[] Elements;
        private int _size;

        public MinHeap(int size)
        {
            Elements = new T[size];
        }

        private static int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private static int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private static int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private static bool IsRoot(int elementIndex) => elementIndex == 0;

        private T GetLeftChild(int elementIndex) => Elements[GetLeftChildIndex(elementIndex)];
        private T GetRightChild(int elementIndex) => Elements[GetRightChildIndex(elementIndex)];
        private T GetParent(int elementIndex) => Elements[GetParentIndex(elementIndex)];

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = Elements[firstIndex];
            Elements[firstIndex] = Elements[secondIndex];
            Elements[secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public T Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return Elements[0];
        }

        public T Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = Elements[0];
            Elements[0] = Elements[_size - 1];
            _size--;

            ReCalculateDown();

            return result;
        }

        public void Add(T element)
        {
            if (_size == Elements.Length)
                throw new IndexOutOfRangeException();

            Elements[_size] = element;
            _size++;

            ReCalculateUp();
        }
        

        private void ReCalculateDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).CompareTo(GetLeftChild(index)) < 0 )
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (Elements[smallerIndex].CompareTo(Elements[index]) >= 0 )
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void ReCalculateUp()
        {
            var index = _size - 1;
            while (!IsRoot(index) && Elements[index].CompareTo(GetParent(index)) < 0 )
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }
}