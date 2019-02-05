using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchDay{
    private TeamScript teamA;
    private TeamScript teamB;
    private string currButtonName;
    private MatchDayResult matchDayResult;

    public MatchDay()
    {

    }

    public MatchDay(TeamScript teamA, TeamScript teamB, string currButtonName)
    {
        this.teamA = teamA;
        this.teamB = teamB;
        this.currButtonName = currButtonName;
    }

    public MatchDay(TeamScript teamA, TeamScript teamB, string currButtonName, MatchDayResult matchDayResult)
    {
        this.teamA = teamA;
        this.teamB = teamB;
        this.currButtonName = currButtonName;
        this.matchDayResult = matchDayResult;
    }

    public TeamScript TeamA
    {
        get
        {
            return teamA;
        }
        set
        {
            teamA = value;
        }
    }

    public TeamScript TeamB
    {
        get
        {
            return teamB;
        }
        set
        {
            teamB = value;
        }
    }

    public string CurrButtonName
    {
        get
        {
            return currButtonName;
        }
        set
        {
            currButtonName = value;
        }
    }

    public MatchDayResult MatchDayResult
    {
        get
        {
            return matchDayResult;
        }
        set
        {
            matchDayResult = value;
        }
    }

}
