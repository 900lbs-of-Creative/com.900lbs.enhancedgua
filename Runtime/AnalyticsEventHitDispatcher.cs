using UnityEngine;

namespace NineHundredLbs.EnhancedGUA
{
    /// <summary>
    /// A simple component that dispatches an <see cref="AnalyticsEventHit"/>. 
    /// </summary>
    public class AnalyticsEventHitDispatcher : MonoBehaviour
    {
        #region Serialized Private Variables
        [SerializeField] private AnalyticsEventHit analyticsEventHit = default;
        #endregion

        #region Public Methods
        /// <summary>
        /// Dispatches <see cref="analyticsEventHit"/> with no given parameters, deferring to editor-serialized values.
        /// </summary>
        public void Dispatch()
        {
            analyticsEventHit.Dispatch();
        }

        /// <summary>
        /// Dispatches <see cref="analyticsEventHit"/> with the given <paramref name="label"/>. Should only be called if
        /// <see cref="AnalyticsEventHit.eventLabelSource"/> is set to <see cref="AnalyticsEventHit.EventLabelSource.CodeOnly"/>.
        /// </summary>
        /// <param name="label">The desired label for the event.</param>
        public void Dispatch(string label)
        {
            analyticsEventHit.Dispatch(label);
        }

        /// <summary>
        /// Dispatches <see cref="analyticsEventHit"/> with the given <paramref name="value"/>. Should only be called if
        /// <see cref="AnalyticsEventHit.eventValueSource"/> is set to <see cref="AnalyticsEventHit.EventValueSource.CodeOnly"/>.
        /// </summary>
        /// <param name="value">The desired value for the event.</param>
        public void Dispatch(int value)
        {
            analyticsEventHit.Dispatch(value);
        }

        /// <summary>
        /// Dispatches <see cref="analyticsEventHit"/> with the given <paramref name="label"/> and <paramref name="value"/>. 
        /// Should only be called if <see cref="AnalyticsEventHit.eventLabelSource"/> is set to <see cref="AnalyticsEventHit.EventLabelSource.CodeOnly"/> 
        /// and <see cref="AnalyticsEventHit.eventValueSource"/> is set to <see cref="AnalyticsEventHit.EventValueSource.CodeOnly"/>.
        /// </summary>
        /// <param name="label">Desired label for this event.</param>
        /// <param name="value">Desired value for this event.</param>
        public void Dispatch(string label, int value)
        {
            analyticsEventHit.Dispatch(label, value);
        }
        #endregion
    }
}
