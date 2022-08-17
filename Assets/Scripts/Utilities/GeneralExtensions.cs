using System.Collections;
using System.Collections.Generic;
using AOP.GamePlay.Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AOP.Extensions
{
    public static class GeneralExtensions
    {
        public static Vector2 CenterOfVectors(this List<Vector2> vectors)
        {
            Vector2 result = default;
            for (int i = 0; i < vectors.Count; i++)
                result += vectors[i];
            result /= vectors.Count;
            return result;
        }

        public static void ShowHideCanvasGroup(this CanvasGroup canvasGroup, bool status)
        {
            canvasGroup.alpha = status ? 1 : 0;
            canvasGroup.interactable = status;
            canvasGroup.blocksRaycasts = status;
        }
        public static void TranslateTransformWithCoverZAxis(this Transform transform, Vector2 position)
        {
            transform.position = transform.NewPositionWithCoverZAxis(position);
        }
        public static Vector3 NewPositionWithCoverZAxis(this Transform transform, Vector2 position)
        {
            Vector3 newPos = position;
            newPos.z = transform.position.z;
            return newPos;
        }
        public static bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        

        public static Direction[] AllDirections = new Direction[] 
        {
            Direction.Right,
            Direction.Left,
            Direction.Up,
            Direction.Down,
            Direction.RightUp,
            Direction.LeftDown,
            Direction.LeftUp,
            Direction.RightDown
        };


    }
}

