using UnityEngine;
using System.Collections;

public static class NameGenerator {
	
	public static string generateName(int length)
	{
		string[] consonants = {"b","c","d","f","g","h","j","k","l","m","n","p","q","r","s","sh","z","zh",
			"t","v","w","x","y"};
		string[] vowels = { "a", "e", "i", "o", "u"};
		string stringName;
		string[] arrayName = new string[length];
		bool Cons;
		int a = Random.Range(0,2);
		if (a == 1) Cons = true; else Cons = false;
		Cons = true;
		int i = 0;
		Cons = false;
		while (i < length)
		{
			if(Cons)
			{
				arrayName[i] = consonants[Random.Range(0,consonants.Length)];
				Cons = false;
				i++;
			}
			else
			{
				arrayName[i] = vowels[Random.Range(0,vowels.Length)];
				Cons = true;
				i++;
			}
		}
		arrayName[0] = arrayName[0].ToUpper();
		stringName = string.Join("", arrayName);
		return stringName;
	}
}
