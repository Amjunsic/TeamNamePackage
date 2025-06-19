using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace TeamName.Debugger
{
    /// <summary>
    /// 로그의 종류를 나타내는 열거형.
    /// </summary>
    public enum logType
    {
        /// <summary>
        /// 일반 정보 로그.
        /// </summary>
        Normal,
        /// <summary>
        /// 경고 로그. (Warning -> Warning 오타인 것 같지만 일단 그대로 둔다)
        /// </summary>
        Warning,
        /// <summary>
        /// 에러 로그.
        /// </summary>
        Error
    }

    /// <summary>
    /// 프로젝트 전역에서 사용할 수 있는 간단한 로그 관리 클래스.
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// 유니티 에디터에서만 동작하는 로그 출력 메서드.
        /// </summary>
        /// <remarks>
        /// [Conditional("UNITY_EDITOR")] 속성 때문에 유니티 에디터 환경이 아니면 이 메서드 호출 자체가 컴파일 시점에 삭제.
        /// </remarks>
        /// <param name="type">출력할 로그의 종류 (Normal, Warning, Error).</param>
        /// <param name="category">로그를 발생시킨 시스템이나 클래스 이름.</param>
        /// <param name="messages">출력할 로그 메시지들. 여러 개를 넘기면 줄바꿈으로 합쳐짐.</param>
        [Conditional("UNITY_EDITOR")]
        public static void Trace(logType type, string category, params string[] messages)
        {
            // 실제 로그 출력은 내부 메서드에 맡김.
            LogInternal(type, category, messages);
        }

        /// <summary>
        /// 실제 로그 포맷을 만들고 콘솔에 출력하는 내부 로직.
        /// </summary>
        /// <param name="type">로그 종류.</param>
        /// <param name="category">로그 카테고리.</param>
        /// <param name="messages">로그 메시지.</param>
        /// <param name="callSiteMethodName">이 메서드를 호출한 메서드의 이름. 컴파일러가 자동으로 채워줘.</param>
        /// <param name="filePath">이 메서드를 호출한 소스 코드 파일의 경로.</param>
        /// <param name="lineNumber">이 메서드를 호출한 소스 코드의 라인 번호.</param>
        private static void LogInternal(logType type, string category, string[] messages,
        [CallerMemberName] string callSiteMethodName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
        {
            // StringBuilder를 사용해서 로그 문자열을 조합.
            StringBuilder sb = new StringBuilder();

            // [카테고리] 형식으로머릿말을 추가.
            sb.Append($"<b>[{category}]</b> ");

            // 전달된 메시지들을 줄바꿈(\n)으로 연결해서 추가.
            if (messages.Length > 0)
            {
                sb.Append(string.Join("\n", messages));
            }

            // 로그를 호출한 파일, 라인, 메서드 정보를 추가해서 디버깅을 쉽게 만들어줌.
            string fileName = Path.GetFileName(filePath);
            sb.Append($"\n<color=grey>at {fileName}:{lineNumber} [{callSiteMethodName}]</color>");

            // 로그 타입에 따라 다른 색상/종류의 Unity Debug 메서드를 호출.
            switch (type)
            {
                case logType.Normal:
                    UnityEngine.Debug.Log(sb.ToString());
                    break;
                case logType.Error:
                    UnityEngine.Debug.LogError(sb.ToString());
                    break;
                case logType.Warning:
                    UnityEngine.Debug.LogWarning(sb.ToString());
                    break;
            }
        }
    }
}