using System.Collections;
using System.Collections.Generic;
using TeamName.Debugger;
using UnityEngine;

namespace TeamName.DataStruct.Singleton
{
    /// <summary>
    /// MonoBehaviour�� ��ӹ޴� Ŭ������ �̱������� ������ִ� �߻� Ŭ����.
    /// </summary>
    /// <typeparam name="T">�̱������� ���� Ŭ���� Ÿ���� ����.</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        /// <summary>
        /// �̱��� �ν��Ͻ��� �����ϴ� ����.
        /// </summary>
        private static T instance;

        /// <summary>
        /// ��Ƽ������ ����ȭ�� ���� ���(lock) ��ü.
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// �� ���� ���¸� Ȯ���ϴ� �÷���.
        /// </summary>
        private static bool _applicationIsQuitting = false;

        /// <summary>
        /// �ܺο��� �̱��� �ν��Ͻ��� �����ϱ� ���� ������Ƽ.
        /// </summary>
        public static T Instance
        {
            get
            {
                // ���� �������� �� ���� ��ü�� �����Ǵ� ���� ����.
                if (_applicationIsQuitting)
                {
                    return null;
                }

                // ������ �������� ���� �ν��Ͻ� ����/���� ������ ���.
                lock (_lock)
                {
                    // �ν��Ͻ��� ���� ������ ������ Ž��.
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();

                        // ������ �ν��Ͻ��� ã�� ������ ��� ���� �α� ���.
                        if (instance == null)
                        {
                            string info = $"[{typeof(T).Name}]";
                            string msg = $"{info} failed to find instance";
                            string guid = "An instance of this singleton is required in the scene, but none was found.";
                            string guid2 = "���� ��ũ��Ʈ���� �������� ������Ʈ�� �߰��Ϸ��� �ߴٸ�, �� ������Ʈ�� ������ ��ġ ���ּ���.";
                            LogManager.Trace(logType.Error, "[MonoSingleton] ", msg, guid, guid2);
                        }
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// �ν��Ͻ��� �ʱ�ȭ�ϰ� �ߺ��� �ν��Ͻ��� �ı�.
        /// </summary>
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
            else if (instance != this)
            {
                string type = typeof(T).Name;
                string msg = $"Duplicate instance detected ";
                string guid = $"Object : [{this.gameObject.name}], Component : {type}";
                LogManager.Trace(logType.Error, $"[MonoSingleton] ", msg, guid);
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// �� ���� �� �÷��׸� �����Ͽ� �ν��Ͻ��� �ٽ� �����Ǵ� ���� ����.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }
    }
}
