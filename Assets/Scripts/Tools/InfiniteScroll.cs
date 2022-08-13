using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace AOP.Tools
{
    public class InfiniteScroll : MonoBehaviour
    {
        private ScrollRect scrollRect;
        private float DeadPointUp;
        private float DeadPointDown;
        public List<RectTransform> items;
        public List<RectTransform> topItems;
        public List<RectTransform> downItems;

        private void Awake()
        {
            TryGetComponent(out scrollRect);
            DeadPointUp = scrollRect.viewport.rect.position.y+scrollRect.viewport.rect.height/2;
            DeadPointDown = scrollRect.viewport.rect.position.y - scrollRect.viewport.rect.height / 2;

            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);

        }

        private void OnDisable()
        {
            scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
        }


        public void MakeInfinite(int columnCount)
        {
            items = new List<RectTransform>();
            foreach (RectTransform item in scrollRect.content.transform)
            {
                items.Add(item);
            }
            for (int i = 0; i < columnCount; i++)
            {
                topItems.Add(items[i]);
            }
            for (int i = items.Count-1; i > items.Count-(columnCount+1); i--)
            {
                downItems.Add(items[i]);
            }
        }

        private void OnScrollValueChanged(Vector2 value)
        {
            foreach (var item in items)
            {
                if (scrollRect.velocity.y > 0)
                {
                    if (item.rect.position.y > DeadPointUp)
                    {

                    }
                }
                else
                {
                    if (item.rect.position.y < DeadPointDown)
                    {

                    }
                }
                
            }
        }


    }
}

