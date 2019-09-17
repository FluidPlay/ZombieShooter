using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game Manager;
    public int Lives = 3;
    public PlayerController Player;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Manager == null)
            Manager = this;
    }

}
