using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Transform clockHours = null;
    [SerializeField]
    private Transform clockMinutes = null;

    private void Update()
    {
        if (clockHours == null || clockMinutes == null)
		{
            return;
		}

        TimeZoneInfo.ClearCachedData();
        DateTime UTCNow = DateTime.UtcNow;
        TimeSpan offset = TimeZoneInfo.Local.BaseUtcOffset;
        DateTime time = UTCNow + offset;
        Debug.LogError(time.Hour + ":" + time.Minute);
        float hourLoopCompletionRate = (float)(time.Hour % 12) / 12.0f;
        float minuteLoopCompletionRate = (float)(time.Minute) / 60.0f;
        Vector3 rotation = clockHours.transform.localEulerAngles;
        clockHours.transform.localEulerAngles = new Vector3(rotation.x, rotation.y, -hourLoopCompletionRate * 360.0f);
        rotation = clockMinutes.transform.localEulerAngles;
        clockMinutes.transform.localEulerAngles = new Vector3(rotation.x, rotation.y, -minuteLoopCompletionRate * 360.0f);
    }
}