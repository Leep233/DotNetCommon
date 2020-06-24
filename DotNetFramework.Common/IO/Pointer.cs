using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Common.IO
{
    /// <summary>
    /// 指针
    /// </summary>
   public class Pointer
    {
        /// <summary>
        /// 对象转指针 注意 这个是非托管内容 注意释放
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        public static IntPtr ToPointer(object structure)
        {
            int size = Marshal.SizeOf(structure);

            return Marshal.AllocHGlobal(size);
        }

        /// <summary>
        /// 对象转指针 注意 这个是非托管内容 注意释放
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IntPtr ToPointer(Type type)
        {
            int size = Marshal.SizeOf(type);

            return Marshal.AllocHGlobal(size);
        }

        /// <summary>
        /// 指针转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ptr"></param>
        /// <param name="isFree">是否释放传入指针</param>
        /// <returns></returns>

        public static T ToStructure<T>(IntPtr ptr,bool isFree = true)
        {
          T t =  (T)Marshal.PtrToStructure(ptr, typeof(T));

            if (isFree)
                Free(ptr);

            return t;
        }

        /// <summary>
        /// 是否指针
        /// </summary>
        /// <param name="ptr"></param>
        public static void Free(IntPtr ptr) {
            Marshal.FreeHGlobal(ptr);
        }

    }
}
