using EnigmaPrototype.Encoders;
using static EnigmaPrototype.Encoders.Build;
using static EnigmaPrototype.Encode;

#region Build Enigma

var rotor1 = BuildRotor(Rotor1Encodings, 0, 'Q');
var rotor2 = BuildRotor(Rotor2Encodings, 0, 'E');
var rotor3 = BuildRotor(Rotor3Encodings, 0, 'V');
var reflector = BuildReflector(ReflectorAEncodings);
var plugboard = BuildPlugboard([('A', 'R'),('G', 'K'),('O', 'X')]);

IEncoder[] rotorSet = [plugboard, rotor3, rotor2, rotor1, reflector];

#endregion


//Input 
var input = "NJGIJDKJNNXCJLJIJ".ToUpper().ToList();
input.ForEach(@char =>
{
    var indexOf = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(@char);
    var output = Roterise(indexOf, rotorSet);
    Console.Write(output);
});

//var _initialSettings = args;

 
