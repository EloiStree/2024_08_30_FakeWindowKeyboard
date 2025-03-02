using System;
using System.Collections;
using UnityEngine;

public class FakeWindowKeyboardStateMono : MonoBehaviour
{

    public FakeWindowKeyboardState m_keyState= new FakeWindowKeyboardState();

    public bool GetValue(int i)
    {
        if (m_keyState == null)
            m_keyState = new FakeWindowKeyboardState();
        return m_keyState.GetValue(i);
    }

    public void SetKeyState(int integerId255, bool isPress)
    {
        m_keyState.SetKeyState(integerId255, isPress);
    }
}

[System.Serializable]
public struct IntegerKeyEvent {

    public int m_integerCommand;
    public DateTime m_whenActionHappened;
}

[System.Serializable]
public class FakeWindowKeyboardState
{
    public bool[] m_keyStateAsHexa255= new bool[256];


    public bool GetValue(int i)
    {
        CheckSize();
        return m_keyStateAsHexa255[i];
    }

    private void CheckSize()
    {
        if (m_keyStateAsHexa255==null || m_keyStateAsHexa255.Length != 256)
            m_keyStateAsHexa255 = new bool[256];
    }

    public FakeWindowKeyboardState()
    {
        m_keyStateAsHexa255 = new bool[256];
    }

    public void SetKeyState(int keyIntId, bool state)
    {
        m_keyStateAsHexa255[keyIntId] = state;
    }

    public bool GetKeyState(int keyIntId)
    {
        return m_keyStateAsHexa255[keyIntId];
    }
}
