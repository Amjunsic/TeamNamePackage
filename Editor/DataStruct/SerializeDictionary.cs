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
        /// target Dictionary�� ���� �����Ͽ� SerializeDictionary ��ü�� �����մϴ�.
        /// </summary>
        /// <param name="target"> ���� ������ Dictionary</param>
        public SerializeDictionary(Dictionary<TKey, TValue> target) : base()
        {
            this.pairs ??= new List<SerializeKeyValuePair<TKey, TValue>> ();
            pairs.Clear();
        }

        //����ȭ �� (��ųʸ� -> ����Ʈ)
        public void OnBeforeSerialize()
        {
            if (this.Count < pairs.Count)
            {
                Debug.Log("�ߺ��� key���� ���� �ذ� ����");
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

        //����ȭ �� (����Ʈ -> ��ųʸ�)
        public void OnAfterDeserialize()
        {
            this.Clear();
            
            foreach (var item in pairs)
            {
                if(!this.TryAdd(item.Key, item.Value))
                {
                    Debug.LogError("�ߺ�Ű ��");
                }
            }
        }



    }
}

