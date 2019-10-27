using UnityEngine;
using System.Runtime.Serialization;
using System.Collections;
 
public class QuaternionSerializationSurrogate : ISerializationSurrogate
{
 
    // Method called to serialize a Vector3 object
    public void GetObjectData(System.Object obj,SerializationInfo info, StreamingContext context)
    {
 
        Quaternion qt = (Quaternion) obj;
        info.AddValue("x", qt.x);
        info.AddValue("y", qt.y);
        info.AddValue("z", qt.z);
        info.AddValue("w", qt.w);
    }
 
    // Method called to deserialize a Vector3 object
    public System.Object SetObjectData(System.Object obj,SerializationInfo info,
                                       StreamingContext context,ISurrogateSelector selector)
    {
 
        Quaternion qt = (Quaternion)obj;
        qt.x = (float)info.GetValue("x", typeof(float));
        qt.y = (float)info.GetValue("y", typeof(float));
        qt.z = (float)info.GetValue("z", typeof(float));
        qt.w = (float)info.GetValue("w", typeof(float));
        obj = qt;
        return obj;
    }
}
 