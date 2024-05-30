using System.Collections.Immutable;
using EnigmaPrototype.Encoders;

namespace EnigmaPrototype;

/// <summary>
/// Static class handling encoding functionality 
/// </summary>
internal static class Encode
{
    /// <summary>
    /// Simulates the rotor circuit on a single keypress
    /// </summary>
    /// <param name="inputIndex">the index of the input keypress</param>
    /// <param name="encoderSet">the rotor set</param>
    /// <returns>output char</returns>
    internal static char Roterise(in int inputIndex, in IEncoder[] encoderSet)
    {
        var plugIn = ForwardEncode(inputIndex, encoderSet[0].Encodings);
        var first = ForwardEncode(plugIn, encoderSet[1].Encodings);
        var second = ForwardEncode(first, encoderSet[2].Encodings);
        var third = ForwardEncode(second, encoderSet[3].Encodings);
    
        var reflect = ForwardEncode(third, encoderSet[4].Encodings);
    
        var fourth = BackwardEncode(reflect, encoderSet[3].Encodings);
        var fifth = BackwardEncode(fourth, encoderSet[2].Encodings);
        var sixth = BackwardEncode(fifth, encoderSet[1].Encodings);
        var plugOut = BackwardEncode(sixth, encoderSet[0].Encodings);
    
        var output = encoderSet[0].Encodings.ElementAt(plugOut);
        return output.Key;
    }
    
    /// <summary>
    /// Simulates the rotor circuit on a single keypress
    /// </summary>
    /// <param name="inputIndex">the index of the input keypress</param>
    /// <param name="encoderSet">the set of encoding components</param>
    /// <param name="rotorPositions">the beginning rotor positions</param>
    /// <returns>the encoding and the new rotor positions</returns>
    internal static (char result, (int, int, int) newPosition) Roterise(
        in int inputIndex,
        IEncoder[] encoderSet,
        (int, int, int) rotorPositions
    )
    {
        var (pos1, pos2, pos3) = rotorPositions;
        
        var first = ForwardEncode(RotorPositionOffset(pos1, inputIndex), encoderSet[0].Encodings);
        var second = ForwardEncode(RotorPositionOffset(pos2, first), encoderSet[1].Encodings);
        var third = ForwardEncode(RotorPositionOffset(pos3, second), encoderSet[2].Encodings);

        var reflect = ForwardEncode(third, encoderSet[3].Encodings);

        var fourth = BackwardEncode(reflect, encoderSet[2].Encodings);
        var fifth = BackwardEncode(fourth, encoderSet[1].Encodings);
        var sixth = BackwardEncode(fifth, encoderSet[0].Encodings);

        var resultIndex = (sixth - pos1 + 26) % 26;

        var newPosition = (AdvanceRotor(pos1), (pos1 % 26 == 25) ? AdvanceRotor(pos2) : pos2, (pos2 % 26 == 25) ? AdvanceRotor(pos3) : pos3);

        return (encoderSet[0].Encodings.ElementAt(resultIndex).Value, newPosition);
        
        int AdvanceRotor(int position) => (position + 1) % 26;
        int RotorPositionOffset(int position, int index) => (index + position) % 26;
    }

    /// <summary>
    /// Simulates a forward (pre-reflector) encoding on an encoder
    /// </summary>
    /// <param name="index">the input index</param>
    /// <param name="encodings">the encoder mapping</param>
    /// <returns>output index</returns>
    private static int ForwardEncode(in int index, in ImmutableDictionary<char, char> encodings)
    {
        var entry = encodings.ElementAt(index);
        var exitIndex = encodings.Keys.ToList().IndexOf(entry.Value);
        return exitIndex;
    }

    /// <summary>
    /// Simulates a backward (post-reflector) encoding on an encoder
    /// </summary>
    /// <param name="index">the input index</param>
    /// <param name="encodings">the encoder mapping</param>
    /// <returns>output index</returns>
    private static int BackwardEncode(in int index, in ImmutableDictionary<char, char> encodings)
    {
        var entry = encodings.ElementAt(index);
        var exitIndex = encodings.Values.ToList().IndexOf(entry.Key);
        return exitIndex;
    }
}