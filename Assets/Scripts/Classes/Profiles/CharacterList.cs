using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : ScriptableObject
{

    public List<Character> list;

    public void Init()
    {
        list = new List<Character>();
    }

    public Character this[int i]
    {
        get { return list[i]; }
    }

    public void AddCharacter(Character c)
    {
        if (!list.Contains(c))
        {
            list.Add(c);
            list.Sort((x, y) => x.CompareTo(y)); //sort alphabetical order
        }
    }

    public void RemoveCharacter(int i)
    {
        list.RemoveAt(i);
    }

    //public void AddCharacter(string firstName, string lastName)
    //{
    //    Character c = CreateCharacter.Create(firstName, lastName);
    //    list.Add(c);
    //}
}
