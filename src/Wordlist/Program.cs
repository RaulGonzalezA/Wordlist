using Wordlist.Application;
using Wordlist.Presentation;

var words = File.ReadLines("wordlist.txt");
var matches = CompoundWordFinder.Find(words);
CompoundWordWriter.Write(matches, Console.Out);
