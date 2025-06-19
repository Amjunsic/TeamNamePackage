using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TeamName.Utils.FileIO
{
    /// <summary>
    /// 파일에서 데이터를 읽어와 리스트로 반환하는 클래스
    /// </summary>
    public static class ReadFile
    {
        /// <summary> 지정된 경로의 파일을 읽어 문자열 리스트로 반환합니다. </summary>
        /// <param name="path">읽을 파일의 경로</param>
        /// <returns>파일의 각 줄을 포함하는 리스트</returns>
        /// <exception cref="FileNotFoundException">파일이 존재하지 않는 경우</exception>
        /// <exception cref="IOException">파일을 읽는 중 오류가 발생한 경우</exception>
        public static List<string> Read(string path)
        {
            List<string> result = new List<string>();

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("파일을 찾을 수 없습니다.", path);
            }
            try
            {
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                while (line != null)
                {
                    result.Add(line);
                    line = sr.ReadLine();
                }
            }
            catch(IOException ex)
            {
                throw new IOException("파일 읽기 실패", ex);
            }

            return result;

        }
    }

    /// <summary>파일을 읽는 기본적인 추상 클래스 </summary>
    /// <typeparam name="T">읽어온 데이터의 타입</typeparam>
    public abstract class File <T>
    {
        /// <summary>파일 데이터 저장 리스트 </summary>
        protected List<string> data;


        /// <summary>파일을 읽어 데이터 리스트를 초기화합니다.</summary>
        /// <param name="path">파일 경로</param>
        /// <exception cref="FileNotFoundException">파일이 존재하지 않는 경우</exception>
        /// <exception cref="IOException">파일을 읽는 중 오류가 발생한 경우</exception>
        public File(string path) => this.data = ReadFile.Read(path);
        public File() { }

        /// <summary>파일 데이터를 특정 형식으로 변환하여 반환하는 추상 메서드</summary>
        /// <returns>변환된 데이터</returns>
        public abstract T Read();
    }

}
