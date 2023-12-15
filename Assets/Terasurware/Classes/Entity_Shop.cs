using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Shop : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public double uid;
		public string Name;
		public double Level;
		public string iconImg;
		public double Gold;
	}
}

