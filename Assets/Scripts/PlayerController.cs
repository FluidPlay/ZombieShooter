using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 50f;
    public float TurnSpeed = 300f;
    private bool _cameraIsDefined;
    public float SmoothFactor = 0.25f;
    private Text scoreGuiText;
    public Transform weaponTransf;

    enum axis { Horizontal, Vertical }
    
    private void Awake()
    {
        _cameraIsDefined = (Camera.main != null);
    }

    void Update()
    {
        if (!_cameraIsDefined)
            return;
        
        if (Input.GetMouseButton(0))
            GetComponent<WeaponRifle>().Fire(weaponTransf.position, transform.rotation);

        //Game.Manager.Lives -= 1;
        
        var pointedWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dt = Time.deltaTime;

        SmoothLookAtPos(transform, pointedWorldPos, TurnSpeed, SmoothFactor, dt);
        
        // Criar método SmoothTranslate
        
        var translation = new Vector2(Input.GetAxis(axis.Horizontal+""), Input.GetAxis(axis.Vertical+""));

        SmoothTranslate(transform, translation, MoveSpeed, SmoothFactor, dt, true);

//        if (Input.GetMouseButton(1)) // 0 = left, 1 = right, 2 = middle
//            transform.position = Vector2.MoveTowards(transform.position, pointedWorldPos, MoveSpeed * dt);
    }
    
    class Armor {
        public void ApplyDamage() {}
    }

    private void SmoothTranslate(Transform transf, Vector2 translation, float moveSpeed, 
                                 float smoothFactor, float dt, bool checkNavmesh)
    {
        if (Mathf.Approximately(translation.x , 0f ) && Mathf.Approximately(translation.y, 0f) )
            return;

        translation = Vector3.Normalize(translation);    // Evita que as diagonais sejam mais rápidas que as ortogonais
        var smoothTranslation = Vector2.Lerp(Vector2.zero, translation, smoothFactor);

        var doTranslation = ! checkNavmesh;
        
        if (checkNavmesh)
        {
            var desiredPos = new Vector2(transf.position.x, transf.position.y) +
                             MoveSpeed * dt * smoothTranslation;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(desiredPos, out hit, 0.1f, NavMesh.AllAreas))
                doTranslation = true;
            
            
            
            
            
//            double longPi = 3.141592653589793238463; 
//            float pi = (float)longPi;
//            
//            
//            //* Coalescência Nula (Null Coallescence)
//            Debug.Log ( "Texto do Placar: "+ scoreGuiText.text ?? "inválido");
//
//
//            var a = 2;
//            
//            int x;
//            if (a > 0)
//                x = 1;
//            else
//                x = 2;
	
            //        <===>

            //int x = a > 0 ? 1 : 2;
        }
        
//        private Camera _gameCamera;
//        public Camera GameCamera { 	get { return _gameCamera ??= Camera.main; } 
//                                    set { _gameCamera = value; } }
        
        
        void PrintComponentPosition (Object component) {
            Debug.Log((component as MonoBehaviour).transform.position.x);
        }
        
        void CheckDamage(object otherScript) {
            if (otherScript is Armor armor) { 
                armor.ApplyDamage(); 
            }
        }

        if (doTranslation)
            transf.Translate(smoothTranslation * dt * moveSpeed, Space.World);
    }

    private void SmoothLookAtPos(Transform transf, Vector3 pointedWorldPos, float turnSpeed, float t, float dt)
    {
        var direction = pointedWorldPos - transf.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        var targetRotation = Quaternion.Euler(0f, 0f, angle);

        var smoothRotation = Quaternion.Slerp(transf.rotation, targetRotation, t);

        transf.rotation = Quaternion.RotateTowards(transf.rotation, smoothRotation, turnSpeed * dt);
    }
}
