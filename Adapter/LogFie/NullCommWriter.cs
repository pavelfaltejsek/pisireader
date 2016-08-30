//  
//  Copyright (c) 2013-2014 Simon Denier & Yannis Guedel
//  
using PISI.Net.Internal;

namespace PISI.Net.Adapter.LogFie
{
    public class NullCommWriter : ICommWriter
    {
        public void Write(SiMessage message)
        {
        }
    }
}