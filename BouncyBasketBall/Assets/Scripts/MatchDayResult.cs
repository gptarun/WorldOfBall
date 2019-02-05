using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchDayResult {
    private TeamScript winner;
    private int winScore;
    private TeamScript loser;
    private int loseScore;

    public MatchDayResult(TeamScript winner, int winScore, TeamScript loser, int loseScore)
    {
        this.winner = winner;
        this.winScore = winScore;
        this.loser = loser;
        this.loseScore = loseScore;
    }

    public TeamScript Winner
    {
        get
        {
            return winner;
        }

        set
        {
            winner = value;
        }
    }

    public int WinScore
    {
        get
        {
            return winScore;
        }
        set
        {
            winScore = value;
        }
    }

    public TeamScript Loser
    {
        get
        {
            return loser;
        }

        set
        {
            loser = value;
        }
    }

    public int LoseScore
    {
        get
        {
            return loseScore;
        }
        set
        {
            loseScore = value;
        }
    }

}
