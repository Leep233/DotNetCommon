using DotNetFramework.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Common.FrameworkTemplate
{
    /// <summary>
    /// 模块事件表
    /// </summary>
    public class ModuleEventTable<T>
    {
        private Dictionary<string, FrameworkEvent<T>> _eventTables;

        /// <summary>
        /// 
        /// </summary>
        public ModuleEventTable()
        {
            _eventTables = new Dictionary<string, FrameworkEvent<T>>();
        }

        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public FrameworkEvent<T> Event(string eventName)
        {
            if (!_eventTables.ContainsKey(eventName))
                _eventTables.Add(eventName, new FrameworkEvent<T>());

            return _eventTables[eventName];
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventName"></param>
        public void RemoveEvent(string eventName) {
            if (!_eventTables.ContainsKey(eventName)) return;
            _eventTables[eventName].RemoveAllListener();
            _eventTables.Remove(eventName);
        }

        /// <summary>
        /// 清空事件表
        /// </summary>
        public void Clear() {
            foreach (var item in _eventTables)
            {
                item.Value.RemoveAllListener();
            }
            _eventTables.Clear();
        }

    }
}
