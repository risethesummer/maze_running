using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeRunning.Utils.Collections
{
    [System.Serializable]
    public struct PairData<TK, TV>
    {
        [field: SerializeField] public TK Key { get; set; }
        [field: SerializeField] public TV Value { get; set; }

        public PairData(TK k, TV v)
        {
            this.Key = k;
            this.Value = v;
        }
    }
    
    [System.Serializable]
    public class SerializableDictionary<TK, TV> : IEnumerable<KeyValuePair<TK, TV>>
    {
        [SerializeField] protected PairData<TK, TV>[] data;
        public IDictionary<TK, TV> Dict { get; private set; }
        public void Init()
        {
            Dict = new Dictionary<TK, TV>();
            foreach (var d in data)
                Dict.Add(d.Key, d.Value);
        }
        
        public ICollection<TV> Values => Dict.Values;
        public ICollection<TK> Keys => Dict.Keys;
        public bool TryGetValue(TK k, out TV v) => Dict.TryGetValue(k, out v);
        public TV this[TK key]
        {
            get => Dict[key];
            set => Dict[key] = value;
        } 
        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator() => Dict.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    [System.Serializable]
    public class NoKeySerializableDictionary<TK, TV> : IEnumerable<KeyValuePair<TK, TV>> where TV : IIdentityElement<TK>
    {
        [field: SerializeField] public TV[] Data { get; set; }
        public IDictionary<TK, TV> Dict { get; private set; }
        public void Init()
        {
            Dict = new Dictionary<TK, TV>();
            foreach (var d in Data)
                Dict.Add(d.Id, d);
        }
        public ICollection<TV> Values => Dict.Values;
        public ICollection<TK> Keys => Dict.Keys;
        public bool TryGetValue(TK k, out TV v) => Dict.TryGetValue(k, out v);
        public TV this[TK key]
        {
            get => Dict[key];
            set => Dict[key] = value;
        } 
        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator() => Dict.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public interface IIdentityElement<out TK>
    {
        public TK Id { get; }
    }
}
