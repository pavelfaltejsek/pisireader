﻿//  
//  Copyright (c) 2013-2014 Simon Denier & Yannis Guedel
//  
using System;
using PISI.Net.Dataframe;

namespace PISI.Net
{
    public interface ISiListener
    {
        void HandleEcard(ISiDataFrame dataFrame);

        void Notify(CommStatus status);

        void Notify(CommStatus errorStatus, String errorMessage);

        bool OnEcardDown(string siNumber);
    }
}