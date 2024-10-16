using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 해당 메모리폴은 초기 인스턴싱을 지원하지 않음. 인스턴싱되고 파괴된 객체들을 관리함.
// 이에 Instantiate에 부하가 발생한다면 초기 인스턴싱 로직을 추가할 필요가 있음
public class MemoryPool<T> where T : Object
{
    public delegate void del_Register(T item);
    Queue<T> m_pool;
    public MemoryPool (int capacity)
    {
        m_pool = new Queue<T>(capacity);
    }
    public void Register(T item)
    {
        m_pool.Enqueue(item);
    }
    public T GetItem()
    {
        return m_pool.Count > 0 ? m_pool.Dequeue() : null;
    }
    public void Clear()
    {
        while (m_pool.Count > 0)
        {
            var pop = m_pool.Dequeue();
            if(pop != null)
            {
                if(pop != null)
                {
                    MonoBehaviour.DestroyImmediate(pop);
                }
            }
        }
    }
}
