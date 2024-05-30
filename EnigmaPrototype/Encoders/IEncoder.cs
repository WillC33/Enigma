using System.Collections.Immutable;

namespace EnigmaPrototype.Encoders;

/// <summary>
/// Interface for an encoder mapping
/// </summary>
internal interface IEncoder
{
 // IEncoder requires an encoding mapping
 public ImmutableDictionary<char, char> Encodings { get; }
}