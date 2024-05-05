using UnityEngine;

public class CheatCode : MonoBehaviour
{
    [SerializeField] private string[] cheatCode = {"s", "o", "s"};
    [SerializeField] private int index;

    public delegate void OnCheatActivated();

    public static OnCheatActivated onCheatActivated;
    
    void Start() {
        index = 0;    
    }
 
    void Update() {
        if (Input.anyKeyDown) {
            if (Input.GetKeyDown(cheatCode[index])) {
                index++;
            }
            else {
                index = 0;    
            }
        }
     
        if (index == cheatCode.Length) {
            onCheatActivated?.Invoke();
            index = 0;
        }
    }
}