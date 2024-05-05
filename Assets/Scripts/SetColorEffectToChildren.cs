using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorEffectToChildren : MonoBehaviour
{
    [SerializeField]
    private Color colorToTurnTo = Color.blue;

    private Renderer[] TriSpiderTeamMembers;
    // Start is called before the first frame update
    void Start()
    {
                
    }

    void Update()
    {
        TriSpiderTeamMembers = GetComponentsInChildren<Renderer>();
        foreach (Renderer teamMember in TriSpiderTeamMembers)
        {
            teamMember.material.color = colorToTurnTo;
        }
    }
}