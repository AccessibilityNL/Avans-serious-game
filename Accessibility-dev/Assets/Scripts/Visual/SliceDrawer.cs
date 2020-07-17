using UnityEngine;
[RequireComponent(typeof(Camera))]
public class SliceDrawer : MonoBehaviour
{
    private new Camera camera;
    public Material lineMeterial;
    public float lineWidth;
    public float depth = 6;

    private Vector3? lineStartPoint = null;
    private int frames = 0;
    void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (frames > 1)
            {
                if (!lineStartPoint.HasValue)
                {
                    return;
                }
                var lineEndPoint = GetMouseCemrapoint();
                var gameObject = new GameObject();
                gameObject.name = "Line";

                var lineRenderer = gameObject.AddComponent<LineRenderer>();

                lineRenderer.material = lineMeterial;
                lineRenderer.SetPositions(new Vector3[] { lineStartPoint.Value, lineEndPoint });
                if (lineStartPoint.Value.Equals(lineEndPoint))
                {
                    return;
                }

                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
                lineRenderer.useWorldSpace = true;

                MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();

                Mesh mesh = new Mesh();
                lineRenderer.BakeMesh(mesh, true);
                meshCollider.sharedMesh = mesh;

                lineStartPoint = null;
            }

            frames++;

            if (frames > 0)
            {
                lineStartPoint = GetMouseCemrapoint();
                
            }
        }


    }

    private Vector3 GetMouseCemrapoint()
    {
        var input = Input.mousePosition;
        input.y = depth;
        var ray = camera.ScreenPointToRay(Input.mousePosition);

        return ray.origin + ray.direction * depth;
    }
}
