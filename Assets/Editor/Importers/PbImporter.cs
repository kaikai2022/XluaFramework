using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, ".pb")]
public class PbImporter : ScriptedImporter {
    public override void OnImportAsset(AssetImportContext ctx) {
        //读取文件内容
        var pbText = File.ReadAllText(ctx.assetPath);
        //转成TextAsset（Unity可识别类型）
        var assetsText = new TextAsset(pbText);

        //将对象assetText添加到导入操作(AssetImportContext)的结果中。
        ctx.AddObjectToAsset("main obj", assetsText);
        //将对象assetText作为导入操作的主要对象。
        ctx.SetMainObject(assetsText);
    }
}