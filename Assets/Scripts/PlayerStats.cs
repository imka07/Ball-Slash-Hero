using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text dailyRecordText, weeklyRecord1Text, weeklyRecord2Text, monthlyRecord1Text, monthlyRecord2Text, bestEverRecordText;
    public Text dailyRecordDateText, weeklyRecord1DateText, weeklyRecord2DateText, monthlyRecord1DateText, monthlyRecord2DateText, bestEverRecordDateText;

    private string test;
    private int dailyRecord = 0;
    private int weeklyRecord1 = 0, weeklyRecord2 = 0;
    private int monthlyRecord1 = 0, monthlyRecord2 = 0;
    private int bestEverRecord = 0;

    private DateTime lastDailyReset;
    private DateTime lastWeeklyReset;
    private DateTime lastMonthlyReset;

    private string dailyKey = "dailyRecord";
    private string weeklyKey1 = "weeklyRecord1";
    private string weeklyKey2 = "weeklyRecord2";
    private string monthlyKey1 = "monthlyRecord1";
    private string monthlyKey2 = "monthlyRecord2";
    private string bestEverKey = "bestEverRecord";

    private string dailyDateKey = "dailyRecordDate";
    private string weeklyDate1Key = "weeklyRecord1Date";
    private string weeklyDate2Key = "weeklyRecord2Date";
    private string monthlyDate1Key = "monthlyRecord1Date";
    private string monthlyDate2Key = "monthlyRecord2Date";
    private string bestEverDateKey = "bestEverRecordDate";

    private string dailyResetKey = "dailyReset";
    private string weeklyResetKey = "weeklyReset";
    private string monthlyResetKey = "monthlyReset";

    private void Start()
    {
        LoadPlayerStats();
        UpdateStatsDisplay();
    }

    public void CheckNewRecord(int score)
    {
        DateTime now = DateTime.Now;

        ResetRecordsIfNecessary(now);

        if (score > dailyRecord)
        {
            dailyRecord = score;
            PlayerPrefs.SetInt(dailyKey, dailyRecord);
            PlayerPrefs.SetString(dailyDateKey, now.ToString());
        }

        if (score > weeklyRecord1)
        {
            weeklyRecord2 = weeklyRecord1;
            PlayerPrefs.SetInt(weeklyKey2, weeklyRecord2);
            PlayerPrefs.SetString(weeklyDate2Key, PlayerPrefs.GetString(weeklyDate1Key));

            weeklyRecord1 = score;
            PlayerPrefs.SetInt(weeklyKey1, weeklyRecord1);
            PlayerPrefs.SetString(weeklyDate1Key, now.ToString());
        }
        else if (score > weeklyRecord2)
        {
            weeklyRecord2 = score;
            PlayerPrefs.SetInt(weeklyKey2, weeklyRecord2);
            PlayerPrefs.SetString(weeklyDate2Key, now.ToString());
        }

        if (score > monthlyRecord1)
        {
            monthlyRecord2 = monthlyRecord1;
            PlayerPrefs.SetInt(monthlyKey2, monthlyRecord2);
            PlayerPrefs.SetString(monthlyDate2Key, PlayerPrefs.GetString(monthlyDate1Key));

            monthlyRecord1 = score;
            PlayerPrefs.SetInt(monthlyKey1, monthlyRecord1);
            PlayerPrefs.SetString(monthlyDate1Key, now.ToString());
        }
        else if (score > monthlyRecord2)
        {
            monthlyRecord2 = score;
            PlayerPrefs.SetInt(monthlyKey2, monthlyRecord2);
            PlayerPrefs.SetString(monthlyDate2Key, now.ToString());
        }

        if (score > bestEverRecord)
        {
            bestEverRecord = score;
            PlayerPrefs.SetInt(bestEverKey, bestEverRecord);
            PlayerPrefs.SetString(bestEverDateKey, now.ToString());
        }

        UpdateStatsDisplay();
    }

    private void ResetRecordsIfNecessary(DateTime now)
    {
        if ((now - lastDailyReset).TotalDays >= 1)
        {
            dailyRecord = 0;
            lastDailyReset = now;
            PlayerPrefs.SetInt(dailyKey, dailyRecord);
            PlayerPrefs.SetString(dailyResetKey, now.ToString());
        }
        if ((now - lastWeeklyReset).TotalDays >= 7)
        {
            weeklyRecord1 = 0;
            weeklyRecord2 = 0;
            lastWeeklyReset = now;
            PlayerPrefs.SetInt(weeklyKey1, weeklyRecord1);
            PlayerPrefs.SetInt(weeklyKey2, weeklyRecord2);
            PlayerPrefs.SetString(weeklyResetKey, now.ToString());
        }

        if ((now - lastMonthlyReset).TotalDays >= 30)
        {
            monthlyRecord1 = 0;
            monthlyRecord2 = 0;
            lastMonthlyReset = now;
            PlayerPrefs.SetInt(monthlyKey1, monthlyRecord1);
            PlayerPrefs.SetInt(monthlyKey2, monthlyRecord2);
            PlayerPrefs.SetString(monthlyResetKey, now.ToString());
        }
    }

    private void LoadPlayerStats()
    {
        dailyRecord = PlayerPrefs.GetInt(dailyKey, 0);
        weeklyRecord1 = PlayerPrefs.GetInt(weeklyKey1, 0);
        weeklyRecord2 = PlayerPrefs.GetInt(weeklyKey2, 0);
        monthlyRecord1 = PlayerPrefs.GetInt(monthlyKey1, 0);
        monthlyRecord2 = PlayerPrefs.GetInt(monthlyKey2, 0);
        bestEverRecord = PlayerPrefs.GetInt(bestEverKey, 0);

        lastDailyReset = DateTime.Parse(PlayerPrefs.GetString(dailyResetKey, DateTime.Now.ToString()));
        lastWeeklyReset = DateTime.Parse(PlayerPrefs.GetString(weeklyResetKey, DateTime.Now.ToString()));
        lastMonthlyReset = DateTime.Parse(PlayerPrefs.GetString(monthlyResetKey, DateTime.Now.ToString()));
    }

    private void UpdateStatsDisplay()
    {
        dailyRecordText.text = dailyRecord.ToString();
        weeklyRecord1Text.text = weeklyRecord1.ToString();
        weeklyRecord2Text.text = weeklyRecord2.ToString();
        monthlyRecord1Text.text = monthlyRecord1.ToString();
        monthlyRecord2Text.text = monthlyRecord2.ToString();
        bestEverRecordText.text = bestEverRecord.ToString();



        dailyRecordDateText.text = PlayerPrefs.GetString(dailyDateKey, "No Record");
        weeklyRecord1DateText.text = PlayerPrefs.GetString(weeklyDate1Key, "No Record");
        weeklyRecord2DateText.text = PlayerPrefs.GetString(weeklyDate2Key, "No Record");
        monthlyRecord1DateText.text = PlayerPrefs.GetString(monthlyDate1Key, "No Record");
        monthlyRecord2DateText.text = PlayerPrefs.GetString(monthlyDate2Key, "No Record");
        bestEverRecordDateText.text = PlayerPrefs.GetString(bestEverDateKey, "No Record");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
