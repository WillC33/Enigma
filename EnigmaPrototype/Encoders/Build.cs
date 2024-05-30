using System.Collections.Immutable;

namespace EnigmaPrototype.Encoders;

/// <summary>
/// Builder class for constructing encoders
/// </summary>
internal static class Build
{
    /// <summary>
    /// Rotor Wiring strings (Wehrmacht Enigma)
    /// </summary>
    internal const string Rotor1Encodings = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
    internal const string Rotor2Encodings = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
    internal const string Rotor3Encodings = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
    internal const string ReflectorAEncodings = "EJMZALYXVBWFCRQUONTSPIKHGD";

    /// <summary>
    /// Builds a new rotor
    /// </summary>
    /// <param name="encoding">the string const of encodings</param>
    /// <param name="position"></param>
    /// <param name="notch"></param>
    /// <returns>the rotor</returns>
    internal static Rotor BuildRotor(in string encoding, in int position, in char notch)
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] rotorEncodings = encoding.ToCharArray();
        var encodings = alphabet
            .Zip(rotorEncodings, (letter, encoding) => new KeyValuePair<char, char>(letter, encoding))
            .ToImmutableDictionary();
        return new Rotor(encodings, position, notch);
    }
    
    /// <summary>
    /// Builds a new reflector
    /// </summary>
    /// <param name="encoding">the string const of encodings</param>
    /// <returns>the reflector</returns>
    internal static Reflector BuildReflector(in string encoding)
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] rotorEncodings = encoding.ToCharArray();
        var encodings = alphabet
            .Zip(rotorEncodings, (letter, encoding) => new KeyValuePair<char, char>(letter, encoding))
            .ToImmutableDictionary();
        return new Reflector(encodings);
    }
    
    /// <summary>
    /// Builds a new plugboard
    /// </summary>
    /// <param name="pairings">the connected pairings array</param>
    /// <returns>the plugboard</returns>
    /// <exception cref="Exception">Plugboard encodings are not supported</exception>
    /// <impure />
    internal static IEncoder BuildPlugboard(in (char from, char to)[] pairings)
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] encodings = [..alphabet];
    
        if (pairings.Length > 13) throw new Exception("Pairings are too long");
        var fromChars = pairings.Select(p => p.from).ToArray();
        var toChars = pairings.Select(p => p.to).ToArray();
        if (fromChars.Distinct().Count() != fromChars.Length || toChars.Distinct().Count() != toChars.Length)
            throw new Exception("Duplicate characters found in pairings");

        foreach (var (from, to) in pairings)
        {
            var toIndex = Array.IndexOf(alphabet, to);
            encodings[toIndex] = from;
            var fromIndex = Array.IndexOf(alphabet, from);
            encodings[fromIndex] = to;
        
        }

        var dictionary = alphabet
            .Zip(encodings, (a, e) => new KeyValuePair<char, char>(a, e))
            .ToImmutableDictionary();

        return new Plugboard(dictionary);
    }
}