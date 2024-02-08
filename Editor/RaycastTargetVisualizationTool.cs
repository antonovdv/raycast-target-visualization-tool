using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace RaycastTargetVisualizationTool.Editor
{
    [EditorTool("Raycast Target Visualization Tool", typeof(RectTransform))]
    class RaycastTargetVisualizationTool : EditorTool
    {
        private GUIContent _iconContent;
        private Graphic[] _graphics = Array.Empty<Graphic>();

        public override GUIContent toolbarIcon => _iconContent ??= EditorGUIUtility.IconContent("FreeformLayoutGroup Icon");

        public override void OnActivated()
        {
            base.OnActivated();

            var targetRectTransform = target as RectTransform;
            if (targetRectTransform != null)
            {
                _graphics = targetRectTransform.GetComponentsInChildren<Graphic>();
            }
        }

        public override void OnToolGUI(EditorWindow window)
        {
            foreach (var graphic in _graphics)
            {
                if (graphic.raycastTarget)
                {
                    var rectTransform = graphic.rectTransform;
                    Vector3[] corners = new Vector3[4];
                    rectTransform.GetWorldCorners(corners);
                    var color = GetRandomColorByInstanceId(rectTransform.GetInstanceID());
                    Handles.DrawSolidRectangleWithOutline(corners, color, new Color(color.r, color.g, color.b, 1f));
                }
            }
        }

        private Color GetRandomColorByInstanceId(int instanceId)
        {
            Random.InitState(instanceId);

            float r = Random.value;
            float g = Random.value;
            float b = Random.value;
            float a = 0.5f;

            return new Color(r, g, b, a);
        }
    }
}