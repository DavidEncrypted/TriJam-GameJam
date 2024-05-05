using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTargetRigidbodyToChildren : MonoBehaviour
{
    private Rigidbody2D targetRB;

    private TriSpiderTeam[] TriSpiderTeamMembers;
    // Start is called before the first frame update
    void Start()
    {
        
        targetRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        // Get all the sprite renderers in the children
        TriSpiderTeamMembers = GetComponentsInChildren<TriSpiderTeam>();

        // Set the color of each sprite renderer
        foreach (TriSpiderTeam teamMember in TriSpiderTeamMembers)
        {
            teamMember.PlayerRB = targetRB;
        }
    }
}