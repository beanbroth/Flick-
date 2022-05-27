using PathCreation;
using UnityEngine;

namespace PathCreation.Examples
{

    [ExecuteInEditMode]
    public class PathPlacer : PathSceneTool
    { 
        public GameObject prefab;
        public GameObject holder;
        public float coinSpacing;
        
        float spacing;
        static float forcedMinSpacing = 0.2f;
        private float meshMinSpacing;

        void Generate()
        {
           
            meshMinSpacing = prefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
            if (pathCreator != null && prefab != null && holder != null)
            {
                DestroyObjects();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(meshMinSpacing + coinSpacing, forcedMinSpacing);
                float dst = 0;

                while (dst < path.length)
                {
                    Vector3 point = path.GetPointAtDistance(dst);
                    Instantiate(prefab, point, Quaternion.identity, holder.transform);
                    dst += spacing;
                }
            }
        }

        void DestroyObjects()
        {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(holder.transform.GetChild(i).gameObject, false);
            }
        }

        protected override void PathUpdated()
        {
            if (pathCreator != null)
            {
                Generate();
            }
        }
    }
}