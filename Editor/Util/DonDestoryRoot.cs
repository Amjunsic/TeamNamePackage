using UnityEngine;

public class DonDestoryRoot : MonoBehaviour
{
    /// <summary>
    /// �� ������Ʈ�� �پ��ִ� GameObject�� �� ��� �ڽĵ���
    /// �� ��ȯ �� �ı����� �ʵ��� ����.
    /// 'Managers'�� ���� �����̳� ������Ʈ�� �ٿ� ���.
    /// </summary>
    private void Awake() => DontDestroyOnLoad(this.gameObject);
    
}