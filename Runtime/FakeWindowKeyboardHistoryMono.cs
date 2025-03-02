using System.Collections.Generic;
using UnityEngine;

public class FakeWindowKeyboardHistoryMono : MonoBehaviour
{

    public int m_maxHistory = 10;
    public List<IntegerKeyEvent> m_keyEvents = new List<IntegerKeyEvent>();
    public List<KeyInfoAsInteger> m_keyIntegerEvents= new List<KeyInfoAsInteger>();

    public void PushIn(IntegerKeyEvent integerKeyEvent)
    {
        m_keyEvents.Add(integerKeyEvent);
        KeyWindowIntegerKeyMap.Instance.GetKeyInfoFromIntId(integerKeyEvent.m_integerCommand, out bool found, out KeyInfoAsInteger info);
        if (found)
        {
            m_keyIntegerEvents.Insert(0,info);
        }
        else
        {
            m_keyIntegerEvents.Insert(0, default);
        }

        while (m_keyEvents.Count > m_maxHistory)
        {
            m_keyEvents.RemoveAt(m_keyEvents.Count-1);
        }
        while (m_keyIntegerEvents.Count > m_maxHistory)
        {
            m_keyIntegerEvents.RemoveAt(m_keyIntegerEvents.Count-1);
        }
    }

}
