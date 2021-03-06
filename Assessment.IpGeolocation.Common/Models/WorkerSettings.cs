﻿using System;
using System.Collections.Generic;
using System.Text;
using Assessment.IpGeolocation.Common.Interfaces;

namespace Assessment.IpGeolocation.Common.Models
{
    public class WorkerSettings
    {
        public const string Name = "Worker";
        public IPGeolocationHostedWorkerSettings GeolocationBatchUpdate { get; set; }
    }

    public abstract class HostedWorkerSettings : IHostedWorkerSettings
    {
        public int Interval { get; set; }
    }

    public class IPGeolocationHostedWorkerSettings : HostedWorkerSettings
    {
        public const string Name = "Worker:GeolocationBatchUpdate";

        public int JobBatchSize { get; set; }
        public int JobItemBatchSize { get; set; }
    }
}
