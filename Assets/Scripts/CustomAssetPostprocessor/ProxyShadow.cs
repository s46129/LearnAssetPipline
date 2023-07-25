using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProxyShadow : AssetPostprocessor
{
    public void OnPostprocessModel(GameObject gameObject)
    {
        List<GameObject> removeObject = new List<GameObject>();
        foreach (Transform childTransform in gameObject.transform)
        {
            Debug.Log(childTransform.name);
            if (childTransform.name.EndsWith(".collider"))
            {
                var collider = gameObject.AddComponent<MeshCollider>();
                var meshFilter = childTransform.gameObject.GetComponent<MeshFilter>();
                if (meshFilter)
                {
                    collider.sharedMesh = meshFilter.sharedMesh;
                }

                gameObject.AddComponent<Rigidbody>();
                var meshRenderer = childTransform.GetComponent<MeshRenderer>();
                meshRenderer.enabled = false;
                removeObject.Add(childTransform.gameObject);
                continue;
            }

            if (childTransform.name.EndsWith(".shadow"))
            {
                var meshRenderer = childTransform.GetComponent<MeshRenderer>();
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                meshRenderer.receiveShadows = true;
            }
            else
            {
                var meshRenderer = childTransform.GetComponent<MeshRenderer>();
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                meshRenderer.receiveShadows = false;
            }
        }

        removeObject.ForEach(Object.DestroyImmediate);
    }
}