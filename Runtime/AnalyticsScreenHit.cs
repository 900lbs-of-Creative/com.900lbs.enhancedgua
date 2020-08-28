using System;
using UnityEngine;

using Strobotnik.GUA;

namespace NineHundredLbs.EnhancedGUA
{
    /// <summary>
    /// An object representing a Google Analytics Screen Hit.
    /// </summary>
    [Serializable]
    public class AnalyticsScreenHit
    {
        #region Enums
        public enum ScreenNameSource
        {
            Database,
            CodeOnly,
            Callback
        }
        #endregion

        #region Serialized Private Variables
        [SerializeField] private AnalyticsHitDatabase hitDatabase = default;
        [SerializeField] private int screenNameIndex = default;
        [SerializeField] private ScreenNameSource screenNameSource = default;
        [SerializeField] private StringCallback screenNameCallback = default;
        #endregion

        #region Public Methods
        /// <summary>
        /// Dispatches this screen hit with no given parameters, deferring to editor-serialized values.
        /// If <see cref="screenNameSource"/> is set to <see cref="ScreenNameSource.CodeOnly"/>, call
        /// <see cref="Dispatch(string)"/> instead.
        /// </summary>
        public void Dispatch()
        {
            string screenName = string.Empty;
            switch (screenNameSource)
            {
                case ScreenNameSource.Database:
                    screenName = hitDatabase.screenNames[screenNameIndex];
                    break;
                case ScreenNameSource.CodeOnly:
                    throw new Exception("Dispatch() called on a CodeOnly AnalyticsScreenHit; please call Dispatch(string) and pass a screen name!");
                case ScreenNameSource.Callback:
                    screenName = screenNameCallback.Invoke();
                    break;
            }
            Analytics.gua.sendAppScreenHit(screenName);
        }

        /// <summary>
        /// Dispatches this event hit with the given <paramref name="screenName"/>. Should only be called if
        /// <see cref="screenNameSource"/> is set to <see cref="ScreenNameSource.CodeOnly"/>.
        /// </summary>
        /// <param name="screenName">The desired screen name for the event.</param>
        public void Dispatch(string screenName)
        {
            Analytics.gua.sendAppScreenHit(screenName);
        }
        #endregion
    }
}
