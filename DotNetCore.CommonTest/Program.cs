
using DotNetCore.Common.Utils;
using DotNetStandard.Common.Events;
using DotNetStandard.Common.IO;
using DotNetStandard.Common.Text;
using System;
using System.Text;

namespace DotNetCore.CommonTest
{
    class Program
    {
        static void Main(string[] args)
        {

            uint num = 0xffffffff;

        //  int num1 =  ByteConverter.SetBitValue(num ,32,false);

            // TestBinaryBuffer();

            // TestSerializable();

            Console.Read();
        }

        static void TestSerializable()
        {
            string str = "张三";
            Student student = new Student() { Name = str, Age = 18, Weight = 55.5f, Height = 175 };
            Console.WriteLine(student.ToString());

            byte [] data =   Serialization.ObjectToBytes(student);

            Student sdt = Serialization.BytesToObject<Student>(data);

            Console.WriteLine(sdt.ToString());
        }

        static void TestBinaryBuffer()
        {
            string str = "张三";
            Student student = new Student() { Name = str, Age = 18, Weight = 55.5f, Height = 175 };
            Console.WriteLine(student.ToString());

            BinaryBuffer buffer = BinaryBuffer.Allocate(100);
            buffer.Write(student.Name,Encoding.UTF8);
            buffer.Write(student.Age);
            buffer.Write(student.Weight);
            buffer.Write(student.Height);

            int LENGTH = buffer.ReadOnlyCache.Length;
            Student st = new Student();
           int len = Encoding.UTF8.GetBytes(str).Length;
  
            st.Name = buffer.ReadChars(len, Encoding.UTF8);
            st.Age = buffer.ReadInt32();
            st.Weight = buffer.ReadSingle();
            st.Height = buffer.ReadInt32();
            Console.WriteLine(st.ToString());
        }

        static void TestLogHelper()
        {
            LogHelper.isEnable = true;


            byte[] datas = new byte[] { 0xa5, 0x5a, 0xff };

            string str = StringConverter.BytesToHexString(datas);

            LogHelper.Debug(str);
        }

        static void TestFrameworkEvent()
        {
            FrameworkEvent frameworkEvent = new FrameworkEvent();
            frameworkEvent.AddListenerEvent(() => {
                Console.WriteLine("Hello world!");
            });

            frameworkEvent.Invoke();
        }
    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public float Weight { get; set; }

        public int Height { get; set; }

        public override string ToString()
        {
            return $"姓名 : {Name} 年龄:{Age} 体重:{Weight} 身高:{Height}";
        }
    }
}
