using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamName.Utils.Convert
{
    /// <summary>
    /// �ڷ����� �ٸ� �ڷ������� ��ȯ�ϴ� Ŭ�����Դϴ�.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Ű ����Ʈ�� �� ����Ʈ�� �������� Dictionary ����Ʈ�� �����մϴ�.
        /// </summary>
        /// <typeparam name="TKey">Dictionary�� Ű Ÿ��</typeparam>
        /// <typeparam name="TValue">Dictionary�� �� Ÿ��</typeparam>
        /// <param name="keys">Ű ����Ʈ</param>
        /// <param name="values">�� ����Ʈ</param>
        /// <returns>Ű-�� ���� �����ϴ� Dictionary ����Ʈ</returns>
        /// <exception cref="ArgumentException">Ű�� ���� ������ ���� ���� ���</exception>
        public static List<Dictionary<TKey,TValue>> ConvertListToDictionaryList<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            List<Dictionary<TKey, TValue>> result = new List<Dictionary<TKey, TValue>>();

            if (keys == null)
            {
                throw new ArgumentException(nameof(keys), "Ű ����Ʈ�� null�Դϴ�.");
            }
            if (values == null)
            {
                throw new ArgumentException(nameof(values), "�� ����Ʈ�� null�Դϴ�.");
            }

            if (values.Count % keys.Count != 0)
            {
                throw new ArgumentException("�� ����Ʈ�� ũ�Ⱑ Ű ����Ʈ�� ũ���� ����� �ƴմϴ�.");
            }


            for (int i = 0; i < values.Count; i+= keys.Count)
            {
                Dictionary<TKey, TValue> item = new Dictionary<TKey, TValue>();
                for (int j = 0; j < keys.Count; j++)
                {
                    item.Add(keys[j], values[i+j]);
                }
                result.Add(item);
            }
            return result;
        }
        /*ToDO ���� Dictionary����Ʈ ä���ִ� �޼��� �����
         public static List<Dictionary<TKey, TValue>> ConvertListToDictionaryList<TKey, TValue>(List<TKey> keys, List<TValue> values)*/

        /// <summary>
        /// Ű ����Ʈ�� �� ����Ʈ�� �������� Dictionary�� �����մϴ�.
        /// </summary>
        /// <typeparam name="TKey">Dictionary�� Ű Ÿ��</typeparam>
        /// <typeparam name="TValue">Dictionary�� �� Ÿ��</typeparam>
        /// <param name="keys">Ű ����Ʈ</param>
        /// <param name="values">�� ����Ʈ</param>
        /// <returns>Ű-�� ���� �����ϴ� Dictionary</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="keys"/>, <paramref name="values"/> ����Ʈ�� null�� ��� �߻��մϴ�.
        /// </exception>
        /// <exception cref="ArgumentException">Ű�� ���� ������ ���� ���� ���</exception>
        public static Dictionary<TKey,TValue> ConvertListToDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys),"Ű ����Ʈ�� null�Դϴ�.");
            }
            else if (values == null)
            {
                throw new ArgumentNullException(nameof(values),"�� ����Ʈ�� null�Դϴ�.");
            }

            if (values.Count != keys.Count)
            {
                throw new ArgumentException("�� ����Ʈ�� ũ��� Ű ����Ʈ�� ũ�Ⱑ ���ƾ� �մϴ�.");
            }

            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            for (int i = 0; i < keys.Count; i++)
            {
                result.Add(keys[i], values[i]);
            }

            return result;
        }
       
        /// <summary>
        /// �� ���� ����Ʈ(Ű�� ��)�� ������ �޾� ���� ��ųʸ� ��ü�� ä�� �ֽ��ϴ�.
        /// ��� ��ųʸ��� ���� ������ ��� �������ϴ�.
        /// </summary>
        /// <typeparam name="TKey">Dictionary�� Ű Ÿ��</typeparam>
        /// <typeparam name="TValue">Dictionary�� �� Ÿ��</typeparam>
        /// <param name="keys">Ű ����Ʈ</param>
        /// <param name="values">�� ����Ʈ</param>
        /// <param name="target">������ ä�� ���� ��� ��ųʸ� ��ü�Դϴ�. ���� ������ ��� �������ϴ�.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="keys"/>, <paramref name="values"/>, �Ǵ� <paramref name="target"/> ����Ʈ�� null�� ��� �߻��մϴ�.
        /// </exception>
        /// <exception cref="ArgumentException">Ű�� ���� ������ ���� ���� ���</exception>
        public static void ConvertListToDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values, Dictionary<TKey, TValue> target)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys), "Ű ����Ʈ�� null�Դϴ�.");
            }
            else if (values == null)
            {
                throw new ArgumentNullException(nameof(values), "�� ����Ʈ�� null�Դϴ�.");
            }
            else if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "target Dictionary�� null�Դϴ�.");
            }
            if (values.Count != keys.Count)
            {
                throw new ArgumentException("�� ����Ʈ�� ũ��� Ű ����Ʈ�� ũ�Ⱑ ���ƾ� �մϴ�.");
            }

            target.Clear();

            for (int i = 0; i < keys.Count; i++)
            {
                target.Add(keys[i], values[i]);
            }
        }


        /// <summary>
        /// ������ Dictionary���� ��� Ű�� ���� �����Ͽ� ���� ������ List�� ��ȯ�մϴ�.
        /// </summary>
        /// <typeparam name="TKey">Dictionary�� ���Ե� Ű�� Ÿ���Դϴ�.</typeparam>
        /// <typeparam name="TValue">Dictionary�� ���Ե� ���� Ÿ���Դϴ�.</typeparam>
        /// <param name="pairs">Ű�� ���� ������ ���� Dictionary�Դϴ�.</param>
        /// <returns>
        /// Dictionary�� Ű�� �����ϴ� List�� ���� �����ϴ� List�� ���� ValueTuple�Դϴ�.
        /// (ù ��° ��Ҵ� Ű List, �� ��° ��Ҵ� �� List)
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="pairs"/>�� null�� ��� �߻��մϴ�.
        /// </exception>
        public static (List<TKey> keys, List<TValue> values) ConvertDictionaryToLists<TKey, TValue>(Dictionary<TKey, TValue> pairs)
        {
            if (pairs == null)
            {
                throw new ArgumentException(nameof(pairs), "�Է� Dictionary�� Null�� �� �����ϴ�.");
            }

            List<TKey> keys = new List<TKey>();
            List<TValue> values = new List<TValue>();

            foreach (var item in pairs)
            {
                keys.Add(item.Key);
                values.Add(item.Value);
            }

            return (keys : keys, values : values);
        }
        /*ToDO ���� ����Ʈ�� ä���ִ� �޼��� �����
      public static void ConvertDictionaryToLists<TKey, TValue>(Dictionary<TKey, TValue> pairs, List<TKey> targetKey, List<TValue> targetValue)*/
    }
}