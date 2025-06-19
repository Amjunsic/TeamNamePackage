using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TeamName.DataStruct
{
    [System.Serializable]
    public class SerializeDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<SerializeKeyValuePair<TKey, TValue>> pairs = new List<SerializeKeyValuePair<TKey, TValue>> ();

        public SerializeDictionary() : base() { }

        /// <summary>
        /// target Dictionary의 값을 복사하여 SerializeDictionary 객체를 생성합니다.
        /// </summary>
        /// <param name="target"> 값을 복사할 Dictionary</param>
        public SerializeDictionary(Dictionary<TKey, TValue> target) : base()
        {
            this.pairs ??= new List<SerializeKeyValuePair<TKey, TValue>> ();
            pairs.Clear();
        }

        //직렬화 전 (딕셔너리 -> 리스트)
        public void OnBeforeSerialize()
        {
            if (this.Count < pairs.Count)
            {
                Debug.Log("중복된 key값이 있음 해결 ㄱㄱ");
                return;
            }

            pairs.Clear ();

            foreach (var item in this)
            {
                pairs.Add(new SerializeKeyValuePair<TKey, TValue>()
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }
        }

        //직렬화 후 (리스트 -> 딕셔너리)
        public void OnAfterDeserialize()
        {
            this.Clear();
            
            foreach (var item in pairs)
            {
                if(!this.TryAdd(item.Key, item.Value))
                {
                    Debug.LogError("중복키 값");
                }
            }
        }



    }
}

