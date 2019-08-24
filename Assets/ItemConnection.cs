using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ItemConnection : MaskableGraphic
{

    public GameObject objectA;
    public GameObject objectB;
    public InventoryItemMenu menu;

    Color currentColor;

    public float width;
    public bool isOnMouse;

    Vector2[] corners = new Vector2[4];

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

        corners[0] = pa + v2;
        corners[1] = pa - v2;
        corners[2] = pb - v2;
        corners[3] = pb + v2;

        for(int i = 0; i < 4; i ++)
            vh.AddVert(corners[i], currentColor, Vector2.zero);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(0, 2, 3);

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

    public bool IsPointInPolygon(Vector2 point, Vector2[] polygon)
    {
        int polygonLength = polygon.Length, i = 0;
        bool inside = false;
        // x, y for tested point.
        float pointX = point.x, pointY = point.y;
        // start / end point for the current polygon segment.
        float startX, startY, endX, endY;
        Vector2 endPoint = polygon[polygonLength - 1];
        endX = endPoint.x;
        endY = endPoint.y;
        while (i < polygonLength) {
            startX = endX; startY = endY;
            endPoint = polygon[i++];
            endX = endPoint.x; endY = endPoint.y;
            //
            inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
                      && /* if so, test if it is under the segment */
                      ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
        }
        return inside;
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

        currentColor = color;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = transform.InverseTransformPoint(mousePosition);

        if(IsPointInPolygon(mousePosition, corners) && !isOnMouse) {
            currentColor = Color.red;

            if (Input.GetMouseButtonDown(0)) {
                menu.OpenMenu(null, null, this, false, true);
            }
        }
    }
}
