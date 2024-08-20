using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventTest : MonoBehaviour
{
    public void StartTimeEvent()
    {
        TimeSystem.OnStart?.Invoke();
    }
}
