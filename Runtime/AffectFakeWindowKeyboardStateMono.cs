using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AffectFakeWindowKeyboardStateMono : MonoBehaviour
{
    public FakeWindowKeyboardStateMono m_target;

    public string m_lastReceived;
    
    public KeyInfoAsInteger m_lastPushedInfo;
    public bool m_lastFound;

    public IntegerKeyEvent m_lastPushed;
    public Queue<IntegerKeyEvent> m_receivedQueue = new Queue<IntegerKeyEvent>();
    public UnityEvent<IntegerKeyEvent> m_onIntegerActionUnityThread;
    public Action<IntegerKeyEvent> m_onIntegerActionsReceivedThread;



    public void PushInAsByteInteger(byte[] bytes) {

        if (bytes == null)
            return;
        if(bytes.Length!=4)
            return;
        int integer = BitConverter.ToInt32(bytes, 0);
        PushInAsInteger(integer);

    }

    public void PushInAsString(string commandAsString)
    {
        m_lastReceived = commandAsString;
        if(int.TryParse(commandAsString, out int integer))
        {
            PushInAsInteger(integer);
            return;
        }

        KeyWindowIntegerKeyMap.Instance.TryToGuessKeyInfo(
            commandAsString,
            out bool found,
            out KeyInfoAsInteger info,
            out bool isPress,
            out bool isRelease
            );
        if(found && info != null ) 
        PushInAsInteger(info.m_windowIntegerId255);

    }

    public void PushInAsInteger(int commandAsInteger)
    {
        m_lastReceived = commandAsInteger.ToString();
        KeyWindowIntegerKeyMap.Instance.TryToGuessKeyInfo(
          commandAsInteger.ToString(),
          out bool found,
          out KeyInfoAsInteger info,
          out bool isPress,
          out bool isRelease
          );
        m_lastFound = found;
        m_lastPushedInfo = info;
        
        if (found)
        {

            if (commandAsInteger == info.m_pressAsInteger)
            {
                SetValueFromId(info.m_windowIntegerId255, true);
                PushNotification(info.m_windowIntegerId255, true);
            }
            if (commandAsInteger == info.m_releaseAsInteger) {

                SetValueFromId(info.m_windowIntegerId255, false);
                PushNotification(info.m_windowIntegerId255, false);

            }
            if (commandAsInteger == info.m_windowIntegerId255) {
                SetValueFromId(info.m_windowIntegerId255, true);
                SetValueFromId(info.m_windowIntegerId255, false);
                PushNotification(info.m_windowIntegerId255, true);
                PushNotification(info.m_windowIntegerId255, false);
            }


        }

    }

    private void PushNotification(int m_windowIntegerId255, bool isPress)
    {
        IntegerKeyEvent integerKeyEvent = new IntegerKeyEvent();
        integerKeyEvent.m_integerCommand = m_windowIntegerId255;
        integerKeyEvent.m_whenActionHappened = DateTime.Now;
        m_receivedQueue.Enqueue(integerKeyEvent);
        m_onIntegerActionsReceivedThread?.Invoke(integerKeyEvent);
        m_lastPushed = integerKeyEvent;
        KeyWindowIntegerKeyMap.Instance.GetKeyInfoFromIntId(m_windowIntegerId255, out _, out m_lastPushedInfo);
    }
    public void Update()
    {
        while (m_receivedQueue.Count > 0)
        {
            IntegerKeyEvent integerKeyEvent = m_receivedQueue.Dequeue();
            m_onIntegerActionUnityThread?.Invoke(integerKeyEvent);
        }
    }

    private void SetValueFromId(int integerId255, bool isPress)
    {
        m_target.SetKeyState(integerId255, isPress);
    }


}
