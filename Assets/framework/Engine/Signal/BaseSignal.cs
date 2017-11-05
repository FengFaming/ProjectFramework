/*
 *  create file time:11/5/2017 8:52:44 PM
 *  Describe:project signal base
* */

using System;
using System.Collections.Generic;

namespace Framework.Engine
{
    public class BaseSignal : IBaseSignal
    {
        /// <summary>
        /// ѭ�����źż�
        /// </summary>
        private event Action<IBaseSignal, object[]> m_BaseListener = delegate { };

        /// <summary>
        /// һ���Ե��źż�
        /// </summary>
        private event Action<IBaseSignal, object[]> m_OnceListener = delegate { };

        /// <summary>
        /// �źŵ���
        /// </summary>
        /// <param name="args"></param>
        public void Dispath(object[] args)
        {
            m_BaseListener(this, args);
            m_OnceListener(this, args);
            m_OnceListener = delegate { };
        }

        /// <summary>
        /// �����ź�
        /// </summary>
        /// <param name="callback"></param>
        public void AddListener(Action<IBaseSignal, object[]> callback)
        {
            foreach (Delegate del in m_BaseListener.GetInvocationList())
            {
                Action<IBaseSignal, object[]> action = (Action<IBaseSignal, object[]>)del;
                if (callback.Equals(action))
                {
                    return;
                }
            }

            m_BaseListener += callback;
        }

        /// <summary>
        /// ���һ�����ź�
        /// </summary>
        /// <param name="callback"></param>
        public void AddOnce(Action<IBaseSignal, object[]> callback)
        {
            foreach (Delegate del in m_OnceListener.GetInvocationList())
            {
                Action<IBaseSignal, object[]> action = (Action<IBaseSignal, object[]>)del;
                if (callback.Equals(action))
                {
                    return;
                }
            }

            m_OnceListener += callback;
        }

        /// <summary>
        /// �Ƴ��ź�
        /// </summary>
        /// <param name="callback"></param>
        public void RemoveListener(Action<IBaseSignal, object[]> callback)
        {
            bool hasAction = false;
            foreach (Delegate del in m_BaseListener.GetInvocationList())
            {
                Action<IBaseSignal, object[]> action = (Action<IBaseSignal, object[]>)del;
                if (callback.Equals(action))
                {
                    hasAction = true;
                    break;
                }
            }

            if (hasAction)
            {
                m_BaseListener -= callback;
            }
        }

        /// <summary>
        /// ��ȡ�ź���
        /// </summary>
        /// <returns></returns>
        public virtual List<Type> GetTypes()
        {
            return new List<Type>();
        }
    }
}
