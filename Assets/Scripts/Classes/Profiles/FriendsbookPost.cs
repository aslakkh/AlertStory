using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Date
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;

    public Date(int year, int month, int day, int hour, int minute)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public override string ToString()
    {
        return string.Format("{0}.{1}.{2} {3}:{4}", day, month, year, hour, minute);
    }
}

[Serializable]
public class FriendsbookPost : IComparable<FriendsbookPost>
{
    public string content;
    //public DateTime date;
    public Date date;
    public FriendsbookProfile to;
    public FriendsbookProfile from;

    public FriendsbookPost()
    {
        content = "";
        //date = new DateTime();
        date = new global::Date();
    }

    public FriendsbookPost(string content, Date date, FriendsbookProfile to, FriendsbookProfile from)
    {
        this.content = content;
        this.date = date;
        this.to = to;
        this.from = from;
    }

    public int CompareTo(FriendsbookPost that)
    {
        if(this.date.year < that.date.year)
        {
            return -1;
        }
        else if(this.date.year > that.date.year)
        {
            return 1;
        }
        else
        {
            if(this.date.month < that.date.month) { return -1; }
            else if(this.date.month > that.date.month) { return 1; }
            else
            {
                if(this.date.day < that.date.day) { return -1; }
                else if(this.date.month > that.date.month) { return 1; }
                else
                {
                    if(this.date.hour < that.date.hour) { return -1; }
                    else if(this.date.hour > that.date.hour) { return 1; }
                    else
                    {
                        if(this.date.minute < that.date.minute) { return -1; }
                        else if(this.date.minute > that.date.minute){ return 1; }
                        else{
                            return 0;
                        }
                    }
                }
            }
        }
    }

    public void SetDate(int year, int month, int day, int hour, int minute)
    {
        date.year = year;
        date.month = month;
        date.day = day;
        date.hour = hour;
        date.minute = minute;
    }
}
