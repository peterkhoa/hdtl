using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Library
{
	public class LanguageBase
	{
		#region List of Properties
		/// <summary>
		/// Internal using only: save Current Culture setting of this language
		/// </summary>
		private CultureInfo m_CurrentCulture;
		/// <summary>
		/// Gets or sets the current culture.
		/// </summary>
		/// <value>The current culture.</value>
		public CultureInfo CurrentCulture
		{
			get
			{
				return m_CurrentCulture;
			}
			set
			{
				m_CurrentCulture = value;
			}
		}

		/// <summary>
		/// Gets the language name.
		/// </summary>
		/// <value>The name of the language.</value>
		public string LanguageName
		{
			get
			{
				return m_CurrentCulture.ThreeLetterISOLanguageName;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="LanguageBase"/> class with default culture.
		/// </summary>
		public LanguageBase ( )
		{
			CurrentCulture = CultureInfo.CurrentCulture;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LanguageBase"/> class.
		/// </summary>
		/// <param name="cultureName">Name of the culture.</param>
		public LanguageBase ( string cultureName )
		{
			CurrentCulture = new CultureInfo ( cultureName );
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LanguageBase"/> class.
		/// </summary>
		/// <param name="culture">The culture id.</param>
		public LanguageBase ( int culture )
		{
			CurrentCulture = new CultureInfo ( culture );
		}
		#endregion

		#region Main Methods
		/// <summary>
		/// Sets the culture.
		/// </summary>
		/// <param name="cultureName">Name of the culture.</param>
		public void SetCulture ( string cultureName )
		{
			m_CurrentCulture = new CultureInfo ( cultureName );
		}

		/// <summary>
		/// Sets the culture.
		/// </summary>
		/// <param name="culture">The culture.</param>
		public void SetCulture ( int culture )
		{
			m_CurrentCulture = new CultureInfo ( culture );
		}
		#endregion
	}
}
