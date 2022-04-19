using System.Runtime.Serialization;

namespace GrpcDemo.Server.Models;

[DataContract]
public class MultiplyResult
{
    [DataMember(Order = 1)]
    public int Result { get; set; }
}