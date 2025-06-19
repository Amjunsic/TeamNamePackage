using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace TeamName.Utils.FileIO
{
    // ReadSheet 클래스는 구글 시트나 CSV 등 외부 시트 파일을 읽어오는 역할을 한다.
    // File<T>를 상속받으며, T는 List<Dictionary<string, string>> 형태이다.
    public class ReadSheet : File<List<Dictionary<string, string>>>
    {
        private string _path;      // 구글 시트(혹은 CSV 파일)의 URL 또는 경로를 저장
        private string _csvData;   // 받아온 CSV 데이터(텍스트)를 저장

        // 생성자: 외부에서 파일 경로(URL)를 받아 멤버 변수에 저장
        public ReadSheet(string path) : base()
        {
            _path = path;  // 생성자 인자로 받은 경로(구글 시트 CSV 주소)를 _path에 저장
        }

        // UnityWebRequest로 비동기적으로 구글 시트 데이터를 받아오는 메서드 (코루틴)
        // onCompleted: 데이터를 모두 읽고 파싱이 끝나면 결과(List<Dictionary<string, string>>)를 전달하는 콜백
        public IEnumerator LoadSheet(System.Action<List<Dictionary<string, string>>> onCompleted)
        {
            // _path에 저장된 URL로 GET 요청을 보냄
            UnityWebRequest request = UnityWebRequest.Get(_path);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // 요청이 성공하면 내려받은 CSV 텍스트를 _csvData에 저장
                _csvData = request.downloadHandler.text;

                // 이미 만들어진 ReadCsv 파서를 사용해 데이터를 파싱
                ReadCsv csv = new ReadCsv();
                var parsed = csv.Parsing(_csvData); // List<Dictionary<string, string>>로 파싱
                onCompleted?.Invoke(parsed); // 결과를 콜백으로 전달
            }
            else
            {
                // 요청 실패 시 콜백에 null 전달
                onCompleted?.Invoke(null);
            }
        }

        // 이미 받아온 _csvData를 파싱해서 List<Dictionary<string, string>> 형태로 반환하는 메서드
        public override List<Dictionary<string, string>> Read()
        {
            // _csvData가 비어있다면 예외 발생
            if (string.IsNullOrEmpty(_csvData))
                throw new System.InvalidOperationException("데이터를 먼저 불러와야 합니다.");

            // 파싱 및 반환
            ReadCsv csv = new ReadCsv();
            return csv.Parsing(_csvData);
        }
    }
}