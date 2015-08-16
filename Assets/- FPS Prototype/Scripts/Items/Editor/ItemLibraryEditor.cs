using UnityEngine;
using UnityEditor;
using FPSRPGPrototype.BaseClasses;

namespace FPSRPGPrototype.EditorExtensions
{
    public class ItemLibraryEditor : EditorWindow
    {
        private Items.ItemLibrary library;
        private Item.ItemTypes currentType = Item.ItemTypes.Quest;
        private Color defaultColor;

        [MenuItem("Itemz")]
        public static void ShowWindow()
        {
            GetWindow<ItemLibraryEditor>();
        }
    }
}
