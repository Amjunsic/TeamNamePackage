using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamName.Utils.Convert
{
    /// <summary>
    /// 자료형을 다른 자료형으로 변환하는 클래스입니다.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// 키 리스트와 값 리스트를 바탕으로 Dictionary 리스트를 생성합니다.
        /// </summary>
        /// <typeparam name="TKey">Dictionary의 키 타입</typeparam>
        /// <typeparam name="TValue">Dictionary의 값 타입</typeparam>
        /// <param name="keys">키 리스트</param>
        /// <param name="values">값 리스트</param>
        /// <returns>키-값 쌍을 저장하는 Dictionary 리스트</returns>
        /// <exception cref="ArgumentException">키와 값의 개수가 맞지 않을 경우</exception>
        public static List<Dictionary<TKey,TValue>> ConvertListToDictionaryList<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            List<Dictionary<TKey, TValue>> result = new List<Dictionary<TKey, TValue>>();

            if (keys == null)
            {
                throw new ArgumentException(nameof(keys), "키 리스트가 null입니다.");
            }
            if (values == null)
            {
                throw new ArgumentException(nameof(values), "값 리스트가 null입니다.");
            }

            if (values.Count % keys.Count != 0)
            {
                throw new ArgumentException("값 리스트의 크기가 키 리스트의 크기의 배수가 아닙니다.");
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
        /*ToDO 기존 Dictionary리스트 채워넣는 메서드 만들기
         public static List<Dictionary<TKey, TValue>> ConvertListToDictionaryList<TKey, TValue>(List<TKey> keys, List<TValue> values)*/

        /// <summary>
        /// 키 리스트와 값 리스트를 바탕으로 Dictionary를 생성합니다.
        /// </summary>
        /// <typeparam name="TKey">Dictionary의 키 타입</typeparam>
        /// <typeparam name="TValue">Dictionary의 값 타입</typeparam>
        /// <param name="keys">키 리스트</param>
        /// <param name="values">값 리스트</param>
        /// <returns>키-값 쌍을 저장하는 Dictionary</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="keys"/>, <paramref name="values"/> 리스트가 null인 경우 발생합니다.
        /// </exception>
        /// <exception cref="ArgumentException">키와 값의 개수가 맞지 않을 경우</exception>
        public static Dictionary<TKey,TValue> ConvertListToDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys),"키 리스트가 null입니다.");
            }
            else if (values == null)
            {
                throw new ArgumentNullException(nameof(values),"값 리스트가 null입니다.");
            }

            if (values.Count != keys.Count)
            {
                throw new ArgumentException("값 리스트의 크기와 키 리스트의 크기가 같아야 합니다.");
            }

            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            for (int i = 0; i < keys.Count; i++)
            {
                result.Add(keys[i], values[i]);
            }

            return result;
        }
       
        /// <summary>
        /// 두 개의 리스트(키와 값)의 내용을 받아 기존 딕셔너리 객체에 채워 넣습니다.
        /// 대상 딕셔너리의 기존 내용은 모두 지워집니다.
        /// </summary>
        /// <typeparam name="TKey">Dictionary의 키 타입</typeparam>
        /// <typeparam name="TValue">Dictionary의 값 타입</typeparam>
        /// <param name="keys">키 리스트</param>
        /// <param name="values">값 리스트</param>
        /// <param name="target">내용을 채워 넣을 대상 딕셔너리 객체입니다. 기존 내용은 모두 지워집니다.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="keys"/>, <paramref name="values"/>, 또는 <paramref name="target"/> 리스트가 null인 경우 발생합니다.
        /// </exception>
        /// <exception cref="ArgumentException">키와 값의 개수가 맞지 않을 경우</exception>
        public static void ConvertListToDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values, Dictionary<TKey, TValue> target)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys), "키 리스트가 null입니다.");
            }
            else if (values == null)
            {
                throw new ArgumentNullException(nameof(values), "값 리스트가 null입니다.");
            }
            else if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "target Dictionary가 null입니다.");
            }
            if (values.Count != keys.Count)
            {
                throw new ArgumentException("값 리스트의 크기와 키 리스트의 크기가 같아야 합니다.");
            }

            target.Clear();

            for (int i = 0; i < keys.Count; i++)
            {
                target.Add(keys[i], values[i]);
            }
        }


        /// <summary>
        /// 지정된 Dictionary에서 모든 키와 값을 추출하여 각각 별도의 List로 반환합니다.
        /// </summary>
        /// <typeparam name="TKey">Dictionary에 포함된 키의 타입입니다.</typeparam>
        /// <typeparam name="TValue">Dictionary에 포함된 값의 타입입니다.</typeparam>
        /// <param name="pairs">키와 값을 추출할 원본 Dictionary입니다.</param>
        /// <returns>
        /// Dictionary의 키를 포함하는 List와 값을 포함하는 List를 담은 ValueTuple입니다.
        /// (첫 번째 요소는 키 List, 두 번째 요소는 값 List)
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="pairs"/>가 null인 경우 발생합니다.
        /// </exception>
        public static (List<TKey> keys, List<TValue> values) ConvertDictionaryToLists<TKey, TValue>(Dictionary<TKey, TValue> pairs)
        {
            if (pairs == null)
            {
                throw new ArgumentException(nameof(pairs), "입력 Dictionary는 Null일 수 없습니다.");
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
        /*ToDO 기존 리스트에 채워넣는 메서드 만들기
      public static void ConvertDictionaryToLists<TKey, TValue>(Dictionary<TKey, TValue> pairs, List<TKey> targetKey, List<TValue> targetValue)*/
    }
}