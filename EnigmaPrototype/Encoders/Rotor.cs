using System.Collections.Immutable;
using EnigmaPrototype.Encoders;

namespace EnigmaPrototype;

/// <summary>
/// The Rotor record
/// </summary>
/// <param name="rotorEncodings">the internal wiring of the rotor</param>
/// <param name="position">the current index that the rotor is turned to</param>
/// <param name="notch">the turnover notch for the rotor</param>
internal sealed record Rotor(in ImmutableDictionary<char, char> rotorEncodings, in int position, in char notch): IEncoder
{
    public ImmutableDictionary<char, char> Encodings { get; init; } = rotorEncodings;
    internal readonly char Notch = notch;
    internal readonly int Position = position;
};