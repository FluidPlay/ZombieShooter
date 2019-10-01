using UnityEngine;
using UnityEngine.AI;
using Utilities;

public class PlayerController : MonoBehaviour {

    [Header("Transform Settings")] public float MoveSpeed = 50f;
    public float TurnSpeed = 300f;
    public float SmoothFactor = 0.25f;
    [Header("Extra Settings")] public float zRotationOffset = -90f;
    public Transform weaponTransf;

    private bool _cameraIsDefined;
    private Quaternion _rotationOffset;

    enum axis {
        Horizontal,
        Vertical
    }

    private void Awake()
    {
        _cameraIsDefined = (Camera.main != null);
    }

    void Update()
    {
        if (!_cameraIsDefined)
            return;

        if (Input.GetMouseButtonDown(0)) {
            GetComponent<WeaponRifle>().Fire(this);
        }

        //Game.Manager.Lives -= 1;

        var pointedWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dt = Time.deltaTime;

        transform.SmoothLookAtPos(pointedWorldPos, TurnSpeed, SmoothFactor, dt);

        // Criar método SmoothTranslate

        var translation = new Vector2(Input.GetAxis(axis.Horizontal + ""), Input.GetAxis(axis.Vertical + ""));

        SmoothTranslate(transform, translation, MoveSpeed, SmoothFactor, dt, true);

//        if (Input.GetMouseButton(1)) // 0 = left, 1 = right, 2 = middle
//            transform.position = Vector2.MoveTowards(transform.position, pointedWorldPos, MoveSpeed * dt);
    }


    private void SmoothTranslate(Transform transf, Vector2 translation, float moveSpeed,
        float smoothFactor, float dt, bool checkNavmesh)
    {
        if (Mathf.Approximately(translation.x, 0f) && Mathf.Approximately(translation.y, 0f))
            return;

        translation = Vector3.Normalize(translation); // Evita que as diagonais sejam mais rápidas que as ortogonais
        var smoothTranslation = Vector2.Lerp(Vector2.zero, translation, smoothFactor);

        var doTranslation = !checkNavmesh;

        if (checkNavmesh) {
            var desiredPos = new Vector2(transf.position.x, transf.position.y) +
                             MoveSpeed * dt * smoothTranslation;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(desiredPos, out hit, 0.1f, NavMesh.AllAreas))
                doTranslation = true;
        }

        if (doTranslation)
            transf.Translate(smoothTranslation * dt * moveSpeed, Space.World);
    }
}