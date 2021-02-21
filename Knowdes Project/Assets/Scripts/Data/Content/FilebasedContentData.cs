

using System;

namespace Knowdes
{
    public interface FilebasedContentData 
    {
        string Path { get; }
        event Action OnPathChanged;
    }
}