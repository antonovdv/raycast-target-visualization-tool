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
        private static Texture _iconTexture;
    
        static HierarchyColoringRaycastTargetVisualizationTool()
        {
            var iconContent = EditorGUIUtility.IconContent("FreeformLayoutGroup Icon", "Show Raycast Target");
            _iconTexture = iconContent.image;
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj != null && ToolManager.activeToolType == typeof(RaycastTargetVisualizationTool))
            {
                bool hasRaycastTarget = obj.GetComponentsInChildren<Graphic>().Any(graphic => graphic.raycastTarget);
                if (hasRaycastTarget)
                {
                    Rect iconRect = new Rect(selectionRect)
                    {
                        x = selectionRect.xMax - 5f,
                        width = 16f,
                        height = 16f
                    };
                    EditorGUI.DrawTextureTransparent(iconRect, _iconTexture);
                }
            }
        }
    }
}