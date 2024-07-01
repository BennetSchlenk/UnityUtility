using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text dayText;
    public TMP_Text weekText;

    public Button AddHourButton;
    public Button AddThreeHoursButton;
    public Button AddHalfHourButton;
    
    private IEventManager eventManager;

    private void Awake()
    {
        eventManager = FindObjectOfType<EventManager>();
    }

    private void Start()
    {
        AddHourButton.onClick.AddListener(() => AddHours(1));
        AddThreeHoursButton.onClick.AddListener(() => AddHours(3));
        AddHalfHourButton.onClick.AddListener(() => AddMinutes(30));
    }
    
    void OnEnable()
    {
        eventManager.AddListener<TimeChangedEvent>(OnTimeChanged);
        eventManager.AddListener<DayChangedEvent>(OnDayChanged);
        eventManager.AddListener<WeekChangedEvent>(OnWeekChanged);
    }

    void OnDisable()
    {
        eventManager.RemoveListener<TimeChangedEvent>(OnTimeChanged);
        eventManager.RemoveListener<DayChangedEvent>(OnDayChanged);
        eventManager.RemoveListener<WeekChangedEvent>(OnWeekChanged);
    }
    
    private void AddHours(int addHours)
    {
        eventManager.Trigger(new AdvanceHoursEvent(this,addHours));
    }
    
    private void AddMinutes(int addMinutes)
    {
        eventManager.Trigger(new AdvanceMinutesEvent(this,addMinutes));
    }
    
    private void OnTimeChanged(TimeChangedEvent e)
    {
        timeText.text = e.Hour.ToString("D2") + ":" + e.Minute.ToString("D2");
    }
    
    private void OnDayChanged(DayChangedEvent e)
    {
        dayText.text = e.Day switch
        {
            1 => "Monday",
            2 => "Tuesday",
            3 => "Wednesday",
            4 => "Thursday",
            5 => "Friday",
            6 => "Saturday",
            7 => "Sunday",
            _ => "XXXXXX"
        };
    }
    
    private void OnWeekChanged(WeekChangedEvent e)
    {
        weekText.text =  "Week: " + e.Week;
    }
}
