using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    public int teamID;
    public string PlayerName;
    private void Awake()
    {
        PlayerName = gameObject.name;
    }
}