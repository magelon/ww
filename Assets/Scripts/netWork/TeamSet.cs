using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSet : MonoBehaviour
{
    int team;
    void Start()
    {
        team = 0;
    }

    public int getATeam()
    {
        int giveTeam = team;
        team++;
        return giveTeam;
    }
}
