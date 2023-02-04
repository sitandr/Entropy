# Entropy
Compiler for Entropy, a programming language where all data is in a constant state of decay. Entropy forces the programmer to give up on precise control, and get an idea across in the short time before their program falls apart. 

The compiler takes these options:

    /h or /? -- print help screen
    /m -- set mutation rate (speed that data decays), must be between 0.001 and 100
    /d -- Debug mode: parse tree and print to screen
    /c -- Transpile to C#

# Syntax

Syntax is quite primitive, but I hope it will be further extended.

### Hello, world:

```
Program MyNamespace MyProgram [
    print "Hello, World";
]
```

## Datatypes

All datatypes in Entropy are built on reals (floats).

### real
A floating point number, which changes slightly *every time it's read from*. There is no way to compare whether two values are equal, since the values are so unstable—only the greater than and less than comparisons are available.

Currently available operations:
- `+` and `-`. Unary is not supported yet, so use `let a = 0 - a` instead. 
- (*unofficial*) `*` and `/`. Devision by zero creates Infinity.

### char
Built on a real, a char rounds off the current value of the real it is built on, and returns the corresponding character.

Char may be created from float by assigning float to char variable. You can't convert char to float back.

### string
An array of chars.

Currently you can just print it, no complex operations are available.

## Commands and operators

### declare

To create a variable, use `declare <name> <type>;`

### let

To assign (or reassign) variables, use `let <variable> = <expression>;` 

### if

```
if <condition>
[
    <some action>
]
```

"Else" is not still supported.

### print

`print <expression>;` writes `object.toString()` to stdout.

## *Unofficial* commands

### println

Adds a newline. Using this command instead of adding `print "\n"` creates new line that *doesn't mutate* so it is easier to navigate the output.

### round

`round(<expression>)` returns **string** with rounded number. Usefull for printing without these extra digits. However, it can't be used for calculations (ha, that would be too easy).

# Build

This code works on .Net v4.8, so it should natively work on all modern windows systems. If you are interested in porting to Linux or updating to Net v7, [see net7 branch](/../../tree/net7). It is much clumsier, trickier and slower, so stay here if you are not.

# More info

See Entropy in action with Drunk Eliza: http://danieltemkin.com/DrunkEliza

Entropy lives here: http://danieltemkin.com/Entropy

More details: http://esolangs.org/wiki/Entropy

JS version: https://github.com/ndrwhr/entropy
