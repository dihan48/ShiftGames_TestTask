using System;
using System.Collections.Generic;
using UnityEditor;

namespace UnityEngine.UIElements
{
    public class PopupList : PopupWindowContent
    {
        private readonly Button button;
        private readonly List<object> items;
        private readonly Action<int> SelectionChange;
        private readonly int selectedIndex;
        private ListView listView;
        private bool IsFirstDraw = true;
        const int itemHeight = 16;

        public PopupList(List<string> items, Button button, Func<int> GetSelectedIndex, Action<int> SelectionChange) : base()
        {
            this.items = new List<object>(items);
            this.button = button;
            this.selectedIndex = GetSelectedIndex();
            this.SelectionChange = SelectionChange;
        }

        public override Vector2 GetWindowSize()
        {
            var height = items.Count * itemHeight;
            var maxHeight = height > 300 ? 300 : height;
            return new Vector2(button.worldBound.width, maxHeight);
        }

        public override void OnOpen()
        {
            listView = CreateListView();
            editorWindow.rootVisualElement.Add(listView);
        }

        public override void OnGUI(Rect rect)
        {
            if (IsFirstDraw)
                Focus();
        }

        private void Focus()
        {
            IsFirstDraw = false;
            listView.SetSelection(selectedIndex);
            listView.ScrollToItem(selectedIndex);
            listView.focusable = true;
            listView.Focus();
        }

        private ListView CreateListView()
        {
            VisualElement makeItem() => new Label();
            void bindItem(VisualElement e, int i) => (e as Label).text = items[i].ToString();

            var listView = new ListView(items, itemHeight, makeItem, bindItem);
            listView.style.flexGrow = 1.0f;

            listView.selectionType = SelectionType.Single;

            listView.onItemsChosen += obj => editorWindow.Close();
            listView.onSelectionChange += obj =>
            {
                button.text = (obj as List<object>)[0].ToString();
                var index = items.IndexOf((obj as List<object>)[0]);
                SelectionChange(index);
            };

            return listView;
        }
    }
}