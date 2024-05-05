using DefaultNamespace;
using UnityEngine;

public class SuckAnimation : MonoBehaviour
{
    public ParticleSystem ps;
    void Start() {
        ps = GetComponent<ParticleSystem>();
    }
 
    void Update()
    {
        if (Globals.sosModeEnabled)
        {
            var main = ps.main;
            main.startColor = Color.white;
        }
    }
}
