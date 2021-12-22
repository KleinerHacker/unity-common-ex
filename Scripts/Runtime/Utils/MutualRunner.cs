using System;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils
{
    /// <summary>
    /// This runner stops stack overflow effect in case of mutual calls.
    /// </summary>
    public sealed class MutualRunner
    {
        private bool _active;

        /// <summary>
        /// Try to run a mutual action. If already an other action with this runner instance is active, the call is ignored.
        /// </summary>
        /// <param name="action">Action to run mutual</param>
        /// <returns>TRUE if action was run, otherwise FALSE</returns>
        public bool Try(Action action)
        {
            if (_active)
                return false;

            _active = true;
            try
            {
                action?.Invoke();
            }
            finally
            {
                _active = false;
            }

            return true;
        }
    }
}