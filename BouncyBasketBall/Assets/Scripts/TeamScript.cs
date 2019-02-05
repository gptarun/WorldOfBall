using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript {
    private string teamName;
    private string shortName;
    private string mode;
    private Image flag;

    public TeamScript(TeamScript teamScript)
    {
        this.teamName = teamScript.teamName;
        this.shortName = teamScript.shortName;
        this.mode = teamScript.mode;
        this.flag = teamScript.flag;
    }

    public TeamScript(string teamName, string shortName, string mode)
    {
        this.teamName = teamName;
        this.shortName = shortName;
        this.mode = mode;
    }

    public TeamScript(string teamName, string shortName, string mode, Image flag)
    {
        this.teamName = teamName;
        this.shortName = shortName;
        this.mode = mode;
        this.flag = flag;
    }

    public string Mode
    {
        get
        {
            return mode;
        }

        set
        {
            mode = value;
        }
    }

    public string ShortName
    {
        get
        {
            return shortName;
        }

        set
        {
            shortName = value;
        }
    }

    public string TeamName
    {
        get
        {
            return teamName;
        }

        set
        {
            teamName = value;
        }
    }

    public Image Flag
    {
        get
        {
            return flag;
        }

        set
        {
            flag = value;
        }
    }

}
