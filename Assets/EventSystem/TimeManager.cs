using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private int hours = 7;
    [SerializeField]
    private int minutes = 0;
    [SerializeField]
    private int week = 1;
    [SerializeField]
    private int day = 0;

    private bool dayCange = false;
    private IEventManager eventManager;

    private void Awake()
    {
        eventManager = FindObjectOfType<EventManager>();
    }
    private void Start()
    {
        dayCange = true;
        eventManager.Trigger(new TimeChangedEvent(this, hours, minutes));
        eventManager.Trigger(new DayChangedEvent(this, day));
        eventManager.Trigger(new WeekChangedEvent(this, week));
    }

    private void OnEnable()
    {
        eventManager.AddListener<AdvanceMinutesEvent>(this.ChangeMin);
        eventManager.AddListener<AdvanceHoursEvent>(this.ChangeHours);
    }

    private void OnDisable()
    {
        eventManager.RemoveListener<AdvanceMinutesEvent>(this.ChangeMin);
        eventManager.RemoveListener<AdvanceHoursEvent>(this.ChangeHours);
    }

    private void Update()
    {
        if (!dayCange) return;
        
        dayCange = false;
        day++;
        eventManager.Trigger(new DayChangedEvent(this, day));
    }

    private void ChangeMin(AdvanceMinutesEvent e)
    {
        Debug.Log("ADD MINUTES");
        if (minutes + e.MinuteAmount >= 60)
        {
            minutes = (minutes + e.MinuteAmount) - 60;
            eventManager.Trigger(new AdvanceHoursEvent(this, 1));
        }
        else
        {
            minutes += e.MinuteAmount;
        }
        eventManager.Trigger(new TimeChangedEvent(this, hours, minutes));
    }

    private void ChangeHours(AdvanceHoursEvent e)
    {
        if (hours + e.HourAmount > 23)
        {
            hours = (hours + e.HourAmount) - 24;
            dayCange = true;
            if (day == 7)
            {
                day = 0;
                week++;
                eventManager.Trigger(new WeekChangedEvent(this, week));
            }
        }
        else
        {
            hours += e.HourAmount;
        }
        eventManager.Trigger(new TimeChangedEvent(this, hours, minutes));
    }
}

#region TimeEvents
public class AdvanceHoursEvent : EventBase
{
    public int HourAmount;

    public AdvanceHoursEvent(object sender, int hourAmount) : base(sender)
    {
        this.HourAmount = hourAmount;
    }
}

public class AdvanceMinutesEvent : EventBase
{
    public int MinuteAmount;

    public AdvanceMinutesEvent(object sender, int minuteAmount) : base(sender)
    {
        this.MinuteAmount = minuteAmount;
    }
}

public class TimeChangedEvent : EventBase
{
    public int Hour;
    public int Minute;

    public TimeChangedEvent(object sender, int hourAmount, int minuteAmount) : base(sender)
    {
        this.Hour = hourAmount;
        this.Minute = minuteAmount;
    }
}

public class DayChangedEvent : EventBase
{
    public int Day;

    public DayChangedEvent(object sender, int day) : base(sender)
    {
        this.Day = day;
    }
}

public class WeekChangedEvent : EventBase
{
    public int Week;

    public WeekChangedEvent(object sender, int week) : base(sender)
    {
        this.Week = week;
    }
}
#endregion