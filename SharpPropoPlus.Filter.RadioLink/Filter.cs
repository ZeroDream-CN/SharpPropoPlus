using SharpPropoPlus.Contracts;
using SharpPropoPlus.Contracts.Interfaces;
using SharpPropoPlus.Filter.Contracts;
using System;

namespace SharpPropoPlus.Filter.RadioLink 
    {
    [ExportPropoPlusFilter("{4CAE1C45-125F-4D1A-BB82-621AFA59B58F}", "RadioLink（通用车控）", "RadioLink（通用车控，独立油门+刹车）")]
    public class Filter : FilterProcessor
    {
        public override string[] Description => new[]
        {
            "适配于 RadioLink RC4GS 车用遥控的过滤器，独立油门和刹车"
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
            for (var i = 0; i < 12; i++)
            {
                inData[i] = channels.Data[i];
            }

            if (channels.Count >= 4)
            {
                var half = (max / 2);
                var steering = inData[0];
                steering = (int)(half + ((steering - half) * 2.5));
                var throttle = (half - inData[1]) * 2.8;
                var brake = (inData[1] - half) * 2.8;

                if (steering < 0) steering = 0;
                if (steering > max) steering = max;
                if (brake < 0) brake = 0;
                if (brake > max) brake = max;
                if (throttle < 0) throttle = 0;
                if (throttle > max) throttle = max;

                outData[0] = steering;
                outData[1] = inData[3];
                outData[2] = (int)throttle;
                outData[3] = (int)brake;
                outData[4] = 77 + (int)(((double)inData[2] / max) * 4);
                outData[5] = inData[5];
                outData[6] = inData[6];
                outData[7] = inData[7];
                outData[8] = inData[8];
                outData[9] = inData[9];
                outData[10] = inData[10];
                outData[11] = inData[11];
                // Console.WriteLine("min=" + min + ", max=" + max + ", inData[2]=" + inData[2] + ", inData[3]=" + inData[3] + ", outData[4]=" + outData[4]);
            }

            return new JoystickData(channels.Count, outData);
        }
    }
}
