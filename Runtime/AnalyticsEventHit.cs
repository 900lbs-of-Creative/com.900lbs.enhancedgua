using System;
using UnityEngine;

using Strobotnik.GUA;

namespace NineHundredLbs.EnhancedGUA
{
    /// <summary>
    /// An object representing a Google Analytics Event Hit. Composed of four parts:
    /// <list type="bullet">
    /// <item>Category: the category of the event hit (e.g. 'Button', 'Toggle')</item>
    /// <item>Action: the action of the event hit (e.g. 'Clicked', 'Toggled')</item>
    /// <item>Label: the label of the event hit (e.g. 'Start Button', 'Mute Toggle')</item>
    /// <item>Value: the value of the event hit (e.g. '5' representing 5s for a video to start playing after a play button was clicked)</item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// See https://support.google.com/analytics/answer/1033068?hl=en for reference on the anatomy of Events.
    /// </remarks>
    [Serializable]
    public class AnalyticsEventHit
    {
        #region Enums
        public enum EventLabelSource
        {
            Constant,
            CodeOnly,
            Callback
        }

        public enum EventValueSource
        {
            Constant,
            CodeOnly,
            Callback
        }
        #endregion

        #region Serialized Private Variables
        [SerializeField] private AnalyticsHitDatabase hitDatabase = default;
        [SerializeField] private int eventCategoryIndex = default;
        [SerializeField] private int eventActionIndex = default;
        [SerializeField] private EventLabelSource eventLabelSource = default;
        [SerializeField] private EventValueSource eventValueSource = default;
        [SerializeField] private string eventLabel = default;
        [SerializeField] private int eventValue = default;
        [SerializeField] private StringCallback eventLabelCallback = default;
        [SerializeField] private IntCallback eventValueCallback = default;
        #endregion

        #region Public Methods
        /// <summary>
        /// Dispatches this event hit with no given parameters, deferring to editor-serialized values.
        /// </summary>
        public void Dispatch()
        {
            Analytics.gua.beginHit(GoogleUniversalAnalytics.HitType.Event);
            Analytics.gua.addEventCategory(GetEventCategory());
            Analytics.gua.addEventAction(GetEventAction());
            Analytics.gua.addEventLabel(GetEventLabel());
            Analytics.gua.addEventValue(GetEventValue());
            Analytics.gua.sendHit();
        }

        /// <summary>
        /// Dispatches this event hit with the given <paramref name="label"/>. Should only be called if
        /// <see cref="eventLabelSource"/> is set to <see cref="EventLabelSource.CodeOnly"/>.
        /// </summary>
        /// <param name="label">The desired label for the event.</param>
        public void Dispatch(string label)
        {
            Analytics.gua.beginHit(GoogleUniversalAnalytics.HitType.Event);
            Analytics.gua.addEventCategory(GetEventCategory());
            Analytics.gua.addEventAction(GetEventAction());
            Analytics.gua.addEventLabel(label);
            Analytics.gua.addEventValue(GetEventValue());
            Analytics.gua.sendHit();
        }

        /// <summary>
        /// Dispatches this event hit with the given <paramref name="value"/>. Should only be called if
        /// <see cref="eventValueSource"/> is set to <see cref="EventValueSource.CodeOnly"/>.
        /// </summary>
        /// <param name="value">The desired value for the event.</param>
        public void Dispatch(int value)
        {
            Analytics.gua.beginHit(GoogleUniversalAnalytics.HitType.Event);
            Analytics.gua.addEventCategory(GetEventCategory());
            Analytics.gua.addEventAction(GetEventAction());
            Analytics.gua.addEventLabel(GetEventLabel());
            Analytics.gua.addEventValue(value);
            Analytics.gua.sendHit();
        }

        /// <summary>
        /// Dispatches this event hit with the given <paramref name="label"/> and <paramref name="value"/>. 
        /// Should only be called if <see cref="eventLabelSource"/> is set to <see cref="EventLabelSource.CodeOnly"/> 
        /// and <see cref="eventValueSource"/> is set to <see cref="EventValueSource.CodeOnly"/>.
        /// </summary>
        /// <param name="label">Desired label for this event.</param>
        /// <param name="value">Desired value for this event.</param>
        public void Dispatch(string label, int value)
        {
            Analytics.gua.beginHit(GoogleUniversalAnalytics.HitType.Event);
            Analytics.gua.addEventCategory(GetEventCategory());
            Analytics.gua.addEventAction(GetEventAction());
            Analytics.gua.addEventLabel(label);
            Analytics.gua.addEventValue(value);
            Analytics.gua.sendHit();
        }
        #endregion

        #region Private Methods
        private string GetEventCategory()
        {
            return hitDatabase.eventCategories[eventCategoryIndex];
        }

        private string GetEventAction()
        {
            return hitDatabase.eventActions[eventActionIndex];
        } 

        private string GetEventLabel()
        {
            switch (eventLabelSource)
            {
                case EventLabelSource.Constant:
                    return eventLabel;
                case EventLabelSource.CodeOnly:
                    throw new Exception("GetEventlabel() called on an AnalyticsEventHit with label set to CodeOnly; please call Dispatch(string) and pass a label.");
                case EventLabelSource.Callback:
                    return eventLabelCallback.Invoke();
                default:
                    return string.Empty;
            }
        }

        private int GetEventValue()
        {
            switch (eventValueSource)
            {
                case EventValueSource.Constant:
                    return eventValue;
                case EventValueSource.CodeOnly:
                    throw new Exception("GetEventValue() called on an AnalyticsEventHit with value set to CodeOnly; please call Dispatch(int) and pass a value.");
                case EventValueSource.Callback:
                    return eventValueCallback.Invoke();
                default:
                    return 0;
            }
        }
        #endregion
    }
}
