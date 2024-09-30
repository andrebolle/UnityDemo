using UnityEngine;
using UnityEditor;
using System.Text;

public class HierarchyLister : EditorWindow
{
    [MenuItem("Tools/List GameObjects and Components")]
    public static void ShowWindow()
    {
        GetWindow<HierarchyLister>("Hierarchy List");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("List GameObjects and Components"))
        {
            string result = ListGameObjectsAndComponents();
            Debug.Log(result);
        }
    }

    private string ListGameObjectsAndComponents()
    {
        StringBuilder sb = new StringBuilder();

        GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject rootObject in rootObjects)
        {
            ListComponents(rootObject, 0, sb);
        }

        return sb.ToString();
    }

    private void ListComponents(GameObject go, int indentLevel, StringBuilder sb)
    {
        string indent = new string('-', indentLevel);
        sb.AppendLine(indent + go.name);

        foreach (Component component in go.GetComponents<Component>())
        {
            sb.AppendLine(indent + "  - " + component.GetType().Name);
        }

        foreach (Transform child in go.transform)
        {
            ListComponents(child.gameObject, indentLevel + 1, sb);
        }
    }
}
