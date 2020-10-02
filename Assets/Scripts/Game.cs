using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game Manager;
    public int Lives = 3;
    public Transform SceneRoot;

    //Backing Field
    [SerializeField]
    private int _score = 0;
    public int Score {
        get { return _score; }
        set {
            _score = value;
            UIScoreTxt.text = (_score + "").PadLeft(4, '0');
        }
    }
    public PlayerController Player;
    [Header("UI")]
    public TextMeshProUGUI UIScoreTxt;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Manager == null)
            Manager = this;
    }

}
