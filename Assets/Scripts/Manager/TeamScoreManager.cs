using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamScoreManager : MonoBehaviour
{
    public static List<Team> Teams = new List<Team>();
    public List<Team> m_Teams;
    public class Team
    {
        public List<ScoreComponent> members = new List<ScoreComponent>();
        public int teamID;
        public int teamScore = 0;
    }
    private void FixedUpdate()
    {
        m_Teams = Teams;
    }
}