using System.ComponentModel;

namespace SharpPropoPlus.Audio.Enums
{
    public enum AudioChannel
    {
        /// <summary>
        /// Automatic
        /// </summary>
        [Description("自动（默认）")] Automatic,

        /// <summary>
        /// Left Channel
        /// </summary>
        Left,

        /// <summary>
        /// Right Channel
        /// </summary>
        Right
    }
}