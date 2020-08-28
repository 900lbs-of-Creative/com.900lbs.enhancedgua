using UnityEditor;
using UnityEngine;

namespace NineHundredLbs.EnhancedGUA.Editor
{
    [CustomPropertyDrawer(typeof(AnalyticsScreenHit))]
    public class AnalyticsScreenHitDrawer : PropertyDrawer
    {
        private float propertyHeight = 0.0f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty screenNameSourceProperty = property.FindPropertyRelative("screenNameSource");
            SerializedProperty hitDatabaseProperty = property.FindPropertyRelative("hitDatabase");
            SerializedProperty screenNameIndexProperty = property.FindPropertyRelative("screenNameIndex");
            SerializedProperty screenNameCallbackProperty = property.FindPropertyRelative("screenNameCallback");

            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PrefixLabel(position, label);
            EditorGUI.BeginChangeCheck();
            EditorGUI.indentLevel++;
            Rect screenNameSourceRect = new Rect(
                position.x, 
                position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, 
                position.width, 
                EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(screenNameSourceRect, screenNameSourceProperty);

            switch ((AnalyticsScreenHit.ScreenNameSource)screenNameSourceProperty.enumValueIndex)
            {
                case AnalyticsScreenHit.ScreenNameSource.Database:
                    Rect hitDatabaseRect = new Rect(
                        position.x,
                        screenNameSourceRect.max.y + EditorGUIUtility.standardVerticalSpacing, 
                        position.width, 
                        EditorGUIUtility.singleLineHeight);

                    EditorGUI.PropertyField(hitDatabaseRect, hitDatabaseProperty);
                    if (hitDatabaseProperty.objectReferenceValue != null)
                    {
                        AnalyticsHitDatabase database = hitDatabaseProperty.objectReferenceValue as AnalyticsHitDatabase;
                        Rect screenNamePopupRect = new Rect(
                            position.x,
                            hitDatabaseRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                            position.width,
                            EditorGUIUtility.singleLineHeight);

                        screenNameIndexProperty.intValue = EditorGUI.Popup(screenNamePopupRect, "Screen Name", screenNameIndexProperty.intValue, database.screenNames);
                        propertyHeight = EditorGUIUtility.singleLineHeight * 5.0f;
                    }
                    else
                    {
                        Rect databaseHelpBoxRect = new Rect(
                            position.x + EditorGUI.indentLevel * 16,
                            hitDatabaseRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                            position.width - EditorGUI.indentLevel * 16,
                            EditorGUIUtility.singleLineHeight * 2.0f);
                        EditorGUI.HelpBox(databaseHelpBoxRect, "Create a custom database to select screen names!", MessageType.Error);
                        propertyHeight = EditorGUIUtility.singleLineHeight * 6.0f;
                    }
                    break;


                case AnalyticsScreenHit.ScreenNameSource.CodeOnly:
                    Rect codeOnlyHelpBoxRect = new Rect(
                        position.x + EditorGUI.indentLevel * 16,
                        screenNameSourceRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                        position.width - EditorGUI.indentLevel * 16,
                        EditorGUIUtility.singleLineHeight * 2.0f);
                    EditorGUI.HelpBox(codeOnlyHelpBoxRect, "Call the Dispatch(string) method to set a custom screen name.", MessageType.Info);
                    propertyHeight = EditorGUIUtility.singleLineHeight * 5.0f;
                    break;


                case AnalyticsScreenHit.ScreenNameSource.Callback:
                    Rect screenNameCallbackRect = new Rect(
                        position.x + EditorGUI.indentLevel * 16,
                        screenNameSourceRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                        position.width - EditorGUI.indentLevel * 16,
                        EditorGUIUtility.singleLineHeight * 3.0f);

                    EditorGUI.indentLevel--;
                    EditorGUI.PropertyField(screenNameCallbackRect, screenNameCallbackProperty);
                    EditorGUI.indentLevel++;
                    // Force the screen name source back to the original value
                    screenNameSourceProperty.enumValueIndex = 2;
                    propertyHeight = EditorGUIUtility.singleLineHeight * 6.0f;
                    break;
            }
            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return propertyHeight;
        }
    }
}
