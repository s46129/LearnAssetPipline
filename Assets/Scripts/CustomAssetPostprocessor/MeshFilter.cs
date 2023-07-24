using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CustomAssetPostprocessor
{
    public class MeshFilter : AssetPostprocessor
    {
        private void OnPostprocessModel(GameObject gameObject)
        {
            var assetGuid = AssetDatabase.GUIDFromAssetPath(assetImporter.assetPath);
            //確認Label設定為MeshFilter的才執行
            if (!AssetDatabase.GetLabels(assetGuid).Contains("MeshFilter"))
            {
                return;
            }

            //找到名子不符合規定的物件
            var nonTarget = gameObject.GetComponentsInChildren<UnityEngine.MeshFilter>()
                .Where(filter => filter.name.EndsWith("_ignore")).ToList();
            //刪除
            foreach (var target in nonTarget)
            {
                Object.DestroyImmediate(target.gameObject);
                Object.DestroyImmediate(target.sharedMesh);
            }
        }
    }
}