using System.Linq;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

namespace RaycastTargetVisualizationTool.Editor
{
    [InitializeOnLoad]
    public class HierarchyColoringRaycastTargetVisualizationTool
    {
        private static readonly Texture _iconTexture;
        private static readonly GUIStyle _style;

        static HierarchyColoringRaycastTargetVisualizationTool()
        {
            _iconTexture = EditorGUIUtility.IconContent("FreeformLayoutGroup Icon").image;
            _style = new GUIStyle
            {
                alignment = TextAnchor.MiddleRight
            };
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
            ToolManager.activeToolChanged += OnActiveToolChanged;
        }

        private static void OnActiveToolChanged()
        {
            EditorApplication.RepaintHierarchyWindow();
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj != null && ToolManager.activeToolType == typeof(RaycastTargetVisualizationTool))
            {
                var graphicComponent = obj.GetComponent<Graphic>();
                if (graphicComponent != null && graphicComponent.raycastTarget)
                {
                    DrawIcon(selectionRect, _iconTexture);
                }
                else
                {
                    var components = obj.GetComponentsInChildren<Graphic>().Where(graphic => graphic.raycastTarget).ToArray();
                    if (components.Length > 0)
                    {
                        DrawLabel(selectionRect, components.Length);
                    }
                }
            }
        }
        
        private static void DrawIcon(Rect selectionRect, Texture texture)
        {
            Rect iconRect = new Rect(selectionRect)
            {
                x = selectionRect.xMax - 15f,
                width = 16f,
                height = 16f
            };
            EditorGUI.DrawTextureTransparent(iconRect, texture);
        }

        private static void DrawLabel(Rect selectionRect, int count)
        {
            Rect iconRect = new Rect(selectionRect)
            {
                x = selectionRect.xMax - 31f,
                width = 32f,
                height = 16f
            };

            EditorGUI.LabelField(iconRect, count.ToString(), _style);
        }
    }
}