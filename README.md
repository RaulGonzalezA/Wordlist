# Wordlist

Wordlist is a .NET 10 console application that reads the supplied [wordlist.txt](wordlist.txt). It prints each six-letter entry that can be formed by concatenating two words from the same list. Each match appears on its own line.

```text
al + bums => albums
```

Matching is case-insensitive and the output preserves the spelling of the target word. When a word has multiple valid splits, the application prints the first one found from left to right.
