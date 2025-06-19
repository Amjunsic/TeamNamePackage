using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace TeamName.Debugger
{
    /// <summary>
    /// �α��� ������ ��Ÿ���� ������.
    /// </summary>
    public enum logType
    {
        /// <summary>
        /// �Ϲ� ���� �α�.
        /// </summary>
        Normal,
        /// <summary>
        /// ��� �α�. (Warning -> Warning ��Ÿ�� �� ������ �ϴ� �״�� �д�)
        /// </summary>
        Warning,
        /// <summary>
        /// ���� �α�.
        /// </summary>
        Error
    }

    /// <summary>
    /// ������Ʈ �������� ����� �� �ִ� ������ �α� ���� Ŭ����.
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// ����Ƽ �����Ϳ����� �����ϴ� �α� ��� �޼���.
        /// </summary>
        /// <remarks>
        /// [Conditional("UNITY_EDITOR")] �Ӽ� ������ ����Ƽ ������ ȯ���� �ƴϸ� �� �޼��� ȣ�� ��ü�� ������ ������ ����.
        /// </remarks>
        /// <param name="type">����� �α��� ���� (Normal, Warning, Error).</param>
        /// <param name="category">�α׸� �߻���Ų �ý����̳� Ŭ���� �̸�.</param>
        /// <param name="messages">����� �α� �޽�����. ���� ���� �ѱ�� �ٹٲ����� ������.</param>
        [Conditional("UNITY_EDITOR")]
        public static void Trace(logType type, string category, params string[] messages)
        {
            // ���� �α� ����� ���� �޼��忡 �ñ�.
            LogInternal(type, category, messages);
        }

        /// <summary>
        /// ���� �α� ������ ����� �ֿܼ� ����ϴ� ���� ����.
        /// </summary>
        /// <param name="type">�α� ����.</param>
        /// <param name="category">�α� ī�װ�.</param>
        /// <param name="messages">�α� �޽���.</param>
        /// <param name="callSiteMethodName">�� �޼��带 ȣ���� �޼����� �̸�. �����Ϸ��� �ڵ����� ä����.</param>
        /// <param name="filePath">�� �޼��带 ȣ���� �ҽ� �ڵ� ������ ���.</param>
        /// <param name="lineNumber">�� �޼��带 ȣ���� �ҽ� �ڵ��� ���� ��ȣ.</param>
        private static void LogInternal(logType type, string category, string[] messages,
        [CallerMemberName] string callSiteMethodName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
        {
            // StringBuilder�� ����ؼ� �α� ���ڿ��� ����.
            StringBuilder sb = new StringBuilder();

            // [ī�װ�] �������θӸ����� �߰�.
            sb.Append($"<b>[{category}]</b> ");

            // ���޵� �޽������� �ٹٲ�(\n)���� �����ؼ� �߰�.
            if (messages.Length > 0)
            {
                sb.Append(string.Join("\n", messages));
            }

            // �α׸� ȣ���� ����, ����, �޼��� ������ �߰��ؼ� ������� ���� �������.
            string fileName = Path.GetFileName(filePath);
            sb.Append($"\n<color=grey>at {fileName}:{lineNumber} [{callSiteMethodName}]</color>");

            // �α� Ÿ�Կ� ���� �ٸ� ����/������ Unity Debug �޼��带 ȣ��.
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