using System;

namespace Chapter6
{
    public class GenericQueue<T> where T : IResettable
    {
        private readonly T[] _items;

        private int _indexHead = 0;
        private int _indexTail = 0;

        public GenericQueue() : this(100)
        { }

        public GenericQueue(int size)
        {
            _items = new T[size];
        }

        public void Enqueue(T value)
        {
            if (_indexHead - _indexTail + 1 >= _items.Length) 
                throw new ApplicationException("Queue is full");

            _items[++_indexHead] = value;
        }

        public T Dequeue()
        {
            if (_indexHead <= 0)
                return default(T);

            return _items[_indexTail++];
        }

        public void Reset()
        {
            for (int i = _indexTail; i < _indexHead; i++)
            {
                _items[i].Reset();
            }
        }

    }
}
