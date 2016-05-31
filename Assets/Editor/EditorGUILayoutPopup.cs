using UnityEditor;
using UnityEngine;

// Creates an instance of a primitive depending on the option selected by the user.
public class EditorGUILayoutPopup : EditorWindow
{
    public static string[] options;
    public int index = 0;
    [MenuItem("Examples/Editor GUILayout Popup usage")]
    static void Init()
    {
        Object[] ob = Resources.FindObjectsOfTypeAll(typeof(MonoScript));
        
        System.Collections.Generic.List<string> t = new System.Collections.Generic.List<string>();
        // Get list of TileType scripts
        foreach (MonoScript tt in ob)
        {
            if (tt.GetClass() != null && tt.GetClass().IsSubclassOf(typeof(Assets.Scripts.Tiles.TileType)))
            {
                Debug.Log("T: " + tt.name);
                t.Add(tt.name);
            }
        }

        options = t.ToArray();

        EditorWindow window = GetWindow(typeof(EditorGUILayoutPopup));
        window.Show();
    }

    void OnGUI()
    {   
        index = EditorGUILayout.Popup(index, options);
    }
}