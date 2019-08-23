using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ItemConnection : MaskableGraphic
{

    public GameObject objectA;
    public GameObject objectB;

    public float width;

    LineRenderer lineRenderer;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (objectA == null || objectB == null) return;

        Vector3 pa = transform.InverseTransformPoint(objectA.transform.position);
        pa.z = 0;
        Vector3 pb = transform.InverseTransformPoint(objectB.transform.position);
        pb.z = 0;

        Vector2 direction = (pb - pa).normalized;

        direction = rotateVector(direction, 90);

        direction *= width;

        Vector3 v2 = new Vector3(direction.x, direction.y, 0);

        vh.AddVert(pa + v2, color, Vector2.zero);
        vh.AddVert(pa - v2, color, Vector2.zero);
        vh.AddVert(pb + v2, color, Vector2.zero);
        vh.AddVert(pb - v2, color, Vector2.zero);


        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(1, 2, 3);


    }

    public Vector2 rotateVector(Vector2 vec, float angle)
    {
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

        float tx = vec.x;
        float ty = vec.y;
        vec.x = (cos * tx) - (sin * ty);
        vec.y = (sin * tx) + (cos * ty);

        return vec;
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAllDirty();
    }
}
