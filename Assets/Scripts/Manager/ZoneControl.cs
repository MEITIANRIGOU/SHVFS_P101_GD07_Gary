using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneControl : MonoBehaviour
{
    List<ScoreComponent> PlayersInZone = new List<ScoreComponent>();

    int Zone_Control_Team_ID = -1;
    public int Zone_Control_Members_Count;

    public int scoreGetEachTick;

    float timer = 0;
    public float tick_inSeconds;
    private void OnTriggerEnter(Collider other)
    {
        timer = 0;

        ScoreComponent Player = other.gameObject.GetComponent<ScoreComponent>();

        if (TeamScoreManager.Teams.Exists(Team => Player.teamID == Team.teamID))
        {
            TeamScoreManager.Team theTeam = TeamScoreManager.Teams.Find(Team => Player.teamID == Team.teamID);
            if (!theTeam.members.Exists(ScoreComponent => ScoreComponent.PlayerName == Player.gameObject.name))
            {
                theTeam.members.Add(Player);
            }
        }
        else
        {
            TeamScoreManager.Teams.Add(new TeamScoreManager.Team());
            TeamScoreManager.Teams[TeamScoreManager.Teams.Count - 1].teamID = Player.teamID;
            TeamScoreManager.Teams[TeamScoreManager.Teams.Count - 1].members.Add(Player);
        }
        PlayersInZone.Add(Player);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!PlayersInZone.Exists(ScoreComponent => ScoreComponent.teamID != PlayersInZone[0].teamID) && PlayersInZone.Count >= Zone_Control_Members_Count)
        {
            Zone_Control_Team_ID = PlayersInZone[0].teamID;

            if (timer < tick_inSeconds)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;

                TeamScoreManager.Teams.Find(Team => Team.teamID == Zone_Control_Team_ID).teamScore++;

                Debug.Log("Team: " + Zone_Control_Team_ID + "\nScore: " + TeamScoreManager.Teams.Find(Team => Team.teamID == Zone_Control_Team_ID).teamScore);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ScoreComponent Player = other.gameObject.GetComponent<ScoreComponent>();
        PlayersInZone.Remove(Player);
    }
}