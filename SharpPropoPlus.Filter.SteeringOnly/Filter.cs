using SharpPropoPlus.Contracts;
using SharpPropoPlus.Contracts.Interfaces;
using SharpPropoPlus.Filter.Contracts;
using System;

namespace SharpPropoPlus.Filter.SteeringOnly
    {
    [ExportPropoPlusFilter("{EE07DC55-6E43-487D-8EC4-27B8B2DE9615}", "Handbrake（仅限手刹）", "Handbrake Only（仅激活手刹）")]
    public class Filter : FilterProcessor
    {
        public override string[] Description => new[]
        {
            "适用于 RadioLink 的过滤器（仅激活手刹）"
        };


        /*
        V-Tail un-mixing
        Take the data going to both tail servos and extract the orthogonal axes.
        It is assumed that the mixed channels are ch1 and ch4
        All other channels do not change
        */
        protected override IJoystickData Process(IJoystickData channels, int max, int min)
        {
            var inData = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            var outData = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            // Copy input data to input buffer
            for (var i = 0; i < 6; i++)
            {
                inData[i] = channels.Data[i];
            }

            if (channels.Count >= 4)
            {
                var throttle = ((max / 2) - inData[1]) * 2.5;
                var brake = (inData[1] - (max / 2)) * 2.5;
                if (brake < 0) brake = 0;
                if (brake > max) brake = max;
                if (throttle < 0) throttle = 0;
                if (throttle > max) throttle = max;

                outData[0] = 0;
                outData[1] = 0;
                outData[2] = 0;
                outData[3] = 0;
                outData[4] = 0;
                outData[5] = 77 + (int)(((double)inData[2] / max) * 4);
                outData[6] = 0;
                outData[7] = 0;
                outData[8] = 0;
                outData[9] = 0;
                outData[10] = 0;
                outData[11] = 0;
                // Console.WriteLine("min=" + min + ", max=" + max + ", inData[1]=" + inData[1] + ", throttle=" + throttle + ", brake=" + brake);
            }

            return new JoystickData(channels.Count, outData);
        }
    }
}
