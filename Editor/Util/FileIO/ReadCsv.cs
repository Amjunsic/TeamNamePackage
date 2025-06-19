using System;
using System.Collections.Generic;
using System.Reflection;
using TeamName.Utils.Convert;

namespace TeamName.Utils.FileIO
{
    /// <summary>CSV ������ �о� Dictionary ����Ʈ�� ��ȯ�ϴ� Ŭ����</summary>
    public class ReadCsv : File<List<Dictionary<string, string>>>
    {
        /// <summary>ReadCsv ������</summary>
        /// <param name="path">CSV ���� ���</param>
        public ReadCsv(string path) : base(path) { }
        public ReadCsv() { }

        /// <summary>CSV �����͸� Dictionary ����Ʈ�� ��ȯ�Ͽ� ��ȯ�մϴ�.</summary>
        /// <returns>CSV �����͸� ������ Dictionary ����Ʈ</returns>
        /// <exception cref="FormatException">CSV ������ �ùٸ��� ���� ���</exception>
        public override List<Dictionary<string, string>> Read()
        {
            if(data.Count == 0)
            {
                throw new FormatException("CSV ������ ��� �ֽ��ϴ�.");
            }

            return Parsing(data);
        }

        public List<Dictionary<string, string>> Parsing(List<string> data)
        {
            //Header����
            List<string> header = new List<string>();
            foreach (string item in data[0].Split(',')) { header.Add(item); }

            //���� ����
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