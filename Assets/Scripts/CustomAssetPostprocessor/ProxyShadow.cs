using UnityEditor;
using UnityEngine;

public class ProxyShadow : AssetPostprocessor
{
    public void OnPostprocessModel(GameObject gameObject)
    {
        var children = gameObject.transform.GetComponentsInChildren<GameObject>();
        if (children == null || children.Length == 0)
        {
            return;
        }

        var componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();

        foreach (var meshRenderer in componentsInChildren)
        {
            if (meshRenderer.transform.name.EndsWith(".shadow"))
            {
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                meshRenderer.receiveShadows = true;
            }
            else
            {
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                meshRenderer.receiveShadows = false;
            }
        }
        
		////嘗試新增MeshCollider，目前還不成功，要再試一下
        // foreach (var child in children)
        // {
        //     if (child.name.EndsWith(".collider"))
        //     {
        //         var addComponent = child.AddComponent<MeshCollider>();
        //         addComponent.sharedMesh = child.GetComponent<MeshFilter>().sharedMesh;
        //         Object.DestroyImmediate(child);
        //     }
        // }
    }
}