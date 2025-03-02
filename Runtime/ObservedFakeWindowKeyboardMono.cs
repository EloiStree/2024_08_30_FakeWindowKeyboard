using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ObservedFakeWindowKeyboardMono : MonoBehaviour
{
    public FakeWindowKeyboardStateMono m_observedKeyboard;
    private FakeWindowKeyboardState m_previousState= new FakeWindowKeyboardState();


    public string m_debugJoinKeysPressed;
    public UnityEvent<string> m_onDebugJoinKeysPressed;
    public List<KeyInfoAsInteger> m_keyDownDebug = new List<KeyInfoAsInteger>();

    public List<int> m_keysIdPressed = new List<int>();
    public List<int> m_keysIdReleased = new List<int>();

    private void OnValidate()
    {

        Refresh();
    }

    private void Update()
    {
        Refresh();
    }
    [ContextMenu("Refresh")]
    public void Refresh()
    {

        if (m_observedKeyboard == null)
            return;

        m_keysIdPressed.Clear();
        m_keysIdReleased.Clear();

        for (int i = 0; i < 256; i++)
        {
            if (m_observedKeyboard.m_keyState.GetValue(i)!= m_previousState.GetKeyState(i))
            {
                if (m_observedKeyboard.GetValue(i))
                {
                    m_keysIdPressed.Add(i);
                }
                else
                {
                    m_keysIdReleased.Add(i);
                }

            }
        }

        m_keyDownDebug.Clear();
        for (int i = 0; i < 256; i++)
        {
            if (m_observedKeyboard.GetValue(i))
            {
                KeyWindowIntegerKeyMap.Instance.GetKeyInfoFromIntId(i,out bool found, out KeyInfoAsInteger info);
                if (found)
                {
                    m_keyDownDebug.Add(info);
                }
            }
        }

        if (m_keyDownDebug.Count == 1)
        {

            m_debugJoinKeysPressed = m_keyDownDebug[0].ToString();
            m_onDebugJoinKeysPressed.Invoke(m_debugJoinKeysPressed);
        }
        else { 

            m_debugJoinKeysPressed = string.Join(",", m_keyDownDebug.Select(x => x.m_windowKeyName).ToArray());
            m_onDebugJoinKeysPressed.Invoke(m_debugJoinKeysPressed);
        }

    }
}
