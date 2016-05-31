using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tile))]
public class EditorGUILayoutPopupV2 : Editor
{
    public static string[] options;
    public static Dictionary<string, Assets.Scripts.Tiles.TileType> TileTypeList = new Dictionary<string, Assets.Scripts.Tiles.TileType>();
    
    static void Init()
    {
        Object[] ob = Resources.FindObjectsOfTypeAll(typeof(MonoScript));

        List<string> t = new List<string>();
        
        // Get list of TileType scripts
        foreach (MonoScript tt in ob)
        {
            if (tt.GetClass() != null && tt.GetClass().IsSubclassOf(typeof(Assets.Scripts.Tiles.TileType)))
            {
                t.Add(tt.name);
                Assets.Scripts.Tiles.TileType t1 = (Assets.Scripts.Tiles.TileType)ScriptableObject.CreateInstance(tt.name);
                TileTypeList.Add(tt.name, t1);
            }
        }

        options = t.ToArray();
    }
    
    public override void OnInspectorGUI()
    {
        // Only run the Init if populating
        if(options == null || options.Length == 0)
            Init();

        DrawDefaultInspector();
        Tile _target = (Tile)target;
        
        _target.Type = EditorGUILayout.Popup("Tile Type 1", _target.Type, options);
        
        _target.TType = TileTypeList[options[_target.Type]];
    }
}
 