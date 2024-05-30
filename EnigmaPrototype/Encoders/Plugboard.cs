using System.Collections.Immutable;

namespace EnigmaPrototype.Encoders;

/// <summary>
/// The Plugboard record
/// </summary>
/// <param name="boardEncodings">the internal wiring of the plugboard</param>
internal sealed record Plugboard(in ImmutableDictionary<char, char> boardEncodings): IEncoder
{
    public ImmutableDictionary<char, char> Encodings { get; init; } = boardEncodings;
};