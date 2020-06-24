using System;
using System.Collections.Generic;

namespace DotNetStandard.Common.Events
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FrameworkEventBase
    {
        /// <summary>
        /// 监听者的数量
        /// </summary>
        public abstract int ListenerCount { get; }
    }

    /// <summary>
    /// 事件
    /// </summary>
    public class FrameworkEvent : FrameworkEventBase
    {
        /// <summary>
        /// 承载所有事件的委托
        /// </summary>
        private Action _handler;

        /// <summary>
        /// 所有监听委托的列表
        /// </summary>
        private List<Action> _listenerActions;

        /// <summary>
        /// 监听者的数量
        /// </summary>
        public override int ListenerCount
        {
            get
            {
                if (_listenerActions == null)
                    return 0;
                return _listenerActions.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FrameworkEvent()
        {
            _handler = new Action(()=> { });
            _listenerActions = new List<Action>();
        }
       
        /// <summary>
        /// 添加一个需要监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void AddListenerEvent(Action method)
        {
            _listenerActions.Add(method);
            _handler += method;
        }

        /// <summary>
        /// 移除一个已经监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void RemoveListenerEvent(Action method)
        {
            _listenerActions.Remove(method);
            _handler -= method;
        }

        /// <summary>
        /// 清空所有监听事件
        /// </summary>
        public void RemoveAllListener()
        {
            if (ListenerCount <= 0)
                return;
            for (int i = 0; i < _listenerActions.Count; i++)
            {
                _handler -= _listenerActions[i];
            }
            _listenerActions.Clear();
        }

        /// <summary>
        /// 触发
        /// </summary>
        public void Invoke()
        {
            _handler?.Invoke();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FrameworkEvent<T> : FrameworkEventBase
    {
        /// <summary>
        /// 承载所有事件的委托
        /// </summary>
        private Action<T> _handler;

        /// <summary>
        /// 所有监听委托的列表
        /// </summary>
        private List<Action<T>> _listenerActions;

        /// <summary>
        /// 监听者的数量
        /// </summary>
        public override int ListenerCount
        {
            get
            {
                if (_listenerActions == null)
                    return 0;
                return _listenerActions.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FrameworkEvent()
        {
            _handler = new Action<T>(o=> { });
            _listenerActions = new List<Action<T>>();
        }


        /// <summary>
        /// 添加一个需要监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void AddListenerEvent(Action<T> method)
        {
            _listenerActions.Add(method);
            _handler += method;
        }

        /// <summary>
        /// 移除一个已经监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void RemoveListenerEvent(Action<T> method)
        {
            _listenerActions.Remove(method);
            _handler -= method;
        }

        /// <summary>
        /// 清空所有监听事件
        /// </summary>
        public void RemoveAllListener()
        {
            if (ListenerCount <= 0)
                return;

            for (int i = 0; i < _listenerActions.Count; i++)
            {
                _handler -= _listenerActions[i];
            }
            _listenerActions.Clear();
        }

        /// <summary>
        /// 触发
        /// </summary>
        public void Invoke(T arg)
        {
            _handler?.Invoke(arg);
        }



    }

   /// <summary>
   /// 
   /// </summary>
   /// <typeparam name="T0"></typeparam>
   /// <typeparam name="T1"></typeparam>
    public class FrameworkEvent<T0,T1> : FrameworkEventBase
    {

        private Action<T0, T1> _handler;

        private List<Action<T0, T1>> _listenerActions;

        /// <summary>
        /// 监听者的数量
        /// </summary>
        public override int ListenerCount
        {
            get
            {
                if (_listenerActions == null)
                    return 0;
                return _listenerActions.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FrameworkEvent()
        {
            _handler = new Action<T0, T1>((t0,t1)=> { });
            _listenerActions = new List<Action<T0, T1>>();
        }


        /// <summary>
        /// 添加一个需要监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void AddListenerEvent(Action<T0, T1> method)
        {
            _listenerActions.Add(method);
            _handler += method;
        }

        /// <summary>
        /// 移除一个已经监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void RemoveListenerEvent(Action<T0, T1> method)
        {
            _listenerActions.Remove(method);
            _handler -= method;
        }

        /// <summary>
        /// 清空所有监听事件
        /// </summary>
        public void RemoveAllListener()
        {
            if (ListenerCount <= 0)
                return;

            for (int i = 0; i < _listenerActions.Count; i++)
            {
                _handler -= _listenerActions[i];
            }
            _listenerActions.Clear();
        }

        /// <summary>
        /// 触发
        /// </summary>
        public void Invoke(T0 arg0, T1 arg1)
        {
            _handler?.Invoke(arg0, arg1);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class FrameworkEvent<T0, T1,T2> : FrameworkEventBase
    {

        private Action<T0, T1, T2> _handler;

        private List<Action<T0, T1, T2>> _listenerActions;

        /// <summary>
        /// 监听者的数量
        /// </summary>
        public override int ListenerCount
        {
            get
            {
                if (_listenerActions == null)
                    return 0;
                return _listenerActions.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FrameworkEvent()
        {
            _handler = new Action<T0, T1, T2>((t0, t1,t2) => { });
            _listenerActions = new List<Action<T0, T1, T2>>();
        }


        /// <summary>
        /// 添加一个需要监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void AddListenerEvent(Action<T0, T1, T2> method)
        {
            _listenerActions.Add(method);
            _handler += method;
        }

        /// <summary>
        /// 移除一个已经监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void RemoveListenerEvent(Action<T0, T1, T2> method)
        {
            _listenerActions.Remove(method);
            _handler -= method;
        }

        /// <summary>
        /// 清空所有监听事件
        /// </summary>
        public void RemoveAllListener()
        {
            if (ListenerCount <= 0)
                return;

            for (int i = 0; i < _listenerActions.Count; i++)
            {
                _handler -= _listenerActions[i];
            }
            _listenerActions.Clear();
        }

        /// <summary>
        /// 触发
        /// </summary>
        public void Invoke(T0 arg0, T1 arg1, T2 arg2)
        {
            _handler?.Invoke(arg0, arg1, arg2);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public class FrameworkEvent<T0, T1, T2,T3> : FrameworkEventBase
    {

        private Action<T0, T1, T2, T3> _handler;

        private List<Action<T0, T1, T2, T3>> _listenerActions;

        /// <summary>
        /// 监听者的数量
        /// </summary>
        public override int ListenerCount
        {
            get
            {
                if (_listenerActions == null)
                    return 0;
                return _listenerActions.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FrameworkEvent()
        {
            _handler = new Action<T0, T1, T2, T3>((t0, t1, t2,t3) => { });
            _listenerActions = new List<Action<T0, T1, T2, T3>>();
        }


        /// <summary>
        /// 添加一个需要监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void AddListenerEvent(Action<T0, T1, T2, T3> method)
        {
            _listenerActions.Add(method);
            _handler += method;
        }

        /// <summary>
        /// 移除一个已经监听的事件
        /// </summary>
        /// <param name="method"></param>
        public void RemoveListenerEvent(Action<T0, T1, T2, T3> method)
        {
            _listenerActions.Remove(method);
            _handler -= method;
        }

        /// <summary>
        /// 清空所有监听事件
        /// </summary>
        public void RemoveAllListener()
        {
            if (ListenerCount <= 0)
                return;

            for (int i = 0; i < _listenerActions.Count; i++)
            {
                _handler -= _listenerActions[i];
            }
            _listenerActions.Clear();
        }

        /// <summary>
        /// 触发
        /// </summary>
        public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            _handler?.Invoke(arg0, arg1, arg2, arg3);
        }

    }

}
