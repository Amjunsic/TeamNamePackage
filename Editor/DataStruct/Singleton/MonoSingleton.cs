using System.Collections;
using System.Collections.Generic;
using TeamName.Debugger;
using UnityEngine;

namespace TeamName.DataStruct.Singleton
{
    /// <summary>
    /// MonoBehaviour를 상속받는 클래스를 싱글톤으로 만들어주는 추상 클래스.
    /// </summary>
    /// <typeparam name="T">싱글톤으로 만들 클래스 타입을 지정.</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        /// <summary>
        /// 싱글톤 인스턴스를 저장하는 변수.
        /// </summary>
        private static T instance;

        /// <summary>
        /// 멀티스레드 동기화를 위한 잠금(lock) 객체.
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// 앱 종료 상태를 확인하는 플래그.
        /// </summary>
        private static bool _applicationIsQuitting = false;

        /// <summary>
        /// 외부에서 싱글톤 인스턴스에 접근하기 위한 프로퍼티.
        /// </summary>
        public static T Instance
        {
            get
            {
                // 앱이 종료중일 때 유령 객체가 생성되는 것을 방지.
                if (_applicationIsQuitting)
                {
                    return null;
                }

                // 스레드 안정성을 위해 인스턴스 접근/생성 로직을 잠금.
                lock (_lock)
                {
                    // 인스턴스가 아직 없으면 씬에서 탐색.
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();

                        // 씬에서 인스턴스를 찾지 못했을 경우 에러 로그 출력.
                        if (instance == null)
                        {
                            string info = $"[{typeof(T).Name}]";
                            string msg = $"{info} failed to find instance";
                            string guid = "An instance of this singleton is required in the scene, but none was found.";
                            string guid2 = "만약 스크립트에서 동적으로 컴포넌트를 추가하려고 했다면, 이 컴포넌트는 씬에서 배치 해주세요.";
                            LogManager.Trace(logType.Error, "[MonoSingleton] ", msg, guid, guid2);
                        }
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// 인스턴스를 초기화하고 중복된 인스턴스는 파괴.
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
        /// 앱 종료 시 플래그를 설정하여 인스턴스가 다시 생성되는 것을 방지.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }
    }
}
