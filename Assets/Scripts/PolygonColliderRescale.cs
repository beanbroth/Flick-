using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonColliderRescale : MonoBehaviour
{
    [SerializeField, Delayed] float size = 1;
    PolygonCollider2D _col;
    List<Vector2> points = new List<Vector2>();
    float cachedSize = 1;
    private void OnValidate()
    {
        if (_col == null)
        {
            _col = GetComponent<PolygonCollider2D>();
        }
        List<Vector2> points = new List<Vector2>(_col.points);
        if (size != cachedSize)
        {
            for (int i = 0; i < points.Count; i++)
            {
                float percent = size / cachedSize;
                points[i] *= percent;
            }
            _col.points = points.ToArray();
            cachedSize = size;
        }
    }
}
