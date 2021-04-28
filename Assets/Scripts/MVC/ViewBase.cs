using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ViewBase 
{
    string Name { get; }

    ISet<string> interestEvents { get;}

    void HandlerEvents(string eventName, object eventParams);
    
}
