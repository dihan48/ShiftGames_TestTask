using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
    public class ButtonPopupList : VisualElement
    {
        public ButtonPopupList(string labelName, List<string> items, Func<int> GetSelectedIndex, Action<int> SelectionChange)
        {
            var label = new Label(labelName);
            label.style.marginRight = new StyleLength(3);
            label.style.width = new StyleLength(100);
            label.style.whiteSpace = WhiteSpace.Normal;

            var button = new Button() { text = items[GetSelectedIndex()] };
            button.clicked += () => UnityEditor.PopupWindow.Show(button.worldBound, new PopupList(items, button, GetSelectedIndex, SelectionChange));
            button.style.flexGrow = new StyleFloat(1);

            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            row.style.justifyContent = Justify.SpaceBetween;
            row.style.alignItems = Align.Center;
            row.style.paddingLeft = new StyleLength(10);
            row.style.paddingRight = new StyleLength(10);

            row.Add(label);
            row.Add(button);

            Add(row);
        }
    }
}