using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CreateDefaultFolders : EditorWindow
    {
        private static string ProjectName => Application.dataPath.Split('/')[^2];
        
        [MenuItem("Assets/Create Default Folders")]
        private static void SetUpFolders()
        {
            var window = CreateInstance<CreateDefaultFolders>();
            var windowRectX = Screen.width / 2;
            var windowRectY = Screen.height / 2;
            window.position = new Rect(windowRectX, windowRectY, 200, 100);
            window.ShowPopup();
        }

        private static void CreateAllFolders()
        {
            var folders = new List<string>
            {
                "Animations",
                "Data",
                "Editor",
                "Effects",
                "Materials",
                "Models",
                "Plugins",
                "Prefabs",
                "Resources",
                "Scenes",
                "Scripts",
                "ScriptTemplates",
                "Shaders",
                "Sprites",
                "Textures",
                "UI",
            };

            foreach (var folder in folders)
            {
                CreateFolder(folder);
            }

            var uiFolders = new List<string>
            {
                "Assets",
                "Fonts",
                "Icons",
            };

            foreach (var subfolder in uiFolders)
            {
                CreateFolder(Path.Combine("UI", subfolder));
            }

            AssetDatabase.Refresh();
        }

        private static void CreateFolder(string folder)
        {
            var path = Path.Combine(Application.dataPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label($"{ProjectName}", centeredStyle);
            GUILayout.Space(20);
            if (GUILayout.Button("Generate!"))
            {
                CreateAllFolders();
                Close();
            }
            if (GUILayout.Button("Cancel"))
            {
                Close();
            }
            GUILayout.EndVertical();
        }
    }
}
