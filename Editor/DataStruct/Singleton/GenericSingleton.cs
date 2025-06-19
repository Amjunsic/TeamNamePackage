using System;

namespace TeamName.DataStruct.Singleton
{
    /// <summary>
    /// MonoBehaviour�� ������� �ʴ� �Ϲ� Ŭ������ ���׸� �̱���.
    /// Lazy&lt;T&gt;�� ����ؼ� ������ �������ϰ� ������ �ʱ�ȭ(Lazy Initialization)�� ������.
    /// </summary>
    /// <typeparam name="T">�̱������� ���� Ŭ����. 'class', 'new()' ���� ������ �־ �Ķ���� ���� �⺻ �����ڰ� �ִ� ���� Ÿ���̾�� ��.</typeparam>
    public abstract class GenericSinglton<T> where T : class, new()
    {
        /// <summary>
        /// ������ �������� ���� �ʱ�ȭ�� ���� Lazy&lt;T&gt;�� ����ؼ� �ν��Ͻ��� ����.
        /// �ν��Ͻ��� ������ ���Ǳ� ������ �������� �ʾ�.
        /// </summary>
        private static readonly Lazy<T> instance = new Lazy<T>(() => new T(), true);

        /// <summary>
        /// �̱��� �ν��Ͻ��� �����ϱ� ���� public ������Ƽ.
        /// ���� ���� �� ���������� �ν��Ͻ��� �� ���� ������.
        /// </summary>
        public static T Instance => instance.Value;

        /// <summary>
        /// �����ڸ� protected�� ���Ƽ� �ܺο��� 'new()'�� ���� �ν��Ͻ��� ����� �� ����.
        /// ���� ��ӹ��� Ŭ���� ���ο����� ȣ�� ���������� �̱��� ������ ������.
        /// </summary>
        protected GenericSinglton() { }
    }
}