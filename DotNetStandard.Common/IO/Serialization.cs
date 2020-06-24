using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DotNetStandard.Common.IO
{
    /// <summary>
    /// 序列化类
    /// </summary>
    public class Serialization
    {
       /// <summary>
       /// 二进制数组转对象
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="data"></param>
       /// <returns></returns>
        public static T BytesToObject<T>(byte[] data)
        {
            T target = default(T);
            using (Stream sm = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                target = (T)bf.Deserialize(sm);
            }
            return target;
        }

        /// <summary>
        /// 对象转2进制数组
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static byte[] ObjectToBytes(object target)
        {
            byte[] data;
            using (Stream sm = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(sm, target);
                sm.Position = 0;
                data = new byte[sm.Length];
                sm.Read(data, 0, data.Length);
            }
            return data;
        }
    }
}
