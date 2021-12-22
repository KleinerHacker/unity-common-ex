using System;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils
{
    /// <summary>
    /// Class to decrement from an start value to zero and run action
    /// </summary>
    public sealed class Decrementing
    {
        private int _counter;

        /// <summary>
        /// TRUE if counter is 0
        /// </summary>
        public bool IsDestroyed => _counter <= 0;

        /// <summary>
        /// Creates the decrementing class with start counter (must be greater than 0)
        /// </summary>
        /// <param name="counter">Start counter, greater than 0</param>
        public Decrementing(int counter)
        {
            this._counter = counter;
        }

        /// <summary>
        /// Try to run given action. This is only called if counter reach 0. With each call of this method the counter is decrement by one.
        /// </summary>
        /// <param name="action">Action to run if counter reach 0</param>
        /// <returns>TRUE if action was run, otherwise FALSE</returns>
        public bool Try(Action action)
        {
            if (_counter <= 0)
                return false;

            _counter--;
            if (_counter <= 0)
            {
                action?.Invoke();
                _counter = 0;

                return true;
            }

            return false;
        }
    }
}