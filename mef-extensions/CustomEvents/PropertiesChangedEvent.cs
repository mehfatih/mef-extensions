using System;

namespace mef_extensions.CustomEvents
{
    public delegate void PropertiesChangedEventHandler(object sender, PropertiesChangedEventArgs args);

    public class PropertiesChangedEventArgs : EventArgs
    {

    }
}