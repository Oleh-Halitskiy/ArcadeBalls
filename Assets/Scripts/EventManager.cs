using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<float> PointAddedEvent;
    public static void StartPointAddedEvent(float amount)
    {
        PointAddedEvent?.Invoke(amount);
    }
}
