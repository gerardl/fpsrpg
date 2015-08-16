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

        [MenuItem("TorchWing/Items")]
        public static void ShowWindow()
        {
            GetWindow<ItemLibraryEditor>();
        }

        void OnGUI()
        {
            defaultColor = GUI.backgroundColor;
            if (Items.ItemLibrary.Instance == null) return;
            library = Items.ItemLibrary.Instance;

            EditorGUILayout.BeginHorizontal();
            currentType = (Item.ItemTypes)EditorGUILayout.EnumPopup(currentType);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add")) library.AddItem(currentType);
            GUI.backgroundColor = defaultColor;
            if (GUILayout.Button("Save")) PrefabUtility.ReplacePrefab(library.gameObject, PrefabUtility.GetPrefabParent(library.gameObject), ReplacePrefabOptions.ConnectToPrefab);
            if (GUILayout.Button("Revert")) PrefabUtility.ResetToPrefabState(library.gameObject);
            EditorGUILayout.EndHorizontal();

            for (int i = library.Items.Count - 1; i >= 0; i--)
            {
                Item item = library.Items[i];
                if (currentType == item.itemType)
                {
                    DrawCommonProperties(item);
                    //if (currentType == Item.ItemTypes.Quest) DrawThingProperties((ItemQuest)item);
                    //if (currentType == Item.ItemTypes.WeaponMelee) DrawWeaponProperties((ItemWeaponMelee)item);
                    //if (currentType == Item.ItemTypes.Staff) DrawStaffProperties((ItemWeaponStaff)item);
                    //if (currentType == Item.ItemTypes.Potion) DrawPotionProperties((ItemPotion)item);
                    //if (currentType == Item.ItemTypes.Wearable) DrawWearableProperties((ItemWearable)item);
                }
            }
        }

        private void DrawCommonProperties(Item item)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ID", GUILayout.Width(20));
            item.id = EditorGUILayout.TextField(item.id, GUILayout.Width(120));
            EditorGUILayout.LabelField("Name:", GUILayout.Width(40));
            item.name = EditorGUILayout.TextField(item.name, GUILayout.Width(120));
            EditorGUILayout.LabelField("Description: ", GUILayout.Width(70));
            item.description = EditorGUILayout.TextField(item.description);
            EditorGUILayout.LabelField("Icon", GUILayout.Width(50));
            //item.icon = (Sprite)EditorGUILayout.ObjectField(item.icon, typeof(Sprite), false, GUILayout.Width(120));

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(120))) library.RemoveItem(item);
            GUI.backgroundColor = defaultColor;
            EditorGUILayout.EndHorizontal();
        }
    }
}
