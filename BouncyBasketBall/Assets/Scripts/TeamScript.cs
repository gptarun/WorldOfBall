using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript {
    private string teamName;
    private string shortName;
    private string mode;
    private Image flag;
    public TeamScript(string teamName, string shortName, string mode, Image flag)
    {
        TeamName = teamName;
        ShortName = shortName;
        Mode = mode;
        Flag = flag;
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
