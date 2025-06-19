using System;
using System.Collections.Generic;
using System.Reflection;
using TeamName.Utils.Convert;

namespace TeamName.Utils.FileIO
{
    /// <summary>CSV 파일을 읽어 Dictionary 리스트로 변환하는 클래스</summary>
    public class ReadCsv : File<List<Dictionary<string, string>>>
    {
        /// <summary>ReadCsv 생성자</summary>
        /// <param name="path">CSV 파일 경로</param>
        public ReadCsv(string path) : base(path) { }
        public ReadCsv() { }

        /// <summary>CSV 데이터를 Dictionary 리스트로 변환하여 반환합니다.</summary>
        /// <returns>CSV 데이터를 저장한 Dictionary 리스트</returns>
        /// <exception cref="FormatException">CSV 형식이 올바르지 않은 경우</exception>
        public override List<Dictionary<string, string>> Read()
        {
            if(data.Count == 0)
            {
                throw new FormatException("CSV 파일이 비어 있습니다.");
            }

            return Parsing(data);
        }

        public List<Dictionary<string, string>> Parsing(List<string> data)
        {
            //Header추출
            List<string> header = new List<string>();
            foreach (string item in data[0].Split(',')) { header.Add(item); }

            //내용 추출
            List<string> items = new List<string>();
            for (int i = 1; i < data.Count; i++)
            {
                foreach (string item in data[i].Split(","))
                    items.Add(item);
            }

            List<Dictionary<string, string>> result = Converter.ConvertListToDictionaryList(header, items);
            return result;
        }

        public List<Dictionary<string, string>> Parsing(string data)
        {
            List<string> Parsingdata = new List<string>(data.Split("\n")); 
            return Parsing(data);
        }
    }
}