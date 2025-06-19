using UnityEngine;

public class DonDestoryRoot : MonoBehaviour
{
    /// <summary>
    /// 이 컴포넌트가 붙어있는 GameObject와 그 모든 자식들을
    /// 씬 전환 시 파괴되지 않도록 설정.
    /// 'Managers'와 같은 컨테이너 오브젝트에 붙여 사용.
    /// </summary>
    private void Awake() => DontDestroyOnLoad(this.gameObject);
    
}