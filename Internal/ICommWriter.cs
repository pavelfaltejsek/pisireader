//  
//  Copyright (c) 2013-2014 Simon Denier & Yannis Guedel
//  
using PISI.Net.Internal;

namespace PISI.Net
{
    public interface ICommWriter
    {
        void Write(SiMessage message);
    }
}