using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.IO;
using HtmlAgilityPack;
using System.Text;


public class TezosWebGLBuildPostprocessor 
{
    [PostProcessBuild(0)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
#if UNITY_WEBGL
        string[] guids1 = AssetDatabase.FindAssets("webgl-frontend", null);
        string assetPath = "";
        foreach (var guid in guids1)
        {
            assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log($"{assetPath}, filename: {Path.GetFileName(assetPath)}");
            if (Path.GetFileName(assetPath) == "webgl-frontend.js")
            {
                File.Copy(assetPath, Path.Combine(pathToBuiltProject, "tezos.js"), true);
                var path = Path.Combine(pathToBuiltProject, "index.html");
                var doc = new HtmlDocument();
                doc.Load(path);
                var node = doc.DocumentNode.SelectSingleNode("//head");
                if (node.SelectSingleNode("//head/script[@src='tezos.js']") == null)
                {
                    HtmlNode scriptNode = HtmlNode.CreateNode(@"<script src=""tezos.js"">");
                    node.AppendChild(scriptNode);
                    //doc.Save(path, Encoding.UTF8);
                    File.WriteAllText(path, doc.DocumentNode.WriteTo());
                }
                
                break;
            }
        }
#endif
    }
}
