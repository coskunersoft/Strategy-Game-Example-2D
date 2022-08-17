using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using AOP.ObjectPooling;

namespace AOP.Tools
{
    public  class InfiniteScroll : MonoBehaviour 
    {
        private ScrollRect scrollRect;
        [SerializeField]private GridLayoutGroup LayoutGroup;
        
        public List<RectTransform> Elements;
        public RectTransform TopLimitPoint;
        public RectTransform DownLimitPoint;
        public float TopPoint = 0;
        public float DownPoint = 0;
        private Vector3  MovementDistance;
        private Vector3 LastChangePosition;

        public async void Init()
        {
            TryGetComponent(out scrollRect);
            scrollRect.content.TryGetComponent(out LayoutGroup);

            var recttransform = scrollRect.transform as RectTransform;
            TopPoint = recttransform.position.y + recttransform.sizeDelta.y+LayoutGroup.cellSize.y;
            DownPoint = recttransform.position.y - recttransform.sizeDelta.y - LayoutGroup.cellSize.y;
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
            Elements = new List<RectTransform>();
            foreach (RectTransform item in scrollRect.content)
            {
                Elements.Add(item);
            }
           
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
            
            await System.Threading.Tasks.Task.Delay(100);
            MovementDistance = scrollRect.content.sizeDelta * Vector3.up;
            LayoutGroup.enabled = false;
            LastChangePosition = Elements[0].position;
        }

        private void OnDisable()
        {
            scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
        }


        private void OnScrollValueChanged(Vector2 value)
        {
            if (Elements.Count <= 0) return;
            if (Mathf.Abs(Elements[0].position.y - LastChangePosition.y) < LayoutGroup.cellSize.y) return;

            if (scrollRect.velocity.y>0)
            {
                foreach (var item in Elements)
                {
                    if (item.transform.position.y > TopLimitPoint.position.y+item.sizeDelta.y)
                    {
                        item.anchoredPosition -=(Vector2)MovementDistance;
                    }
                }
            }
            else if (scrollRect.velocity.y<0)
            {
                foreach (var item in Elements)
                {
                    if (item.transform.position.y < DownLimitPoint.position.y - item.sizeDelta.y)
                    {
                        item.anchoredPosition +=(Vector2) MovementDistance;
                    }
                }
            }
            LastChangePosition = Elements[0].position;
        }


    }
}

