﻿using System;

namespace SharpPropoPlus.Contracts.Interfaces
{
    public interface IPropoMetadata
    {
        string Name { get; }

        string Description { get; }
        
        string UniqueIdentifier { get; }
    }
}