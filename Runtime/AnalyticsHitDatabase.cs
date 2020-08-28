using UnityEngine;

namespace NineHundredLbs.EnhancedGUA
{
    [CreateAssetMenu(fileName ="New Analytics Hit Database", menuName = "Create New Analytics Hit Database")]
    public class AnalyticsHitDatabase : ScriptableObject
    {
        [Tooltip("Event hit categories.")]
        public string[] eventCategories;
        
        [Tooltip("Event hit actions.")]
        public string[] eventActions;

        [Tooltip("Screen names.")]
        public string[] screenNames;
    }
}
