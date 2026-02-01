using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class TapePlacerNetwork : NetworkBehaviour
{
    private TapePlacer _placer;
    [SerializeField]
    private Material _tapeMat;
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private TapeData _tapeData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _placer = GetComponent<TapePlacer>();
        _placer.OnQuadPlaced += OnQuadPlaced;
    }

    private void OnQuadPlaced(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        // Tell server to relay to others
        RelayQuadPlacedServerRpc(a, b, c, d, _tapeData.name);
    }

    [ServerRpc(InvokePermission = RpcInvokePermission.Everyone)]
    private void RelayQuadPlacedServerRpc(Vector3 a, Vector3 b, Vector3 c, Vector3 d, string tapeString, ServerRpcParams serverRpcParams = default)
    {
        ulong sender = serverRpcParams.Receive.SenderClientId;

        var targets = NetworkManager.Singleton.ConnectedClientsIds
            .Where(id => id != sender)
            .ToArray();

        var rpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams { TargetClientIds = targets }
        };

        SpawnLocalObjectClientRpc(a, b, c, d, tapeString, rpcParams);
    }

    [ClientRpc]
    private void SpawnLocalObjectClientRpc(Vector3 a, Vector3 b, Vector3 c, Vector3 d, string tapeString, ClientRpcParams rpcParams = default)
    {
        Debug.LogWarning($"Executing RPC {a}");

        foreach (TapeData tape in _playerSettings.AllTapes)
        {
            if (tape.name == tapeString)
            {
                _tapeMat = tape.material;
                break;
            }
        }

        GenerateGeometry(a, b, c, d);
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
        newObj.gameObject.name = $"RemoteTapeMesh{_tapeCount}";
        _tapeCount++;
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
