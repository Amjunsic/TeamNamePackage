using System;

namespace TeamName.DataStruct.Singleton
{
    /// <summary>
    /// MonoBehaviour를 상속하지 않는 일반 클래스용 제네릭 싱글톤.
    /// Lazy&lt;T&gt;를 사용해서 스레드 세이프하고 게으른 초기화(Lazy Initialization)를 지원해.
    /// </summary>
    /// <typeparam name="T">싱글톤으로 만들 클래스. 'class', 'new()' 제약 조건이 있어서 파라미터 없는 기본 생성자가 있는 참조 타입이어야 함.</typeparam>
    public abstract class GenericSinglton<T> where T : class, new()
    {
        /// <summary>
        /// 스레드 세이프한 지연 초기화를 위해 Lazy&lt;T&gt;를 사용해서 인스턴스를 보관.
        /// 인스턴스는 실제로 사용되기 전까지 생성되지 않아.
        /// </summary>
        private static readonly Lazy<T> instance = new Lazy<T>(() => new T(), true);

        /// <summary>
        /// 싱글톤 인스턴스에 접근하기 위한 public 프로퍼티.
        /// 최초 접근 시 내부적으로 인스턴스가 한 번만 생성돼.
        /// </summary>
        public static T Instance => instance.Value;

        /// <summary>
        /// 생성자를 protected로 막아서 외부에서 'new()'로 직접 인스턴스를 만드는 걸 방지.
        /// 오직 상속받은 클래스 내부에서만 호출 가능해져서 싱글톤 패턴이 유지돼.
        /// </summary>
        protected GenericSinglton() { }
    }
}