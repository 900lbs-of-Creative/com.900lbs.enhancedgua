using UnityEngine;

namespace NineHundredLbs.EnhancedGUA
{
    /// <summary>
    /// A simple component that dispatches an <see cref="AnalyticsScreenHit"/>.
    /// </summary>
    public class AnalyticsScreenHitDispatcher : MonoBehaviour
    {
        #region Serialized Private Variables
        [SerializeField] private AnalyticsScreenHit analyticsScreenHit = default;
        #endregion

        #region Public Methods
        /// <summary>
        /// Dispatches <see cref="analyticsScreenHit"/> with no given parameters, deferring to editor-serialized values.
        /// </summary>
        public void Dispatch()
        {
            analyticsScreenHit.Dispatch();
        }

        /// <summary>
        /// Dispatches <see cref="analyticsScreenHit"/> with the given <paramref name="screenName"/>. Should only be called if
        /// <see cref="AnalyticsScreenHit.eventLabelSource"/> is set to <see cref="AnalyticsScreenHit.ScreenNameSource.CodeOnly"/>.
        /// </summary>
        /// <param name="label">The desired screenName for the event.</param>
        public void Dispatch(string screenName)
        {
            analyticsScreenHit.Dispatch(screenName);
        }
        #endregion
    }
}
