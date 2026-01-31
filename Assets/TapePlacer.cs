using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class TapePlacer : MonoBehaviour
{
    private TapeRaycaster _raycaster;
    private InputSystem_Actions _input;
    private bool _isTapeHeld;

    private Vector3 _lastLeftPoint;
    private Vector3 _lastRightPoint;

    private Vector3 _currentLeftPoint;
    private Vector3 _currentRightPoint;

    [SerializeField]
    private Material _tapeMat;

    // The tape connecting the last section to the player
    private Mesh _tempMesh;
    private GameObject _tempObj;

    void UpdateQuadMesh()
    {
        var l1 = _lastLeftPoint;
        var r1 = _lastRightPoint;
        
        var l2 = _currentLeftPoint;
        var r2 = _currentRightPoint;

        (var verts, var tris) = GetVerts(l1, r1, l2, r2);

        _tempMesh.Clear();              // clears old data but keeps the same Mesh object
        _tempMesh.vertices = verts;
        _tempMesh.triangles = tris;
        _tempMesh.RecalculateNormals();
    }


    void Start()
    {
        _raycaster = GetComponent<TapeRaycaster>();
        _raycaster.OnNewPointReached += NewPoint;
        _input = new();
        _input.Enable();
        _input.Player.Tape.performed += _ => StartTaping();
        _input.Player.Tape.canceled += _ => EndTaping();

        _tempObj = new GameObject();
        _tempObj.name = "TempTape";
        _tempMesh = new Mesh();
        _tempObj.AddComponent<MeshFilter>().sharedMesh = _tempMesh;
        _tempObj.AddComponent<MeshRenderer>().sharedMaterial = _tapeMat;
    }

    private void StartTaping()
    {
        _isTapeHeld = true;
        _lastLeftPoint = _raycaster.RaycastLeft();
        _lastRightPoint = _raycaster.RaycastRight();
    }

    private void EndTaping()
    {
        _isTapeHeld = false;
        Vector3 finalLeft = _raycaster.RaycastLeft();
        Vector3 finalRight = _raycaster.RaycastRight();
        GenerateGeometry(_lastLeftPoint, _lastRightPoint, finalLeft, finalRight);
    }

    private int _tapeCount;

    private void GenerateGeometry(Vector3 l1, Vector3 r1, Vector3 l2, Vector3 r2)
    {
        Mesh box = GenerateThickQuad(l1, r1, l2, r2);
        box.name = $"GeneratedMesh{_tapeCount}";
        GameObject newObj = new GameObject();
        newObj.AddComponent<MeshFilter>().sharedMesh = box;
        newObj.AddComponent<MeshRenderer>().sharedMaterial = _tapeMat;
        var collider = newObj.AddComponent<MeshCollider>();
        collider.sharedMesh = box;
        collider.convex = true;
        collider.enabled = false;
        newObj.gameObject.name = $"TapeMesh{_tapeCount}";
        _tapeCount ++;

        StartCoroutine(MakePhysical(collider));
    }

    IEnumerator MakePhysical(Collider col)
    {
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTapeHeld)
        {
            _currentLeftPoint = _raycaster.LeftRaycastPoint;
            _currentRightPoint = _raycaster.RightRaycastPoint;
            Debug.DrawLine(_lastLeftPoint, _currentLeftPoint, Color.red);
            Debug.DrawLine(_lastRightPoint, _currentRightPoint, Color.blue);
            UpdateQuadMesh();
            _tempObj.gameObject.SetActive(true);
        }
        else
        {
            _tempObj.gameObject.SetActive(false);
        }
    }

    private void NewPoint(Vector3 left, Vector3 right)
    {
        if (!_isTapeHeld)
            return;

        GenerateGeometry(_lastLeftPoint, _lastRightPoint, left, right);
        _lastLeftPoint = left;
        _lastRightPoint = right;
    }

    private (Vector3[], int[]) GetVerts(Vector3 l1, Vector3 r1, Vector3 l2, Vector3 r2, float thickness = 0.02f)
    {
        Vector3 offset = Vector3.up * thickness;

        l1.y = r1.y = Mathf.Lerp(l1.y, r1.y, 0.5f);
        l2.y = r2.y = Mathf.Lerp(l2.y, r2.y, 0.5f);

        l1 += offset;
        l2 += offset;
        r1 += offset;
        r2 += offset;

        // 8 vertices total
        Vector3[] verts = new Vector3[]
        {
            // Top
            l1, r1, l2, r2,

            // Bottom
            // l1b, r1b, l2b, r2b
        };

        Vector3 normal = Vector3.Cross(r1 - l1, l2 - l1);
        bool facingUp = Vector3.Dot(normal, Vector3.up) > 0f;

        // 12 triangles (6 faces × 2 tris)
        int[] tris = new int[]
        {
            // Top face
            2, 1, 0,
            3, 1, 2,
        };

        if (facingUp)
        {
            tris = new int[]
        {
            // Top face
            0, 1, 2,
            2, 1, 3,
            };
        }

        return (verts, tris);
    }

    private Mesh GenerateThickQuad(Vector3 l1, Vector3 r1, Vector3 l2, Vector3 r2, float thickness = 0.02f)
    {
        Mesh mesh = new Mesh();

        (var verts, var tris) = GetVerts(l1, r1, l2, r2, thickness);
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
