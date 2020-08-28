using UnityEditor;
using UnityEngine;

namespace NineHundredLbs.EnhancedGUA.Editor
{
    [CustomPropertyDrawer(typeof(AnalyticsEventHit))]
    public class AnalyticsEventHitDrawer : PropertyDrawer
    {
        private float propertyHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty hitDatabaseProperty = property.FindPropertyRelative("hitDatabase");
            SerializedProperty eventCategoryIndexProperty = property.FindPropertyRelative("eventCategoryIndex");
            SerializedProperty eventActionIndexProperty = property.FindPropertyRelative("eventActionIndex");
            SerializedProperty eventLabelSourceProperty = property.FindPropertyRelative("eventLabelSource");
            SerializedProperty eventValueSourceProperty = property.FindPropertyRelative("eventValueSource");
            SerializedProperty eventLabelProperty = property.FindPropertyRelative("eventLabel");
            SerializedProperty eventValueProperty = property.FindPropertyRelative("eventValue");
            SerializedProperty eventLabelCallbackProperty = property.FindPropertyRelative("eventLabelCallback");
            SerializedProperty eventValueCallbackProperty = property.FindPropertyRelative("eventValueCallback");

            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();
            EditorGUI.indentLevel++;
            Rect hitDatabaseRect = new Rect(
                position.x,
                position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                position.width,
                EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(hitDatabaseRect, hitDatabaseProperty);
            propertyHeight = EditorGUIUtility.singleLineHeight * 2.0f + hitDatabaseRect.height;
            if (hitDatabaseProperty.objectReferenceValue != null)
            {
                AnalyticsHitDatabase hitDatabase = hitDatabaseProperty.objectReferenceValue as AnalyticsHitDatabase;
                // Draw category select
                Rect eventCategorySelectionRect = new Rect(
                    position.x,
                    hitDatabaseRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                    position.width,
                    EditorGUIUtility.singleLineHeight);
                eventCategoryIndexProperty.intValue = EditorGUI.Popup(eventCategorySelectionRect, "Category", eventCategoryIndexProperty.intValue, hitDatabase.eventCategories);

                // Draw action select
                Rect eventActionSelectionRect = new Rect(
                    position.x,
                    eventCategorySelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                    position.width,
                    EditorGUIUtility.singleLineHeight);
                eventActionIndexProperty.intValue = EditorGUI.Popup(eventActionSelectionRect, "Action", eventActionIndexProperty.intValue, hitDatabase.eventActions);

                // Draw label select
                Rect eventLabelSelectionRect = new Rect();
                var eventLabelSource = (AnalyticsEventHit.EventLabelSource)eventLabelSourceProperty.enumValueIndex;
                if (eventLabelSource == AnalyticsEventHit.EventLabelSource.Constant)
                {
                    Rect eventLabelLabelRect = EditorGUI.PrefixLabel(
                        new Rect(
                            position.x,
                            eventActionSelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                            position.width,
                            EditorGUIUtility.singleLineHeight),
                        new GUIContent("Label"));

                    Rect eventLabelFieldRect = new Rect(
                        eventLabelLabelRect.x,
                        eventLabelLabelRect.y,
                        eventLabelLabelRect.width * 0.7f,
                        eventLabelLabelRect.height);

                    Rect eventLabelSourceRect = new Rect(
                        eventLabelLabelRect.x + eventLabelLabelRect.width * 0.7f,
                        eventLabelLabelRect.y,
                        eventLabelLabelRect.width * 0.3f,
                        eventLabelLabelRect.height);

                    EditorGUI.indentLevel--;
                    EditorGUI.PropertyField(eventLabelFieldRect, eventLabelProperty, GUIContent.none);
                    EditorGUI.indentLevel++;
                    EditorGUI.PropertyField(eventLabelSourceRect, eventLabelSourceProperty, GUIContent.none);
                    eventLabelSelectionRect = new Rect(
                        eventLabelLabelRect.position.x,
                        eventLabelLabelRect.position.y,
                        position.width,
                        EditorGUIUtility.singleLineHeight);
                }
                else if (eventLabelSource == AnalyticsEventHit.EventLabelSource.CodeOnly)
                {
                    Rect eventLabelLabelRect = EditorGUI.PrefixLabel(
                        new Rect(
                            position.x,
                            eventActionSelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                            position.width,
                            EditorGUIUtility.singleLineHeight),
                        new GUIContent("Label"));

                    Rect eventLabelHelpBoxRect = new Rect(
                        eventLabelLabelRect.x,
                        eventLabelLabelRect.y,
                        eventLabelLabelRect.width * 0.7f,
                        EditorGUIUtility.singleLineHeight * 2.0f);

                    Rect eventLabelSourceRect = new Rect(
                        eventLabelLabelRect.x + eventLabelLabelRect.width * 0.7f,
                        eventLabelLabelRect.y,
                        eventLabelLabelRect.width * 0.3f,
                        eventLabelLabelRect.height);

                    EditorGUI.indentLevel--;
                    if ((AnalyticsEventHit.EventValueSource)eventValueSourceProperty.enumValueIndex == AnalyticsEventHit.EventValueSource.CodeOnly)
                        EditorGUI.HelpBox(eventLabelHelpBoxRect, "Call the Dispatch(string, int) method to change label and value.", MessageType.Info);
                    else
                        EditorGUI.HelpBox(eventLabelHelpBoxRect, "Call the Dispatch(string) method to change label.", MessageType.Info);
                    EditorGUI.indentLevel++;
                    EditorGUI.PropertyField(eventLabelSourceRect, eventLabelSourceProperty, GUIContent.none);

                    eventLabelSelectionRect = new Rect(
                        eventLabelLabelRect.position.x,
                        eventLabelLabelRect.position.y,
                        position.width,
                        EditorGUIUtility.singleLineHeight * 2.0f);
                }
                else if (eventLabelSource == AnalyticsEventHit.EventLabelSource.Callback)
                {
                    Rect eventLabelCallbackRect = new Rect(
                        position.x + EditorGUI.indentLevel * 16,
                        eventActionSelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                        position.width * 0.7f - EditorGUI.indentLevel * 16,
                        EditorGUIUtility.singleLineHeight * 3.0f);

                    Rect eventLabelSourceRect = new Rect(
                        position.x + position.width * 0.7f,
                        eventActionSelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                        position.width * 0.3f,
                        EditorGUIUtility.singleLineHeight);

                    EditorGUI.indentLevel--;
                    EditorGUI.PropertyField(eventLabelCallbackRect, eventLabelCallbackProperty);
                    EditorGUI.indentLevel++;
                    EditorGUI.PropertyField(eventLabelSourceRect, eventLabelSourceProperty, GUIContent.none);

                    eventLabelSelectionRect = new Rect(
                        position.x,
                        eventLabelCallbackRect.position.y,
                        position.width,
                        EditorGUIUtility.singleLineHeight * 3.0f);
                }

                // Draw value select
                Rect eventValueSelectionRect = new Rect();
                var eventValueSource = (AnalyticsEventHit.EventValueSource)eventValueSourceProperty.enumValueIndex;
                if (eventValueSource == AnalyticsEventHit.EventValueSource.Constant)
                {
                    Rect eventValueLabelRect = EditorGUI.PrefixLabel(
                        new Rect(
                            position.x,
                            eventLabelSelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                            position.width,
                            EditorGUIUtility.singleLineHeight),
                        new GUIContent("Value"));

                    Rect eventValueFieldRect = new Rect(
                        eventValueLabelRect.x,
                        eventValueLabelRect.y,
                        eventValueLabelRect.width * 0.7f,
                        eventValueLabelRect.height);

                    Rect eventValueSourceRect = new Rect(
                        eventValueLabelRect.x + eventValueLabelRect.width * 0.7f,
                        eventValueLabelRect.y,
                        eventValueLabelRect.width * 0.3f,
                        eventValueLabelRect.height);

                    EditorGUI.indentLevel--;
                    EditorGUI.PropertyField(eventValueFieldRect, eventValueProperty, GUIContent.none);
                    EditorGUI.indentLevel++;
                    EditorGUI.PropertyField(eventValueSourceRect, eventValueSourceProperty, GUIContent.none);

                    eventValueSelectionRect = new Rect(
                        eventValueLabelRect.position.x,
                        eventValueLabelRect.position.y,
                        position.width,
                        EditorGUIUtility.singleLineHeight);
                }
                else if (eventValueSource == AnalyticsEventHit.EventValueSource.CodeOnly)
                {
                    Rect eventValueLabelRect = EditorGUI.PrefixLabel(
                        new Rect(
                            position.x,
                            eventLabelSelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                            position.width,
                            EditorGUIUtility.singleLineHeight),
                        new GUIContent("Value"));

                    Rect eventValueHelpBoxRect = new Rect(
                        eventValueLabelRect.x,
                        eventValueLabelRect.y,
                        eventValueLabelRect.width * 0.7f,
                        EditorGUIUtility.singleLineHeight * 2.0f);

                    Rect eventValueSourceRect = new Rect(
                        eventValueLabelRect.x + eventValueLabelRect.width * 0.7f,
                        eventValueLabelRect.y,
                        eventValueLabelRect.width * 0.3f,
                        eventValueLabelRect.height);

                    EditorGUI.indentLevel--;
                    if ((AnalyticsEventHit.EventLabelSource)eventLabelSourceProperty.enumValueIndex == AnalyticsEventHit.EventLabelSource.CodeOnly)
                        EditorGUI.HelpBox(eventValueHelpBoxRect, "Call the Dispatch(string, int) method to change label and value.", MessageType.Info);
                    else
                        EditorGUI.HelpBox(eventValueHelpBoxRect, "Call the Dispatch(string) method to change value.", MessageType.Info);
                    EditorGUI.indentLevel++;
                    EditorGUI.PropertyField(eventValueSourceRect, eventValueSourceProperty, GUIContent.none);

                    eventValueSelectionRect = new Rect(
                        eventValueLabelRect.position.x,
                        eventValueLabelRect.position.y,
                        position.width,
                        EditorGUIUtility.singleLineHeight * 2.0f);
                }
                else if (eventValueSource == AnalyticsEventHit.EventValueSource.Callback)
                {
                    Rect eventValueCallbackRect = new Rect(
                        position.x + EditorGUI.indentLevel * 16,
                        eventLabelSelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                        position.width * 0.7f - EditorGUI.indentLevel * 16,
                        EditorGUIUtility.singleLineHeight * 3.0f);

                    Rect eventValueSourceRect = new Rect(
                        position.x + position.width * 0.7f,
                        eventLabelSelectionRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                        position.width * 0.3f,
                        EditorGUIUtility.singleLineHeight);

                    EditorGUI.indentLevel--;
                    EditorGUI.PropertyField(eventValueCallbackRect, eventValueCallbackProperty);
                    eventLabelSourceProperty.enumValueIndex = (int)eventLabelSource;
                    EditorGUI.indentLevel++;
                    EditorGUI.PropertyField(eventValueSourceRect, eventValueSourceProperty, GUIContent.none);
                    eventValueSelectionRect = new Rect(
                        position.x,
                        eventValueCallbackRect.position.y,
                        position.width,
                        EditorGUIUtility.singleLineHeight * 3.0f);
                }

                propertyHeight = EditorGUIUtility.singleLineHeight * 2.0f + hitDatabaseRect.height + eventCategorySelectionRect.height + eventActionSelectionRect.height + eventLabelSelectionRect.height + eventValueSelectionRect.height;
            }
            else
            {
                Rect hitDatabaseHelpBoxRect = new Rect(
                    position.x + EditorGUI.indentLevel * 16,
                    hitDatabaseRect.max.y + EditorGUIUtility.standardVerticalSpacing,
                    position.width - EditorGUI.indentLevel * 16,
                    EditorGUIUtility.singleLineHeight * 2.0f);
                EditorGUI.HelpBox(hitDatabaseHelpBoxRect, "Create a custom database to select category, action, label, and value!", MessageType.Error);
                propertyHeight = EditorGUIUtility.singleLineHeight * 2.0f + hitDatabaseRect.height + hitDatabaseHelpBoxRect.height;
            }
            EditorGUI.EndChangeCheck();
            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return propertyHeight;
        }
    }
}
