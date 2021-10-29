﻿using System;
using System.Collections.ObjectModel;

namespace SharpPropoPlus.vJoyMonitor
{
    public class DeviceInformationCollection : Collection<DeviceInformation>
    {
        public new void Add(DeviceInformation item)
        {
            base.Add(item);
        }

        public void Add(int deviceId, string name, Guid productGuid, VjdStat status)
        {
            base.Add(new DeviceInformation(deviceId, name, productGuid, status));
        }
    }
}