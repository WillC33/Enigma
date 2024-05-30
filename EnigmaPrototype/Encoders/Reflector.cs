using System.Collections.Immutable;
using EnigmaPrototype.Encoders;

namespace EnigmaPrototype;

/// <summary>
/// The Reflector record
/// </summary>
/// <param name="reflectorEncodings">the internal wiring of the reflector</param>
internal sealed record Reflector(in ImmutableDictionary<char, char> reflectorEncodings): IEncoder
{
    public ImmutableDictionary<char, char> Encodings { get; init; } = reflectorEncodings;
};