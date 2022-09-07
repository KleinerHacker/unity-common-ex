using System;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils
{
    public abstract class Counting
    {
        private float _counter;
        protected readonly float _max;
        private readonly bool _autoReset;

        /// <summary>
        /// TRUE if counter is on limit and auto reset was set to FALSE
        /// </summary>
        public bool IsDestroyed => !_autoReset && CheckCounter(_counter);

        protected Counting(float maxValue, bool autoReset)
        {
            _max = maxValue;
            _autoReset = autoReset;

            _counter = ResetCounterValue;
        }

        /// <summary>
        /// Try to run given action. This is only called if counter reach limit. With each call of this method the counter is decrement by given value.
        /// </summary>
        /// <param name="value">Value to counting by</param>
        /// <param name="action">Action to run if counter reach limit</param>
        /// <returns>TRUE if action was run, otherwise FALSE</returns>
        public bool Try(float value, Action action)
        {
            _counter = CalcCounter(_counter, value);
            if (CheckCounter(_counter))
            {
                action?.Invoke();
                _counter = _autoReset ? ResetCounterValue : ZeroCounterValue;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to run given action. This is only called if counter reach limit. With each call of this method the counter is counting by one.
        /// </summary>
        /// <param name="action">Action to run if counter reach limit</param>
        /// <returns>TRUE if action was run, otherwise FALSE</returns>
        public bool Try(Action action) => Try(1f, action);

        protected abstract float CalcCounter(float counter, float value);
        protected abstract bool CheckCounter(float counter);
        protected abstract float ResetCounterValue { get; }
        protected abstract float ZeroCounterValue { get; }
    }

    /// <summary>
    /// Class to decrement from an start value to zero and run action
    /// </summary>
    public sealed class Decrementing : Counting
    {
        /// <summary>
        /// Creates the decrementing class with start value (must be greater than 0)
        /// </summary>
        /// <param name="startValue">Start value, greater than 0</param>
        /// <param name="autoReset">Automatic reset to max counter if counter was reached 0</param>
        public Decrementing(float startValue, bool autoReset = false) : base(startValue, autoReset)
        {
        }

        protected override float CalcCounter(float counter, float value) => counter - value;

        protected override bool CheckCounter(float counter) => counter <= 0f;

        protected override float ResetCounterValue => _max;

        protected override float ZeroCounterValue => 0f;
    }

    /// <summary>
    /// Class to increment from 0 to a max value and run action
    /// </summary>
    public sealed class Incrementing : Counting
    {
        /// <summary>
        /// Creates the incrementing with max value
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="autoReset"></param>
        public Incrementing(float maxValue, bool autoReset = true) : base(maxValue, autoReset)
        {
        }

        protected override float CalcCounter(float counter, float value) => counter + value;

        protected override bool CheckCounter(float counter) => counter >= _max;

        protected override float ResetCounterValue => 0f;

        protected override float ZeroCounterValue => _max;
    }

    public sealed class ActionCounting
    {
        private int _counter;

        public void StartAction()
        {
            _counter++;
        }

        public bool StopAction()
        {
            if (_counter - 1 <= 0)
            {
                _counter = 0;
                return true;
            }
            
            _counter--;
            return false;
        }
    }
}