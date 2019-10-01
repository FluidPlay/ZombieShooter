using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[System.Serializable]                    // Possibilita a serialização de classes aninhadas
[RequireComponent(typeof(Rigidbody))]    // Se o gameObject host não tiver esse componente, adiciona 
public class PropertiesAndExtraOperators : MonoBehaviour {
    
    [SerializeField]
    private Camera _gameCamera;

    public Camera GameCamera {
        get {
            _gameCamera = Camera.main;
            return _gameCamera;
        }
        set { _gameCamera = value; }
    }
    
//    public Camera GameCamera { 	get { return _gameCamera ??= Camera.main; } 
//                                set { _gameCamera = value; } }
    
    [SerializeField] // Serializes (saves) and show private fields
    private Collider col;

    [HideInInspector] // Hide public fields, they're still serialized
    public Rigidbody rb;
    
    public Text scoreGuiText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    class Armor {
        public void ApplyDamage()
        { }
    }

    [ContextMenu("Do Something")]
    void DoSomething()
    {
        Debug.Log("Perform operation");
    }

    [Range(-100, 100)] public int Speed = 0;

    [TextArea] // Text goes below the field title
    public string SomeTextArea;

    [Multiline(8)] public string playerBiography = "";

    [Header("Health Settings")] public int health = 0;
    [Space] public int maxHealth = 100;
    [Space(10)] public int StartingHealth = 50;

    public enum MouseButton {
        [InspectorName("Left Click")] Left = 0,
        [InspectorName("Right Click")] Right = 1,
        [InspectorName("Middle Click")] Middle = 2,
    }

    public MouseButton buttonSelector;

    [Tooltip("A float using the Range attribute")] [Range(0f, 1f)] [SerializeField]
    float AfloatNumber;
    
    [ContextMenuItem("RandomValue", nameof(RandomizeValueFromRightClick))]
    public float randomValue;
    private void RandomizeValueFromRightClick()
    {
        randomValue = Random.Range(-5f, 5f);
    }

    public float rangedFloat;
    [ContextMenu("Choose Random Values")]
    private void ChooseRandomValues()
    {
        rangedFloat = Random.Range(-5f, 5f);
    }

    //=======================

    private void ExtraCSharpOperators()
    {

    //====== Coalescência Nula (Null Coallescence)
    Debug.Log ( "Texto do Placar: "+ scoreGuiText.text ?? "inválido");

    //====== Operador Ternário (Condicional)    
    int a = 2;
    
//    int x;
//    if (a > 0)
//        x = 1;
//    else
//        x = 2;

    //Equivale a:
    int x = a > 0 ? 1 : 2;

    //* Propriedade (Property)
    // Campo com Getter e Setter reativos (disparam eventos quando lidos/salvos)
    //
    //        private Camera _gameCamera;
    //        public Camera GameCamera { 	get { return _gameCamera ??= Camera.main; } 
    //                                    set { _gameCamera = value; } }
    
    // public Camera GameCamera { get; set; } // Auto-propriedade
    
    //====== Typecast Padrão (roda em tempo de compilação)
    double longPi = 3.141592653589793238463; 
    float pi = (float)longPi;
    
    //======= Operador As 
    /// Em runtime, converte entre dois structs ou duas classes
    void PrintComponentPosition (Object component) {
        Debug.Log((component as MonoBehaviour).transform.position.x);
    }

    //======= Operador Is
    /// Verifica tipo e retorna um boolean
    void CheckDamage(object otherScript) {
        if (otherScript is Armor armor) {
            armor.ApplyDamage(); 
        }
    }

    //public Collider col;
    //======== Outras opções: GetType(), typeof(), cast + null test
    Debug.Log(col.GetType() == typeof(SphereCollider)); // false 

    /// typeof é avaliado em tempo de compilação e GetType() em runtime (tempo de execução)
    var sphereCol = col as SphereCollider;
    if (sphereCol != null)
        Debug.Log("Found Sphere Collider");	
    

}
}
