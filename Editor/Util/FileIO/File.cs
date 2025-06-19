using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TeamName.Utils.FileIO
{
    /// <summary>
    /// ���Ͽ��� �����͸� �о�� ����Ʈ�� ��ȯ�ϴ� Ŭ����
    /// </summary>
    public static class ReadFile
    {
        /// <summary> ������ ����� ������ �о� ���ڿ� ����Ʈ�� ��ȯ�մϴ�. </summary>
        /// <param name="path">���� ������ ���</param>
        /// <returns>������ �� ���� �����ϴ� ����Ʈ</returns>
        /// <exception cref="FileNotFoundException">������ �������� �ʴ� ���</exception>
        /// <exception cref="IOException">������ �д� �� ������ �߻��� ���</exception>
        public static List<string> Read(string path)
        {
            List<string> result = new List<string>();

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("������ ã�� �� �����ϴ�.", path);
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
                throw new IOException("���� �б� ����", ex);
            }

            return result;

        }
    }

    /// <summary>������ �д� �⺻���� �߻� Ŭ���� </summary>
    /// <typeparam name="T">�о�� �������� Ÿ��</typeparam>
    public abstract class File <T>
    {
        /// <summary>���� ������ ���� ����Ʈ </summary>
        protected List<string> data;


        /// <summary>������ �о� ������ ����Ʈ�� �ʱ�ȭ�մϴ�.</summary>
        /// <param name="path">���� ���</param>
        /// <exception cref="FileNotFoundException">������ �������� �ʴ� ���</exception>
        /// <exception cref="IOException">������ �д� �� ������ �߻��� ���</exception>
        public File(string path) => this.data = ReadFile.Read(path);
        public File() { }

        /// <summary>���� �����͸� Ư�� �������� ��ȯ�Ͽ� ��ȯ�ϴ� �߻� �޼���</summary>
        /// <returns>��ȯ�� ������</returns>
        public abstract T Read();
    }

}
