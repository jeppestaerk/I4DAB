﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Personkartotek
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class Adresse
	{
		public virtual string Vejnavn
		{
			get;
			set;
		}

		public virtual int Husnummer
		{
			get;
			set;
		}

		public virtual int Postnummer
		{
			get;
			set;
		}

		public virtual string By
		{
			get;
			set;
		}

		public virtual int AdresseID
		{
			get;
			set;
		}

		public virtual IEnumerable<AdresseAlternativ> HjemEkstra
		{
			get;
			set;
		}

	}
}
